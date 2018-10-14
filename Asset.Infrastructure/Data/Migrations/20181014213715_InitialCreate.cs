using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Asset.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Asset");

            migrationBuilder.CreateSequence(
                name: "UserHilo",
                schema: "Asset",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Asset",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR Asset.UserHilo"),
                    UpdateDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()"),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User",
                schema: "Asset");

            migrationBuilder.DropSequence(
                name: "UserHilo",
                schema: "Asset");
        }
    }
}
