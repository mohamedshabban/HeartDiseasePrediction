using Microsoft.EntityFrameworkCore.Migrations;

namespace HearPrediction.Api.Migrations
{
	public partial class EditTheDataTypeofImage : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "ProfileImg",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: true,
				oldClrType: typeof(byte[]),
				oldType: "varbinary(max)",
				oldNullable: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<byte[]>(
				name: "ProfileImg",
				table: "AspNetUsers",
				type: "varbinary(max)",
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)",
				oldNullable: true);
		}
	}
}
