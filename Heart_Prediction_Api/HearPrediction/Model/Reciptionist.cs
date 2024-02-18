using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearPrediction.Api.Model
{
	public class Reciptionist
	{
		[Key]
		public int Id { get; set; }
		public string UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		public virtual ApplicationUser User { get; set; }
		//public ICollection<Patient> Patients { get; set; }
		//public ICollection<Doctor> Doctors { get; set; }
		//public ICollection<MedicalAnalyst> MedicalAnalysts { get; set; }
		//public Reciptionist() 
		//{
		//	Patients =new Collection <Patient>(); 
		//	Doctors =new Collection <Doctor>(); 
		//	MedicalAnalysts =new Collection <MedicalAnalyst>(); 
		//}
	}
}
