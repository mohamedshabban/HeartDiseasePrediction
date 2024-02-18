using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace HearPrediction.Api.Migrations
{
	public partial class AddingMedicalTestAndPrediction : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_MedicalTest_Patients_PatientSSN",
				table: "MedicalTest");

			migrationBuilder.DropPrimaryKey(
				name: "PK_MedicalTest",
				table: "MedicalTest");

			migrationBuilder.DropColumn(
				name: "Name",
				table: "MedicalTest");

			migrationBuilder.DropColumn(
				name: "RefferenceRange",
				table: "MedicalTest");

			migrationBuilder.DropColumn(
				name: "Result",
				table: "MedicalTest");

			migrationBuilder.RenameTable(
				name: "MedicalTest",
				newName: "MedicalTests");

			migrationBuilder.RenameColumn(
				name: "Unit",
				table: "MedicalTests",
				newName: "Diet");

			migrationBuilder.RenameIndex(
				name: "IX_MedicalTest_PatientSSN",
				table: "MedicalTests",
				newName: "IX_MedicalTests_PatientSSN");

			migrationBuilder.AlterColumn<long>(
				name: "PatientSSN",
				table: "Appointments",
				type: "bigint",
				nullable: true,
				oldClrType: typeof(long),
				oldType: "bigint");

			migrationBuilder.AddColumn<float>(
				name: "BMI",
				table: "MedicalTests",
				type: "real",
				nullable: false,
				defaultValue: 0f);

			migrationBuilder.AddColumn<float>(
				name: "BloodPresure",
				table: "MedicalTests",
				type: "real",
				nullable: false,
				defaultValue: 0f);

			migrationBuilder.AddColumn<float>(
				name: "Cholesterol",
				table: "MedicalTests",
				type: "real",
				nullable: false,
				defaultValue: 0f);

			migrationBuilder.AddColumn<float>(
				name: "Diabets",
				table: "MedicalTests",
				type: "real",
				nullable: false,
				defaultValue: 0f);

			migrationBuilder.AddColumn<int>(
				name: "DoctorId",
				table: "MedicalTests",
				type: "int",
				nullable: true);

			migrationBuilder.AddColumn<float>(
				name: "FamilyHistory",
				table: "MedicalTests",
				type: "real",
				nullable: false,
				defaultValue: 0f);

			migrationBuilder.AddColumn<float>(
				name: "HeartRate",
				table: "MedicalTests",
				type: "real",
				nullable: false,
				defaultValue: 0f);

			migrationBuilder.AddColumn<int>(
				name: "MedicalAnalystId",
				table: "MedicalTests",
				type: "int",
				nullable: true);

			migrationBuilder.AddColumn<float>(
				name: "MedicationUse",
				table: "MedicalTests",
				type: "real",
				nullable: false,
				defaultValue: 0f);

			migrationBuilder.AddColumn<float>(
				name: "Obesity",
				table: "MedicalTests",
				type: "real",
				nullable: false,
				defaultValue: 0f);

			migrationBuilder.AddColumn<float>(
				name: "PhysicalActivityDaysPerWeek",
				table: "MedicalTests",
				type: "real",
				nullable: false,
				defaultValue: 0f);

			migrationBuilder.AddColumn<float>(
				name: "PhysicalInactivity",
				table: "MedicalTests",
				type: "real",
				nullable: false,
				defaultValue: 0f);

			migrationBuilder.AddColumn<float>(
				name: "PreviousHeartProblems",
				table: "MedicalTests",
				type: "real",
				nullable: false,
				defaultValue: 0f);

			migrationBuilder.AddColumn<float>(
				name: "SedentaryHoursPerDay",
				table: "MedicalTests",
				type: "real",
				nullable: false,
				defaultValue: 0f);

			migrationBuilder.AddColumn<float>(
				name: "SleepHoursPerDay",
				table: "MedicalTests",
				type: "real",
				nullable: false,
				defaultValue: 0f);

			migrationBuilder.AddColumn<float>(
				name: "Smoking",
				table: "MedicalTests",
				type: "real",
				nullable: false,
				defaultValue: 0f);

			migrationBuilder.AddColumn<float>(
				name: "StressLevel",
				table: "MedicalTests",
				type: "real",
				nullable: false,
				defaultValue: 0f);

			migrationBuilder.AddColumn<float>(
				name: "Triglycerides",
				table: "MedicalTests",
				type: "real",
				nullable: false,
				defaultValue: 0f);

			migrationBuilder.AddPrimaryKey(
				name: "PK_MedicalTests",
				table: "MedicalTests",
				column: "Id");

			migrationBuilder.CreateTable(
				name: "Predictions",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					date = table.Column<DateTime>(type: "datetime2", nullable: false),
					Result = table.Column<bool>(type: "bit", nullable: false),
					probability = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PatientId = table.Column<long>(type: "bigint", nullable: false),
					MedicalId = table.Column<int>(type: "int", nullable: false),
					DoctorId = table.Column<int>(type: "int", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Predictions", x => x.Id);
					table.ForeignKey(
						name: "FK_Predictions_Doctors_DoctorId",
						column: x => x.DoctorId,
						principalTable: "Doctors",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Predictions_MedicalTests_MedicalId",
						column: x => x.MedicalId,
						principalTable: "MedicalTests",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Predictions_Patients_PatientId",
						column: x => x.PatientId,
						principalTable: "Patients",
						principalColumn: "SSN",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_MedicalTests_DoctorId",
				table: "MedicalTests",
				column: "DoctorId");

			migrationBuilder.CreateIndex(
				name: "IX_MedicalTests_MedicalAnalystId",
				table: "MedicalTests",
				column: "MedicalAnalystId");

			migrationBuilder.CreateIndex(
				name: "IX_Predictions_DoctorId",
				table: "Predictions",
				column: "DoctorId");

			migrationBuilder.CreateIndex(
				name: "IX_Predictions_MedicalId",
				table: "Predictions",
				column: "MedicalId");

			migrationBuilder.CreateIndex(
				name: "IX_Predictions_PatientId",
				table: "Predictions",
				column: "PatientId");

			migrationBuilder.AddForeignKey(
				name: "FK_MedicalTests_Doctors_DoctorId",
				table: "MedicalTests",
				column: "DoctorId",
				principalTable: "Doctors",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_MedicalTests_MedicalAnalysts_MedicalAnalystId",
				table: "MedicalTests",
				column: "MedicalAnalystId",
				principalTable: "MedicalAnalysts",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_MedicalTests_Patients_PatientSSN",
				table: "MedicalTests",
				column: "PatientSSN",
				principalTable: "Patients",
				principalColumn: "SSN",
				onDelete: ReferentialAction.Restrict);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_MedicalTests_Doctors_DoctorId",
				table: "MedicalTests");

			migrationBuilder.DropForeignKey(
				name: "FK_MedicalTests_MedicalAnalysts_MedicalAnalystId",
				table: "MedicalTests");

			migrationBuilder.DropForeignKey(
				name: "FK_MedicalTests_Patients_PatientSSN",
				table: "MedicalTests");

			migrationBuilder.DropTable(
				name: "Predictions");

			migrationBuilder.DropPrimaryKey(
				name: "PK_MedicalTests",
				table: "MedicalTests");

			migrationBuilder.DropIndex(
				name: "IX_MedicalTests_DoctorId",
				table: "MedicalTests");

			migrationBuilder.DropIndex(
				name: "IX_MedicalTests_MedicalAnalystId",
				table: "MedicalTests");

			migrationBuilder.DropColumn(
				name: "BMI",
				table: "MedicalTests");

			migrationBuilder.DropColumn(
				name: "BloodPresure",
				table: "MedicalTests");

			migrationBuilder.DropColumn(
				name: "Cholesterol",
				table: "MedicalTests");

			migrationBuilder.DropColumn(
				name: "Diabets",
				table: "MedicalTests");

			migrationBuilder.DropColumn(
				name: "DoctorId",
				table: "MedicalTests");

			migrationBuilder.DropColumn(
				name: "FamilyHistory",
				table: "MedicalTests");

			migrationBuilder.DropColumn(
				name: "HeartRate",
				table: "MedicalTests");

			migrationBuilder.DropColumn(
				name: "MedicalAnalystId",
				table: "MedicalTests");

			migrationBuilder.DropColumn(
				name: "MedicationUse",
				table: "MedicalTests");

			migrationBuilder.DropColumn(
				name: "Obesity",
				table: "MedicalTests");

			migrationBuilder.DropColumn(
				name: "PhysicalActivityDaysPerWeek",
				table: "MedicalTests");

			migrationBuilder.DropColumn(
				name: "PhysicalInactivity",
				table: "MedicalTests");

			migrationBuilder.DropColumn(
				name: "PreviousHeartProblems",
				table: "MedicalTests");

			migrationBuilder.DropColumn(
				name: "SedentaryHoursPerDay",
				table: "MedicalTests");

			migrationBuilder.DropColumn(
				name: "SleepHoursPerDay",
				table: "MedicalTests");

			migrationBuilder.DropColumn(
				name: "Smoking",
				table: "MedicalTests");

			migrationBuilder.DropColumn(
				name: "StressLevel",
				table: "MedicalTests");

			migrationBuilder.DropColumn(
				name: "Triglycerides",
				table: "MedicalTests");

			migrationBuilder.RenameTable(
				name: "MedicalTests",
				newName: "MedicalTest");

			migrationBuilder.RenameColumn(
				name: "Diet",
				table: "MedicalTest",
				newName: "Unit");

			migrationBuilder.RenameIndex(
				name: "IX_MedicalTests_PatientSSN",
				table: "MedicalTest",
				newName: "IX_MedicalTest_PatientSSN");

			migrationBuilder.AlterColumn<long>(
				name: "PatientSSN",
				table: "Appointments",
				type: "bigint",
				nullable: false,
				defaultValue: 0L,
				oldClrType: typeof(long),
				oldType: "bigint",
				oldNullable: true);

			migrationBuilder.AddColumn<string>(
				name: "Name",
				table: "MedicalTest",
				type: "nvarchar(max)",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "RefferenceRange",
				table: "MedicalTest",
				type: "nvarchar(max)",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "Result",
				table: "MedicalTest",
				type: "nvarchar(max)",
				nullable: true);

			migrationBuilder.AddPrimaryKey(
				name: "PK_MedicalTest",
				table: "MedicalTest",
				column: "Id");

			migrationBuilder.AddForeignKey(
				name: "FK_MedicalTest_Patients_PatientSSN",
				table: "MedicalTest",
				column: "PatientSSN",
				principalTable: "Patients",
				principalColumn: "SSN",
				onDelete: ReferentialAction.Restrict);
		}
	}
}
