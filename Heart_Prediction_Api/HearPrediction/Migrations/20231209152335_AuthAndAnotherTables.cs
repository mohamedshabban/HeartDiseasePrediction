﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace HearPrediction.Api.Migrations
{
	public partial class AuthAndAnotherTables : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "AspNetRoles",
				columns: table => new
				{
					Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUsers",
				columns: table => new
				{
					Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
					FullName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
					FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					Gender = table.Column<int>(type: "int", nullable: false),
					ProfileImg = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
					UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
					PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
					SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
					TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
					LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
					LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
					AccessFailedCount = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUsers", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Labs",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					PhoneNumber = table.Column<long>(type: "bigint", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Labs", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Specializations",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Specializations", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetRoleClaims",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserClaims",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetUserClaims_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserLogins",
				columns: table => new
				{
					LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
					ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
					ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
					table.ForeignKey(
						name: "FK_AspNetUserLogins_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserRoles",
				columns: table => new
				{
					UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserTokens",
				columns: table => new
				{
					UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
					table.ForeignKey(
						name: "FK_AspNetUserTokens_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Reciptionists",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Reciptionists", x => x.Id);
					table.ForeignKey(
						name: "FK_Reciptionists_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Doctors",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
					IsAvailable = table.Column<bool>(type: "bit", nullable: false),
					SpecializationId = table.Column<int>(type: "int", nullable: false),
					UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
					ReciptionistId = table.Column<int>(type: "int", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Doctors", x => x.Id);
					table.ForeignKey(
						name: "FK_Doctors_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Doctors_Reciptionists_ReciptionistId",
						column: x => x.ReciptionistId,
						principalTable: "Reciptionists",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Doctors_Specializations_SpecializationId",
						column: x => x.SpecializationId,
						principalTable: "Specializations",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "MedicalAnalysts",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
					LabId = table.Column<int>(type: "int", nullable: false),
					ReciptionistId = table.Column<int>(type: "int", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MedicalAnalysts", x => x.Id);
					table.ForeignKey(
						name: "FK_MedicalAnalysts_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_MedicalAnalysts_Labs_LabId",
						column: x => x.LabId,
						principalTable: "Labs",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_MedicalAnalysts_Reciptionists_ReciptionistId",
						column: x => x.ReciptionistId,
						principalTable: "Reciptionists",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Patients",
				columns: table => new
				{
					SSN = table.Column<long>(type: "bigint", nullable: false),
					Insurance_No = table.Column<int>(type: "int", nullable: false),
					UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
					DoctorId = table.Column<int>(type: "int", nullable: true),
					LabId = table.Column<int>(type: "int", nullable: true),
					ReciptionistId = table.Column<int>(type: "int", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Patients", x => x.SSN);
					table.ForeignKey(
						name: "FK_Patients_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Patients_Doctors_DoctorId",
						column: x => x.DoctorId,
						principalTable: "Doctors",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Patients_Labs_LabId",
						column: x => x.LabId,
						principalTable: "Labs",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Patients_Reciptionists_ReciptionistId",
						column: x => x.ReciptionistId,
						principalTable: "Reciptionists",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Appointments",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					date = table.Column<DateTime>(type: "datetime2", nullable: false),
					StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
					EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
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

			migrationBuilder.CreateTable(
				name: "MedicalTest",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					date = table.Column<DateTime>(type: "datetime2", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
					RefferenceRange = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PatientSSN = table.Column<long>(type: "bigint", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MedicalTest", x => x.Id);
					table.ForeignKey(
						name: "FK_MedicalTest_Patients_PatientSSN",
						column: x => x.PatientSSN,
						principalTable: "Patients",
						principalColumn: "SSN",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Prescriptions",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					MedicineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					date = table.Column<DateTime>(type: "datetime2", nullable: false),
					PatientSSN = table.Column<long>(type: "bigint", nullable: false),
					DoctorId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Prescriptions", x => x.Id);
					table.ForeignKey(
						name: "FK_Prescriptions_Doctors_DoctorId",
						column: x => x.DoctorId,
						principalTable: "Doctors",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Prescriptions_Patients_PatientSSN",
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

			migrationBuilder.CreateIndex(
				name: "IX_AspNetRoleClaims_RoleId",
				table: "AspNetRoleClaims",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "RoleNameIndex",
				table: "AspNetRoles",
				column: "NormalizedName",
				unique: true,
				filter: "[NormalizedName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserClaims_UserId",
				table: "AspNetUserClaims",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserLogins_UserId",
				table: "AspNetUserLogins",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserRoles_RoleId",
				table: "AspNetUserRoles",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "EmailIndex",
				table: "AspNetUsers",
				column: "NormalizedEmail");

			migrationBuilder.CreateIndex(
				name: "UserNameIndex",
				table: "AspNetUsers",
				column: "NormalizedUserName",
				unique: true,
				filter: "[NormalizedUserName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_Doctors_ReciptionistId",
				table: "Doctors",
				column: "ReciptionistId");

			migrationBuilder.CreateIndex(
				name: "IX_Doctors_SpecializationId",
				table: "Doctors",
				column: "SpecializationId");

			migrationBuilder.CreateIndex(
				name: "IX_Doctors_UserId",
				table: "Doctors",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_MedicalAnalysts_LabId",
				table: "MedicalAnalysts",
				column: "LabId");

			migrationBuilder.CreateIndex(
				name: "IX_MedicalAnalysts_ReciptionistId",
				table: "MedicalAnalysts",
				column: "ReciptionistId");

			migrationBuilder.CreateIndex(
				name: "IX_MedicalAnalysts_UserId",
				table: "MedicalAnalysts",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_MedicalTest_PatientSSN",
				table: "MedicalTest",
				column: "PatientSSN");

			migrationBuilder.CreateIndex(
				name: "IX_Patients_DoctorId",
				table: "Patients",
				column: "DoctorId");

			migrationBuilder.CreateIndex(
				name: "IX_Patients_Insurance_No",
				table: "Patients",
				column: "Insurance_No",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Patients_LabId",
				table: "Patients",
				column: "LabId");

			migrationBuilder.CreateIndex(
				name: "IX_Patients_ReciptionistId",
				table: "Patients",
				column: "ReciptionistId");

			migrationBuilder.CreateIndex(
				name: "IX_Patients_UserId",
				table: "Patients",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_Prescriptions_DoctorId",
				table: "Prescriptions",
				column: "DoctorId");

			migrationBuilder.CreateIndex(
				name: "IX_Prescriptions_PatientSSN",
				table: "Prescriptions",
				column: "PatientSSN");

			migrationBuilder.CreateIndex(
				name: "IX_Reciptionists_UserId",
				table: "Reciptionists",
				column: "UserId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Appointments");

			migrationBuilder.DropTable(
				name: "AspNetRoleClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserLogins");

			migrationBuilder.DropTable(
				name: "AspNetUserRoles");

			migrationBuilder.DropTable(
				name: "AspNetUserTokens");

			migrationBuilder.DropTable(
				name: "MedicalAnalysts");

			migrationBuilder.DropTable(
				name: "MedicalTest");

			migrationBuilder.DropTable(
				name: "Prescriptions");

			migrationBuilder.DropTable(
				name: "AspNetRoles");

			migrationBuilder.DropTable(
				name: "Patients");

			migrationBuilder.DropTable(
				name: "Doctors");

			migrationBuilder.DropTable(
				name: "Labs");

			migrationBuilder.DropTable(
				name: "Reciptionists");

			migrationBuilder.DropTable(
				name: "Specializations");

			migrationBuilder.DropTable(
				name: "AspNetUsers");
		}
	}
}
