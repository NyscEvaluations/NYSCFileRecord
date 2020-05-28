using Microsoft.EntityFrameworkCore.Migrations;

namespace NYSCFileRecord.Data.Migrations
{
    public partial class UpdatedStatusInCDSReccordsToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "CDSRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CDSRecord");
        }
    }
}
