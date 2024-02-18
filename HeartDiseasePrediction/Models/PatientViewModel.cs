using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeartDiseasePrediction.Models
{
	public class PatientViewModel
	{
		[Key]
		[Display(Name = "National ID")]
		public long SSN { get; set; }

		[Required, Display(Name = "Insurance Number")]
		public int Insurance_No { get; set; }
		public string UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		public virtual ApplicationUser User { get; set; }
		public virtual ICollection<PrescriptionViewModel> prescriptions { get; set; }
		public PatientViewModel()
		{
			prescriptions = new Collection<PrescriptionViewModel>();
		}
	}
}
