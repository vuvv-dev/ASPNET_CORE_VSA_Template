using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FA1.Src.Migrations.M_AppDbContext
{
    /// <inheritdoc />
    public partial class M2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "todo",
                table: "user_token",
                type: "character varying(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "expired_at",
                schema: "todo",
                table: "user_token",
                type: "TIMESTAMP WITH TIME ZONE",
                nullable: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Discriminator", schema: "todo", table: "user_token");

            migrationBuilder.DropColumn(name: "expired_at", schema: "todo", table: "user_token");
        }
    }
}
