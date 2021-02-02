using Microsoft.EntityFrameworkCore.Migrations;

namespace ReadingRoomStore.Migrations
{
    public partial class _BooksTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BooksName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BooksAouthers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BooksPublishers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BooKsMaterial = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.BooksId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books");
        }
    }
}
