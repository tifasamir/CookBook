﻿// <auto-generated />
using CodeBook.db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeBook.Migrations
{
    [DbContext(typeof(CodeBookContext))]
    [Migration("20211110195151_start-app")]
    partial class startapp
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("CodeBook.Viewer", b =>
                {
                    b.Property<int>("ViewerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("LessonId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("fileurl")
                        .HasColumnType("TEXT");

                    b.Property<string>("imageurl")
                        .HasColumnType("TEXT");

                    b.Property<string>("snippeturl")
                        .HasColumnType("TEXT");

                    b.HasKey("ViewerId");

                    b.HasIndex("LessonId");

                    b.ToTable("Viewer");
                });

            modelBuilder.Entity("CodeBook.models.Chapter", b =>
                {
                    b.Property<int>("ChapterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("LangId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("desc")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.HasKey("ChapterId");

                    b.HasIndex("LangId");

                    b.ToTable("Chapter");
                });

            modelBuilder.Entity("CodeBook.models.Lang", b =>
                {
                    b.Property<int>("LangId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("desc")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.HasKey("LangId");

                    b.ToTable("Lang");
                });

            modelBuilder.Entity("CodeBook.models.Lesson", b =>
                {
                    b.Property<int>("LessonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ChapterId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("desc")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.HasKey("LessonId");

                    b.HasIndex("ChapterId");

                    b.ToTable("Lesson");
                });

            modelBuilder.Entity("CodeBook.Viewer", b =>
                {
                    b.HasOne("CodeBook.models.Lesson", "Lesson")
                        .WithMany("Views")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CodeBook.models.Chapter", b =>
                {
                    b.HasOne("CodeBook.models.Lang", "Lang")
                        .WithMany("Chapters")
                        .HasForeignKey("LangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CodeBook.models.Lesson", b =>
                {
                    b.HasOne("CodeBook.models.Chapter", "Chapter")
                        .WithMany("Lessons")
                        .HasForeignKey("ChapterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}