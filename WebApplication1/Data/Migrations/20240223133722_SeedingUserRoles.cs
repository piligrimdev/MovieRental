using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    public partial class SeedingUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash],
            [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'47732651-0440-409b-8d45-d181b007f360', N'admin@nigga.com', N'ADMIN@NIGGA.COM', N'admin@nigga.com', N'ADMIN@NIGGA.COM', 1, N'AQAAAAEAACcQAAAAEHUoHiDhu3Pk0/BuhPhGFsJQ2xlyTTfbWLXX5pkDJU3Hvcp2bbCuTFrQT9kHnDX20g==', N'VBD5TURW7WIIQSIV5DVL6XULE3VXURT6', N'2bc2b98e-9c48-461f-bd1e-e1c1a9ff313f', NULL, 0, 0, NULL, 1, 0)");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp],
            [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'85c64167-3b1f-4b14-ab83-166a0ed2c410', N'a@b.com', N'A@B.COM', N'a@b.com', N'A@B.COM', 1, N'AQAAAAEAACcQAAAAEO1sqxnqF2mJAedh4v3ZpI9cMqQUVxkRMpO/WXdGrp2bD2wiYSU7NIGeZNo0q/e7aw==', N'OZCIVMV3DNGTUPOLCLV4EI2QLKRGD526', N'ae58d757-845c-491e-a3bc-0372e54586c6', NULL, 0, 0, NULL, 1, 0)");

            migrationBuilder.Sql(@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
            VALUES (N'fa41dad4-929f-447e-bff1-02f6c53dead7', N'CanManageMovies', N'CANMANAGEMOVIES', N'8ed900f6-bb96-4589-814b-f4d3caad2d3c')"
            );

            migrationBuilder.Sql(@"INSERT INTO[dbo].[AspNetUserRoles]
            ([UserId], [RoleId]) VALUES(N'47732651-0440-409b-8d45-d181b007f360', N'fa41dad4-929f-447e-bff1-02f6c53dead7')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
