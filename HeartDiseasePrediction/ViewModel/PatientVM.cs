﻿using HeartDiseasePrediction.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeartDiseasePrediction.ViewModel
{
	public class PatientVM
	{
		[Key]
		[Required(ErrorMessage = "SSN Is Required")]
		[Display(Name = "SSN")]
		public long SSN { get; set; }
		[Display(Name = "First Name"), StringLength(100)]
		[Required(ErrorMessage = "First Name Is Required")]
		public string FirstName { get; set; }
		[Display(Name = "Last Name"), StringLength(100)]
		[Required(ErrorMessage = "Last Name Is Required")]
		public string LastName { get; set; }
		[Display(Name = "Full Name"), StringLength(250)]
		[Required(ErrorMessage = "Full Name Is Required")]
		public string FullName { get; set; }
		[Display(Name = "Gender")]
		[Required(ErrorMessage = "Gender Is Required")]
		public Gender Gender { get; set; }
		[Required(ErrorMessage = "Phone Number Is Required")]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }
		[Required(ErrorMessage = "Insurance Number Is Required")]
		[Display(Name = "Insurance Number")]
		public int Insurance_No { get; set; }
		[Required(ErrorMessage = "Birth Date Is Required")]
		[Display(Name = "Birth Date")]
		public DateTime BirthDate { get; set; }
		[Display(Name = "Email"), StringLength(200)]
		[Required(ErrorMessage = "Email Is Required")]
		public string Email { get; set; }
		[Display(Name = "Profile Image")]
		public byte[] ProfileImg { get; set; }
		[NotMapped]
		[Display(Name = "Upload File")]
		public IFormFile ImageFile { get; set; }
		[Display(Name = "Age")]
		public int Age => CalculateAge();
		private int CalculateAge()
		{
			int age = DateTime.Now.Year - BirthDate.Year;
			if (DateTime.Now.DayOfYear < BirthDate.DayOfYear)
			{
				age--;
			}
			return age;
		}
	}
}
