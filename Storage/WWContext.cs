using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using Database;

namespace Storage
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WWContext : DbContext
    {
        public WWContext()
            : base("name=WWContext"){
            
        }

        public virtual DbSet<BotStatus> BotStatus { get; set; }
        public virtual DbSet<GlobalBan> GlobalBan { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<DailyCount> DailyCount { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<GameKill> GameKill { get; set; }
        public virtual DbSet<GamePlayer> GamePlayer { get; set; }
        public virtual DbSet<GlobalStats> GlobalStats { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<GroupRanking> GroupRanking { get; set; }
        public virtual DbSet<GroupStats> GroupStats { get; set; }
        public virtual DbSet<KillMethod> KillMethod { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerStats> PlayerStats { get; set; }
        public virtual DbSet<RefreshDate> RefreshDate { get; set; }
        public virtual DbSet<NotifyGame> NotifyGame { get; set; }
        public virtual DbSet<v_WaitList> v_WaitList { get; set; }
        public virtual DbSet<v_GroupRanking> v_GroupRanking { get; set; }
        public virtual DbSet<v_IdleKill24HoursMain> v_IdleKill24HoursMain { get; set; }

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
        
        public virtual ObjectResult<getPlayTime_Result> getPlayTime(int? playerCount)
        {
            var playerCountParameter = playerCount.HasValue ?
                new ObjectParameter("playerCount", playerCount) :
                new ObjectParameter("playerCount", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getPlayTime_Result>("getPlayTime", playerCountParameter);
        }
    
        public virtual ObjectResult<getRoles_Result> getRoles(string groupName)
        {
            var groupNameParameter = groupName != null ?
                new ObjectParameter("groupName", groupName) :
                new ObjectParameter("groupName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getRoles_Result>("getRoles", groupNameParameter);
        }
    
        public virtual ObjectResult<PlayerMostKilled_Result> PlayerMostKilled(int? pid)
        {
            var pidParameter = pid.HasValue ?
                new ObjectParameter("pid", pid) :
                new ObjectParameter("pid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PlayerMostKilled_Result>("PlayerMostKilled", pidParameter);
        }
    
        public virtual ObjectResult<PlayerMostKilledBy_Result> PlayerMostKilledBy(int? pid)
        {
            var pidParameter = pid.HasValue ?
                new ObjectParameter("pid", pid) :
                new ObjectParameter("pid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PlayerMostKilledBy_Result>("PlayerMostKilledBy", pidParameter);
        }
    
        public virtual ObjectResult<PlayerRoles_Result> PlayerRoles(int? pid)
        {
            var pidParameter = pid.HasValue ?
                new ObjectParameter("pid", pid) :
                new ObjectParameter("pid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PlayerRoles_Result>("PlayerRoles", pidParameter);
        }
    
        public virtual ObjectResult<GlobalDay1Death_Result1> GlobalDay1Death()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GlobalDay1Death_Result1>("GlobalDay1Death");
        }
    
        public virtual ObjectResult<GlobalDay1Lynch_Result1> GlobalDay1Lynch()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GlobalDay1Lynch_Result1>("GlobalDay1Lynch");
        }
    
        public virtual ObjectResult<GlobalNight1Death_Result1> GlobalNight1Death()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GlobalNight1Death_Result1>("GlobalNight1Death");
        }
    
        public virtual ObjectResult<GlobalSurvivor_Result1> GlobalSurvivor()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GlobalSurvivor_Result1>("GlobalSurvivor");
        }
    
        public virtual ObjectResult<GroupDay1Death_Result1> GroupDay1Death(Nullable<long> groupid)
        {
            var groupidParameter = groupid.HasValue ?
                new ObjectParameter("groupid", groupid) :
                new ObjectParameter("groupid", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GroupDay1Death_Result1>("GroupDay1Death", groupidParameter);
        }
    
        public virtual ObjectResult<GroupDay1Lynch_Result1> GroupDay1Lynch(Nullable<long> groupid)
        {
            var groupidParameter = groupid.HasValue ?
                new ObjectParameter("groupid", groupid) :
                new ObjectParameter("groupid", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GroupDay1Lynch_Result1>("GroupDay1Lynch", groupidParameter);
        }
    
        public virtual ObjectResult<GroupNight1Death_Result1> GroupNight1Death(Nullable<long> groupid)
        {
            var groupidParameter = groupid.HasValue ?
                new ObjectParameter("groupid", groupid) :
                new ObjectParameter("groupid", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GroupNight1Death_Result1>("GroupNight1Death", groupidParameter);
        }
    
        public virtual ObjectResult<GroupSurvivor_Result1> GroupSurvivor(Nullable<long> groupid)
        {
            var groupidParameter = groupid.HasValue ?
                new ObjectParameter("groupid", groupid) :
                new ObjectParameter("groupid", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GroupSurvivor_Result1>("GroupSurvivor", groupidParameter);
        }
    
        public virtual ObjectResult<getDailyCounts_Result> getDailyCounts()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getDailyCounts_Result>("getDailyCounts");
        }
    
        public virtual ObjectResult<int?> GetIdleKills24Hours(Nullable<int> userid)
        {
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetIdleKills24Hours", useridParameter);
        }
    
        public virtual int RestoreAccount(int? oldTGId, int? newTGId)
        {
            var oldTGIdParameter = oldTGId.HasValue ?
                new ObjectParameter("oldTGId", oldTGId) :
                new ObjectParameter("oldTGId", typeof(int));
    
            var newTGIdParameter = newTGId.HasValue ?
                new ObjectParameter("newTGId", newTGId) :
                new ObjectParameter("newTGId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RestoreAccount", oldTGIdParameter, newTGIdParameter);
        }
    }
}
