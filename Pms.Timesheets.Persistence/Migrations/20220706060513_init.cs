﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pms.Timesheets.Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "timesheet",
                columns: table => new
                {
                    id = table.Column<string>(type: "VARCHAR(35)", nullable: false),
                    EEId = table.Column<string>(type: "VARCHAR(8)", nullable: false),
                    CutoffDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    PayrollCode = table.Column<string>(type: "VARCHAR(6)", nullable: false),
                    BankCategory = table.Column<string>(type: "VARCHAR(6)", nullable: false),
                    TotalHours = table.Column<double>(type: "DOUBLE(6,2)", nullable: false),
                    TotalOT = table.Column<double>(type: "DOUBLE(6,2)", nullable: false),
                    TotalRDOT = table.Column<double>(type: "DOUBLE(6,2)", nullable: false),
                    TotalHOT = table.Column<double>(type: "DOUBLE(6,2)", nullable: false),
                    TotalND = table.Column<double>(type: "DOUBLE(6,2)", nullable: false),
                    TotalTardy = table.Column<double>(type: "DOUBLE(6,2)", nullable: false),
                    Allowance = table.Column<double>(type: "DOUBLE(8,2)", nullable: false),
                    RawPCV = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    IsConfirmed = table.Column<double>(type: "DOUBLE(8,2)", nullable: false),
                    Page = table.Column<byte>(type: "TINYINT", nullable: false, comment: "Time System API Page"),
                    DateCreated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timesheet", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_timesheet_EEId",
                table: "timesheet",
                column: "EEId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "timesheet");
        }
    }
}
