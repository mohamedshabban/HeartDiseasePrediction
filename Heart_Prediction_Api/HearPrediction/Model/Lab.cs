﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HearPrediction.Api.Model
{
	public class Lab
	{
		[Key]
		public int Id { get; set; }
		[Display(Name = "Name")]
		[MaxLength(100)]
		public string Name { get; set; }
		public long PhoneNumber { get; set; }
		public virtual ICollection<Patient> Patients { get; set; }
		public virtual ICollection<MedicalAnalyst> MedicalAnalysts { get; set; }
		//public Lab() 
		//{ 
		//	Patients = new Collection<Patient>();
		//	MedicalAnalysts = new Collection<MedicalAnalyst>();
		//}
	}
}
