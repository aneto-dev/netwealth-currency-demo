// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Netwealth.Data;

namespace NetWealth.Data.Migrations
{
    [DbContext(typeof(NetwealthDbContext))]
    [Migration("20220505163201_Add_Country_Currency_Table2")]
    partial class Add_Country_Currency_Table2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NetWealth.Data.Entities.CountryCurrency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)");

                    b.Property<short>("Reference")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("CountryCurrencies");
                });
#pragma warning restore 612, 618
        }
    }
}
