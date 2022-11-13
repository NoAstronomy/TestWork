using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWork.Db.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Группы",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Группы", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Сотрудники",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Сотрудники", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "СотрудникиГруппы",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_СотрудникиГруппы", x => new { x.EmployeeId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_СотрудникиГруппы_Группы_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Группы",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_СотрудникиГруппы_Сотрудники_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Сотрудники",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Группы",
                columns: new[] { "Id", "CreatedAtUtc", "Name" },
                values: new object[] { new Guid("b6072d11-1643-4003-87e9-d09547db8b2c"), new DateTime(2022, 11, 13, 14, 8, 12, 446, DateTimeKind.Utc).AddTicks(9667), "Администраторы" });

            migrationBuilder.InsertData(
                table: "Группы",
                columns: new[] { "Id", "CreatedAtUtc", "Name" },
                values: new object[] { new Guid("b2f1195a-00d3-4f68-a1a4-472ee50ebd72"), new DateTime(2022, 11, 13, 14, 8, 12, 447, DateTimeKind.Utc).AddTicks(212), "Руководство" });

            migrationBuilder.CreateIndex(
                name: "IX_Группы_Name",
                table: "Группы",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_СотрудникиГруппы_GroupId",
                table: "СотрудникиГруппы",
                column: "GroupId");

            migrationBuilder.Sql(@"
                DECLARE @administratorsGroupId uniqueidentifier = (SELECT [ID] FROM [Группы] WHERE [NAME] = 'Администраторы');
                DECLARE @leadershipGroupId uniqueidentifier = (SELECT [ID] FROM [Группы] WHERE [NAME] = 'Руководство');

                DECLARE @user1Id uniqueidentifier = NEWID();
                DECLARE @user2Id uniqueidentifier = NEWID();
                DECLARE @user3Id uniqueidentifier = NEWID();

                INSERT INTO [Сотрудники] 
                VALUES
                (@user1Id, 'Первый тестовый пользователь', 'rodya_1994@mail.ru', GETDATE()),
                (@user2Id, 'Второй тестовый пользователь', 'rodya1994@gmail.com', GETDATE()),
                (@user3Id, 'Третий тестовый пользователь', null, GETDATE());

                INSERT INTO [СотрудникиГруппы] 
                VALUES 
                (@user1Id, @administratorsGroupId),
                (@user2Id, @leadershipGroupId),
                (@user3Id, @administratorsGroupId),
                (@user3Id, @leadershipGroupId);
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "СотрудникиГруппы");

            migrationBuilder.DropTable(
                name: "Группы");

            migrationBuilder.DropTable(
                name: "Сотрудники");
        }
    }
}
