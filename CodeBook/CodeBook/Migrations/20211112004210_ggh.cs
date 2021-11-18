using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeBook.Migrations
{
    public partial class ggh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    SettingId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    key = table.Column<string>(nullable: true),
                    value = table.Column<string>(nullable: true),
                    desc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.SettingId);
                });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "desc", "key", "value" },
                values: new object[] { 1, "", "sLang", "" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "desc", "key", "value" },
                values: new object[] { 2, "", "sChapter", "" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "desc", "key", "value" },
                values: new object[] { 3, "", "sLesson", "" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "desc", "key", "value" },
                values: new object[] { 4, "", "sView", "" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "desc", "key", "value" },
                values: new object[] { 5, "", "materialurl", "" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "desc", "key", "value" },
                values: new object[] { 6, "", "addLangShortCut", "" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "desc", "key", "value" },
                values: new object[] { 7, "", "addChapterShortCut", "" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "desc", "key", "value" },
                values: new object[] { 8, "", "addLessonShortCut", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Setting");
        }
    }
}
