use werewolf;

create table Admin
(
  UserId int not null
    primary key
);

create table BotStatus
(
  Id        int auto_increment
    primary key,
  BotName   varchar(50) not null,
  BotStatus varchar(50) not null,
  BotLink   longtext    not null
);

create table DailyCount
(
  Id     int auto_increment
    primary key,
  Day    date not null,
  Groups int  not null,
  Games  int  not null,
  Users  int  not null
);

create table GlobalBan
(
  Id         int auto_increment
    primary key,
  TelegramId int      not null,
  Reason     longtext not null,
  Expires    datetime not null,
  BannedBy   longtext not null,
  BanDate    datetime null,
  Name       longtext null
);

create table GlobalStats
(
  Id                        int auto_increment
    primary key,
  GamesPlayed               int      not null,
  PlayersKilled             int      not null,
  PlayersSurvived           int      not null,
  MostKilledFirstNight      longtext null,
  MostKilledFirstPercent    int      null,
  MostLynchedFirstDay       longtext null,
  MostLynchedFirstPercent   int      null,
  MostKilledFirstDay        longtext null,
  MostKilledFirstDayPercent int      null,
  BestSurvivor              longtext null,
  BestSurvivorPercent       int      null,
  LastRun                   datetime null,
  TotalPlayers              int      null,
  TotalGroups               int      null,
  MostKilledFirstNightId    int      null,
  MostLynchedFirstDayId     int      null,
  MostKilledFirstDayId      int      null,
  BestSurvivorId            int      null
);

create table `Group`
(
  Id                  int auto_increment
    primary key,
  Name                longtext                 not null,
  GroupId             bigint                   not null,
  Preferred           tinyint(1)               null,
  Language            longtext                 null,
  DisableNotification tinyint(1)               null,
  UserName            longtext                 null,
  BotInGroup          tinyint(1)               null,
  ShowRoles           tinyint(1)               null,
  Mode                varchar(50) charset utf8 null,
  DayTime             int                      null,
  NightTime           int                      null,
  LynchTime           int                      null,
  AllowTanner         tinyint(1)               null,
  AllowFool           tinyint(1)               null,
  AllowCult           tinyint(1)               null,
  ShowRolesEnd        varchar(50) charset utf8 null,
  MaxPlayers          int                      null,
  DisableFlee         tinyint(1)               null,
  CreatedBy           longtext                 null,
  ImageFile           longtext                 null,
  Description         longtext                 null,
  GroupLink           longtext                 null,
  MemberCount         int                      null,
  AllowExtend         tinyint(1)               null,
  MaxExtend           int                      null,
  EnableSecretLynch   tinyint(1)               null,
  RandomModes         tinyint(1)               null,
  Flags               bigint                   null
);

create table Game
(
  Id          int auto_increment
    primary key,
  GroupName   longtext                 not null,
  GroupId     bigint                   not null,
  TimeStarted datetime                 null,
  TimeEnded   datetime                 null,
  Winner      varchar(50) charset utf8 null,
  GrpId       int                      null,
  Mode        varchar(50) charset utf8 null,
  constraint FK_Game_Group_GrpId
  foreign key (GrpId) references `Group` (Id)
    on update cascade
    on delete cascade
);

create index IX_GrpId
  on Game (GrpId);

create table GroupRanking
(
  Id            int auto_increment
    primary key,
  GroupId       int                       not null,
  Language      varchar(450) charset utf8 not null,
  PlayersCount  int                       not null,
  MinutesPlayed decimal(18, 10)           not null,
  LastRefresh   datetime                  not null,
  Ranking       decimal(18, 10)           null,
  GamesPlayed   int                       not null,
  `Show`        tinyint(1)                null
);

