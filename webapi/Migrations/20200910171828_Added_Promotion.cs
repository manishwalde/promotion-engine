using Microsoft.EntityFrameworkCore.Migrations;

namespace webapi.Migrations
{
    public partial class Added_Promotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumberOfUnit = table.Column<int>(nullable: false),
                    SkuIds = table.Column<string>(nullable: true),
                    ForPrice = table.Column<float>(nullable: false),
                    PromotionType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promotions");
        }
    }
}
