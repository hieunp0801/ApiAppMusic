﻿// <auto-generated />
using System;
using ApiAppMusic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiAppMusic.Migrations
{
    [DbContext(typeof(MusicDBContext))]
    [Migration("20230412172911_playlist")]
    partial class playlist
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApiAppMusic.Models.Music", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileMusic")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NameMusic")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("SingerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SingerId");

                    b.ToTable("musics");
                });

            modelBuilder.Entity("ApiAppMusic.Models.MusicPlaylist", b =>
                {
                    b.Property<int>("IdMusic")
                        .HasColumnType("int");

                    b.Property<int>("IdPlaylist")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("IdMusic", "IdPlaylist");

                    b.HasIndex("IdPlaylist");

                    b.ToTable("musicPlaylists");
                });

            modelBuilder.Entity("ApiAppMusic.Models.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("playlists");
                });

            modelBuilder.Entity("ApiAppMusic.Models.Singer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DateofBirth")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ImageSinger")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NameSinger")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("singers");
                });

            modelBuilder.Entity("ApiAppMusic.Models.Music", b =>
                {
                    b.HasOne("ApiAppMusic.Models.Singer", "Singer")
                        .WithMany()
                        .HasForeignKey("SingerId");

                    b.Navigation("Singer");
                });

            modelBuilder.Entity("ApiAppMusic.Models.MusicPlaylist", b =>
                {
                    b.HasOne("ApiAppMusic.Models.Music", "Music")
                        .WithMany("MusicPlaylists")
                        .HasForeignKey("IdMusic")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiAppMusic.Models.Playlist", "Playlist")
                        .WithMany("MusicPlaylists")
                        .HasForeignKey("IdPlaylist")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Music");

                    b.Navigation("Playlist");
                });

            modelBuilder.Entity("ApiAppMusic.Models.Music", b =>
                {
                    b.Navigation("MusicPlaylists");
                });

            modelBuilder.Entity("ApiAppMusic.Models.Playlist", b =>
                {
                    b.Navigation("MusicPlaylists");
                });
#pragma warning restore 612, 618
        }
    }
}
