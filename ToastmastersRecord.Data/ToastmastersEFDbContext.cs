using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToastmastersRecord.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ToastmastersRecord.Data
{
    public class ToastmastersEFDbContext : DbContext
    {
        static ToastmastersEFDbContext()
        {
            //Database.SetInitializer<ToastmastersEFDbContext>(new ToastmastersEFDbInitializer());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "User ID=samplesam;Password=Password1;Host=localhost;Port=5432;Database=ToastmastersDb;Pooling=true;";
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<EnvelopeEntityBase>()
                .HasKey(e => new { e.StreamId, e.UserId, e.Id });

            modelBuilder.Entity<RoleRequestEnvelopeEntity>().ToTable("RoleRequestEvents");
            modelBuilder.Entity<RolePlacementEnvelopeEntity>().ToTable("RolePlacementEvents");

            modelBuilder
                .Entity<RoleRequestMeeting>()
                .HasKey(e => new { e.RoleRequestId, e.MeetingId });

            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<RoleRequestEnvelopeEntity> RoleRequestEvents { get; set; }
        public virtual DbSet<RolePlacementEnvelopeEntity> RolePlacementEvents { get; set; }
        public virtual DbSet<ClubMeetingEnvelopeEntity> ClubMeetingEvents { get; set; }
        public virtual DbSet<MemberEnvelopeEntity> MemberEvents { get; set; }
        public virtual DbSet<MemberEntity> Members { get; set; }
        public virtual DbSet<RoleRequestEntity> RoleRequests { get; set; }
        public virtual DbSet<RoleRequestMeeting> RoleRequestMeetings { get; set; }
        public virtual DbSet<MemberMessageEntity> Messages { get; set; }
        public virtual DbSet<ClubMeetingEntity> ClubMeetings { get; set; }
        public virtual DbSet<RolePlacementEntity> RolePlacements { get; set; }
        public virtual DbSet<RoleTypeEntity> RoleTypes { get; set; }
        public virtual DbSet<MemberHistoryAggregate> MemberHistories { get; set; }
        public virtual DbSet<DayOffEntity> DaysOff { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }
    }
}
