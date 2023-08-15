﻿// <auto-generated />
using System;
using Datos.ContextoEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Datos.Migrations
{
    [DbContext(typeof(LibreriaContexto))]
    [Migration("20230815000957_Modifiacion de usuario")]
    partial class Modifiaciondeusuario
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Dominio_Interfaces.EnitdadesNegocio.Cabaña", b =>
                {
                    b.Property<int>("NumeroHabitacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NumeroHabitacion"));

                    b.Property<int>("CantidadPersonasMax")
                        .HasColumnType("int");

                    b.Property<bool>("EstaHabilitada")
                        .HasColumnType("bit");

                    b.Property<string>("Foto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdTipoCabaña")
                        .HasColumnType("int");

                    b.Property<bool>("PoseeJacuzzi")
                        .HasColumnType("bit");

                    b.HasKey("NumeroHabitacion");

                    b.HasIndex("IdTipoCabaña");

                    b.ToTable("Cabañas");
                });

            modelBuilder.Entity("Dominio_Interfaces.EnitdadesNegocio.Mantenimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("CostoMantenimiento")
                        .HasColumnType("float");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("FechaMantenimiento")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCabaña")
                        .HasColumnType("int");

                    b.Property<string>("NombreEmpleado")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("IdCabaña");

                    b.ToTable("Mantenimientos");
                });

            modelBuilder.Entity("Dominio_Interfaces.EnitdadesNegocio.TipoCabaña", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("CostoPorHuesped")
                        .HasColumnType("float");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Nombre")
                        .IsUnique();

                    b.ToTable("TiposCabañas");
                });

            modelBuilder.Entity("Dominio_Interfaces.EnitdadesNegocio.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Dominio_Interfaces.EntidadesAuxiliares.Parametros", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NombreParametro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Valor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Parametros");
                });

            modelBuilder.Entity("Dominio_Interfaces.EnitdadesNegocio.Cabaña", b =>
                {
                    b.HasOne("Dominio_Interfaces.EnitdadesNegocio.TipoCabaña", "TipoCabaña")
                        .WithMany()
                        .HasForeignKey("IdTipoCabaña")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Dominio_Interfaces.ValueObjects.Cabaña.DescripcionCabaña", "Descripcion", b1 =>
                        {
                            b1.Property<int>("CabañaNumeroHabitacion")
                                .HasColumnType("int");

                            b1.Property<string>("valor")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("CabañaNumeroHabitacion");

                            b1.ToTable("Cabañas");

                            b1.WithOwner()
                                .HasForeignKey("CabañaNumeroHabitacion");
                        });

                    b.OwnsOne("Dominio_Interfaces.ValueObjects.Cabaña.NombreCabaña", "Nombre", b1 =>
                        {
                            b1.Property<int>("CabañaNumeroHabitacion")
                                .HasColumnType("int");

                            b1.Property<string>("valor")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)");

                            b1.HasKey("CabañaNumeroHabitacion");

                            b1.HasIndex("valor")
                                .IsUnique();

                            b1.ToTable("Cabañas");

                            b1.WithOwner()
                                .HasForeignKey("CabañaNumeroHabitacion");
                        });

                    b.Navigation("Descripcion")
                        .IsRequired();

                    b.Navigation("Nombre")
                        .IsRequired();

                    b.Navigation("TipoCabaña");
                });

            modelBuilder.Entity("Dominio_Interfaces.EnitdadesNegocio.Mantenimiento", b =>
                {
                    b.HasOne("Dominio_Interfaces.EnitdadesNegocio.Cabaña", "Cabaña")
                        .WithMany()
                        .HasForeignKey("IdCabaña")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cabaña");
                });

            modelBuilder.Entity("Dominio_Interfaces.EnitdadesNegocio.Usuario", b =>
                {
                    b.OwnsOne("Dominio_Interfaces.ValueObjects.Usuario.ApellidoUsuarioVO", "Apellido", b1 =>
                        {
                            b1.Property<int>("UsuarioId")
                                .HasColumnType("int");

                            b1.Property<string>("Valor")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UsuarioId");

                            b1.ToTable("Usuarios");

                            b1.WithOwner()
                                .HasForeignKey("UsuarioId");
                        });

                    b.OwnsOne("Dominio_Interfaces.ValueObjects.Usuario.ContraseñaUsuarioVO", "Contraseña", b1 =>
                        {
                            b1.Property<int>("UsuarioId")
                                .HasColumnType("int");

                            b1.Property<string>("Valor")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UsuarioId");

                            b1.ToTable("Usuarios");

                            b1.WithOwner()
                                .HasForeignKey("UsuarioId");
                        });

                    b.OwnsOne("Dominio_Interfaces.ValueObjects.Usuario.EmailUsuarioVO", "Email", b1 =>
                        {
                            b1.Property<int>("UsuarioId")
                                .HasColumnType("int");

                            b1.Property<string>("Valor")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)");

                            b1.HasKey("UsuarioId");

                            b1.HasIndex("Valor")
                                .IsUnique();

                            b1.ToTable("Usuarios");

                            b1.WithOwner()
                                .HasForeignKey("UsuarioId");
                        });

                    b.OwnsOne("Dominio_Interfaces.ValueObjects.Usuario.NombreUsuarioVO", "Nombre", b1 =>
                        {
                            b1.Property<int>("UsuarioId")
                                .HasColumnType("int");

                            b1.Property<string>("Valor")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UsuarioId");

                            b1.ToTable("Usuarios");

                            b1.WithOwner()
                                .HasForeignKey("UsuarioId");
                        });

                    b.Navigation("Apellido")
                        .IsRequired();

                    b.Navigation("Contraseña")
                        .IsRequired();

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Nombre")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
