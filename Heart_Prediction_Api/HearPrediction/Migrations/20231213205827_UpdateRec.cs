using Microsoft.EntityFrameworkCore.Migrations;

namespace HearPrediction.Api.Migrations
{
	public partial class UpdateRec : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Doctors_Reciptionists_ReciptionistId",
				table: "Doctors");

			migrationBuilder.DropForeignKey(
				name: "FK_MedicalAnalysts_Reciptionists_ReciptionistId",
				table: "MedicalAnalysts");

			migrationBuilder.DropForeignKey(
				name: "FK_Patients_Reciptionists_ReciptionistId",
				table: "Patients");

			migrationBuilder.DropIndex(
				name: "IX_Patients_ReciptionistId",
				table: "Patients");

			migrationBuilder.DropIndex(
				name: "IX_MedicalAnalysts_ReciptionistId",
				table: "MedicalAnalysts");

			migrationBuilder.DropIndex(
				name: "IX_Doctors_ReciptionistId",
				table: "Doctors");

			migrationBuilder.DropColumn(
				name: "ReciptionistId",
				table: "Patients");

			migrationBuilder.DropColumn(
				name: "ReciptionistId",
				table: "MedicalAnalysts");

			migrationBuilder.DropColumn(
				name: "ReciptionistId",
				table: "Doctors");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<int>(
				name: "ReciptionistId",
				table: "Patients",
				type: "int",
				nullable: true);

			migrationBuilder.AddColumn<int>(
				name: "ReciptionistId",
				table: "MedicalAnalysts",
				type: "int",
				nullable: true);

			migrationBuilder.AddColumn<int>(
				name: "ReciptionistId",
				table: "Doctors",
				type: "int",
				nullable: true);

			migrationBuilder.CreateIndex(
				name: "IX_Patients_ReciptionistId",
				table: "Patients",
				column: "ReciptionistId");

			migrationBuilder.CreateIndex(
				name: "IX_MedicalAnalysts_ReciptionistId",
				table: "MedicalAnalysts",
				column: "ReciptionistId");

			migrationBuilder.CreateIndex(
				name: "IX_Doctors_ReciptionistId",
				table: "Doctors",
				column: "ReciptionistId");

			migrationBuilder.AddForeignKey(
				name: "FK_Doctors_Reciptionists_ReciptionistId",
				table: "Doctors",
				column: "ReciptionistId",
				principalTable: "Reciptionists",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_MedicalAnalysts_Reciptionists_ReciptionistId",
				table: "MedicalAnalysts",
				column: "ReciptionistId",
				principalTable: "Reciptionists",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Patients_Reciptionists_ReciptionistId",
				table: "Patients",
				column: "ReciptionistId",
				principalTable: "Reciptionists",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}
	}
}
