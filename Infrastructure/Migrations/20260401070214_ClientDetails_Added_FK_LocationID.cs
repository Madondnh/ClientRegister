using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClientDetails_Added_FK_LocationID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientDetails_Location_Id",
                table: "ClientDetails");

            migrationBuilder.AddColumn<string>(
                name: "LocationId",
                table: "ClientDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ClientDetails_LocationId",
                table: "ClientDetails",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientDetails_Location_LocationId",
                table: "ClientDetails",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientDetails_Location_LocationId",
                table: "ClientDetails");

            migrationBuilder.DropIndex(
                name: "IX_ClientDetails_LocationId",
                table: "ClientDetails");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "ClientDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientDetails_Location_Id",
                table: "ClientDetails",
                column: "Id",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
