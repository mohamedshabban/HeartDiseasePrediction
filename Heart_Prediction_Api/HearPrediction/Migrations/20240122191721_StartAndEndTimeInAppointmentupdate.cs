using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace HearPrediction.Api.Migrations
{
	public partial class StartAndEndTimeInAppointmentupdate : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Appointments",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					date = table.Column<DateTime>(type: "datetime2", nullable: false),
					StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
					EndTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Detail = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
					Status = table.Column<bool>(type: "bit", nullable: false),
					PatientSSN = table.Column<long>(type: "bigint", nullable: true),
					DoctorId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Appointments", x => x.Id);
					table.ForeignKey(
						name: "FK_Appointments_Doctors_DoctorId",
						column: x => x.DoctorId,
						principalTable: "Doctors",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Appointments_Patients_PatientSSN",
						column: x => x.PatientSSN,
						principalTable: "Patients",
						principalColumn: "SSN",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Appointments_DoctorId",
				table: "Appointments",
				column: "DoctorId");

			migrationBuilder.CreateIndex(
				name: "IX_Appointments_PatientSSN",
				table: "Appointments",
				column: "PatientSSN");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Appointments");
		}
	}
}
