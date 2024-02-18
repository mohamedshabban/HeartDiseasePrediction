using Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeartDiseasePrediction.Models
{
    public class MedicalAnalystViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
        public int LabId { get; set; }
        [ForeignKey(nameof(LabId))]
        public virtual Lab lab { get; set; }
    }
}
