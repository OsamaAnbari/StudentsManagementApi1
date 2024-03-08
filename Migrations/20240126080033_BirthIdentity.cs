using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Students_Management_Api.Migrations
{
    /// <inheritdoc />
    public partial class BirthIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tc",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "birth",
                table: "Student",
                newName: "Birth");

            migrationBuilder.AddColumn<string>(
                name: "IdentityNo",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityNo",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "Birth",
                table: "Student",
                newName: "birth");

            migrationBuilder.AddColumn<string>(
                name: "Tc",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
