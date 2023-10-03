using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BLG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class thisisthierddmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "uplaodimages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tiltle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    urlhandle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_uplaodimages", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "uplaodimages");
        }
    }
}