create table GroupStats
(
  Id                     int auto_increment
    primary key,
  GroupId                bigint   not null,
  GamesPlayed            int      not null,
  MostKilledFirstNight   longtext null,
  MostKilledFirstPercent int      null,
  MostLynchedFirstNight  longtext null,
  MostLynchFirstPercent  int      null,
  MostDeadFirstDay       longtext null,
  MostDeadFirstPercent   int      null,
  BestSurvivor           longtext null,
  BestSurvivorPercent    int      null,
  LastRun                datetime null,
  GroupName              longtext null
);

create table KillMethod
(
  Id   int auto_increment
    primary key,
  Name varchar(50) charset utf8 not null
);

create table NotifyGame
(
  UserId  int    not null,
  GroupId bigint not null,
  primary key (UserId, GroupId)
);

create table Player
(
  Id            int auto_increment
    primary key,
  TelegramId    int                       not null,
  Name          longtext                  not null,
  UserName      longtext                  null,
  Banned        tinyint(1)                null,
  BannedBy      longtext                  null,
  HasPM         tinyint(1)                null,
  BanReason     longtext                  null,
  ImageFile     longtext                  null,
  Language      longtext                  null,
  TempBanCount  int                       null,
  HasPM2        tinyint(1)                null,
  HasDebugPM    tinyint(1)                null,
  Achievements  bigint                    null,
  WebUserId     varchar(128) charset utf8 null,
  DonationLevel int                       null,
  Founder       tinyint(1)                null,
  CustomGifSet  longtext                  null,
  GifPurchased  tinyint(1)                null
);

create table GameKill
(
  Id           int auto_increment
    primary key,
  GameId       int      not null,
  KillerId     int      not null,
  VictimId     int      not null,
  TimeStamp    datetime not null,
  KillMethodId int      not null,
  Day          int      not null,
  constraint FK_GameKill_Game_GameId
  foreign key (GameId) references Game (Id)
    on update cascade
    on delete cascade,
  constraint FK_GameKill_Player_KillerId
  foreign key (KillerId) references Player (Id)
    on update cascade
    on delete cascade,
  constraint FK_GameKill_KillMethod_KillMethodId
  foreign key (KillMethodId) references KillMethod (Id)
    on update cascade
    on delete cascade
);

create index IX_GameId
  on GameKill (GameId);

create index IX_KillMethodId
  on GameKill (KillMethodId);

create index IX_KillerId
  on GameKill (KillerId);

create table GamePlayer
(
  Id       int auto_increment
    primary key,
  PlayerId int                      not null,
  GameId   int                      not null,
  Survived tinyint(1)               not null,
  Won      tinyint(1)               not null,
  Role     varchar(50) charset utf8 null,
  constraint FK_GamePlayer_Player_PlayerId
  foreign key (PlayerId) references Player (Id)
    on update cascade
    on delete cascade,
  constraint FK_GamePlayer_Game_GameId
  foreign key (GameId) references Game (Id)
);

create index IX_GameId
  on GamePlayer (GameId);

create index IX_PlayerId
  on GamePlayer (PlayerId);

create table GroupAdmin
(
  GroupId  int not null,
  PlayerId int not null,
  primary key (GroupId, PlayerId),
  constraint FK_GroupAdmin_Group_GroupId
  foreign key (GroupId) references `Group` (Id)
    on update cascade
    on delete cascade,
  constraint FK_GroupAdmin_Player_PlayerId
  foreign key (PlayerId) references Player (Id)
    on update cascade
    on delete cascade
);

create index IX_GroupId
  on GroupAdmin (GroupId);

create index IX_PlayerId
  on GroupAdmin (PlayerId);

create table PlayerStats
(
  Id                    int auto_increment
    primary key,
  PlayerId              int                      not null,
  GamesPlayed           int                      not null,
  GamesWon              int                      not null,
  GamesLost             int                      not null,
  MostCommonRole        varchar(50) charset utf8 not null,
  MostKilled            longtext                 null,
  MostKilledBy          longtext                 null,
  MostCommonRolePercent int                      not null,
  GamesSurvived         int                      not null,
  LastRun               datetime                 null
);

