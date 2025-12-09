using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeManager.Migrations
{
    /// <inheritdoc />
    public partial class NombreDeLaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Recipes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerUserId",
                table: "Recipes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "Ingredients",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_OwnerId",
                table: "Recipes",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_OwnerUserId",
                table: "Recipes",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Users_OwnerId",
                table: "Recipes",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Users_OwnerUserId",
                table: "Recipes",
                column: "OwnerUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Users_OwnerId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Users_OwnerUserId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_OwnerId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_OwnerUserId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                table: "Recipes");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "Ingredients",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);
        }
    }
}
