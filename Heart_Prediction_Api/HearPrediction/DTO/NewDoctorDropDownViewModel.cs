using HearPrediction.Api.Model;
using System.Collections.Generic;

namespace HearPrediction.Api.DTO
{
	public class NewDoctorDropDownViewModel
	{
		public NewDoctorDropDownViewModel()
		{
			specializations = new List<Specialization>();
		}
		public List<Specialization> specializations { get; set; }
	}
}
