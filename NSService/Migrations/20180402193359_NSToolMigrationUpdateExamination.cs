using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NSService.Migrations
{
    public partial class NSToolMigrationUpdateExamination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Examinations");

            migrationBuilder.AddColumn<string>(
                name: "ExaminationType",
                table: "Examinations",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExaminationType",
                table: "Examinations");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Examinations",
                nullable: true);
        }
    }
}