create table RefreshDate
(
  `Lock` char     not null
    primary key,
  Date   datetime not null
);

create table `__MigrationHistory`
(
  MigrationId    varchar(150) charset utf8 not null
    primary key,
  ContextKey     varchar(300) charset utf8 not null,
  Model          longblob                  not null,
  ProductVersion varchar(32) charset utf8  not null
);

create view v_BotInGroups as
  select
    count(`werewolf`.`Group`.`Id`) AS `Groups`,
    case `werewolf`.`Group`.`BotInGroup`
    when 0
      then 'No'
    when 1
      then 'Yes' end               AS `Has Bot?`
  from `werewolf`.`Group`
  where `werewolf`.`Group`.`BotInGroup` is not null
  group by `werewolf`.`Group`.`BotInGroup`;

create view v_GroupRanking as
  select
    `grp`.`Id`            AS `GroupId`,
    `grp`.`GroupId`       AS `TelegramId`,
    `grprk`.`Language`    AS `Language`,
    `grp`.`Description`   AS `Description`,
    `grp`.`GroupLink`     AS `GroupLink`,
    `grprk`.`Ranking`     AS `Ranking`,
    `grprk`.`LastRefresh` AS `LastRefresh`,
    `grp`.`Name`          AS `Name`
  from (`werewolf`.`Group` `grp`
    join `werewolf`.`GroupRanking` `grprk` on (`grp`.`Id` = `grprk`.`GroupId`))
  where (`grp`.`Preferred` = 1 or `grp`.`Preferred` is null) and `grp`.`GroupLink` is not null and
        (`grprk`.`Show` is null or `grprk`.`Show` = 1);

create view v_IdleKill24HoursMain as
  select
    count(`gk`.`GameId`) AS `Idles`,
    `p`.`Name`           AS `Name`,
    `p`.`UserName`       AS `UserName`
  from ((`werewolf`.`GameKill` `gk`
    join `werewolf`.`Game` `g` on (`gk`.`GameId` = `g`.`Id`)) join `werewolf`.`Player` `p`
      on (`p`.`Id` = `gk`.`VictimId`))
  where `g`.`GroupId` = -1001030085238 and `gk`.`KillMethodId` = 16 and
        `g`.`TimeStarted` > current_timestamp() + interval -1 day
  group by `p`.`Name`, `p`.`UserName`;

create view v_InactivePlayersMain as
  select
    `p`.`Id`         AS `Id`,
    `p`.`Name`       AS `Name`,
    `p`.`TelegramId` AS `TelegramId`,
    `x`.`last`       AS `last`
  from (`werewolf`.`Player` `p`
    join (select
            max(`g`.`TimeStarted`) AS `last`,
            `gp`.`PlayerId`        AS `PlayerId`
          from (`werewolf`.`GamePlayer` `gp`
            join `werewolf`.`Game` `g` on (`gp`.`GameId` = `g`.`Id`))
          where `g`.`GrpId` = 2882
          group by `gp`.`PlayerId`) `x` on (`p`.`Id` = `x`.`PlayerId`))
  where `x`.`last` < current_timestamp() + interval -14 day;

create view v_LanguageCounts as
  select
    count(`werewolf`.`Group`.`Id`) AS `Groups`,
    `werewolf`.`Group`.`Language`  AS `Language`
  from `werewolf`.`Group`
  group by `werewolf`.`Group`.`Language`
  order by count(`werewolf`.`Group`.`Id`) desc
  limit 100;

