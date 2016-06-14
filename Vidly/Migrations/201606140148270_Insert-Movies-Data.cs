namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InsertMoviesData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[Movies]([Name],[Duration],[GenreId]) VALUES('Hangover','160','4')");
            Sql("INSERT INTO [dbo].[Movies]([Name],[Duration],[GenreId]) VALUES('Die Hard','180','1')");
            Sql("INSERT INTO [dbo].[Movies]([Name],[Duration],[GenreId]) VALUES('The Terminator','80','1')");
            Sql("INSERT INTO [dbo].[Movies]([Name],[Duration],[GenreId]) VALUES('Toy Story','120','3')");
            Sql("INSERT INTO [dbo].[Movies]([Name],[Duration],[GenreId]) VALUES('Titanic','120','7')");
            Sql("INSERT INTO [dbo].[Movies]([Name],[Duration],[GenreId]) VALUES('Shrek','120','5')");
            Sql("INSERT INTO [dbo].[Movies]([Name],[Duration],[GenreId]) VALUES('Harry Potter','120','2')");

        }

        public override void Down()
        {
        }
    }
}
