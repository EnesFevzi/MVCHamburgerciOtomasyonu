using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCHamburgerciOtomasyonu.DataAccess.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ImageID");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuID);
                    table.ForeignKey(
                        name: "FK_Menus_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Menus_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ImageID");
                });

            migrationBuilder.CreateTable(
                name: "MenuSizes",
                columns: table => new
                {
                    MenuSizeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceModifier = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuSizes", x => x.MenuSizeID);
                    table.ForeignKey(
                        name: "FK_MenuSizes_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MessageStatus = table.Column<bool>(type: "bit", nullable: false),
                    SenderUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReceiverUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_ReceiverUserId",
                        column: x => x.ReceiverUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SenderUserId",
                        column: x => x.SenderUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemID);
                    table.ForeignKey(
                        name: "FK_CartItems_Menus_MenuID",
                        column: x => x.MenuID,
                        principalTable: "Menus",
                        principalColumn: "MenuID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MenuSizeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MenuID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Menus_MenuID",
                        column: x => x.MenuID,
                        principalTable: "Menus",
                        principalColumn: "MenuID");
                    table.ForeignKey(
                        name: "FK_Orders_MenuSizes_MenuSizeID",
                        column: x => x.MenuSizeID,
                        principalTable: "MenuSizes",
                        principalColumn: "MenuSizeID");
                });

            migrationBuilder.CreateTable(
                name: "Extras",
                columns: table => new
                {
                    ExtraID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CartItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extras", x => x.ExtraID);
                    table.ForeignKey(
                        name: "FK_Extras_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Extras_CartItems_CartItemID",
                        column: x => x.CartItemID,
                        principalTable: "CartItems",
                        principalColumn: "CartItemID");
                    table.ForeignKey(
                        name: "FK_Extras_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ImageID");
                    table.ForeignKey(
                        name: "FK_Extras_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("343f8370-28d4-4ade-91df-7965041b98f1"), "95966d4b-ca4c-4711-80a5-f446fbca86c3", "Superadmin", "SUPERADMIN" },
                    { new Guid("f0a0b477-42aa-47fd-9e01-a81da466848d"), "ae37c50f-7c71-44d2-920d-1047302ac5e9", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageID", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "FileName", "FileType", "IsDeleted", "ModifiedBy", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("01673030-c382-45f8-84dc-a095bf6a7532"), "Admin Test", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2196), null, null, "user-images/user.png", "image/png", false, null, null },
                    { new Guid("09086832-f4c9-4b09-8df4-055014d961c5"), "Admin Test", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2132), null, null, "menu-images/double-whopper-jr.png", "image/png", false, null, null },
                    { new Guid("0f0e98fa-6b24-4c0e-9d2c-9afa9220a610"), "Admin Test", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2188), null, null, "extra-images/citir-peynir.png", "image/png", false, null, null },
                    { new Guid("0fc8f1ca-5366-4cb7-8492-5d17687cb648"), "Admin Test", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2127), null, null, "menu-images/chicken-royale-menu.png", "image/png", false, null, null },
                    { new Guid("109baf81-2778-42e5-84cd-9b95ea88f1a7"), "Admin Test", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2135), null, null, "menu-images/fish-royale-menu.png", "image/png", false, null, null },
                    { new Guid("585cecbc-cf45-4198-910f-4fc0b0d07c2d"), "Admin Test", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2124), null, null, "menu-images/cheeseburger-menu.png", "image/png", false, null, null },
                    { new Guid("81fcbc63-54c7-4ddb-92aa-f677c1a144bb"), "Admin Test", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2186), null, null, "extra-images/chicken-tenders.png", "image/png", false, null, null },
                    { new Guid("88057a22-8de7-4c7a-b712-c4053403ae60"), "Admin Test", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2183), null, null, "extra-images/bk-king-nuggets-1.png", "image/png", false, null, null },
                    { new Guid("930abce6-6f8c-4144-9332-ed04a7f0c40a"), "Admin Test", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2198), null, null, "user-images/superadmin.png", "image/png", false, null, null },
                    { new Guid("9cbc9994-f594-4ffb-97b2-45d09f9f10f4"), "Admin Test", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2140), null, null, "menu-images/tavukburger-menu.png", "image/png", false, null, null },
                    { new Guid("b5c2acf1-6b30-4e5d-8af4-5b195eeec91a"), "Admin Test", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2193), null, null, "extra-images/tirtikli-patates.png", "image/png", false, null, null },
                    { new Guid("ba68b7c8-504c-4abd-bb46-5add111b48bc"), "Admin Test", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2137), null, null, "menu-images/kofteburger-menu.png", "image/png", false, null, null },
                    { new Guid("c213dfec-2010-494c-a132-91fb6d8334cc"), "Admin Test", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2129), null, null, "menu-images/king-chicken-1.png", "image/png", false, null, null },
                    { new Guid("f71f4b9a-aa60-461d-b398-de31001bf214"), "Admin Test", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2119), null, null, "menu-images/big-king-menu.png", "image/png", false, null, null },
                    { new Guid("f8158844-15d4-4014-be20-745a147449b5"), "Admin Test", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2191), null, null, "extra-images/sogan-halkasi.png", "image/png", false, null, null }
                });

            migrationBuilder.InsertData(
                table: "MenuSizes",
                columns: new[] { "MenuSizeID", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "PriceModifier", "SizeName", "UserID" },
                values: new object[,]
                {
                    { new Guid("14b9c7df-6cf5-4a99-ae1c-c0a59ee4102c"), null, new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2699), null, null, false, null, null, 15.00m, "Büyük Boy", null },
                    { new Guid("9a9cdfac-f735-47cf-a24d-f93ec613e09b"), null, new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2695), null, null, false, null, null, 0m, "Normal Boy", null },
                    { new Guid("de6c42d5-1cb2-430d-8e48-d4fe931d4843"), null, new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2701), null, null, false, null, null, 20.00m, "King Boy", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ImageID", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("b207b056-26ac-4be9-b6a5-07eb8c9e8d76"), 0, "d1a8cf46-3696-40ab-b34a-dcf422e940cf", "user@gmail.com", true, "User", new Guid("01673030-c382-45f8-84dc-a095bf6a7532"), "User", false, null, "USER@GMAIL.COM", "USER@GMAIL.COM", "AQAAAAEAACcQAAAAEMLtSOP4wySOea7I85ESKSnSe+Jpk6cTwFETa5NznbkDPzGSkdQD341VTnxZCYdr1g==", "+905439999988", true, "38a1a4f6-3ac7-40ef-bba7-76e6599bfa0a", false, "user@gmail.com" },
                    { new Guid("cb94223b-ccb8-4f2f-93d7-0df96a7f065c"), 0, "d41f1d5d-8b02-4866-a09f-a494447762cf", "superadmin@gmail.com", true, "Super", new Guid("930abce6-6f8c-4144-9332-ed04a7f0c40a"), "Admin", false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEMyo+4RD2Qm6q2AAfDC8iOnq4smX1K1kkhcIHJUmf6t4ebn59mr+aXuBHHgOWdZtEg==", "+905439999999", true, "6f209e06-0ba8-448a-bbcc-85b6f2b8e01c", false, "superadmin@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Extras",
                columns: new[] { "ExtraID", "CartItemID", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageID", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name", "OrderID", "Price", "UserID" },
                values: new object[,]
                {
                    { new Guid("20f69585-ac98-4213-8cfb-07d3b37927aa"), null, null, new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(1817), null, null, new Guid("0f0e98fa-6b24-4c0e-9d2c-9afa9220a610"), false, null, null, "Çıtır Peynir", null, 25.00m, null },
                    { new Guid("5fbdf069-889e-4f44-9984-55719389ee3c"), null, null, new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(1785), null, null, new Guid("b5c2acf1-6b30-4e5d-8af4-5b195eeec91a"), false, null, null, "Tırtıklı Patates", null, 10.00m, null },
                    { new Guid("866d8813-a009-451a-a4a2-283509b60ae6"), null, null, new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(1821), null, null, new Guid("81fcbc63-54c7-4ddb-92aa-f677c1a144bb"), false, null, null, "Chicken Tenders", null, 45.00m, null },
                    { new Guid("941ea186-ad0e-407a-9694-99f5160b680f"), null, null, new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(1814), null, null, new Guid("f8158844-15d4-4014-be20-745a147449b5"), false, null, null, "Soğan Halkası", null, 20.00m, null },
                    { new Guid("ea151fc1-5451-49ae-84ee-dd4315d021ca"), null, null, new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(1819), null, null, new Guid("88057a22-8de7-4c7a-b712-c4053403ae60"), false, null, null, "I-FEAT King Nuggets", null, 35.00m, null }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuID", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageID", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name", "Price", "UserID" },
                values: new object[,]
                {
                    { new Guid("31df718a-1182-48ef-a5ed-419db6a217fa"), "Super Admın", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2509), null, null, new Guid("c213dfec-2010-494c-a132-91fb6d8334cc"), false, null, null, "King Chicken", 150.00m, null },
                    { new Guid("4454da72-6c4c-4f57-b51a-76c50c8c8c05"), "Super Admın", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2514), null, null, new Guid("09086832-f4c9-4b09-8df4-055014d961c5"), false, null, null, "Double Whopper", 160.00m, null },
                    { new Guid("5be15229-6970-4e16-84e3-afa6a0124977"), "Super Admın", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2521), null, null, new Guid("109baf81-2778-42e5-84cd-9b95ea88f1a7"), false, null, null, "Fish Royale", 180.00m, null },
                    { new Guid("66c8e428-7a09-4449-a605-64f9497ae2ce"), "Super Admın", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2530), null, null, new Guid("ba68b7c8-504c-4abd-bb46-5add111b48bc"), false, null, null, "Köfte Burger", 220.00m, null },
                    { new Guid("6c885842-c524-42ef-af4d-da3e70952e18"), "Super Admın", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2533), null, null, new Guid("585cecbc-cf45-4198-910f-4fc0b0d07c2d"), false, null, null, "CheeseBurger Menü", 250.00m, null },
                    { new Guid("b1ef787b-b80b-46e9-b827-51c9c05770a6"), "Super Admın", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2518), null, null, new Guid("f71f4b9a-aa60-461d-b398-de31001bf214"), false, null, null, "Big King", 170.00m, null },
                    { new Guid("b68520c4-3c03-4dd1-b3e9-3024d9c53d55"), "Super Admın", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2527), null, null, new Guid("0fc8f1ca-5366-4cb7-8492-5d17687cb648"), false, null, null, "Chicken Royale", 200.00m, null },
                    { new Guid("fd87ed69-9f92-4ff1-8d13-8435117a63e0"), "Super Admın", new DateTime(2023, 11, 12, 19, 10, 44, 658, DateTimeKind.Local).AddTicks(2524), null, null, new Guid("9cbc9994-f594-4ffb-97b2-45d09f9f10f4"), false, null, null, "Tavuk Burger Menü", 190.00m, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[] { new Guid("f0a0b477-42aa-47fd-9e01-a81da466848d"), new Guid("b207b056-26ac-4be9-b6a5-07eb8c9e8d76"), "AppUserRole" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[] { new Guid("343f8370-28d4-4ade-91df-7965041b98f1"), new Guid("cb94223b-ccb8-4f2f-93d7-0df96a7f065c"), "AppUserRole" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                name: "IX_AspNetUsers_ImageID",
                table: "AspNetUsers",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_MenuID",
                table: "CartItems",
                column: "MenuID");

            migrationBuilder.CreateIndex(
                name: "IX_Extras_CartItemID",
                table: "Extras",
                column: "CartItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Extras_ImageID",
                table: "Extras",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Extras_OrderID",
                table: "Extras",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Extras_UserID",
                table: "Extras",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ImageID",
                table: "Menus",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_UserID",
                table: "Menus",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MenuSizes_UserID",
                table: "MenuSizes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverUserId",
                table: "Messages",
                column: "ReceiverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderUserId",
                table: "Messages",
                column: "SenderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MenuID",
                table: "Orders",
                column: "MenuID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MenuSizeID",
                table: "Orders",
                column: "MenuSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserID",
                table: "Orders",
                column: "UserID");
        }

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
                name: "Extras");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "MenuSizes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
