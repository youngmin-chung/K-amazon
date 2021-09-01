using Microsoft.EntityFrameworkCore.Migrations;

namespace K_amazon.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GraphicName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CPU = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    GPU = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    RAM = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SSD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CostPrice = table.Column<decimal>(type: "money", nullable: false),
                    MSRP = table.Column<decimal>(type: "money", nullable: false),
                    QtyOnHand = table.Column<int>(type: "int", nullable: false),
                    QtyOnBackOrder = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
