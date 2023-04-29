﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogSite_API.Migrations
{
    /// <inheritdoc />
    public partial class CommenterToCommetTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Commenter",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Commenter",
                table: "Comments");
        }
    }
}
