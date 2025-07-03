using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gfcf14_art_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialArtworkSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "artworks",
                columns: table => new
                {
                    date = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    image = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_artworks", x => x.date);
                });

            migrationBuilder.CreateTable(
                name: "artwork_links",
                columns: table => new
                {
                    artwork_date = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_artwork_links", x => new { x.artwork_date, x.type, x.url });
                    table.ForeignKey(
                        name: "FK_artwork_links_artworks_artwork_date",
                        column: x => x.artwork_date,
                        principalTable: "artworks",
                        principalColumn: "date",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "artwork_links");

            migrationBuilder.DropTable(
                name: "artworks");
        }
    }
}
