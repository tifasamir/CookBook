// <auto-generated />
using CodeBook.db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeBook.Migrations
{
    [DbContext(typeof(CodeBookContext))]
    [Migration("20211112121100_fix")]
    partial class fix
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

            modelBuilder.Entity("CodeBook.models.Setting", b =>
                {
                    b.Property<int>("SettingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("desc")
                        .HasColumnType("TEXT");

                    b.Property<string>("key")
                        .HasColumnType("TEXT");

                    b.Property<string>("value")
                        .HasColumnType("TEXT");

                    b.HasKey("SettingId");

                    b.ToTable("Setting");

                    b.HasData(
                        new
                        {
                            SettingId = 1,
                            desc = "",
                            key = "sLang",
                            value = ""
                        },
                        new
                        {
                            SettingId = 2,
                            desc = "",
                            key = "sChapter",
                            value = ""
                        },
                        new
                        {
                            SettingId = 3,
                            desc = "",
                            key = "sLesson",
                            value = ""
                        },
                        new
                        {
                            SettingId = 4,
                            desc = "",
                            key = "sView",
                            value = ""
                        },
                        new
                        {
                            SettingId = 5,
                            desc = "",
                            key = "materialurl",
                            value = ""
                        },
                        new
                        {
                            SettingId = 6,
                            desc = "",
                            key = "addLangShortCut",
                            value = ""
                        },
                        new
                        {
                            SettingId = 7,
                            desc = "",
                            key = "addChapterShortCut",
                            value = ""
                        },
                        new
                        {
                            SettingId = 8,
                            desc = "",
                            key = "addLessonShortCut",
                            value = ""
                        });
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
