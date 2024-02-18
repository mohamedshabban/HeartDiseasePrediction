using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeartDiseasePrediction.Models
{
	public class DoctorViewModel
	{
		public int Id { get; set; }
		[Display(Name = "Address")]
		[Required(ErrorMessage = "Doctor Address Is Required"), MaxLength(250)]
		public string Address { get; set; }
		public bool IsAvailable { get; set; }
		[Display(Name = "Specialization")]
		[Required(ErrorMessage = "Doctor Specialization Is Required")]
		public int SpecializationId { get; set; }
		[ForeignKey(nameof(SpecializationId))]
		public virtual Specialization DoctorSpecialization { get; set; }
		public string UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		public virtual ApplicationUser User { get; set; }
		public virtual ICollection<PrescriptionViewModel> prescriptions { get; set; }
		public DoctorViewModel()
		{
			prescriptions = new Collection<PrescriptionViewModel>();
		}

	}
}
