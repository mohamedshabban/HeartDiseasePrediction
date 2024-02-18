using HeartDiseasePrediction.Models;
using HeartDiseasePrediction.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HeartDiseasePrediction.Controllers
{
	//[Authorize(Roles = "MedicalAnalyst")]
	public class DoctorController : Controller
	{
		private readonly IToastNotification _toastNotification;
		Uri baseAddress = new Uri("https://localhost:44304/api");
		HttpClient _client;
		public DoctorController(IToastNotification toastNotification)
		{
			_toastNotification = toastNotification;
			_client = new HttpClient();
			_client.BaseAddress = baseAddress;
		}
		//get all doctors in list
		public async Task<IActionResult> Index()
		{
			var accessToken = HttpContext.Session.GetString("JWToken");
			var sp = _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			List<DoctorViewModel> doctorViewModel = new List<DoctorViewModel>();
			HttpResponseMessage response = _client.GetAsync(_client.BaseAddress +
				"/Doctor/").Result;
			if (response.IsSuccessStatusCode)
			{
				string data = await response.Content.ReadAsStringAsync();
				doctorViewModel = JsonConvert.DeserializeObject<List<DoctorViewModel>>(data);
			}
			return View(doctorViewModel);
		}
		[HttpPost]
		public async Task<IActionResult> Index(string search)
		{
			var accessToken = HttpContext.Session.GetString("JWToken");
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			List<DoctorViewModel> doctorViewModel = new List<DoctorViewModel>();
			HttpResponseMessage response = _client.GetAsync(_client.BaseAddress +
				$"/Doctor/Search?search={search}").Result;
			if (response.IsSuccessStatusCode)
			{
				string data = await response.Content.ReadAsStringAsync();
				doctorViewModel = JsonConvert.DeserializeObject<List<DoctorViewModel>>(data);
			}
			return View(doctorViewModel);
		}
		//get doctor details
		public async Task<IActionResult> DoctorDetails(int id)
		{
			try
			{
				var accessToken = HttpContext.Session.GetString("JWToken");
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
				HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress +
					$"/Doctor/GetDoctorById?id={id}");
				if (response.IsSuccessStatusCode)
				{
					string data = response.Content.ReadAsStringAsync().Result;
					var doctor = JsonConvert.DeserializeObject<DoctorVM>(data);
					return View(doctor);
				}
				else
				{
					return RedirectToAction("Index");
				}
			}
			catch (Exception ex)
			{
				TempData["errorMessage"] = ex.Message;
				return View();
			}
		}
		[HttpGet]
		public async Task<IActionResult> DoctorProfile()
		{
			return View();
		}

		//Edit details of doctor
		[HttpGet]
		public async Task<IActionResult> EditDoctor(int id)
		{
			try
			{
				var accessToken = HttpContext.Session.GetString("JWToken");
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
				HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress +
					$"/Doctor/GetDoctorById?id={id}");
				if (response.IsSuccessStatusCode)
				{
					string data = response.Content.ReadAsStringAsync().Result;
					var doctor = JsonConvert.DeserializeObject<DoctorVM>(data);
					return View(doctor);
				}
				else
				{
					return RedirectToAction("Index");
				}

			}
			catch (Exception ex)
			{
				TempData["errorMessage"] = ex.Message;
				return View();
			}
		}
		[HttpPost]
		public async Task<IActionResult> EditDoctor(int id, DoctorVM model)
		{
			try
			{
				var accessToken = HttpContext.Session.GetString("JWToken");
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
				string data = JsonConvert.SerializeObject(model);
				StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await _client.PutAsync(_client.BaseAddress +
					$"/Doctor/EditDoctor?id={id}", content);
				if (response.IsSuccessStatusCode)
				{
					TempData["successMessage"] = "Doctor Details Updated.";
					_toastNotification.AddSuccessToastMessage("Doctor Updated successfully");
					return RedirectToAction("Index");
				}
			}
			catch (Exception ex)
			{
				TempData["errorMessage"] = ex.Message;
				_toastNotification.AddErrorToastMessage("Doctor Updated Failed");
				return View();
			}
			return View();
		}

		//Delete docotor 
		public async Task<IActionResult> DeleteDoctor(int id)
		{
			try
			{
				var accessToken = HttpContext.Session.GetString("JWToken");
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
				HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress +
					$"/Doctor/{id}").Result;
				if (response.IsSuccessStatusCode)
				{
					TempData["successMessage"] = "Doctor Details Deleted.";
					_toastNotification.AddAlertToastMessage("Doctor Deleted successfully");
					return RedirectToAction("Index");
				}
			}
			catch (Exception ex)
			{
				TempData["errorMessage"] = ex.Message;
				return View();
			}
			return View();
		}
	}
}
