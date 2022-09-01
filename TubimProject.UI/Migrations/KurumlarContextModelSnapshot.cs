﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TubimProject.Infrastructure.Repositories;

#nullable disable

namespace TubimProject.UI.Migrations
{
    [DbContext(typeof(KurumlarContext))]
    partial class KurumlarContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TubimProject.Domain.Entities.Tpds.KT_KURUMLAR", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("KT_KURUMLAR");
                });

            modelBuilder.Entity("TubimProject.Domain.Entities.Tpds.KT_MADDETURLERI", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("KT_MADDETURLERI");
                });

            modelBuilder.Entity("TubimProject.Domain.Entities.Tpds.UT_MADDE", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AltTuru")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AnaTuru")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Birimi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Durum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EgmMaddeId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("JandarmaMaddeId")
                        .HasColumnType("bigint");

                    b.Property<string>("KaynakUlke")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("MalzemeNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Miktari")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("OlayId")
                        .HasColumnType("bigint");

                    b.Property<long>("OlayNumarasi")
                        .HasColumnType("bigint");

                    b.Property<string>("ServisAnaTuru")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OlayId");

                    b.ToTable("UT_MADDE");
                });

            modelBuilder.Entity("TubimProject.Domain.Entities.Tpds.UT_OLAY", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Durum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HedefUlkesi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KacakcilikYontemi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("KurumOlayId")
                        .HasColumnType("bigint");

                    b.Property<int>("KurumRef")
                        .HasColumnType("int");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("MudahaleEdenBirim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("OlayNumarasi")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("OlayTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("SistemNo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UT_OLAY");
                });

            modelBuilder.Entity("TubimProject.Domain.Entities.Tpds.UT_OLAYDETAY", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("OlayBeldeKoy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OlayBolgesi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OlayBoylam")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OlayEnlem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("OlayId")
                        .HasColumnType("bigint");

                    b.Property<string>("OlayIlcesi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OlayIli")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OlayMahalle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OlayId");

                    b.ToTable("UT_OLAYDETAY");
                });

            modelBuilder.Entity("TubimProject.Domain.Entities.Tpds.UT_SAHIS", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Cinsiyet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DogumTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("Durum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("MedeniDurumu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Meslek")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OlayId")
                        .HasColumnType("int");

                    b.Property<long>("OlayNumarasi")
                        .HasColumnType("bigint");

                    b.Property<int?>("SupheliId")
                        .HasColumnType("int");

                    b.Property<long?>("SupheliNo")
                        .HasColumnType("bigint");

                    b.Property<string>("TCKimlikNoPasaportNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uyrugu")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UT_SAHISLAR");
                });

            modelBuilder.Entity("TubimProject.Domain.Entities.Tpds.UT_SUCTANIM", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<long>("OlayId")
                        .HasColumnType("bigint");

                    b.Property<string>("SucTanimi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UzunSucTanimi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OlayId");

                    b.ToTable("UT_SUCTANIM");
                });

            modelBuilder.Entity("TubimProject.Domain.Entities.Tpds.UT_MADDE", b =>
                {
                    b.HasOne("TubimProject.Domain.Entities.Tpds.UT_OLAY", "UT_OLAY")
                        .WithMany("UT_MADDE")
                        .HasForeignKey("OlayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UT_OLAY");
                });

            modelBuilder.Entity("TubimProject.Domain.Entities.Tpds.UT_OLAYDETAY", b =>
                {
                    b.HasOne("TubimProject.Domain.Entities.Tpds.UT_OLAY", "UT_OLAY")
                        .WithMany("UT_OLAYDETAY")
                        .HasForeignKey("OlayId");

                    b.Navigation("UT_OLAY");
                });

            modelBuilder.Entity("TubimProject.Domain.Entities.Tpds.UT_SUCTANIM", b =>
                {
                    b.HasOne("TubimProject.Domain.Entities.Tpds.UT_OLAY", "UT_OLAY")
                        .WithMany("UT_SUCTANIM")
                        .HasForeignKey("OlayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UT_OLAY");
                });

            modelBuilder.Entity("TubimProject.Domain.Entities.Tpds.UT_OLAY", b =>
                {
                    b.Navigation("UT_MADDE");

                    b.Navigation("UT_OLAYDETAY");

                    b.Navigation("UT_SUCTANIM");
                });
#pragma warning restore 612, 618
        }
    }
}
