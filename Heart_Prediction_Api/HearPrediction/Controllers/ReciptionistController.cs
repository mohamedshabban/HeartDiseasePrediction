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
	public class ReciptionistController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		public ReciptionistController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		//Get all Reciptionists from db
		[HttpGet]
		public async Task<IActionResult> GetAllReciptionists()
		{
			var reciptionist = await _unitOfWork.reciptionist.GetReciptionists();
			return Ok(reciptionist);
		}

		//Get Reciptionist details from db
		[HttpGet("GetReciptionistById")]
		public async Task<IActionResult> GetReciptionistDetails(int id)
		{
			var reciptionist = await _unitOfWork.reciptionist.GetReciptionist(id);
			if (reciptionist == null)
				return NotFound($"No Reciptionist was found with Id: {id}");

			var reciptionistDetail = new ReciptionistFormDTO
			{
				FullName = reciptionist.User.FullName,
				FirstName = reciptionist.User.FirstName,
				LastName = reciptionist.User.LastName,
				BirthDate = reciptionist.User.BirthDate,
				Email = reciptionist.User.Email,
				Gender = reciptionist.User.Gender,
				PhoneNumber = reciptionist.User.PhoneNumber,
				ProfileImg = reciptionist.User.ProfileImg,
			};
			return Ok(reciptionistDetail);
		}

		//Search For Reciptionist
		[HttpGet("Search")]
		public async Task<IActionResult> SearchForReciptionist([FromQuery] string search)
		{
			try
			{
				var result = await _unitOfWork.reciptionist.FilterReciptionist(search);
				return Ok(result);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Error in search for data");
			}
		}

		//Edit the Reciptionist 
		[HttpPut("EditReciptionist")]
		public async Task<IActionResult> EditReciptionist(int id, [FromBody] ReciptionistFormDTO model)
		{
			var reciptionist = await _unitOfWork.reciptionist.GetReciptionist(id);
			if (reciptionist == null)
				return NotFound($"No Reciptionist was found with Id: {id}");

			reciptionist.User.PhoneNumber = model.PhoneNumber;
			reciptionist.User.Email = model.Email;
			reciptionist.User.FirstName = model.FirstName;
			reciptionist.User.LastName = model.LastName;
			reciptionist.User.FullName = model.FullName;
			reciptionist.User.Gender = model.Gender;
			reciptionist.User.BirthDate = model.BirthDate;
			reciptionist.User.ProfileImg = model.ProfileImg;

			await _unitOfWork.Complete();
			return Ok(reciptionist);
		}

		//Delete the Reciptionist from db
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var reciptionist = _unitOfWork.reciptionist.Get_Reciptionist(id);
			if (reciptionist == null)
				return NotFound($"No Reciptionist was found with Id: {id}");
			try
			{
				_unitOfWork.reciptionist.Remove(reciptionist);
				await _unitOfWork.Complete();
				return Ok(reciptionist);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
			}
		}
	}
}
