using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.Demo.Migrations
{
    public partial class propertiestablessettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppPropertySettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    RequiredRegEx = table.Column<bool>(type: "bit", nullable: false),
                    RegExRule = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPropertySettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppPropertySettings_Name_ProviderName_ProviderKey",
                table: "AppPropertySettings",
                columns: new[] { "Name", "ProviderName", "ProviderKey" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppPropertySettings");
        }
    }
}
