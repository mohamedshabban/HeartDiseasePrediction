using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class Prediction
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public bool Result { get; set; }
        public string probability { get; set; }
        public long PatientId { get; set; }
        [ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; }
        //public int MedicalId { get; set; }
        //[ForeignKey(nameof(MedicalId))]
        //public MedicalTest MedicalTest { get; set; }
        //public ICollection<MedicalTest> MedicalTest { get; set; }
        public Prediction()
        {
            date = DateTime.Now;
        }
    }
}
