﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PollosHermano.CoreBancario.Infraestructure.Core.DbContexts;

namespace PollosHermano.CoreBancario.Infraestructure.Core.Migrations
{
    [DbContext(typeof(PollosHermanoCoreBancarioDBContext))]
    [Migration("20211201180159_Migration_20211201120102")]
    partial class Migration_20211201120102
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.CatTipoCuenta", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint")
                        .HasColumnName("Id");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("Descripcion");

                    b.HasKey("Id");

                    b.ToTable("CatTipoCuenta", "dbo");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApellidoMaterno")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("ApellidoMaterno");

                    b.Property<string>("ApellidoPaterno")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("ApellidoPaterno");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("Date")
                        .HasColumnName("FechaNacimiento");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Cliente", "dbo");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.Contrato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdPreContrato")
                        .HasColumnType("int")
                        .HasColumnName("IdPreContrato");

                    b.HasKey("Id");

                    b.HasIndex("IdPreContrato");

                    b.ToTable("Contrato", "dbo");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.Cuenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdCliente")
                        .HasColumnType("int")
                        .HasColumnName("IdCliente");

                    b.Property<int>("IdContrato")
                        .HasColumnType("int")
                        .HasColumnName("IdContrato");

                    b.Property<byte>("IdTipoCuenta")
                        .HasColumnType("tinyint")
                        .HasColumnName("IdTipoCuenta");

                    b.Property<string>("NumeroCuenta")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)")
                        .HasColumnName("NumeroCuenta");

                    b.HasKey("Id");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdContrato");

                    b.HasIndex("IdTipoCuenta");

                    b.ToTable("Cuenta", "dbo");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.PreContrato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApellidoMaternoProspecto")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("ApellidoMaternoProspecto");

                    b.Property<string>("ApellidoPaternoProspecto")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("ApellidoPaternoProspecto");

                    b.Property<DateTime>("FechaNacimientoProspecto")
                        .HasColumnType("Date")
                        .HasColumnName("FechaNacimientoProspecto");

                    b.Property<int>("IdVendedor")
                        .HasColumnType("int")
                        .HasColumnName("IdVendedor");

                    b.Property<string>("NombreProspecto")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("NombreProspecto");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Numero");

                    b.HasKey("Id");

                    b.HasIndex("IdVendedor");

                    b.ToTable("PreContrato", "dbo");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.Sucursal", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint")
                        .HasColumnName("Id");

                    b.Property<string>("Direccion")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("Direccion");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Sucursal", "dbo");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.Vendedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApellidoMaterno")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("ApellidoMaterno");

                    b.Property<string>("ApellidoPaterno")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("ApellidoPaterno");

                    b.Property<int>("IdZona")
                        .HasColumnType("int")
                        .HasColumnName("IdZona");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("Nombre");

                    b.HasKey("Id");

                    b.HasIndex("IdZona");

                    b.ToTable("Vendedor", "dbo");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.Zona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Estatus")
                        .HasColumnType("bit")
                        .HasColumnName("Estatus");

                    b.Property<byte>("IdSucursal")
                        .HasColumnType("tinyint")
                        .HasColumnName("IdSucursal");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("Nombre");

                    b.HasKey("Id");

                    b.HasIndex("IdSucursal");

                    b.ToTable("Zona", "dbo");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.Contrato", b =>
                {
                    b.HasOne("PollosHermano.CoreBancario.Entities.Core.PreContrato", "PreContrato")
                        .WithMany("Contratos")
                        .HasForeignKey("IdPreContrato")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PreContrato");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.Cuenta", b =>
                {
                    b.HasOne("PollosHermano.CoreBancario.Entities.Core.Cliente", "Cliente")
                        .WithMany("Cuentas")
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PollosHermano.CoreBancario.Entities.Core.Contrato", "Contrato")
                        .WithMany("Cuentas")
                        .HasForeignKey("IdContrato")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PollosHermano.CoreBancario.Entities.Core.CatTipoCuenta", "CatTipoCuenta")
                        .WithMany("Cuentas")
                        .HasForeignKey("IdTipoCuenta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CatTipoCuenta");

                    b.Navigation("Cliente");

                    b.Navigation("Contrato");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.PreContrato", b =>
                {
                    b.HasOne("PollosHermano.CoreBancario.Entities.Core.Vendedor", "Vendedor")
                        .WithMany("PreContratos")
                        .HasForeignKey("IdVendedor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vendedor");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.Vendedor", b =>
                {
                    b.HasOne("PollosHermano.CoreBancario.Entities.Core.Zona", "Zona")
                        .WithMany("Vendedors")
                        .HasForeignKey("IdZona")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Zona");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.Zona", b =>
                {
                    b.HasOne("PollosHermano.CoreBancario.Entities.Core.Sucursal", "Sucursal")
                        .WithMany("Zonas")
                        .HasForeignKey("IdSucursal")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sucursal");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.CatTipoCuenta", b =>
                {
                    b.Navigation("Cuentas");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.Cliente", b =>
                {
                    b.Navigation("Cuentas");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.Contrato", b =>
                {
                    b.Navigation("Cuentas");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.PreContrato", b =>
                {
                    b.Navigation("Contratos");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.Sucursal", b =>
                {
                    b.Navigation("Zonas");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.Vendedor", b =>
                {
                    b.Navigation("PreContratos");
                });

            modelBuilder.Entity("PollosHermano.CoreBancario.Entities.Core.Zona", b =>
                {
                    b.Navigation("Vendedors");
                });
#pragma warning restore 612, 618
        }
    }
}
