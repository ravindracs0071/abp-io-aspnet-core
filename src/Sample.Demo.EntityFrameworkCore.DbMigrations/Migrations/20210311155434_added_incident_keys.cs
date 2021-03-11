using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.Demo.Migrations
{
    public partial class added_incident_keys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
             name: "FK_AppIncidentDetails_AppIncidentMasters_IncidentMasterId",
             table: "AppIncidentDetails",
             column: "IncidentMasterId",
             principalTable: "AppIncidentMasters",
             principalColumn: "Id",
             onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
             name: "FK_AppReviewDetails_AppIncidentDetails_IncidentDetailId",
             table: "AppReviewDetails",
             column: "IncidentDetailId",
             principalTable: "AppIncidentDetails",
             principalColumn: "Id",
             onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppReviewDetails_AppIncidentDetails_IncidentDetailId",
                table: "AppReviewDetails");
            migrationBuilder.DropForeignKey(
                name: "FK_AppIncidentDetails_AppIncidentMasters_IncidentMasterId",
                table: "AppIncidentDetails");
        }
    }
}
