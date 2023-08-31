using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Amount", "Name", "Unit" },
                values: new object[,]
                {
                    { new Guid("01de28c6-c36b-4af4-827c-0a24d9e06a6b"), 50.0, "sugar", 0 },
                    { new Guid("0c257fe9-6a05-4180-8cbd-a0b2fb7762b5"), 20.0, "red pepper flakes", 1 },
                    { new Guid("222f5dc0-fe84-44fa-a28b-9a374cbf95cb"), 12.0, "ladyfingers", 1 },
                    { new Guid("3d39b5bc-8b25-4bcf-97eb-b7c7071ec457"), 200.0, "pasta", 0 },
                    { new Guid("3f29b668-20bf-4836-b4b9-7f38c0ab4ee7"), 100.0, "flour", 0 },
                    { new Guid("42ac06dd-ade7-49e4-b5c3-7c6df9ca394e"), 50.0, "Parmesan cheese", 0 },
                    { new Guid("5b97465e-c4d5-4a10-8af4-8872d8c8984c"), 25.0, "capers", 0 },
                    { new Guid("5c52832a-d88c-4de0-a3af-b844e3183221"), 250.0, "mascarpone cheese", 0 },
                    { new Guid("5e8d07cf-b63b-4de9-bbf2-ff9bf628d44b"), 4.0, "anchovy fillets", 1 },
                    { new Guid("6cd66f6d-4899-45e4-9a31-c68fdb2c4dfa"), 100.0, "pancetta", 0 },
                    { new Guid("71ad94e1-161d-42ea-8ebe-ab36a9f0b12b"), 250.0, "heavy cream", 2 },
                    { new Guid("842c6c80-2799-4e36-bef7-7e7f72fc3121"), 1.0, "tomatoes", 3 },
                    { new Guid("87d3a2b4-67ab-4773-8301-9c9cf88daa96"), 200.0, "spaghetti", 0 },
                    { new Guid("9b2507c0-78fc-4571-8728-78c4557f41d2"), 1.0, "tomato", 1 },
                    { new Guid("9da8f35a-e6b3-4c30-b1e5-82c73820fae3"), 50.0, "olives", 0 },
                    { new Guid("bc0a5d53-5b84-49cb-b7f0-89d03c0c6589"), 100.0, "espresso", 2 },
                    { new Guid("d1b73503-1bd8-48e4-945c-4d6806c8565a"), 2.0, "eggs", 1 },
                    { new Guid("ddd74ae5-1345-495f-85ce-50e3f9733770"), 75.0, "cheese", 0 },
                    { new Guid("f8829d14-56fb-4d45-9b9d-87e1cbdca91d"), 10.0, "cocoa powder", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");
        }
    }
}
