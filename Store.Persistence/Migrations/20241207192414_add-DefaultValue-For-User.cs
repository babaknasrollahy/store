using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Store.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addDefaultValueForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Wallet",
                columns: new[] { "Id", "Discriminator", "InsertTime", "IsRemoved", "RemoveTime", "UpdateTime", "UserId" },
                values: new object[,]
                {
                    { new Guid("47004665-ed8b-483e-88a6-004e0545cba2"), "PersonalWallet", new DateTime(2024, 12, 7, 22, 54, 13, 875, DateTimeKind.Local).AddTicks(7957), false, null, null, 1 },
                    { new Guid("95196937-23b5-41e7-83b3-79bc53aaa52c"), "CompanyWallet", new DateTime(2024, 12, 7, 22, 54, 13, 875, DateTimeKind.Local).AddTicks(7925), false, null, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyWalletId", "Email", "InsertTime", "IsActive", "IsRemoved", "Name", "Password", "PersonalWalletId", "RemoveTime", "UpdateTime" },
                values: new object[] { 1, new Guid("95196937-23b5-41e7-83b3-79bc53aaa52c"), "FarzadNasrolahy@gmail.com", new DateTime(2024, 12, 7, 22, 54, 13, 875, DateTimeKind.Local).AddTicks(7971), true, false, "Admin", "AQAAAAEAACcQAAAAEHnFZoWnWUarSFruZpp5Kis9Rm4Jw7saVY9v9o6SuQpfE9yM9+3SggktjucAf5JnoA==", new Guid("47004665-ed8b-483e-88a6-004e0545cba2"), null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: new Guid("47004665-ed8b-483e-88a6-004e0545cba2"));

            migrationBuilder.DeleteData(
                table: "Wallet",
                keyColumn: "Id",
                keyValue: new Guid("95196937-23b5-41e7-83b3-79bc53aaa52c"));
        }
    }
}
