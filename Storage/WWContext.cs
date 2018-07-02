using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using Storage.Result;

namespace Storage
{
    public partial class WWContext : DbContext
    {
        public WWContext()
            : base("data source=localhost;initial catalog=werewolf;user id=sa;password=zaqwsxA111111") { }

        public virtual DbSet<BotStatus> BotStatus { get; set; }
        public virtual DbSet<GlobalBan> GlobalBans { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<DailyCount> DailyCount { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameKill> GameKills { get; set; }
        public virtual DbSet<GamePlayer> GamePlayers { get; set; }
        public virtual DbSet<GlobalStats> GlobalStats { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupRanking> GroupRanking { get; set; }
        public virtual DbSet<GroupStats> GroupStats { get; set; }
        public virtual DbSet<KillMethod> KillMethod { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerStats> PlayerStats { get; set; }
        public virtual DbSet<RefreshDate> RefreshDate { get; set; }
        public virtual DbSet<NotifyGame> NotifyGame { get; set; }
        public virtual DbSet<v_WaitList> WaitListView { get; set; }
        public virtual DbSet<v_GroupRanking> v_GroupRanking { get; set; }
        public virtual DbSet<v_IdleKill24HoursMain> VIdleKill24HoursMain { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BotStatus>()
                .Property(e => e.BotName)
                .IsUnicode(false);

            modelBuilder.Entity<BotStatus>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<BotStatus>()
                .Property(e => e.BotLink)
                .IsUnicode(false);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.GamePlayer)
                .WithRequired(e => e.Game)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Game)
                .WithOptional(e => e.Group)
                .HasForeignKey(e => e.GrpId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Player)
                .WithMany(e => e.Group)
                .Map(m => m.ToTable("GroupAdmin").MapLeftKey("GroupId").MapRightKey("PlayerId"));

            modelBuilder.Entity<GroupRanking>()
                .Property(e => e.MinutesPlayed)
                .HasPrecision(18, 10);

            modelBuilder.Entity<GroupRanking>()
                .Property(e => e.Ranking)
                .HasPrecision(18, 10);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.GameKill)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.KillerId);

            modelBuilder.Entity<RefreshDate>()
                .Property(e => e.Lock)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<v_GroupRanking>()
                .Property(e => e.Ranking)
                .HasPrecision(18, 10);
        }

        public virtual IEnumerable<getPlayTime_Result> getPlayTime(int? playerCount)
        {
            var playerCountParameter = playerCount.HasValue
                ? new ObjectParameter("playerCount", playerCount)
                : new ObjectParameter("playerCount", typeof(int));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction<getPlayTime_Result>("getPlayTime",
                playerCountParameter);
        }

        public virtual IEnumerable<getRoles_Result> getRoles(string groupName)
        {
            var groupNameParameter = groupName != null
                ? new ObjectParameter("groupName", groupName)
                : new ObjectParameter("groupName", typeof(string));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction<getRoles_Result>("getRoles",
                groupNameParameter);
        }

        public virtual ObjectResult<PlayerMostKilled_Result> PlayerMostKilled(int? pid)
        {
            var pidParameter = pid.HasValue ? new ObjectParameter("pid", pid) : new ObjectParameter("pid", typeof(int));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction<PlayerMostKilled_Result>(
                "PlayerMostKilled", pidParameter);
        }

        public virtual IEnumerable<PlayerMostKilledBy_Result> PlayerMostKilledBy(int? pid)
        {
            var pidParameter = pid.HasValue ? new ObjectParameter("pid", pid) : new ObjectParameter("pid", typeof(int));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction<PlayerMostKilledBy_Result>(
                "PlayerMostKilledBy", pidParameter);
        }

        public virtual IEnumerable<PlayerRoles_Result> PlayerRoles(int? pid)
        {
            var pidParameter = pid.HasValue ? new ObjectParameter("pid", pid) : new ObjectParameter("pid", typeof(int));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction<PlayerRoles_Result>("PlayerRoles",
                pidParameter);
        }

        public virtual ObjectResult<GlobalDay1Death_Result1> GlobalDay1Death()
        {
            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction<GlobalDay1Death_Result1>(
                "GlobalDay1Death");
        }

        public virtual ObjectResult<GlobalDay1Lynch_Result1> GlobalDay1Lynch()
        {
            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction<GlobalDay1Lynch_Result1>(
                "GlobalDay1Lynch");
        }

        public virtual ObjectResult<GlobalNight1Death_Result1> GlobalNight1Death()
        {
            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction<GlobalNight1Death_Result1>(
                "GlobalNight1Death");
        }

        public virtual ObjectResult<GlobalSurvivor_Result1> GlobalSurvivor()
        {
            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction<GlobalSurvivor_Result1>(
                "GlobalSurvivor");
        }

        public virtual ObjectResult<GroupDay1Death_Result1> GroupDay1Death(Nullable<long> groupid)
        {
            var groupidParameter = groupid.HasValue
                ? new ObjectParameter("groupid", groupid)
                : new ObjectParameter("groupid", typeof(long));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction<GroupDay1Death_Result1>(
                "GroupDay1Death", groupidParameter);
        }

        public virtual ObjectResult<GroupDay1Lynch_Result1> GroupDay1Lynch(Nullable<long> groupid)
        {
            var groupidParameter = groupid.HasValue
                ? new ObjectParameter("groupid", groupid)
                : new ObjectParameter("groupid", typeof(long));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction<GroupDay1Lynch_Result1>(
                "GroupDay1Lynch", groupidParameter);
        }

        public virtual ObjectResult<GroupNight1Death_Result1> GroupNight1Death(Nullable<long> groupid)
        {
            var groupidParameter = groupid.HasValue
                ? new ObjectParameter("groupid", groupid)
                : new ObjectParameter("groupid", typeof(long));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction<GroupNight1Death_Result1>(
                "GroupNight1Death", groupidParameter);
        }

        public virtual ObjectResult<GroupSurvivor_Result1> GroupSurvivor(Nullable<long> groupid)
        {
            var groupidParameter = groupid.HasValue
                ? new ObjectParameter("groupid", groupid)
                : new ObjectParameter("groupid", typeof(long));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction<GroupSurvivor_Result1>("GroupSurvivor",
                groupidParameter);
        }

        public virtual ObjectResult<getDailyCounts_Result> getDailyCounts()
        {
            return ((IObjectContextAdapter) this).ObjectContext
                .ExecuteFunction<getDailyCounts_Result>("getDailyCounts");
        }

        public virtual IEnumerable<int?> GetIdleKills24Hours(int? userid)
        {
            var useridParameter = userid.HasValue
                ? new ObjectParameter("userid", userid)
                : new ObjectParameter("userid", typeof(int));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction<int?>("GetIdleKills24Hours",
                useridParameter);
        }

        public virtual int RestoreAccount(int? oldTgId, int? newTgId)
        {
            var oldTgIdParameter = oldTgId.HasValue
                ? new ObjectParameter("oldTGId", oldTgId)
                : new ObjectParameter("oldTGId", typeof(int));

            var newTgIdParameter = newTgId.HasValue
                ? new ObjectParameter("newTGId", newTgId)
                : new ObjectParameter("newTGId", typeof(int));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction("RestoreAccount", oldTgIdParameter,
                newTgIdParameter);
        }
    }
}