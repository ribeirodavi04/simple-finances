﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SimpleFinances.Infrastructure.Context;

#nullable disable

namespace SimpleFinances.Infrastructure.Migrations
{
    [DbContext(typeof(SimpleFinancesDbContext))]
    [Migration("20250318170429_TableCartao")]
    partial class TableCartao
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SimpleFinances.Domain.Entities.Cartao", b =>
                {
                    b.Property<int>("CartaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CartaoId"));

                    b.Property<string>("Banco")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DataFechamento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DataVencimento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Limite")
                        .HasColumnType("numeric");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CartaoId");

                    b.ToTable("Cartoes");
                });
#pragma warning restore 612, 618
        }
    }
}
