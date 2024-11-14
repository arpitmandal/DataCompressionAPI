using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataCompressionAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigrationCreateInitialSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileSize",
                table: "FileLogs",
                newName: "ReducedSize");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "FileLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "FileLogs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Error",
                table: "FileLogs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "FileLogs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FinalSize",
                table: "FileLogs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "InitialSize",
                table: "FileLogs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FileLogs_CreatedBy",
                table: "FileLogs",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_FileLogs_Users_CreatedBy",
                table: "FileLogs",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileLogs_Users_CreatedBy",
                table: "FileLogs");

            migrationBuilder.DropIndex(
                name: "IX_FileLogs_CreatedBy",
                table: "FileLogs");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FileLogs");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "FileLogs");

            migrationBuilder.DropColumn(
                name: "Error",
                table: "FileLogs");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "FileLogs");

            migrationBuilder.DropColumn(
                name: "FinalSize",
                table: "FileLogs");

            migrationBuilder.DropColumn(
                name: "InitialSize",
                table: "FileLogs");

            migrationBuilder.RenameColumn(
                name: "ReducedSize",
                table: "FileLogs",
                newName: "FileSize");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
