using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Data.Migrations
{
    /// <inheritdoc />
    public partial class generalmigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsSite",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NewSiteName = table.Column<string>(type: "TEXT", nullable: false),
                    Siteurl = table.Column<string>(type: "TEXT", nullable: false),
                    SiteUrlQueryString = table.Column<string>(type: "TEXT", nullable: false),
                    IsSearchHttpGetBased = table.Column<bool>(type: "INTEGER", nullable: false),
                    PoliticalLeaning = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsSite", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsSiteSearchInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NewsSiteName = table.Column<string>(type: "TEXT", nullable: false),
                    isMainSiteNews = table.Column<bool>(type: "INTEGER", nullable: false),
                    NewsContainerClassName = table.Column<string>(type: "TEXT", nullable: false),
                    TitleClassName = table.Column<string>(type: "TEXT", nullable: false),
                    DescriptionClassName = table.Column<string>(type: "TEXT", nullable: true),
                    NewsImageClassName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsSiteSearchInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0c8e65b1-e7eb-4a9b-a958-5078f2810109", null, "Admin", "ADMIN" },
                    { "54ea22d5-ee3e-4ced-9a23-e93281293eef", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "NewsSite",
                columns: new[] { "Id", "IsSearchHttpGetBased", "NewSiteName", "PoliticalLeaning", "SiteUrlQueryString", "Siteurl" },
                values: new object[,]
                {
                    { new Guid("82e2c7dd-2a02-48a9-b2b7-2843a97bd698"), true, "ElDestape", "Left", "https://www.eldestapeweb.com/buscar/", "https://www.eldestapeweb.com" },
                    { new Guid("82ee607b-cee3-4b13-a23a-55b8411b6666"), true, "A24", "Center", "https://www.a24.com/contenidos/resultado.html?search=", "https://www.a24.com" },
                    { new Guid("94f4bc7f-43c1-4c16-9d47-aaae4c5d09ff"), true, "LaNacion", "Center-Right", "https://www.lanacion.com.ar/buscador/?query=", "https://www.lanacion.com.ar" },
                    { new Guid("cb51a47d-1b77-4440-a2c4-9f93f5275c06"), true, "TodoNoticias", "Center-Right", "https://tn.com.ar/buscar/", "https://tn.com.ar" },
                    { new Guid("f4392654-788d-45fc-9057-2cce2930c2f6"), true, "Pagina12", "Left", "https://www.pagina12.com.ar/buscar?q=", "https://www.pagina12.com.ar" }
                });

            migrationBuilder.InsertData(
                table: "NewsSiteSearchInfo",
                columns: new[] { "Id", "DescriptionClassName", "NewsContainerClassName", "NewsImageClassName", "NewsSiteName", "TitleClassName", "isMainSiteNews" },
                values: new object[,]
                {
                    { new Guid("007749e6-42ab-4a02-8947-d1544eb1700d"), "gs-bidi-start-align", "gsc-webResult", "gs-image", "A24", "gs-title", false },
                    { new Guid("13e4de78-0115-442f-b7c2-4eaeb3fe8949"), "queryly_item_description", "queryly_item_row", null, "LaNacion", "queryly_item_title", false },
                    { new Guid("647b6141-f11c-41fb-b0ec-3e207915db4c"), null, "item-3", "lazyautosizes", "ElDestape", "titulo", false },
                    { new Guid("7980bb4c-3894-4d80-9994-773734af11db"), "subhead", "grid-item", "image", "LaNacion", "title", true },
                    { new Guid("79e2a8d5-10c8-4d42-81be-650eeb4e7753"), "title-suffix", "headline-card", "image", "Pagina12", "title", true },
                    { new Guid("82ca2217-e582-4740-9129-94cb4ad5a3c1"), "ignore-parser", "news-item", null, "A24", "news-title", true },
                    { new Guid("9fc0bf27-4606-43b0-882c-37394522eb93"), "card__subheadline", "brick_one", "image", "TodoNoticias", "card__headline", true },
                    { new Guid("b9124587-2def-4b02-b501-34cb4602a794"), null, "item-6", "lazyautosizes", "ElDestape", "titulo", true },
                    { new Guid("ed0bae73-9e76-4fb6-88c8-9dcec943b7f4"), "p12-separator--left--primary", "article-item", "image", "Pagina12", "title", false },
                    { new Guid("f5f023ef-d4c5-4f7d-bd49-e4bb918d3a77"), "card__subheadline", "card__container", "image", "TodoNoticias", "card__headline", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "NewsSite");

            migrationBuilder.DropTable(
                name: "NewsSiteSearchInfo");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
