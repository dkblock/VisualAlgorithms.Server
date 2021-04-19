﻿using Algorithmix.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Algorithmix.Database
{
    public class ApplicationContext : IdentityDbContext<ApplicationUserEntity>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AlgorithmEntity>()
                .HasOne<AlgorithmTimeComplexityEntity>()
                .WithOne()
                .HasForeignKey<AlgorithmEntity>(a => a.TimeComplexityId);

            builder.Entity<ApplicationUserEntity>()
                .HasOne<GroupEntity>()
                .WithMany()
                .HasForeignKey(u => u.GroupId);

            builder.Entity<TestEntity>()
                .HasOne<AlgorithmEntity>()
                .WithMany()
                .HasForeignKey(t => t.AlgorithmId);

            builder.Entity<TestQuestionEntity>()
                .HasOne<TestEntity>()
                .WithMany()
                .HasForeignKey(q => q.TestId);

            builder.Entity<TestAnswerEntity>()
                .HasOne<TestQuestionEntity>()
                .WithMany()
                .HasForeignKey(a => a.QuestionId);

            builder.Entity<UserAnswerEntity>()
                .HasKey(ua => new { ua.QuestionId, ua.UserId });

            builder.Entity<UserAnswerEntity>()
                .HasOne<TestQuestionEntity>()
                .WithMany()
                .HasForeignKey(ua => ua.QuestionId);

            builder.Entity<UserAnswerEntity>()
                .HasOne<ApplicationUserEntity>()
                .WithMany()
                .HasForeignKey(ua => ua.UserId);

            builder.Entity<UserTestResultEntity>()
                .HasKey(utr => new { utr.TestId, utr.UserId });

            builder.Entity<UserTestResultEntity>()
                .HasOne<TestEntity>()
                .WithMany()
                .HasForeignKey(utr => utr.TestId);

            builder.Entity<UserTestResultEntity>()
                .HasOne<ApplicationUserEntity>()
                .WithMany()
                .HasForeignKey(utr => utr.UserId);
        }

        public DbSet<ApplicationUserEntity> ApplicationUsers { get; set; }
        public DbSet<AlgorithmEntity> Algorithms { get; set; }
        public DbSet<AlgorithmTimeComplexityEntity> AlgorithmTimeComplexities { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<TestEntity> Tests { get; set; }
        public DbSet<TestAnswerEntity> TestAnswers { get; set; }
        public DbSet<TestQuestionEntity> TestQuestions { get; set; }
        public DbSet<UserAnswerEntity> UserAnswers { get; set; }
        public DbSet<UserTestResultEntity> UserTestResults { get; set; }
    }
}