create view v_NonDefaultGroups as
  select
    `werewolf`.`Group`.`Name`                AS `Name`,
    `werewolf`.`Group`.`Language`            AS `Language`,
    `werewolf`.`Group`.`ShowRoles`           AS `ShowRoles`,
    `werewolf`.`Group`.`ShowRolesEnd`        AS `ShowRolesEnd`,
    `werewolf`.`Group`.`Mode`                AS `Mode`,
    `werewolf`.`Group`.`DayTime`             AS `DayTime`,
    `werewolf`.`Group`.`NightTime`           AS `NightTime`,
    `werewolf`.`Group`.`LynchTime`           AS `LynchTime`,
    `werewolf`.`Group`.`AllowFool`           AS `AllowFool`,
    `werewolf`.`Group`.`AllowTanner`         AS `AllowTanner`,
    `werewolf`.`Group`.`AllowCult`           AS `AllowCult`,
    `werewolf`.`Group`.`UserName`            AS `UserName`,
    `werewolf`.`Group`.`BotInGroup`          AS `BotInGroup`,
    `werewolf`.`Group`.`DisableNotification` AS `DisableNotification`,
    `werewolf`.`Group`.`MaxPlayers`          AS `MaxPlayers`,
    `werewolf`.`Group`.`DisableFlee`         AS `DisableFlee`,
    `werewolf`.`Group`.`Preferred`           AS `Preferred`,
    `werewolf`.`Group`.`GroupId`             AS `GroupId`
  from `werewolf`.`Group`
  where `werewolf`.`Group`.`DayTime` <> 60 or `werewolf`.`Group`.`NightTime` <> 90 or
        `werewolf`.`Group`.`LynchTime` <> 90 and `werewolf`.`Group`.`AllowTanner` <> 1 or
        `werewolf`.`Group`.`AllowFool` <> 1 or `werewolf`.`Group`.`AllowCult` <> 1 or
        `werewolf`.`Group`.`Mode` <> 'Player' or `werewolf`.`Group`.`ShowRoles` <> 1 or
        `werewolf`.`Group`.`ShowRolesEnd` <> 'Living' or `werewolf`.`Group`.`MaxPlayers` <> 35 or
        `werewolf`.`Group`.`DisableFlee` = 1;

create view v_PreferredGroups as
  select distinct
    `werewolf`.`Group`.`Id`          AS `Id`,
    `werewolf`.`Group`.`Name`        AS `Name`,
    `werewolf`.`Group`.`GroupId`     AS `GroupId`,
    `werewolf`.`Group`.`Language`    AS `Language`,
    `werewolf`.`Group`.`UserName`    AS `UserName`,
    `werewolf`.`Group`.`Description` AS `Description`,
    `werewolf`.`Group`.`GroupLink`   AS `GroupLink`
  from `werewolf`.`Group`
  where `werewolf`.`Group`.`Preferred` = 1;

create view v_PublicGroups as
  select
    `werewolf`.`Group`.`Id`                AS `Id`,
    `werewolf`.`Group`.`Name`              AS `Name`,
    case `werewolf`.`Group`.`UserName`
    when 'mainwerewolfindo'
      then 'Bahasa Indo'
    when 'werewolfgameindonesia'
      then 'Bahasa Indo'
    when 'Bobervidihay'
      then 'Russian'
    else `werewolf`.`Group`.`Language` end AS `Language`,
    `werewolf`.`Group`.`MemberCount`       AS `MemberCount`,
    `werewolf`.`Group`.`GroupLink`         AS `GroupLink`
  from `werewolf`.`Group`
  where `werewolf`.`Group`.`GroupLink` is not null and `werewolf`.`Group`.`GroupId` <> -1001055238687 and
        `werewolf`.`Group`.`BotInGroup` = 1 and `werewolf`.`Group`.`GroupId` <> -1001062468289 and
        `werewolf`.`Group`.`Preferred` = 1;

