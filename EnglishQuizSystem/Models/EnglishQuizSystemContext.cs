using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EnglishQuizSystem.Models
{
    public partial class EnglishQuizSystemContext : DbContext
    {
        public EnglishQuizSystemContext()
        {
        }

        public EnglishQuizSystemContext(DbContextOptions<EnglishQuizSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<Quiz> Quizzes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserAnswer> UserAnswers { get; set; } = null!;
        public virtual DbSet<UserQuiz> UserQuizzes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyConStr"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("answer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsCorrect).HasColumnName("is_correct");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.Text)
                    .HasColumnType("ntext")
                    .HasColumnName("text");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__answer__question__5812160E");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("question");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Difficulty).HasColumnName("difficulty");

                entity.Property(e => e.QuizId).HasColumnName("quiz_id");

                entity.Property(e => e.Text)
                    .HasColumnType("ntext")
                    .HasColumnName("text");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("FK__question__quiz_i__59063A47");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable("quiz");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CodeActive)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("code_active");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.UserName, "UQ__user__7C9273C469183D65")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("user_name");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__user__role_id__59FA5E80");
            });

            modelBuilder.Entity<UserAnswer>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.QuestionId, e.AnswerId, e.QuizId })
                    .HasName("user_answer_pk");

                entity.ToTable("user_answer");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.AnswerId).HasColumnName("answer_id");

                entity.Property(e => e.QuizId).HasColumnName("quiz_id");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.UserAnswers)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_answ__answe__5AEE82B9");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.UserAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_answ__quest__5BE2A6F2");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.UserAnswers)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_answ__quiz___5CD6CB2B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAnswers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_answ__user___5DCAEF64");
            });

            modelBuilder.Entity<UserQuiz>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.QuizId })
                    .HasName("user_topic_pk");

                entity.ToTable("user_quiz");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.QuizId).HasColumnName("quiz_id");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.UserQuizzes)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_quiz__quiz___5EBF139D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserQuizzes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_quiz__user___5FB337D6");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
