using HearPrediction.Api.Data.Services.IRepository;
using HearPrediction.Api.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HearPrediction.Api.Data.Services.Repository
{
	public class SpecializationRepository : ISpecializationRepository
	{
		public readonly AppDbContext _context;

		public SpecializationRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Specialization>> GetSpecializations()
		{
			return await _context.Specializations.ToListAsync();
		}
	}
}