create view v_SummaryTotals as
  select
    (select count(`werewolf`.`Player`.`Id`) AS `Expr1`
     from `werewolf`.`Player`)                                    AS `Players`,
    (select count(`werewolf`.`Game`.`Id`) AS `Expr1`
     from `werewolf`.`Game`)                                      AS `Games`,
    (select count(`werewolf`.`Group`.`Id`) AS `Expr1`
     from `werewolf`.`Group`)                                     AS `Groups`,
    (select count(`werewolf`.`GamePlayer`.`Id`) AS `Expr1`
     from `werewolf`.`GamePlayer`
     where `werewolf`.`GamePlayer`.`Survived` = 0)                AS `Deaths`,
    (select count(`GamePlayer_1`.`Id`) AS `Expr1`
     from `werewolf`.`GamePlayer` `GamePlayer_1`
     where `GamePlayer_1`.`Survived` = 1)                         AS `Survivors`,
    (select min(`g`.`TimeStarted`) AS `Expr1`
     from (`werewolf`.`GamePlayer` `gp`
       join `werewolf`.`Game` `g` on (`gp`.`GameId` = `g`.`Id`))) AS `Since`;

create view v_WaitList as
  select
    `g`.`Name` AS `Name`,
    `p`.`Name` AS `Expr1`
  from ((`werewolf`.`NotifyGame` `n`
    join `werewolf`.`Group` `g` on (`n`.`GroupId` = `g`.`GroupId`)) join `werewolf`.`Player` `p`
      on (`p`.`TelegramId` = `n`.`UserId`));

create view v_WinRatios as
  select
    `x`.`Players`                                                                                      AS `Players`,
    count(`x`.`GameId`)                                                                                AS `Wins`,
    `x`.`Winner`                                                                                       AS `Team`,
    round(count(`x`.`GameId`) * 100.0 / sum(count(`x`.`GameId`)) over (partition by `x`.`Players`), 2) AS `%`
  from (select
          count(`gp`.`PlayerId`) AS `Players`,
          `gp`.`GameId`          AS `GameId`,
          case when `gm`.`Winner` = 'Wolves'
            then 'Wolf'
          else `gm`.`Winner` end AS `Winner`
        from (`werewolf`.`Game` `gm`
          join `werewolf`.`GamePlayer` `gp` on (`gp`.`GameId` = `gm`.`Id`))
        where `gm`.`Winner` is not null
        group by `gp`.`GameId`, `gm`.`Winner`
        having count(`gp`.`PlayerId`) >= 5) `x`
  group by `x`.`Winner`, `x`.`Players`;

create procedure GetIdleKills24Hours(IN p_userid int)
  BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.

    -- Insert statements for procedure here
    select count(GameId)
    from
      GameKill gk
      join Player p on p.Id = gk.VictimId
    where p.TelegramId = p_userid
          and KillMethodId = 16
          and gk.TimeStamp > TIMESTAMPADD(day, -1, NOW());

  END;

create procedure GlobalDay1Death()
  BEGIN

    select
      (day1death * 100 / games) as pct,
      p.Name,
      p.TelegramId
    from
      (select
         day1death,
         VictimId,
         count(gp.id) as games
       from GamePlayer gp
         join
         (select
            count(GameId) as day1death,
            VictimId
          from
            (select
               VictimId,
               GameId
             from GameKill gk
             where Day = 1 and KillMethodId <> 8
             group by VictimId, GameId) x
          group by VictimId) y
           on gp.PlayerId = y.VictimId
       group by VictimId, day1death
       having count(gp.id) > 99) as totals
      join Player p on p.id = totals.VictimId
    order by pct desc
    limit 1;

  END;

create procedure GlobalDay1Lynch()
  BEGIN
    select
      (day1death * 100 / games) as pct,
      p.Name,
      p.TelegramId
    from
      (select
         day1death,
         VictimId,
         count(gp.Id) as games
       from GamePlayer gp
         join
         (select
            count(GameId) as day1death,
            VictimId
          from
            (select
               VictimId,
               GameId
             from GameKill gk
             where Day = 1 and KillMethodId = 1 and KillMethodId <> 8
             group by VictimId, GameId) x
          group by VictimId) y
           on gp.PlayerId = y.VictimId
       group by VictimId, day1death
       having count(gp.Id) > 99) totals
      join Player p on p.id = totals.VictimId
    order by pct desc
    limit 1;

  END;

