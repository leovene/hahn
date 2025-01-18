using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hahn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.UniqueConstraint("AK_Departments_DepartmentId", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "ArtObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObjectId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ArtistDisplayName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Culture = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medium = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Dimensions = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ObjectDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObjectURL = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsPublicDomain = table.Column<bool>(type: "bit", nullable: false),
                    MetadataDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtObjects_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Constituents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConstituentId = table.Column<int>(type: "int", nullable: false),
                    ArtObjectId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WikidataURL = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ULANURL = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constituents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Constituents_ArtObjects_ArtObjectId",
                        column: x => x.ArtObjectId,
                        principalTable: "ArtObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtObjectId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_ArtObjects_ArtObjectId",
                        column: x => x.ArtObjectId,
                        principalTable: "ArtObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtObjectId = table.Column<int>(type: "int", nullable: false),
                    Term = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WikidataURL = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AATURL = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_ArtObjects_ArtObjectId",
                        column: x => x.ArtObjectId,
                        principalTable: "ArtObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtObjects_DepartmentId",
                table: "ArtObjects",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Constituents_ArtObjectId",
                table: "Constituents",
                column: "ArtObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ArtObjectId",
                table: "Images",
                column: "ArtObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ArtObjectId",
                table: "Tags",
                column: "ArtObjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Constituents");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "ArtObjects");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
