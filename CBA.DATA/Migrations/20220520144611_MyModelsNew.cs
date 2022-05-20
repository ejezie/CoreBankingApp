using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CBA.DATA.Migrations
{
    public partial class MyModelsNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customers",
                newName: "ID");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Customers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerInfo",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerLongID",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Customers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Customers",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortCode = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseIncomeEntries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntryType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseIncomeEntries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FineNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FineNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GLCategories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    MainGLCategory = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GLCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MembershipTypes",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SignUpFee = table.Column<short>(type: "smallint", nullable: false),
                    DurationInMonths = table.Column<byte>(type: "tinyint", nullable: false),
                    DiscountRate = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainCategory = table.Column<int>(type: "int", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ClientAccounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BranchID = table.Column<int>(type: "int", nullable: false),
                    AccountStatus = table.Column<int>(type: "int", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    ConsumerID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    DaysCount = table.Column<int>(type: "int", nullable: true),
                    dailyInterestAccrued = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoanInterestRatePerMonth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SavingsWithdrawalCount = table.Column<int>(type: "int", nullable: true),
                    CurrentLien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoanMonthlyInterestRepay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoanMonthlyRepay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoanMonthlyPrincipalRepay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoanPrincipalRemaining = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TermsOfLoan = table.Column<int>(type: "int", nullable: true),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LinkedAccountID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAccounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ClientAccounts_Branches_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientAccounts_ClientAccounts_LinkedAccountID",
                        column: x => x.LinkedAccountID,
                        principalTable: "ClientAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientAccounts_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAccounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BranchID = table.Column<int>(type: "int", nullable: false),
                    AccountStatus = table.Column<int>(type: "int", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    DaysCount = table.Column<int>(type: "int", nullable: true),
                    dailyInterestAccrued = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoanInterestRatePerMonth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SavingsWithdrawalCount = table.Column<int>(type: "int", nullable: true),
                    CurrentLien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoanMonthlyInterestRepay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoanMonthlyRepay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoanMonthlyPrincipalRepay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoanPrincipalRemaining = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TermsOfLoan = table.Column<int>(type: "int", nullable: true),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LinkedAccountID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAccounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerAccounts_Branches_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerAccounts_CustomerAccounts_LinkedAccountID",
                        column: x => x.LinkedAccountID,
                        principalTable: "CustomerAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerAccounts_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GLAccounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    AccountBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BranchID = table.Column<int>(type: "int", nullable: false),
                    GLCategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GLAccounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GLAccounts_Branches_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GLAccounts_GLCategories_GLCategoryID",
                        column: x => x.GLCategoryID,
                        principalTable: "GLCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountTypeManagements",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentCreditInterestRate = table.Column<double>(type: "float", nullable: false),
                    CurrentMinimumBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    COT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentInterestExpenseGlID = table.Column<int>(type: "int", nullable: true),
                    COTIncomeGlID = table.Column<int>(type: "int", nullable: true),
                    SavingsCreditInterestRate = table.Column<double>(type: "float", nullable: false),
                    SavingsMinimumBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SavingsInterestExpenseGlID = table.Column<int>(type: "int", nullable: true),
                    SavingsInterestPayableGlID = table.Column<int>(type: "int", nullable: true),
                    LoanDebitInterestRate = table.Column<double>(type: "float", nullable: false),
                    LoanInterestIncomeGlID = table.Column<int>(type: "int", nullable: true),
                    LoanInterestReceivableGlID = table.Column<int>(type: "int", nullable: true),
                    IsOpened = table.Column<bool>(type: "bit", nullable: false),
                    FinancialDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypeManagements", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccountTypeManagements_GLAccounts_COTIncomeGlID",
                        column: x => x.COTIncomeGlID,
                        principalTable: "GLAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountTypeManagements_GLAccounts_CurrentInterestExpenseGlID",
                        column: x => x.CurrentInterestExpenseGlID,
                        principalTable: "GLAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountTypeManagements_GLAccounts_LoanInterestIncomeGlID",
                        column: x => x.LoanInterestIncomeGlID,
                        principalTable: "GLAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountTypeManagements_GLAccounts_LoanInterestReceivableGlID",
                        column: x => x.LoanInterestReceivableGlID,
                        principalTable: "GLAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountTypeManagements_GLAccounts_SavingsInterestExpenseGlID",
                        column: x => x.SavingsInterestExpenseGlID,
                        principalTable: "GLAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountTypeManagements_GLAccounts_SavingsInterestPayableGlID",
                        column: x => x.SavingsInterestPayableGlID,
                        principalTable: "GLAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GlPostings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DebitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DrGlAccountID = table.Column<int>(type: "int", nullable: true),
                    CrGlAccountID = table.Column<int>(type: "int", nullable: true),
                    PostInitiatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlPostings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GlPostings_GLAccounts_CrGlAccountID",
                        column: x => x.CrGlAccountID,
                        principalTable: "GLAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GlPostings_GLAccounts_DrGlAccountID",
                        column: x => x.DrGlAccountID,
                        principalTable: "GLAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TellerPostings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostingType = table.Column<int>(type: "int", nullable: false),
                    ConsumerAccountID = table.Column<int>(type: "int", nullable: false),
                    CustomerAccountID = table.Column<int>(type: "int", nullable: true),
                    PostInitiatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TillAccountID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TellerPostings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TellerPostings_CustomerAccounts_CustomerAccountID",
                        column: x => x.CustomerAccountID,
                        principalTable: "CustomerAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TellerPostings_GLAccounts_TillAccountID",
                        column: x => x.TillAccountID,
                        principalTable: "GLAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TellerTills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GlAccounID = table.Column<int>(type: "int", nullable: false),
                    GlAccountID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TellerTills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TellerTills_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TellerTills_GLAccounts_GlAccountID",
                        column: x => x.GlAccountID,
                        principalTable: "GLAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TillAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GlAccountID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TillAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TillAccounts_GLAccounts_GlAccountID",
                        column: x => x.GlAccountID,
                        principalTable: "GLAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Address", "CustomerInfo", "CustomerLongID", "Email", "FullName", "Gender", "PhoneNumber" },
                values: new object[] { "Bermuda Triangle", "smart", "64545566", "Jamesbond007@gmail.com", "James Bond", 0, "007007007007" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypeManagements_COTIncomeGlID",
                table: "AccountTypeManagements",
                column: "COTIncomeGlID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypeManagements_CurrentInterestExpenseGlID",
                table: "AccountTypeManagements",
                column: "CurrentInterestExpenseGlID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypeManagements_LoanInterestIncomeGlID",
                table: "AccountTypeManagements",
                column: "LoanInterestIncomeGlID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypeManagements_LoanInterestReceivableGlID",
                table: "AccountTypeManagements",
                column: "LoanInterestReceivableGlID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypeManagements_SavingsInterestExpenseGlID",
                table: "AccountTypeManagements",
                column: "SavingsInterestExpenseGlID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypeManagements_SavingsInterestPayableGlID",
                table: "AccountTypeManagements",
                column: "SavingsInterestPayableGlID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAccounts_BranchID",
                table: "ClientAccounts",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAccounts_CustomerID",
                table: "ClientAccounts",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAccounts_LinkedAccountID",
                table: "ClientAccounts",
                column: "LinkedAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccounts_BranchID",
                table: "CustomerAccounts",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccounts_CustomerID",
                table: "CustomerAccounts",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccounts_LinkedAccountID",
                table: "CustomerAccounts",
                column: "LinkedAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_GLAccounts_BranchID",
                table: "GLAccounts",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_GLAccounts_GLCategoryID",
                table: "GLAccounts",
                column: "GLCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_GlPostings_CrGlAccountID",
                table: "GlPostings",
                column: "CrGlAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_GlPostings_DrGlAccountID",
                table: "GlPostings",
                column: "DrGlAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_TellerPostings_CustomerAccountID",
                table: "TellerPostings",
                column: "CustomerAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_TellerPostings_TillAccountID",
                table: "TellerPostings",
                column: "TillAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_TellerTills_GlAccountID",
                table: "TellerTills",
                column: "GlAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_TellerTills_UserId1",
                table: "TellerTills",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_TillAccounts_GlAccountID",
                table: "TillAccounts",
                column: "GlAccountID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTypeManagements");

            migrationBuilder.DropTable(
                name: "ClientAccounts");

            migrationBuilder.DropTable(
                name: "ExpenseIncomeEntries");

            migrationBuilder.DropTable(
                name: "FineNames");

            migrationBuilder.DropTable(
                name: "GlPostings");

            migrationBuilder.DropTable(
                name: "MembershipTypes");

            migrationBuilder.DropTable(
                name: "TellerPostings");

            migrationBuilder.DropTable(
                name: "TellerTills");

            migrationBuilder.DropTable(
                name: "TillAccounts");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "CustomerAccounts");

            migrationBuilder.DropTable(
                name: "GLAccounts");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "GLCategories");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerInfo",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerLongID",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Customers",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Customers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Customers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FirstName", "Gender", "LastName" },
                values: new object[] { "James", 2, "Bond" });
        }
    }
}
