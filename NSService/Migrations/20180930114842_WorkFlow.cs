using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NSService.Migrations
{
    public partial class WorkFlow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodPressureData_Examinations_Id",
                table: "BloodPressureData");

            migrationBuilder.DropForeignKey(
                name: "FK_BodyTemperatureData_Examinations_Id",
                table: "BodyTemperatureData");

            migrationBuilder.DropForeignKey(
                name: "FK_SpOData_Examinations_Id",
                table: "SpOData");

            migrationBuilder.RenameColumn(
                name: "TemperatureVAlue",
                table: "BodyTemperatureData",
                newName: "TemperatureValue");

            migrationBuilder.AddColumn<int>(
                name: "ExaminationId",
                table: "SpOData",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "Patients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "Examinations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "WorkFlowId",
                table: "Examinations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExaminationId",
                table: "BodyTemperatureData",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExaminationId",
                table: "BloodPressureData",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Password = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workflows",
                columns: table => new
                {
                    WorkFlowId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatientId = table.Column<int>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    WorkFlowName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflows", x => x.WorkFlowId);
                    table.ForeignKey(
                        name: "FK_Workflows_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkFlowStep",
                columns: table => new
                {
                    WorkFlowStepId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkFlowId = table.Column<int>(nullable: true),
                    WorkFlowStepName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowStep", x => x.WorkFlowStepId);
                    table.ForeignKey(
                        name: "FK_WorkFlowStep_Workflows_WorkFlowId",
                        column: x => x.WorkFlowId,
                        principalTable: "Workflows",
                        principalColumn: "WorkFlowId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpOData_ExaminationId",
                table: "SpOData",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_WorkFlowId",
                table: "Examinations",
                column: "WorkFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyTemperatureData_ExaminationId",
                table: "BodyTemperatureData",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodPressureData_ExaminationId",
                table: "BloodPressureData",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_Workflows_PatientId",
                table: "Workflows",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowStep_WorkFlowId",
                table: "WorkFlowStep",
                column: "WorkFlowId");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodPressureData_Examinations_ExaminationId",
                table: "BloodPressureData",
                column: "ExaminationId",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BodyTemperatureData_Examinations_ExaminationId",
                table: "BodyTemperatureData",
                column: "ExaminationId",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_Workflows_WorkFlowId",
                table: "Examinations",
                column: "WorkFlowId",
                principalTable: "Workflows",
                principalColumn: "WorkFlowId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SpOData_Examinations_ExaminationId",
                table: "SpOData",
                column: "ExaminationId",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodPressureData_Examinations_ExaminationId",
                table: "BloodPressureData");

            migrationBuilder.DropForeignKey(
                name: "FK_BodyTemperatureData_Examinations_ExaminationId",
                table: "BodyTemperatureData");

            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_Workflows_WorkFlowId",
                table: "Examinations");

            migrationBuilder.DropForeignKey(
                name: "FK_SpOData_Examinations_ExaminationId",
                table: "SpOData");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkFlowStep");

            migrationBuilder.DropTable(
                name: "Workflows");

            migrationBuilder.DropIndex(
                name: "IX_SpOData_ExaminationId",
                table: "SpOData");

            migrationBuilder.DropIndex(
                name: "IX_Examinations_WorkFlowId",
                table: "Examinations");

            migrationBuilder.DropIndex(
                name: "IX_BodyTemperatureData_ExaminationId",
                table: "BodyTemperatureData");

            migrationBuilder.DropIndex(
                name: "IX_BloodPressureData_ExaminationId",
                table: "BloodPressureData");

            migrationBuilder.DropColumn(
                name: "ExaminationId",
                table: "SpOData");

            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "WorkFlowId",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "ExaminationId",
                table: "BodyTemperatureData");

            migrationBuilder.DropColumn(
                name: "ExaminationId",
                table: "BloodPressureData");

            migrationBuilder.RenameColumn(
                name: "TemperatureValue",
                table: "BodyTemperatureData",
                newName: "TemperatureVAlue");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodPressureData_Examinations_Id",
                table: "BloodPressureData",
                column: "Id",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BodyTemperatureData_Examinations_Id",
                table: "BodyTemperatureData",
                column: "Id",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpOData_Examinations_Id",
                table: "SpOData",
                column: "Id",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
