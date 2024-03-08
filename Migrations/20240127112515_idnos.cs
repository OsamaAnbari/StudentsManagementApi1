using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Students_Management_Api.Migrations
{
    /// <inheritdoc />
    public partial class idnos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "birth",
                table: "Teacher",
                newName: "Birth");

            migrationBuilder.RenameColumn(
                name: "Tc",
                table: "Teacher",
                newName: "IdentityNo");

            migrationBuilder.RenameColumn(
                name: "birth",
                table: "Supervisor",
                newName: "Birth");

            migrationBuilder.RenameColumn(
                name: "Tc",
                table: "Supervisor",
                newName: "IdentityNo");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityNo",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birth",
                table: "Teacher",
                newName: "birth");

            migrationBuilder.RenameColumn(
                name: "IdentityNo",
                table: "Teacher",
                newName: "Tc");

            migrationBuilder.RenameColumn(
                name: "Birth",
                table: "Supervisor",
                newName: "birth");

            migrationBuilder.RenameColumn(
                name: "IdentityNo",
                table: "Supervisor",
                newName: "Tc");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityNo",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
