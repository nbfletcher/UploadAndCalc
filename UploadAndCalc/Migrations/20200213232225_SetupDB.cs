using Microsoft.EntityFrameworkCore.Migrations;

namespace UploadAndCalc.Migrations
{
    public partial class SetupDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalcItems",
                columns: table => new
                {
                    CalcItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalcItemValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalcItems", x => x.CalcItemId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalcItems");
        }
    }
}
