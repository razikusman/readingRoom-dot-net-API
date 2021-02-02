using Microsoft.EntityFrameworkCore.Migrations;

namespace ReadingRoomStore.Migrations
{
    public partial class _DonatorsTable_created : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase(
                collation: "SQL_Latin1_General_CP1_CI_AS");

            migrationBuilder.CreateTable(
                name: "Donators",
                columns: table => new
                {
                    DonatorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                        //.Annotation("SqlServer:Identity", "1, 1"),
                    DonatorPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonatorUserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donators", x => x.DonatorId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donators");

            //migrationBuilder.AlterDatabase(,
            //    oldCollation: "SQL_Latin1_General_CP1_CI_AS");
        }
    }
}
