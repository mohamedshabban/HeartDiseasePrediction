using HearPrediction.Api.Enums;
using HearPrediction.Api.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearPrediction.Api.DTO
{
	public class DoctorFormDTO
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
		[Required(ErrorMessage = "Phone Number Is Required")]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }
		[Display(Name = "Birth Date")]
		[Required(ErrorMessage = "Birth Date Is Required")]
		public DateTime BirthDate { get; set; }
		[Display(Name = "Doctor Specialization")]
		[Required(ErrorMessage = "Doctor Specialization Is Required")]
		public int Specialization { get; set; }
		public virtual Specialization Specializations { get; set; }

		[Display(Name = "Address"), StringLength(250)]
		[Required(ErrorMessage = "Address Is Required")]
		public string Address { get; set; }
		[Display(Name = "Email"), StringLength(200)]
		[Required(ErrorMessage = "Email Is Required")]
		public string Email { get; set; }
		[Display(Name = "Profile Image")]
		public string ProfileImg { get; set; }
		[NotMapped]
		[Display(Name = "Upload File")]
		public IFormFile? ImageFile { get; set; }
	}
}
