namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InsertGenreData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[Genres]([Id],[Name]) VALUES(1,'Action')");
            Sql("INSERT INTO [dbo].[Genres]([Id],[Name]) VALUES(2,'Adventure')");
            Sql("INSERT INTO [dbo].[Genres]([Id],[Name]) VALUES(3,'Animation')");
            Sql("INSERT INTO [dbo].[Genres]([Id],[Name]) VALUES(4,'Comedy')");
            Sql("INSERT INTO [dbo].[Genres]([Id],[Name]) VALUES(5,'Fantasy')");
            Sql("INSERT INTO [dbo].[Genres]([Id],[Name]) VALUES(6,'Family')");
            Sql("INSERT INTO [dbo].[Genres]([Id],[Name]) VALUES(7,'Romance')");
        }

        public override void Down()
        {
        }
    }
}
