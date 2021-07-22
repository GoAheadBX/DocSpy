using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DocSpy.Migrations
{
    public partial class Delete_Country_time_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppCountry");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AppCountry");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppCountry");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "AppCountry");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AppCountry",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "AppCountry",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppCountry",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "AppCountry",
                type: "uuid",
                nullable: true);
        }
    }
}
