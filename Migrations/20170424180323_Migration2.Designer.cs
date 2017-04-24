using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BlackBeltTest2.Models;

namespace BlackBeltTest2.Migrations
{
    [DbContext(typeof(MasterContext))]
    [Migration("20170424180323_Migration2")]
    partial class Migration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("BlackBeltTest2.Models.Idea", b =>
                {
                    b.Property<int>("IdeaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<int>("UserId");

                    b.Property<DateTime>("created_at");

                    b.Property<DateTime>("updated_at");

                    b.HasKey("IdeaId");

                    b.HasIndex("UserId");

                    b.ToTable("Idea");
                });

            modelBuilder.Entity("BlackBeltTest2.Models.Like", b =>
                {
                    b.Property<int>("LikeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdeaId");

                    b.Property<int>("UserId");

                    b.Property<DateTime>("created_at");

                    b.Property<DateTime>("updated_at");

                    b.HasKey("LikeId");

                    b.HasIndex("IdeaId");

                    b.HasIndex("UserId");

                    b.ToTable("Like");
                });

            modelBuilder.Entity("BlackBeltTest2.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<DateTime>("created_at");

                    b.Property<DateTime>("updated_at");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("BlackBeltTest2.Models.Idea", b =>
                {
                    b.HasOne("BlackBeltTest2.Models.User", "User")
                        .WithMany("Ideas")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BlackBeltTest2.Models.Like", b =>
                {
                    b.HasOne("BlackBeltTest2.Models.Idea", "Idea")
                        .WithMany("IdeasLikes")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BlackBeltTest2.Models.User", "User")
                        .WithMany("UserLikes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
