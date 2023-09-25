using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BLG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class thisisthierdmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Postblogcategory",
                columns: table => new
                {
                    Categoriesid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    postsid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postblogcategory", x => new { x.Categoriesid, x.postsid });
                    table.ForeignKey(
                        name: "FK_Postblogcategory_category_Categoriesid",
                        column: x => x.Categoriesid,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Postblogcategory_postblog_postsid",
                        column: x => x.postsid,
                        principalTable: "postblog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Postblogcategory_postsid",
                table: "Postblogcategory",
                column: "postsid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Postblogcategory");
        }
    }
}
