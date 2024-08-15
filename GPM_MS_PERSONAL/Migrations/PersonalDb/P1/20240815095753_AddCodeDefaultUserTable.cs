using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace application.Migrations.PersonalDb.P1
{
    public partial class AddCodeDefaultUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CodeDefaultUser",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRoleId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedByType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedByType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeDefaultUser", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodeDefaultUser");
        }
    }
}
