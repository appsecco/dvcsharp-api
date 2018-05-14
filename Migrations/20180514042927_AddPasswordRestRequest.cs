using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace dvcsharpcoreapi.Migrations
{
    public partial class AddPasswordRestRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.AlterColumn<string>(
            //     name: "role",
            //     table: "Users",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldNullable: true);

            // migrationBuilder.AlterColumn<string>(
            //     name: "password",
            //     table: "Users",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldNullable: true);

            // migrationBuilder.AlterColumn<string>(
            //     name: "name",
            //     table: "Users",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldNullable: true);

            // migrationBuilder.AlterColumn<string>(
            //     name: "email",
            //     table: "Users",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PasswordResetRequests",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    key = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    passwordConfirmation = table.Column<string>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetRequests", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasswordResetRequests");

            // migrationBuilder.AlterColumn<string>(
            //     name: "role",
            //     table: "Users",
            //     nullable: true,
            //     oldClrType: typeof(string));

            // migrationBuilder.AlterColumn<string>(
            //     name: "password",
            //     table: "Users",
            //     nullable: true,
            //     oldClrType: typeof(string));

            // migrationBuilder.AlterColumn<string>(
            //     name: "name",
            //     table: "Users",
            //     nullable: true,
            //     oldClrType: typeof(string));

            // migrationBuilder.AlterColumn<string>(
            //     name: "email",
            //     table: "Users",
            //     nullable: true,
            //     oldClrType: typeof(string));
        }
    }
}
