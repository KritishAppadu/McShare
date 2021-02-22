using Microsoft.EntityFrameworkCore.Migrations;

namespace McShare.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<string>(name: "Customer ID", type: "nvarchar(450)", nullable: false),
                    CustomerName = table.Column<string>(name: "Customer Name", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateofBirth = table.Column<string>(name: "Date of Birth", type: "nvarchar(max)", nullable: true),
                    IncorpDate = table.Column<string>(name: "Incorp Date", type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactDetail = table.Column<double>(name: "Contact Detail", type: "float", nullable: false),
                    RegistrationNumber = table.Column<string>(name: "Registration Number", type: "nvarchar(max)", nullable: true),
                    CustomerType = table.Column<string>(name: "Customer Type", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NumberofShares = table.Column<int>(name: "Number of Shares", type: "int", nullable: false),
                    Sharesperunitprice = table.Column<double>(name: "Shares per unit price", type: "float", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "ErrorLog",
                columns: table => new
                {
                    ErrorID = table.Column<int>(name: "Error ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ErrorDescrition = table.Column<string>(name: "Error Descrition", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLog", x => x.ErrorID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "ErrorLog");
        }
    }
}
