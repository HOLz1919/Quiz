using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Quiz.Server.Models;
using Quiz.Shared;
using Quiz.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<QuestionView>(e => e.ToView("QuestionView").HasNoKey());
            builder.Entity<UserMatchView>(e => e.ToView("UserMatchView").HasNoKey());
            builder.Entity<MatchView>(e => e.ToView("MatchView").HasNoKey());

            builder.Entity<UserMatch>().HasKey(um => new { um.MatchId, um.ApplicationUserId });
            builder.Entity<UserMatch>()
                .HasOne(bc => bc.ApplicationUser)
                .WithMany(b => b.UserMatches)
                . HasForeignKey(bc => bc.ApplicationUserId);
            builder.Entity<UserMatch>()
                .HasOne(bc => bc.Match)
                .WithMany(c => c.UserMatches)
                .HasForeignKey(bc => bc.MatchId);


            builder.Entity<MatchQuestion>().HasKey(mq => new { mq.MatchId, mq.QuestionId });
            builder.Entity<MatchQuestion>()
                .HasOne(bc => bc.Question)
                .WithMany(b => b.MatchQuestions)
                .HasForeignKey(bc => bc.QuestionId);
            builder.Entity<MatchQuestion>()
                .HasOne(bc => bc.Match)
                .WithMany(c => c.MatchQuestions)
                .HasForeignKey(bc => bc.MatchId);


            base.OnModelCreating(builder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<UserMatch> UserMatches { get; set; }
        public DbSet<MatchQuestion> MatchQuestions { get; set; }

        #region Views

        public DbSet<QuestionView> QuestionViews { get; set; }
        public DbSet<UserMatchView> UserMatchViews { get; set; }
        public DbSet<MatchView> MatchViews { get; set; }


        #endregion


    }
}
