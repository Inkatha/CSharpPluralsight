namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, Name) VALUES (1, 'Jazz')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2, 'Hip-Hop')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3, 'R&B')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4, 'Blues')");
        }

        public override void Down()
        {
            Sql("DELETE FROM Genres Where Id IN (1, 2, 3, 4)");
        }
    }
}
