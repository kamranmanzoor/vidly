namespace Vidly.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'acc512df-4461-4220-be38-0315f83956b4', N'guest@vidly.com', 0, N'AHce/bJRP26irvHmpQTwCwKuqPNjUtKRSohh0yTQL99RBHWlIT3ravOORJevfTGZUA==', N'b758597f-a38f-495a-96c3-ec07e99adf47', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd1799145-dadf-49a0-9236-753938714de9', N'admin@vidly.com', 0, N'AP0gwkLyfh4aGKWMZmEa2f+B7OTEEgC91BXDs5/JDxLa10s4/dS/c5B7l21VAsuM0Q==', N'ab01d540-7d13-46f9-9c22-d2173f1d0d0e', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'6cba4b6f-6d8b-44f0-8a00-749d83b1ac72', N'CanManageMovies')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd1799145-dadf-49a0-9236-753938714de9', N'6cba4b6f-6d8b-44f0-8a00-749d83b1ac72')
");

        }

        public override void Down()
        {
        }
    }
}
