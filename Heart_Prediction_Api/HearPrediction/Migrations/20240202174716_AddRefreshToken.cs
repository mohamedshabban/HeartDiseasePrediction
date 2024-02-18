﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace HearPrediction.Api.Migrations
{
	public partial class AddRefreshToken : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "RefreshToken",
				columns: table => new
				{
					ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
					CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
					RevokedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_RefreshToken", x => new { x.ApplicationUserId, x.Id });
					table.ForeignKey(
						name: "FK_RefreshToken_AspNetUsers_ApplicationUserId",
						column: x => x.ApplicationUserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "RefreshToken");
		}
	}
}
