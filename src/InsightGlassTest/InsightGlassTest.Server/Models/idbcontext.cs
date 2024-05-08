using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace InsightGlassTest.Server.Models;

public partial class idbcontext : DbContext
{
    public idbcontext()
    {
    }

    public idbcontext(DbContextOptions<idbcontext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Aspnetrole> Aspnetroles { get; set; }

    public virtual DbSet<Aspnetroleclaim> Aspnetroleclaims { get; set; }

    public virtual DbSet<Aspnetuser> Aspnetusers { get; set; }

    public virtual DbSet<Aspnetuserclaim> Aspnetuserclaims { get; set; }

    public virtual DbSet<Aspnetuserlogin> Aspnetuserlogins { get; set; }

    public virtual DbSet<Aspnetusertoken> Aspnetusertokens { get; set; }

    public virtual DbSet<Blogcomment> Blogcomments { get; set; }

    public virtual DbSet<Blogpost> Blogposts { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Companyfeedback> Companyfeedbacks { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Jobopening> Jobopenings { get; set; }

    public virtual DbSet<Seeker> Seekers { get; set; }

    public virtual DbSet<Seekerexperience> Seekerexperiences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => new { e.ApplicationJobId, e.ApplicationSeekerUserId, e.ApplicationCompanyUserId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.Property(e => e.ApplicationJobId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.ApplicationCompanyUser).WithMany(p => p.Applications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyApplicationId");

            entity.HasOne(d => d.ApplicationSeekerUser).WithMany(p => p.Applications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SeekerApplicationId");
        });

        modelBuilder.Entity<Aspnetrole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Aspnetroleclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.Role).WithMany(p => p.Aspnetroleclaims).HasConstraintName("FK_AspNetRoleClaims_AspNetRoles_RoleId");
        });

        modelBuilder.Entity<Aspnetuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasMany(d => d.FollowsUsers).WithMany(p => p.ThisUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "Userfollowing",
                    r => r.HasOne<Aspnetuser>().WithMany()
                        .HasForeignKey("FollowsUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_FollowsUserId"),
                    l => l.HasOne<Aspnetuser>().WithMany()
                        .HasForeignKey("ThisUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ThisUserId"),
                    j =>
                    {
                        j.HasKey("ThisUserId", "FollowsUserId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("userfollowing");
                        j.HasIndex(new[] { "FollowsUserId" }, "FK_FollowsUserId");
                    });

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Aspnetuserrole",
                    r => r.HasOne<Aspnetrole>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId"),
                    l => l.HasOne<Aspnetuser>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("aspnetuserroles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });

            entity.HasMany(d => d.ThisUsers).WithMany(p => p.FollowsUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "Userfollowing",
                    r => r.HasOne<Aspnetuser>().WithMany()
                        .HasForeignKey("ThisUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ThisUserId"),
                    l => l.HasOne<Aspnetuser>().WithMany()
                        .HasForeignKey("FollowsUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_FollowsUserId"),
                    j =>
                    {
                        j.HasKey("ThisUserId", "FollowsUserId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("userfollowing");
                        j.HasIndex(new[] { "FollowsUserId" }, "FK_FollowsUserId");
                    });
        });

        modelBuilder.Entity<Aspnetuserclaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserclaims).HasConstraintName("FK_AspNetUserClaims_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetuserlogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetuserlogins).HasConstraintName("FK_AspNetUserLogins_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Aspnetusertoken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.HasOne(d => d.User).WithMany(p => p.Aspnetusertokens).HasConstraintName("FK_AspNetUserTokens_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Blogcomment>(entity =>
        {
            entity.HasKey(e => new { e.BlogCommentId, e.BlogUserId, e.BlogId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.Property(e => e.BlogCommentId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.BlogUser).WithMany(p => p.Blogcomments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCommentId");
        });

        modelBuilder.Entity<Blogpost>(entity =>
        {
            entity.HasKey(e => new { e.BlogId, e.BlogUserId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.Property(e => e.BlogId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.BlogUser).WithMany(p => p.Blogposts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserBlogId");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyUserId).HasName("PRIMARY");

            entity.HasOne(d => d.CompanyUser).WithOne(p => p.Company)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyuserId");
        });

        modelBuilder.Entity<Companyfeedback>(entity =>
        {
            entity.HasKey(e => new { e.FeedbackSeekerUserId, e.FeedbackCompanyUserId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.HasOne(d => d.FeedbackCompanyUser).WithMany(p => p.Companyfeedbacks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyUserId1");

            entity.HasOne(d => d.FeedbackSeekerUser).WithMany(p => p.Companyfeedbacks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SeekerUserId1");
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Jobopening>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.JobCompanyUserId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.Property(e => e.JobId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.JobCompanyUser).WithMany(p => p.Jobopenings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobCompanyUserId");
        });

        modelBuilder.Entity<Seeker>(entity =>
        {
            entity.HasKey(e => e.SeekerUserId).HasName("PRIMARY");

            entity.HasOne(d => d.SeekerUser).WithOne(p => p.Seeker)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_userId");
        });

        modelBuilder.Entity<Seekerexperience>(entity =>
        {
            entity.HasKey(e => new { e.ExperienceId, e.ExperienceSeekerUserId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.Property(e => e.ExperienceId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.ExperienceSeekerUser).WithMany(p => p.Seekerexperiences)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SeekerExpId1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
