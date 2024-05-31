﻿// <auto-generated />
using System;
using FamilyHubs.Report.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FamilyHubs.Report.Data.Migrations
{
    [DbContext(typeof(ReportDbContext))]
    partial class ReportDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dim")
                .HasAnnotation("ProductVersion", "7.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FamilyHubs.Report.Data.Entities.DateDim", b =>
                {
                    b.Property<int>("DateKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DateKey"));

                    b.Property<byte>("CalendarQuarterNumberOfYear")
                        .HasColumnType("tinyint");

                    b.Property<short>("CalendarYearNumber")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<string>("DateString")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<byte>("DayNumberOfMonth")
                        .HasColumnType("tinyint");

                    b.Property<byte>("DayNumberOfWeek")
                        .HasColumnType("tinyint");

                    b.Property<short>("DayNumberOfYear")
                        .HasColumnType("smallint");

                    b.Property<string>("DayOfWeekName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("IsLeapYear")
                        .HasColumnType("bit");

                    b.Property<bool>("IsWeekend")
                        .HasColumnType("bit");

                    b.Property<string>("MonthName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<byte>("MonthNumberOfYear")
                        .HasColumnType("tinyint");

                    b.Property<byte>("WeekNumberOfYear")
                        .HasColumnType("tinyint");

                    b.HasKey("DateKey");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("DateKey"));

                    b.ToTable("DateDim", "dim");
                });

            modelBuilder.Entity("FamilyHubs.Report.Data.Entities.ServiceSearchFact", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .HasPrecision(7)
                        .HasColumnType("datetime2(7)");

                    b.Property<int>("DateKey")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified")
                        .HasPrecision(7)
                        .HasColumnType("datetime2(7)");

                    b.Property<long>("ServiceSearchId")
                        .HasColumnType("bigint");

                    b.Property<int>("ServiceSearchesKey")
                        .HasColumnType("int");

                    b.Property<int>("TimeKey")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"));

                    b.HasIndex("DateKey");

                    b.HasIndex("ServiceSearchesKey");

                    b.HasIndex("TimeKey");

                    b.ToTable("ServiceSearchFacts", "dim");
                });

            modelBuilder.Entity("FamilyHubs.Report.Data.Entities.ServiceSearchesDim", b =>
                {
                    b.Property<int>("ServiceSearchesKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceSearchesKey"));

                    b.Property<DateTime>("Created")
                        .HasPrecision(7)
                        .HasColumnType("datetime2(7)");

                    b.Property<long>("EventId")
                        .HasColumnType("bigint");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("HttpRequestCorrelationId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("HttpRequestTimestamp")
                        .HasPrecision(7)
                        .HasColumnType("datetime2(7)");

                    b.Property<short?>("HttpResponseCode")
                        .HasColumnType("smallint");

                    b.Property<DateTime?>("HttpResponseTimestamp")
                        .HasPrecision(7)
                        .HasColumnType("datetime2(7)");

                    b.Property<DateTime>("Modified")
                        .HasPrecision(7)
                        .HasColumnType("datetime2(7)");

                    b.Property<long?>("OrganisationId")
                        .HasColumnType("bigint");

                    b.Property<string>("OrganisationName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<byte?>("OrganisationTypeId")
                        .HasColumnType("tinyint");

                    b.Property<string>("OrganisationTypeName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte?>("RoleTypeId")
                        .HasColumnType("tinyint");

                    b.Property<string>("RoleTypeName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<byte>("SearchRadiusMiles")
                        .HasColumnType("tinyint");

                    b.Property<long>("ServiceSearchId")
                        .HasColumnType("bigint");

                    b.Property<byte>("ServiceTypeId")
                        .HasColumnType("tinyint");

                    b.Property<string>("ServiceTypeName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UserEmail")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserName")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("ServiceSearchesKey");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("ServiceSearchesKey"));

                    b.ToTable("ServiceSearchesDim", "dim");
                });

            modelBuilder.Entity("FamilyHubs.Report.Data.Entities.TimeDim", b =>
                {
                    b.Property<int>("TimeKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TimeKey"));

                    b.Property<byte>("HourNumberOfDay")
                        .HasColumnType("tinyint");

                    b.Property<byte>("MinuteNumberOfHour")
                        .HasColumnType("tinyint");

                    b.Property<byte>("SecondNumberOfMinute")
                        .HasColumnType("tinyint");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time(0)");

                    b.Property<string>("TimeString")
                        .IsRequired()
                        .HasColumnType("varchar(8)");

                    b.HasKey("TimeKey");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("TimeKey"));

                    b.ToTable("TimeDim", "dim");
                });

            modelBuilder.Entity("FamilyHubs.Report.Data.Entities.ServiceSearchFact", b =>
                {
                    b.HasOne("FamilyHubs.Report.Data.Entities.DateDim", "DateDim")
                        .WithMany()
                        .HasForeignKey("DateKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FamilyHubs.Report.Data.Entities.ServiceSearchesDim", "ServiceSearchesDim")
                        .WithMany()
                        .HasForeignKey("ServiceSearchesKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FamilyHubs.Report.Data.Entities.TimeDim", "TimeDim")
                        .WithMany()
                        .HasForeignKey("TimeKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DateDim");

                    b.Navigation("ServiceSearchesDim");

                    b.Navigation("TimeDim");
                });
#pragma warning restore 612, 618
        }
    }
}
