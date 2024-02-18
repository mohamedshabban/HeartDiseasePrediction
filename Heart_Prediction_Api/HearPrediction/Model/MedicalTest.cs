using System;
using System.ComponentModel.DataAnnotations;

namespace HearPrediction.Api.Model
{
	public class MedicalTest
	{
		[Key]
		public int Id { get; set; }
		public DateTime date { get; set; }
		public float Cholesterol { get; set; }
		public float BloodPresure { get; set; }
		public float HeartRate { get; set; }
		public float Diabets { get; set; }
		public float FamilyHistory { get; set; }
		public float Smoking { get; set; }
		public float Obesity { get; set; }
		public float PhysicalInactivity { get; set; }
		public string Diet { get; set; }
		public float PreviousHeartProblems { get; set; }
		public float MedicationUse { get; set; }
		public float StressLevel { get; set; }
		public float SedentaryHoursPerDay { get; set; }
		public float BMI { get; set; }
		public float Triglycerides { get; set; }
		public float PhysicalActivityDaysPerWeek { get; set; }
		public float SleepHoursPerDay { get; set; }
		//public int PredictionId { get; set; }
		//[ForeignKey(nameof(PredictionId))]
		//public Prediction prediction { get; set; }
		//public long PatientSSN { get; set; }
		//[ForeignKey(nameof(PatientSSN))]
		//public Patient patient { get; set; }
		//public int MedicalAnalystId { get; set; }
		//[ForeignKey(nameof(MedicalAnalystId))]
		//public MedicalAnalyst medicalAnalyst { get; set; }
		public MedicalTest()
		{
			date = DateTime.Now;
		}
	}
}
