using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeartDiseasePrediction.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
		[Display(Name = "Email")]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[Display(Name = "Password")]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Confirm Password")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

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
		[Display(Name = "Age")]
		public int Age => CalculateAge();

		[Display(Name = "Address"), StringLength(250)]
		[Required(ErrorMessage = "Address Is Required")]
		public string Address { get; set; }
		[Display(Name = "Profile Image")]
		public string ProfileImg { get; set; }
		[NotMapped]
		[Display(Name = "Upload File")]
		public IFormFile? ImageFile { get; set; }
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
