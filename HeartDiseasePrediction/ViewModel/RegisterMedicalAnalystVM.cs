﻿using HeartDiseasePrediction.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace HeartDiseasePrediction.ViewModel
{
	public class RegisterMedicalAnalystVM
	{
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
		[Display(Name = "Lab")]
		[Required(ErrorMessage = "Lab Is Required")]
		public int Lab { get; set; }
		//public IEnumerable<Lab> Labs { get; set; }
		[Required(ErrorMessage = "Phone Number Is Required")]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }
		[Required(ErrorMessage = "Birth Date Is Required")]
		[Display(Name = "Birth Date")]
		public DateTime BirthDate { get; set; }

		[Display(Name = "Email"), StringLength(200)]
		[Required(ErrorMessage = "Email Is Required")]
		public string Email { get; set; }
		[Required, StringLength(100, MinimumLength = 6)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Display(Name = "Confirm Password")]
		[Required(ErrorMessage = "Confirm Password Is Required"), StringLength(250)]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Passwords Not Match")]
		public string ConfirmPassword { get; set; }
	}
}
