using HearPrediction.Api.Data.Services;
using HearPrediction.Api.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HearPrediction.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MedicalAnalystController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		public MedicalAnalystController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		//Get All MedicalAnalysts from db
		[HttpGet]
		public async Task<IActionResult> GetAllMedicalAnalyst()
		{
			try
			{
				var medicalAnalyst = await _unitOfWork.medicalAnalyst.GetMedicalAnalysts();
				return Ok(medicalAnalyst);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message + "  " + ex.StackTrace);
			}
		}

		//Get MedicalAnalyst Details db
		[HttpGet("GetMedicalAnalystById")]
		public async Task<IActionResult> GetMedicalAnalystDetails(int id)
		{
			var medicalAnalyst = await _unitOfWork.medicalAnalyst.GetMedicalAnalyst(id);
			if (medicalAnalyst == null)
				return NotFound($"No Medical Analyst was found with Id: {id}");

			var medicalAnalystDetail = new MedicalAnalystFormDTO
			{
				FullName = medicalAnalyst.User.FullName,
				FirstName = medicalAnalyst.User.FirstName,
				LastName = medicalAnalyst.User.LastName,
				BirthDate = medicalAnalyst.User.BirthDate,
				Email = medicalAnalyst.User.Email,
				PhoneNumber = medicalAnalyst.User.PhoneNumber,
				Gender = medicalAnalyst.User.Gender,
				ProfileImg = medicalAnalyst.User.ProfileImg,
				LabId = medicalAnalyst.LabId,
			};
			return Ok(medicalAnalystDetail);
		}
		//Search For Medical Analyst
		[HttpGet("Search")]
		public async Task<IActionResult> SearchForMedicalAnalyst([FromQuery] string search)
		{
			try
			{
				var result = await _unitOfWork.medicalAnalyst.FilterMedicalAnalyst(search);
				return Ok(result);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Error in searchin for data");
			}
		}

		//Edit MedicalAnalyst
		[HttpPut("EditMedicalAnalyst")]
		public async Task<IActionResult> EditMedicalAnalyst(int id, [FromBody] MedicalAnalystFormDTO model)
		{
			var medicalAnalyst = await _unitOfWork.medicalAnalyst.GetMedicalAnalyst(id);
			if (medicalAnalyst == null)
				return NotFound($"No Medical Analyst was found with Id: {id}");

			medicalAnalyst.User.PhoneNumber = model.PhoneNumber;
			medicalAnalyst.User.Email = model.Email;
			medicalAnalyst.User.FirstName = model.FirstName;
			medicalAnalyst.User.LastName = model.LastName;
			medicalAnalyst.User.FullName = model.FullName;
			medicalAnalyst.User.BirthDate = model.BirthDate;
			medicalAnalyst.User.Gender = model.Gender;
			medicalAnalyst.User.ProfileImg = model.ProfileImg;
			medicalAnalyst.LabId = model.LabId;

			await _unitOfWork.Complete();
			return Ok(medicalAnalyst);
		}

		//Delete MedicalAnalyst
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var medicalAnalyst = _unitOfWork.medicalAnalyst.Get_MedicalAnalyst(id);
			if (medicalAnalyst == null)
				return NotFound($"No Medical Analyst was found with Id: {id}");
			try
			{
				_unitOfWork.medicalAnalyst.Remove(medicalAnalyst);
				await _unitOfWork.Complete();
				return Ok(medicalAnalyst);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
			}
		}
	}
}