create procedure GlobalNight1Death()
  BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.

    -- Insert statements for procedure here
    select
      deaths * 100 / games as pct,
      p.Name,
      p.TelegramId
    from
      (select
         count(gp.id) as games,
         VictimId,
         deaths
       from GamePlayer gp
         join
         (select
            count(id) as deaths,
            VictimId
          from GameKill gk
          where Day = 1 and KillMethodId <> 1 and KillMethodId <> 8
          group by VictimId) as x
           on gp.PlayerId = x.VictimId
       group by VictimId, deaths) as y
      join Player p on p.id = y.VictimId
    where games > 99
    order by pct desc
    limit 1;

  END;

create procedure GlobalSurvivor()
  BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.

    select
      (survived * 100.0 / games) as pct,
      p.Name,
      p.TelegramId
    from
      (select
         count(Id)       as games,
         sum(case survived
             when 1
               then 1
             else 0 end) as survived,
         PlayerId
       from GamePlayer
       group by PlayerId
       having count(Id) > 99) as x
      join Player p on p.id = x.PlayerId
    order by pct desc;

  END;

create procedure GroupDay1Death(IN p_groupid bigint)
  BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.

    -- Insert statements for procedure here
    select
      (day1death * 100 / games) as pct,
      p.Name,
      p.TelegramId
    from
      (select
         day1death,
         VictimId,
         count(gp.id) as games
       from GamePlayer gp
         join
         (select
            count(gameid) as day1death,
            VictimId
          from
            (select
               VictimId,
               GameId
             from GameKill gk
             where Day = 1 and KillMethodId <> 8
             group by VictimId, GameId) as x
            join Game g on g.Id = gameid
          where g.GroupId = p_groupid
          group by VictimId) as y
           on gp.PlayerId = y.VictimId
       group by VictimId, day1death
       having count(gp.Id) > 19) as totals
      join Player p on p.Id = totals.VictimId
    order by pct desc
    limit 1;

  END;

create procedure GroupDay1Lynch(IN p_groupid bigint)
  BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.

    -- Insert statements for procedure here
    select
      (day1death * 100 / games) as pct,
      p.Name,
      p.TelegramId
    from
      (select
         day1death,
         VictimId,
         count(gp.id) as games
       from GamePlayer gp
         join
         (select
            count(gameid) as day1death,
            VictimId
          from
            (select
               VictimId,
               GameId
             from GameKill gk
             where Day = 1 and KillMethodId = 1 and KillMethodId <> 8
             group by VictimId, GameId) as x
            join Game g on g.Id = gameid
          where g.GroupId = p_groupid
          group by VictimId) as y
           on gp.PlayerId = y.VictimId
       group by VictimId, day1death
       having count(gp.Id) > 19) as totals
      join Player p on p.Id = totals.VictimId
    order by pct desc
    limit 1;

  END;

create procedure GroupNight1Death(IN p_groupid bigint)
  BEGIN
    select
      (day1death * 100 / games) as pct,
      p.Name,
      p.TelegramId
    from
      (select
         day1death,
         VictimId,
         count(gp.id) as games
       from GamePlayer gp
         join
         (select
            count(GameId) as day1death,
            VictimId
          from
            (select
               VictimId,
               GameId
             from GameKill gk
             where Day = 1 and KillMethodId <> 1 and KillMethodId <> 8
             group by VictimId, GameId) as x
            join Game g on g.Id = gameid
          where g.GroupId = p_groupid
          group by VictimId) as y
           on gp.PlayerId = y.VictimId
       group by VictimId, day1death
       having count(gp.Id) > 19) as totals
      join Player p on p.Id = totals.VictimId
    order by pct desc
    limit 1;

  END;

