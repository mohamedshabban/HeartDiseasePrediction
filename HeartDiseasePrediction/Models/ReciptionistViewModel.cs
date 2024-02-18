using System.ComponentModel.DataAnnotations.Schema;

namespace HeartDiseasePrediction.Models
{
	public class ReciptionistViewModel
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		public virtual ApplicationUser User { get; set; }
	}
}
