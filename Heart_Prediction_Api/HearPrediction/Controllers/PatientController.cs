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
	public class PatientController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		public PatientController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		//Get All Patients from db
		[HttpGet]
		public async Task<IActionResult> GetAllPatients()
		{
			var pateints = await _unitOfWork.Patients.GetPatients();
			return Ok(pateints);
		}

		//Get patient details from db
		[HttpGet("GetPatientBySSN")]
		public async Task<IActionResult> GetPatientDetails(long ssn)
		{
			var pateint = await _unitOfWork.Patients.GetPatient(ssn);
			if (pateint == null)
				return NotFound($"No patient was found with SSN: {ssn}");
			var patientDetail = new UserFormDTO
			{
				FullName = pateint.User.FullName,
				FirstName = pateint.User.FirstName,
				LastName = pateint.User.LastName,
				SSN = pateint.SSN,
				Insurance_No = pateint.Insurance_No,
				BirthDate = pateint.User.BirthDate,
				Email = pateint.User.Email,
				Gender = pateint.User.Gender,
				PhoneNumber = pateint.User.PhoneNumber,
				ProfileImg = pateint.User.ProfileImg,
			};
			return Ok(patientDetail);
		}

		//Search For Patient
		[HttpGet("Search")]
		public async Task<IActionResult> SearchForPatient([FromQuery] string search)
		{
			try
			{
				var result = await _unitOfWork.Patients.FilterPatients(search);
				return Ok(result);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Error in search for data");
			}
		}

		//Get Patient's Appointment details
		[HttpGet("GetPatientApp")]
		public async Task<IActionResult> GetPatientAppDetails(long ssn)
		{
			var patientDetailApp = new UserDetailDTO
			{
				Patient = await _unitOfWork.Patients.GetPatient(ssn),
				Appointments = await _unitOfWork.appointment.GetAppointmentWithPatientAsync(ssn),
				CountAppointments = await _unitOfWork.appointment.CountAppointments(ssn)
			};
			return Ok(patientDetailApp);
		}

		//Edit Patient 
		[HttpPut("EditPatient")]
		public async Task<IActionResult> EditPatient(long ssn, [FromBody] UserFormDTO model)
		{
			var patient = await _unitOfWork.Patients.GetPatient(ssn);
			if (patient == null)
				return NotFound($"No patient was found with SSN: {ssn}");

			patient.User.PhoneNumber = model.PhoneNumber;
			patient.SSN = model.SSN;
			patient.Insurance_No = model.Insurance_No;
			patient.User.FirstName = model.FirstName;
			patient.User.Email = model.Email;
			patient.User.LastName = model.LastName;
			patient.User.FullName = model.FullName;
			patient.User.Gender = model.Gender;
			patient.User.BirthDate = model.BirthDate;
			patient.User.ProfileImg = model.ProfileImg;

			await _unitOfWork.Complete();
			return Ok(patient);
		}

		//Delete Patient
		[HttpDelete("{ssn}")]
		public async Task<IActionResult> Delete(long ssn)
		{
			var patient = _unitOfWork.Patients.Get_Patient(ssn);
			if (patient == null)
				return NotFound($"No patient was found with SSN: {ssn}");
			try
			{
				_unitOfWork.Patients.Remove(patient);
				await _unitOfWork.Complete();
				return Ok(patient);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
			}
		}
	}
}