create procedure GroupSurvivor(IN p_groupid bigint)
  BEGIN
    select
      (survived * 100 / games) as pct,
      p.Name,
      p.TelegramId
    from
      (select
         count(gp.Id)    as games,
         sum(case survived
             when 1
               then 1
             else 0 end) as survived,
         gp.PlayerId
       from GamePlayer gp
         join Game g on gp.GameId = g.Id
       where g.GroupId = p_groupid
       group by gp.PlayerId
       having count(gp.Id) > 19) as x
      join Player p on x.PlayerId = p.Id
    order by pct desc
    limit 1;
  END;

create procedure PlayerMostKilled(IN p_pid int)
  BEGIN
    select
      p.Name,
      p.TelegramId,
      count(gk.Id) as times
    from GameKill gk
      join Player p on gk.VictimId = p.Id
      join Player p2 on gk.KillerId = p2.Id
    where p2.TelegramId = p_pid and gk.KillMethodId <> 8
    group by p.Name, p.TelegramId
    order by count(gk.Id) desc
    limit 1;
  END;

create procedure PlayerMostKilledBy(IN p_pid int)
  BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.

    -- Insert statements for procedure here
    select
      p.Name,
      p.TelegramId,
      count(gk.Id) as times
    from GameKill gk
      join Player p on gk.KillerId = p.Id
      join Player p2 on gk.VictimId = p2.Id
    where p2.TelegramId = p_pid and gk.KillMethodId <> 8
    group by p.Name, p.TelegramId
    order by count(gk.Id) desc;

  END;

create procedure PlayerRoles(IN p_pid int)
  BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.

    -- Insert statements for procedure here
    select
      count(gp.Id) as times,
      role
    from GamePlayer gp
      join Player p on p.Id = gp.PlayerId
    where p.TelegramId = p_pid
    group by role
    order by count(gp.id) desc;

  END;

create procedure getDailyCounts()
  BEGIN

    SELECT
      counts.Day,
      counts.Games,
      counts.Groups,
      playercount.Players
    FROM (SELECT
            SUM(games)     AS Games,
            Day,
            COUNT(GroupId) AS `Groups`
          FROM (SELECT
                  COUNT(Id) AS games,
                  Day,
                  GroupId
                FROM (SELECT
                        Id,
                        GroupId,
                        CONVERT(TimeStarted, date) AS Day
                      FROM `Game`
                      where TimeStarted >= TIMESTAMPADD(DAY, -32, NOW())) AS x
                GROUP BY Day, GroupId) AS y
          GROUP BY Day) AS counts INNER JOIN
      (SELECT
         COUNT(PlayerId) AS players,
         Day
       FROM (SELECT DISTINCT
               gp.PlayerId,
               CONVERT(g.TimeStarted, date) AS Day
             FROM Game AS g INNER JOIN
               GamePlayer AS gp ON g.Id = gp.GameId
             where timestarted >= TIMESTAMPADD(DAY, -32, NOW())) AS x_1
       GROUP BY Day) as playercount ON counts.Day = playercount.Day
    order by counts.Day DESC;
  END;

create procedure getPlayTime(IN p_playerCount int)
  BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.

    -- Insert statements for procedure here

    select
      AVG(Time) as Average,
      Min(Time) as Minimum,
      Max(Time) as Maximum
    from
      (select
         count(gp.id)                                  as Players,
         g.Id,
         TIMESTAMPDIFF(minute, TimeStarted, TimeEnded) as Time
       from
         Game g
         join GamePlayer gp on g.Id = gp.GameId
       group by g.Id, TimeStarted, TimeEnded
       having count(gp.id) = p_playerCount) x;

  END;

create procedure getRoles(IN p_groupName text)
  BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.

    select
      name,
      role
    from GamePlayer gp
      join Player p on gp.PlayerId = p.Id
    where gameid = (select max(id)
                    from Game
                    where GroupName = p_groupName);

  END;

