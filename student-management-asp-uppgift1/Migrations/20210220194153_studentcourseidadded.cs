using Microsoft.EntityFrameworkCore.Migrations;

namespace student_management_asp_uppgift1.Migrations
{
    public partial class studentcourseidadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Study",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Study");
        }
    }
}
