using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PhoneNumber_RegionNumber = table.Column<int>(type: "integer", unicode: false, maxLength: 5, nullable: false),
                    PhoneNumber_PhoneNumberValue = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", unicode: false, maxLength: 255, nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", unicode: false, maxLength: 255, nullable: true),
                    JwtVersion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_UserLoginHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    PhoneNumber_RegionNumber = table.Column<int>(type: "integer", nullable: false),
                    PhoneNumber_PhoneNumberValue = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_UserLoginHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_UserAccessFail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LockEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AccessFailCount = table.Column<int>(type: "integer", nullable: false),
                    isLockOut = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_UserAccessFail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_UserAccessFail_T_User_UserId",
                        column: x => x.UserId,
                        principalTable: "T_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_UserAccessFail_UserId",
                table: "T_UserAccessFail",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_UserAccessFail");

            migrationBuilder.DropTable(
                name: "T_UserLoginHistory");

            migrationBuilder.DropTable(
                name: "T_User");
        }
    }
}
