using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaIdResponsavelNaArea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdResponsavel",
                table: "Areas",
                newName: "IdFuncionarioResponsavel");

            migrationBuilder.AddColumn<Guid>(
                name: "FuncionarioResponsavelId",
                table: "Areas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Areas_FuncionarioResponsavelId",
                table: "Areas",
                column: "FuncionarioResponsavelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Funcionarios_FuncionarioResponsavelId",
                table: "Areas",
                column: "FuncionarioResponsavelId",
                principalTable: "Funcionarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Funcionarios_FuncionarioResponsavelId",
                table: "Areas");

            migrationBuilder.DropIndex(
                name: "IX_Areas_FuncionarioResponsavelId",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "FuncionarioResponsavelId",
                table: "Areas");

            migrationBuilder.RenameColumn(
                name: "IdFuncionarioResponsavel",
                table: "Areas",
                newName: "IdResponsavel");
        }
    }
}
