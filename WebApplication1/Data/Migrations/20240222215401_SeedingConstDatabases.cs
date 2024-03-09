using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    public partial class SeedingConstDatabases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO MembershipTypes (Name, MonthDuration, DiscountInPercents, Price)" +
                " VALUES ('Pay as you go', 0, 0, 0)");
            migrationBuilder.Sql("INSERT INTO MembershipTypes (Name, MonthDuration, DiscountInPercents, Price)" +
                " VALUES ('Monthly', 1, 10, 30)");
            migrationBuilder.Sql("INSERT INTO MembershipTypes (Name, MonthDuration, DiscountInPercents, Price)" +
                " VALUES ('Once in 3 months', 3, 20, 40)");
            migrationBuilder.Sql("INSERT INTO MembershipTypes (Name, MonthDuration, DiscountInPercents, Price)" +
                " VALUES ('Annualy', 12, 30, 300)");

            migrationBuilder.Sql("INSERT INTO Genres (GenreName) VALUES ('Action')");
            migrationBuilder.Sql("INSERT INTO Genres (GenreName) VALUES ('Horror/Triller')");
            migrationBuilder.Sql("INSERT INTO Genres (GenreName) VALUES ('Comedy')");
            migrationBuilder.Sql("INSERT INTO Genres (GenreName) VALUES ('Fucking PORN???')");
            migrationBuilder.Sql("INSERT INTO Genres (GenreName) VALUES ('Philosofical')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
