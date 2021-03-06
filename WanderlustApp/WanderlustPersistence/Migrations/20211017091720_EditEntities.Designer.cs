// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WanderlustPersistence.Infrastructure;

namespace WanderlustPersistence.Migrations
{
    [DbContext(typeof(WanderlustContext))]
    [Migration("20211017091720_EditEntities")]
    partial class EditEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("CountryTraditionalFood", b =>
                {
                    b.Property<Guid>("CountryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TypicalFoodsId")
                        .HasColumnType("uuid");

                    b.HasKey("CountryId", "TypicalFoodsId");

                    b.HasIndex("TypicalFoodsId");

                    b.ToTable("CountryTraditionalFood");
                });

            modelBuilder.Entity("CountryUser", b =>
                {
                    b.Property<Guid>("CountriesVisitedId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("VisitedByUsersId")
                        .HasColumnType("uuid");

                    b.HasKey("CountriesVisitedId", "VisitedByUsersId");

                    b.HasIndex("VisitedByUsersId");

                    b.ToTable("CountryUser");
                });

            modelBuilder.Entity("RegionUser", b =>
                {
                    b.Property<Guid>("RegionsVisitedId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("VisitedByUsersId")
                        .HasColumnType("uuid");

                    b.HasKey("RegionsVisitedId", "VisitedByUsersId");

                    b.HasIndex("VisitedByUsersId");

                    b.ToTable("RegionUser");
                });

            modelBuilder.Entity("TownUser", b =>
                {
                    b.Property<Guid>("TownsVisitedId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("VisitedByUsersId")
                        .HasColumnType("uuid");

                    b.HasKey("TownsVisitedId", "VisitedByUsersId");

                    b.HasIndex("VisitedByUsersId");

                    b.ToTable("TownUser");
                });

            modelBuilder.Entity("TraditionalFoodUser", b =>
                {
                    b.Property<Guid>("EatenByUsersId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TraditionalFoodsEatenId")
                        .HasColumnType("uuid");

                    b.HasKey("EatenByUsersId", "TraditionalFoodsEatenId");

                    b.HasIndex("TraditionalFoodsEatenId");

                    b.ToTable("TraditionalFoodUser");
                });

            modelBuilder.Entity("WanderlustPersistence.Entity.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Description")
                        .HasMaxLength(2500)
                        .HasColumnType("character varying(2500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("WanderlustPersistence.Entity.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("WanderlustPersistence.Entity.Sight", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<bool>("IsUnescoSight")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<Guid?>("SightTypeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TownId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SightTypeId");

                    b.HasIndex("TownId");

                    b.ToTable("Sight");
                });

            modelBuilder.Entity("WanderlustPersistence.Entity.SightType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<int>("SightOrigin")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("SightType");
                });

            modelBuilder.Entity("WanderlustPersistence.Entity.Town", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("WanderlustPersistence.Entity.TraditionalFood", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Recipe")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.HasKey("Id");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("WanderlustPersistence.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CountryTraditionalFood", b =>
                {
                    b.HasOne("WanderlustPersistence.Entity.Country", null)
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WanderlustPersistence.Entity.TraditionalFood", null)
                        .WithMany()
                        .HasForeignKey("TypicalFoodsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CountryUser", b =>
                {
                    b.HasOne("WanderlustPersistence.Entity.Country", null)
                        .WithMany()
                        .HasForeignKey("CountriesVisitedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WanderlustPersistence.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("VisitedByUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RegionUser", b =>
                {
                    b.HasOne("WanderlustPersistence.Entity.Region", null)
                        .WithMany()
                        .HasForeignKey("RegionsVisitedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WanderlustPersistence.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("VisitedByUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TownUser", b =>
                {
                    b.HasOne("WanderlustPersistence.Entity.Town", null)
                        .WithMany()
                        .HasForeignKey("TownsVisitedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WanderlustPersistence.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("VisitedByUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TraditionalFoodUser", b =>
                {
                    b.HasOne("WanderlustPersistence.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("EatenByUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WanderlustPersistence.Entity.TraditionalFood", null)
                        .WithMany()
                        .HasForeignKey("TraditionalFoodsEatenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WanderlustPersistence.Entity.Region", b =>
                {
                    b.HasOne("WanderlustPersistence.Entity.Country", "Country")
                        .WithMany("Regions")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("WanderlustPersistence.Entity.Sight", b =>
                {
                    b.HasOne("WanderlustPersistence.Entity.SightType", "SightType")
                        .WithMany()
                        .HasForeignKey("SightTypeId");

                    b.HasOne("WanderlustPersistence.Entity.Town", "Town")
                        .WithMany("Sights")
                        .HasForeignKey("TownId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SightType");

                    b.Navigation("Town");
                });

            modelBuilder.Entity("WanderlustPersistence.Entity.Town", b =>
                {
                    b.HasOne("WanderlustPersistence.Entity.Region", "Region")
                        .WithMany("Towns")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("WanderlustPersistence.Entity.Country", b =>
                {
                    b.Navigation("Regions");
                });

            modelBuilder.Entity("WanderlustPersistence.Entity.Region", b =>
                {
                    b.Navigation("Towns");
                });

            modelBuilder.Entity("WanderlustPersistence.Entity.Town", b =>
                {
                    b.Navigation("Sights");
                });
#pragma warning restore 612, 618
        }
    }
}
