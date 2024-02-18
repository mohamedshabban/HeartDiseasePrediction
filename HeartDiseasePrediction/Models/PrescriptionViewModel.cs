using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeartDiseasePrediction.Models
{
	public class PrescriptionViewModel
	{
		public int Id { get; set; }
		[Required]
		public string MedicineName { get; set; }
		public DateTime date { get; set; }
		public long PatientSSN { get; set; }
		[ForeignKey(nameof(PatientSSN))]
		public virtual PatientViewModel Patient { get; set; }
		public int DoctorId { get; set; }
		[ForeignKey(nameof(DoctorId))]
		public virtual DoctorViewModel Doctor { get; set; }
		public PrescriptionViewModel()
		{
			date = DateTime.Now;
		}
	}
}
