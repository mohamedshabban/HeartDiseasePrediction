using System.ComponentModel.DataAnnotations;

namespace HeartDiseasePrediction.Models
{
	public class Specialization
	{
		public int Id { get; set; }
		[Display(Name = "Specialization Name")]
		[MaxLength(100)]
		public string Name { get; set; }
	}
}
