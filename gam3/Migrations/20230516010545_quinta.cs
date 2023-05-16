using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gam3.Migrations
{
    /// <inheritdoc />
    public partial class quinta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Pedidos_PedidoId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_PedidoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "PrecoUnitario",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Pedidos");

            migrationBuilder.CreateTable(
                name: "ItensPedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensPedido_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_PedidoId",
                table: "ItensPedido",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensPedido");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Pedidos",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "Pedidos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoUnitario",
                table: "Pedidos",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "Pedidos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_PedidoId",
                table: "Pedidos",
                column: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Pedidos_PedidoId",
                table: "Pedidos",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id");
        }
    }
}
