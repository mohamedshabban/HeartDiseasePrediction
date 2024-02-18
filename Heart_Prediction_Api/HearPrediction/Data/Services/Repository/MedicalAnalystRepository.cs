﻿using HearPrediction.Api.Data.Services.IRepository;
using HearPrediction.Api.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HearPrediction.Api.Data.Services.Repository
{
	public class MedicalAnalystRepository : IMedicalAnalystRepository
	{
		private readonly AppDbContext _context;
		public MedicalAnalystRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task Add(MedicalAnalyst medicalAnalyst) => await _context.MedicalAnalysts.AddAsync(medicalAnalyst);

		public async Task<IEnumerable<MedicalAnalyst>> GetMedicalAnalysts()
		{
			return await _context.MedicalAnalysts
				 .Include(m => m.User)
				 .Include(m => m.Lab)
				 .ToListAsync();
		}

		public async Task<MedicalAnalyst> GetMedicalAnalyst(int id)
		{
			return await _context.MedicalAnalysts
				 .Include(m => m.User)
				 .Include(m => m.Lab)
				 .FirstOrDefaultAsync(m => m.Id == id);
		}

		public async Task<MedicalAnalyst> GetProfile(string userId)
		{
			return await _context.MedicalAnalysts
				 .Include(m => m.User)
				 .Include(m => m.Lab)
				 .Include(m => m.medicalTests)
				 .FirstOrDefaultAsync(m => m.UserId == userId);
		}

		public void Remove(MedicalAnalyst medicalAnalyst) =>
			_context.MedicalAnalysts.Remove(medicalAnalyst);

		public MedicalAnalyst Get_MedicalAnalyst(int id)
		{
			return _context.MedicalAnalysts
				 .Include(m => m.User)
				 .Include(m => m.Lab)
				 .Include(m => m.medicalTests)
				 .FirstOrDefault(m => m.Id == id);
		}

		public async Task<IEnumerable<MedicalAnalyst>> FilterMedicalAnalyst(string search)
		{
			var medicalAnalysts = await GetMedicalAnalysts();
			if (!string.IsNullOrEmpty(search))
			{
				medicalAnalysts = await _context.MedicalAnalysts.
				Where(x => x.User.FullName.Contains(search)).ToListAsync();
			}
			return medicalAnalysts;
		}

	}
}
