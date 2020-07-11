using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NYSCFileRecord.Data.Migrations
{
    public partial class UpdateFileRecordModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentLocation",
                table: "FileRecord",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateReturned",
                table: "FileRecord",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "shelfNumber",
                table: "FileRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentLocation",
                table: "FileRecord");

            migrationBuilder.DropColumn(
                name: "DateReturned",
                table: "FileRecord");

            migrationBuilder.DropColumn(
                name: "shelfNumber",
                table: "FileRecord");
        }
    }
}
