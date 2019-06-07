using Microsoft.EntityFrameworkCore.Migrations;

namespace MRS.Data.Migrations
{
    public partial class DeleteDevices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Devices_DeviceId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Devices",
                table: "Devices");

            migrationBuilder.RenameTable(
                name: "Devices",
                newName: "MrsDevice");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MrsDevice",
                table: "MrsDevice",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MrsDevice_DeviceId",
                table: "AspNetUsers",
                column: "DeviceId",
                principalTable: "MrsDevice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MrsDevice_DeviceId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MrsDevice",
                table: "MrsDevice");

            migrationBuilder.RenameTable(
                name: "MrsDevice",
                newName: "Devices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devices",
                table: "Devices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Devices_DeviceId",
                table: "AspNetUsers",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
