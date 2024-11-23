using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedSpecialties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "http://res.cloudinary.com/dx3xe3xxp/image/upload/v1680960692/da-net7/goeulqkn8z9bdisnryiy.png", "أمراض القلب" },
                    { 2, "http://res.cloudinary.com/dx3xe3xxp/image/upload/v1680960705/da-net7/vgyf9fpn4yznhtihnhds.png", "الأمراض النفسية" },
                    { 3, "http://res.cloudinary.com/dx3xe3xxp/image/upload/v1680960810/da-net7/k7l6rs6oaliku1zgvtef.png", "الأمراض الجلدية" },
                    { 4, "https://res.cloudinary.com/dx3xe3xxp/image/upload/v1680960909/da-net7/eqkehqhlgqjro8yvzqhh.png", "طب النساء والتوليد" },
                    { 5, "http://res.cloudinary.com/dx3xe3xxp/image/upload/v1680960853/da-net7/sz1zgbptcmdt71coxmcf.png", "طب الأطفال" },
                    { 6, "http://res.cloudinary.com/dx3xe3xxp/image/upload/v1680960844/da-net7/jfvznth944kstrmwv3yt.png", "علم الأورام" },
                    { 7, "http://res.cloudinary.com/dx3xe3xxp/image/upload/v1681515080/da-net7/njgxuneb3oppmbxvycfq.png", "الأمراض العصبية" },
                    { 8, "http://res.cloudinary.com/dx3xe3xxp/image/upload/v1680960887/da-net7/illqiu1xxq1pchw1mwqj.png", "طب العيون" },
                    { 9, "http://res.cloudinary.com/dx3xe3xxp/image/upload/v1680960896/da-net7/aeqgqwcdfl5hdreupoca.png", "طب الجهاز الهضمي" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
