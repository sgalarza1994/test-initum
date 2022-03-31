using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaTurnoApi.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Queues",
                columns: table => new
                {
                    QueueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QueueName = table.Column<string>(type: "varchar(300)", nullable: true),
                    TimeAttention = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queues", x => x.QueueId);
                });

            migrationBuilder.CreateTable(
                name: "Turns",
                columns: table => new
                {
                    TurnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QueueId = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HourAttention = table.Column<string>(type: "varchar(20)", nullable: true),
                    Status = table.Column<string>(type: "varchar(10)", nullable: true),
                    PersonId = table.Column<string>(type: "varchar(20)", nullable: true),
                    PersonName = table.Column<string>(type: "varchar(300)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turns", x => x.TurnId);
                    table.ForeignKey(
                        name: "FK_Turns_Queues_QueueId",
                        column: x => x.QueueId,
                        principalTable: "Queues",
                        principalColumn: "QueueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Queues",
                columns: new[] { "QueueId", "QueueName", "TimeAttention" },
                values: new object[] { 1, "Cola 1", 2 });

            migrationBuilder.InsertData(
                table: "Queues",
                columns: new[] { "QueueId", "QueueName", "TimeAttention" },
                values: new object[] { 2, "Cola 2", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Turns_QueueId",
                table: "Turns",
                column: "QueueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Turns");

            migrationBuilder.DropTable(
                name: "Queues");
        }
    }
}
