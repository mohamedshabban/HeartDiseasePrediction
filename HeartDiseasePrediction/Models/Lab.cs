using HeartDiseasePrediction.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class Lab
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Name")]
        [MaxLength(100)]
        public string Name { get; set; }
        public long PhoneNumber { get; set; }
        public virtual ICollection<MedicalAnalystViewModel> MedicalAnalysts { get; set; }
        public Lab()
        {
            MedicalAnalysts = new Collection<MedicalAnalystViewModel>();
        }
    }
}
