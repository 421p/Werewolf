USE `werewolf`;

/****** Object:  Table [Group]    Script Date: 5/27/2018 10:01:48 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE TABLE `Group` (
  `Id`                  int PRIMARY KEY AUTO_INCREMENT,
  `Name`                text        NOT NULL,
  GroupId               bigint      NOT NULL,
  `Preferred`           Tinyint     NULL,
  `Language`            text        NULL,
  `DisableNotification` Tinyint     NULL,
  `UserName`            text        NULL,
  `BotInGroup`          Tinyint     NULL,
  `ShowRoles`           Tinyint     NULL,
  `Mode`                varchar(50) NULL,
  `DayTime`             int         NULL,
  `NightTime`           int         NULL,
  `LynchTime`           int         NULL,
  `AllowTanner`         Tinyint     NULL,
  `AllowFool`           Tinyint     NULL,
  `AllowCult`           Tinyint     NULL,
  `ShowRolesEnd`        varchar(50) NULL,
  `MaxPlayers`          int         NULL,
  `DisableFlee`         Tinyint     NULL,
  `CreatedBy`           text        NULL,
  `ImageFile`           text        NULL,
  `Description`         text        NULL,
  `GroupLink`           text        NULL,
  `MemberCount`         int         NULL,
  `AllowExtend`         Tinyint     NULL,
  `MaxExtend`           int         NULL,
  `EnableSecretLynch`   Tinyint     NULL,
  `RandomModes`         Tinyint     NULL,
  `Flags`               bigint      NULL
);
/****** Object:  View [db_owner].[v_BotInGroups]    Script Date: 5/27/2018 10:01:49 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE VIEW `v_BotInGroups`
  AS
    SELECT
      COUNT(Id)        AS Groups,
      CASE BotInGroup
      WHEN 0
        THEN 'No'
      WHEN 1
        THEN 'Yes' END AS 'Has Bot?'
    FROM `Group`
    WHERE (BotInGroup IS NOT NULL)
    GROUP BY BotInGroup;

/****** Object:  Table [Game]    Script Date: 5/27/2018 10:01:49 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE TABLE Game (
  `Id`          int AUTO_INCREMENT NOT NULL,
  `GroupName`   text               NOT NULL,
  `GroupId`     bigint             NOT NULL,
  `TimeStarted` datetime(3)        NULL,
  `TimeEnded`   datetime(3)        NULL,
  `Winner`      varchar(50)        NULL,
  `GrpId`       int                NULL,
  `Mode`        varchar(50)        NULL,
  CONSTRAINT `PK_Game` PRIMARY KEY
    (
      `Id` ASC
    )
);
/****** Object:  Table [GamePlayer]    Script Date: 5/27/2018 10:01:49 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE TABLE GamePlayer (
  `Id`       int AUTO_INCREMENT NOT NULL,
  `PlayerId` int                NOT NULL,
  `GameId`   int                NOT NULL,
  `Survived` Tinyint            NOT NULL,
  `Won`      Tinyint            NOT NULL,
  `Role`     varchar(50)        NULL,
  CONSTRAINT `PK_GamePlayer` PRIMARY KEY
    (
      `Id` ASC
    )
);
/****** Object:  Table [Player]    Script Date: 5/27/2018 10:01:49 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE TABLE Player (
  `Id`            int AUTO_INCREMENT NOT NULL,
  `TelegramId`    int                NOT NULL,
  `Name`          text               NOT NULL,
  `UserName`      text               NULL,
  `Banned`        Tinyint            NULL,
  `BannedBy`      text               NULL,
  `HasPM`         Tinyint            NULL,
  `BanReason`     text               NULL,
  `ImageFile`     text               NULL,
  `Language`      text               NULL,
  `TempBanCount`  int                NULL,
  `HasPM2`        Tinyint            NULL,
  `HasDebugPM`    Tinyint            NULL,
  `Achievements`  bigint             NULL,
  `WebUserId`     varchar(128)       NULL,
  `DonationLevel` int                NULL,
  `Founder`       Tinyint            NULL,
  `CustomGifSet`  text               NULL,
  `GifPurchased`  Tinyint            NULL,
  CONSTRAINT `PK_Player` PRIMARY KEY
    (
      `Id` ASC
    )
);
/****** Object:  View [db_owner].[v_InactivePlayersMain]    Script Date: 5/27/2018 10:01:49 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE VIEW `v_InactivePlayersMain`
  AS
    SELECT
      p.Id,
      p.Name,
      p.TelegramId,
      x.last
    FROM Player AS p INNER JOIN
      (SELECT
         MAX(g.TimeStarted) AS last,
         gp.PlayerId
       FROM GamePlayer AS gp INNER JOIN
         Game AS g ON gp.GameId = g.Id
       WHERE (g.GrpId = 2882)
       GROUP BY gp.PlayerId) AS x ON p.Id = x.PlayerId
    WHERE (x.last < TIMESTAMPADD(day, -14, NOW()));

/****** Object:  View [db_owner].[v_LanguageCounts]    Script Date: 5/27/2018 10:01:49 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE VIEW `v_LanguageCounts`
  AS
    SELECT
      COUNT(Id) AS Groups,
      Language
    FROM `Group`
    GROUP BY Language
    ORDER BY Groups DESC
    limit 100;
/****** Object:  View [db_owner].[v_NonDefaultGroups]    Script Date: 5/27/2018 10:01:49 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE VIEW `v_NonDefaultGroups`
  AS
    SELECT
      Name,
      Language,
      ShowRoles,
      ShowRolesEnd,
      Mode,
      DayTime,
      NightTime,
      LynchTime,
      AllowFool,
      AllowTanner,
      AllowCult,
      UserName,
      BotInGroup,
      DisableNotification,
      MaxPlayers,
      DisableFlee,
      Preferred,
      GroupId
    FROM `Group`
    WHERE (DayTime <> 60) OR
          (NightTime <> 90) OR
          (LynchTime <> 90) AND (AllowTanner <> 1) OR
          (AllowFool <> 1) OR
          (AllowCult <> 1) OR
          (Mode <> 'Player') OR
          (ShowRoles <> 1) OR
          (ShowRolesEnd <> 'Living') OR
          (MaxPlayers <> 35) OR
          (DisableFlee = 1);

/****** Object:  View [db_owner].[v_PreferredGroups]    Script Date: 5/27/2018 10:01:49 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE VIEW `v_PreferredGroups`
  AS
    SELECT DISTINCT
      Id,
      Name,
      GroupId,
      Language,
      UserName,
      Description,
      GroupLink
    FROM `Group`
    WHERE (Preferred = 1);

/****** Object:  View [db_owner].[v_PublicGroups]    Script Date: 5/27/2018 10:01:50 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE VIEW `v_PublicGroups`
  AS
    SELECT
      Id,
      Name,
      (CASE username
       WHEN 'mainwerewolfindo'
         THEN 'Bahasa Indo'
       WHEN 'werewolfgameindonesia'
         THEN 'Bahasa Indo'
       WHEN 'Bobervidihay'
         THEN 'Russian'
       ELSE Language END) AS Language,
      MemberCount,
      GroupLink
    FROM `Group`
    WHERE
      (GroupLink IS NOT NULL) AND (GroupId <> -1001055238687) AND (BotInGroup = 1) AND (GroupId <> -1001062468289) AND
      (Preferred = 1);

/****** Object:  View [db_owner].[v_SummaryTotals]    Script Date: 5/27/2018 10:01:50 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE VIEW `v_SummaryTotals`
  AS
    SELECT
      (SELECT COUNT(Id) AS Expr1
       FROM Player)                     AS Players,
      (SELECT COUNT(Id) AS Expr1
       FROM Game)                       AS Games,
      (SELECT COUNT(Id) AS Expr1
       FROM `Group`)                    AS Groups,
      (SELECT COUNT(Id) AS Expr1
       FROM GamePlayer
       WHERE (Survived = 0))            AS Deaths,
      (SELECT COUNT(Id) AS Expr1
       FROM GamePlayer AS GamePlayer_1
       WHERE (Survived = 1))            AS Survivors,
      (SELECT MIN(g.TimeStarted) AS Expr1
       FROM GamePlayer AS gp INNER JOIN
         Game AS g ON gp.GameId = g.Id) AS Since;
/****** Object:  Table [NotifyGame]    Script Date: 5/27/2018 10:01:50 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE TABLE NotifyGame (
  `UserId`  int    NOT NULL,
  `GroupId` bigint NOT NULL
);
/****** Object:  View [db_owner].[v_WaitList]    Script Date: 5/27/2018 10:01:50 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE VIEW `v_WaitList`
  AS
    SELECT
      g.Name,
      p.Name AS Expr1
    FROM NotifyGame AS n INNER JOIN
      `Group` AS g ON n.GroupId = g.GroupId
      INNER JOIN
      Player AS p ON p.TelegramId = n.UserId;

/****** Object:  View [db_owner].[v_WinRatios]    Script Date: 5/27/2018 10:01:50 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE VIEW `v_WinRatios`
  AS
    SELECT
      x.Players,
      Count(x.GameId)                                                                        AS Wins,
      x.Winner                                                                               AS Team,
      Round((COUNT(x.GameId) * 100.0 / sum(count(x.GameId)) OVER (PARTITION BY Players)), 2) AS `%`
    FROM (SELECT
            count(gp.PlayerId) AS Players,
            gp.GameId,
            CASE WHEN gm.Winner = 'Wolves'
              THEN 'Wolf'
            ELSE gm.Winner END AS Winner
          FROM Game AS gm INNER JOIN
            GamePlayer AS gp ON gp.GameId = gm.Id
          WHERE gm.Winner IS NOT NULL
          GROUP BY gp.GameId, gm.Winner
          HAVING COUNT(gp.PlayerId) >= 5) AS x
    GROUP BY x.Winner, x.Players;
/****** Object:  Table [GameKill]    Script Date: 5/27/2018 10:01:50 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE TABLE GameKill (
  `Id`           int AUTO_INCREMENT NOT NULL,
  `GameId`       int                NOT NULL,
  `KillerId`     int                NOT NULL,
  `VictimId`     int                NOT NULL,
  `TimeStamp`    datetime(3)        NOT NULL,
  `KillMethodId` int                NOT NULL,
  `Day`          int                NOT NULL,
  CONSTRAINT `PK_GameKill` PRIMARY KEY
    (
      `Id` ASC
    )
);
/****** Object:  View [v_IdleKill24HoursMain]    Script Date: 5/27/2018 10:01:50 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE VIEW `v_IdleKill24HoursMain`
  AS
    select
      count(gameid) as Idles,
      p.Name,
      p.UserName
    from
      GameKill gk
      join Game g on gk.GameId = g.Id
      join Player p on p.Id = gk.VictimId
    where g.GroupId = -1001030085238
          and KillMethodId = 16
          and TimeStarted > TIMESTAMPADD(day, -1, NOW())
    group by p.Name, p.UserName;


/****** Object:  Table [GroupRanking]    Script Date: 5/27/2018 10:01:50 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE TABLE GroupRanking (
  `GroupId`       int                NOT NULL,
  `Language`      varchar(450)       NOT NULL,
  `PlayersCount`  int                NOT NULL,
  `MinutesPlayed` decimal(18, 10)    NOT NULL,
  `LastRefresh`   datetime(3)        NOT NULL,
  `Ranking`       decimal(18, 10)    NULL,
  `Id`            int AUTO_INCREMENT NOT NULL,
  `GamesPlayed`   int                NOT NULL,
  `Show`          Tinyint            NULL,
  CONSTRAINT `PK_groupranking` PRIMARY KEY
    (
      `Id` ASC
    )
);
/****** Object:  View [v_GroupRanking]    Script Date: 5/27/2018 10:01:50 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE view `v_GroupRanking` as
  select
    grp.id          as GroupId,
    grp.groupid     as TelegramId,
    grprk.language  as Language,
    grp.description as Description,
    grp.grouplink   as GroupLink,
    grprk.ranking   as Ranking,
    grprk.LastRefresh,
    grp.Name

  from `Group` grp
    join GroupRanking grprk on grp.id = grprk.groupid
  where
    (grp.preferred = 1 or grp.preferred is null) and not (grouplink is null) and (grprk.show is null or grprk.show = 1);
/****** Object:  Table [db_owner].[BotStatus]    Script Date: 5/27/2018 10:01:50 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE TABLE BotStatus (
  `Id`        int AUTO_INCREMENT NOT NULL,
  `BotName`   varchar(50)        NOT NULL,
  `BotStatus` varchar(50)        NOT NULL,
  `BotLink`   text               NOT NULL,
  CONSTRAINT `PK_BotStatus` PRIMARY KEY
    (
      `Id` ASC
    )
);
/****** Object:  Table [db_owner].[ContestTerms]    Script Date: 5/27/2018 10:01:50 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE TABLE ContestTerms (
  `TelegramId`  int     NOT NULL,
  `AgreedTerms` Tinyint NOT NULL,
  CONSTRAINT `PK_ContestTerms` PRIMARY KEY
    (
      `TelegramId` ASC
    )
);
/****** Object:  Table [db_owner].[GlobalBan]    Script Date: 5/27/2018 10:01:50 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE TABLE GlobalBan (
  `Id`         int AUTO_INCREMENT NOT NULL,
  `TelegramId` int                NOT NULL,
  `Reason`     text               NOT NULL,
  `Expires`    datetime(3)        NOT NULL,
  `BannedBy`   text               NOT NULL,
  `BanDate`    datetime(3)        NULL,
  `Name`       text               NULL,
  CONSTRAINT `PK_GlobalBan` PRIMARY KEY
    (
      `Id` ASC
    )
);
/****** Object:  Table [__MigrationHistory]    Script Date: 5/27/2018 10:01:50 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE TABLE __MigrationHistory (
  `MigrationId`    varchar(150) NOT NULL,
  `ContextKey`     varchar(300) NOT NULL,
  `Model`          Longblob     NOT NULL,
  `ProductVersion` varchar(32)  NOT NULL,
  CONSTRAINT `PK___MigrationHistory` PRIMARY KEY
    (
      `MigrationId` ASC,
      `ContextKey` ASC
    )
);
/****** Object:  Table [Admin]    Script Date: 5/27/2018 10:01:50 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE TABLE Admin (
  `UserId` int NOT NULL,
  CONSTRAINT `PK_Admin` PRIMARY KEY
    (
      `UserId` ASC
    )
);

CREATE TABLE DailyCount (
  `Id`     int AUTO_INCREMENT NOT NULL,
  `Day`    date               NOT NULL,
  `Groups` int                NOT NULL,
  `Games`  int                NOT NULL,
  `Users`  int                NOT NULL,
  CONSTRAINT `PK_DailyCount` PRIMARY KEY
    (
      `Id` ASC
    )
);
/****** Object:  Table [GlobalStats]    Script Date: 5/27/2018 10:01:51 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE TABLE GlobalStats (
  `Id`                        int AUTO_INCREMENT NOT NULL,
  `GamesPlayed`               int                NOT NULL,
  `PlayersKilled`             int                NOT NULL,
  `PlayersSurvived`           int                NOT NULL,
  `MostKilledFirstNight`      text               NULL,
  `MostKilledFirstPercent`    int                NULL,
  `MostLynchedFirstDay`       text               NULL,
  `MostLynchedFirstPercent`   int                NULL,
  `MostKilledFirstDay`        text               NULL,
  `MostKilledFirstDayPercent` int                NULL,
  `BestSurvivor`              text               NULL,
  `BestSurvivorPercent`       int                NULL,
  `LastRun`                   datetime(3)        NULL,
  `TotalPlayers`              int                NULL,
  `TotalGroups`               int                NULL,
  `MostKilledFirstNightId`    int                NULL,
  `MostLynchedFirstDayId`     int                NULL,
  `MostKilledFirstDayId`      int                NULL,
  `BestSurvivorId`            int                NULL,
  PRIMARY KEY
    (
      `Id` ASC
    )
);
/****** Object:  Table [GroupAdmin]    Script Date: 5/27/2018 10:01:51 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE TABLE GroupAdmin (
  `GroupId`  int NOT NULL,
  `PlayerId` int NOT NULL
);
/****** Object:  Table [GroupStats]    Script Date: 5/27/2018 10:01:51 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE TABLE GroupStats (
  `Id`                     int AUTO_INCREMENT NOT NULL,
  `GroupId`                bigint             NOT NULL,
  `GamesPlayed`            int                NOT NULL,
  `MostKilledFirstNight`   text               NULL,
  `MostKilledFirstPercent` int                NULL,
  `MostLynchedFirstNight`  text               NULL,
  `MostLynchFirstPercent`  int                NULL,
  `MostDeadFirstDay`       text               NULL,
  `MostDeadFirstPercent`   int                NULL,
  `BestSurvivor`           text               NULL,
  `BestSurvivorPercent`    int                NULL,
  `LastRun`                datetime(3)        NULL,
  `GroupName`              text               NULL,
  PRIMARY KEY
    (
      `Id` ASC
    )
);
/****** Object:  Table [KillMethod]    Script Date: 5/27/2018 10:01:51 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE TABLE KillMethod (
  `Id`   int AUTO_INCREMENT NOT NULL,
  `Name` varchar(50)        NOT NULL,
  CONSTRAINT `PK_KillMethod` PRIMARY KEY
    (
      `Id` ASC
    )
);
/****** Object:  Table [PlayerStats]    Script Date: 5/27/2018 10:01:51 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE TABLE PlayerStats (
  `Id`                    int AUTO_INCREMENT NOT NULL,
  `PlayerId`              int                NOT NULL,
  `GamesPlayed`           int                NOT NULL,
  `GamesWon`              int                NOT NULL,
  `GamesLost`             int                NOT NULL,
  `MostCommonRole`        varchar(50)        NOT NULL,
  `MostKilled`            text               NULL,
  `MostKilledBy`          text               NULL,
  `MostCommonRolePercent` int                NOT NULL,
  `GamesSurvived`         int                NOT NULL,
  `LastRun`               datetime(3)        NULL,
  PRIMARY KEY
    (
      `Id` ASC
    )
);
/****** Object:  Table [RefreshDate]    Script Date: 5/27/2018 10:01:51 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

CREATE TABLE RefreshDate (
  `Lock` char(1)     NOT NULL,
  `Date` datetime(3) NOT NULL,
  CONSTRAINT `PK_RefreshDate` PRIMARY KEY
    (
      `Lock` ASC
    )
);

ALTER TABLE `GroupRanking`
  ALTER PlayersCount set default 0;
ALTER TABLE `GroupRanking`
  alter MinutesPlayed set default 0;
ALTER TABLE `GroupRanking`
  alter GamesPlayed set default 0;
ALTER TABLE `Player`
  alter Language set default 'English';
ALTER TABLE `Player`
  alter DonationLevel set default 0;
ALTER TABLE `Player`
  alter `GifPurchased` set default 0;
ALTER TABLE `RefreshDate`
  alter `Lock` set default 0;

ALTER TABLE `Game`
  ADD CONSTRAINT `FK_Game_Group` FOREIGN KEY (`GrpId`)
REFERENCES `Group` (`Id`)
  ON DELETE CASCADE;

ALTER TABLE `GameKill`
  ADD CONSTRAINT `FK_GameKill_Game` FOREIGN KEY (`GameId`)
REFERENCES `Game` (`Id`)
  ON DELETE CASCADE;

ALTER TABLE `GameKill`
  ADD CONSTRAINT `FK_GameKill_Killer` FOREIGN KEY (`KillerId`)
REFERENCES `Player` (`Id`)
  ON DELETE CASCADE;


ALTER TABLE `GameKill`
  ADD CONSTRAINT `FK_GameKill_KillMethod` FOREIGN KEY (`KillMethodId`)
REFERENCES `KillMethod` (`Id`)
  ON DELETE CASCADE;

ALTER TABLE `GamePlayer`
  ADD CONSTRAINT `FK_GamePlayer_Game` FOREIGN KEY (`GameId`)
REFERENCES `Game` (`Id`);

ALTER TABLE `GamePlayer`
  ADD CONSTRAINT `FK_GamePlayer_Player` FOREIGN KEY (`PlayerId`)
REFERENCES `Player` (`Id`)
  ON DELETE CASCADE;

ALTER TABLE `GroupAdmin`
  ADD CONSTRAINT `FK_GroupAdmin_Group` FOREIGN KEY (`GroupId`)
REFERENCES `Group` (`Id`)
  ON DELETE CASCADE;

ALTER TABLE `GroupAdmin`
  ADD CONSTRAINT `FK_GroupAdmin_Player` FOREIGN KEY (`PlayerId`)
REFERENCES `Player` (`Id`)
  ON DELETE CASCADE;

ALTER TABLE `RefreshDate`
  ADD CONSTRAINT `CK_RefreshDate_Locked` CHECK ((`Lock` = 'X'));

/****** Object:  StoredProcedure [getDailyCounts]    Script Date: 5/27/2018 10:01:51 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
DELIMITER //

CREATE PROCEDURE getDailyCounts()

  BEGIN

    SELECT
      counts.Day,
      counts.Games,
      counts.Groups,
      playercount.Players
    FROM (SELECT
            SUM(games)     AS Games,
            Day,
            COUNT(GroupId) AS Groups
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

//

DELIMITER ;


/****** Object:  StoredProcedure [GetIdleKills24Hours]    Script Date: 5/27/2018 10:01:51 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
DELIMITER //

CREATE PROCEDURE GetIdleKills24Hours(
  -- Add the parameters for the stored procedure here
  p_userid int)
  BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.

    -- Insert statements for procedure here
    select count(gameid)
    from
      gamekill gk
      join player p on p.Id = gk.VictimId
    where p.TelegramId = p_userid
          and KillMethodId = 16
          and gk.TimeStamp > TIMESTAMPADD(day, -1, NOW());

  END;

//

DELIMITER ;


/****** Object:  StoredProcedure [getPlayTime]    Script Date: 5/27/2018 10:01:51 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
DELIMITER //

CREATE PROCEDURE getPlayTime(
  p_playerCount int)
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

//

DELIMITER ;


/****** Object:  StoredProcedure [getRoles]    Script Date: 5/27/2018 10:01:51 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
DELIMITER //

CREATE PROCEDURE getRoles(
  p_groupName text)
  BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.

    select
      name,
      role
    from GamePlayer gp
      join player p on gp.PlayerId = p.Id
    where gameid = (select max(id)
                    from game
                    where GroupName = p_groupName);

  END;

//

DELIMITER ;


/****** Object:  StoredProcedure [GlobalDay1Death]    Script Date: 5/27/2018 10:01:51 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
DELIMITER //

CREATE PROCEDURE GlobalDay1Death()

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

//

DELIMITER ;


/****** Object:  StoredProcedure [GlobalDay1Lynch]    Script Date: 5/27/2018 10:01:51 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
DELIMITER //

CREATE PROCEDURE GlobalDay1Lynch()

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

//

DELIMITER ;


/****** Object:  StoredProcedure [GlobalNight1Death]    Script Date: 5/27/2018 10:01:51 AM ******/
/* SET ANSI_NULLS ON */

/* SET QUOTED_IDENTIFIER ON */

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
DELIMITER //

CREATE PROCEDURE GlobalNight1Death()

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

    //

    DELIMITER ;


    /****** Object:  StoredProcedure [GlobalSurvivor]    Script Date: 5/27/2018 10:01:51 AM ******/
    /* SET ANSI_NULLS ON */

    /* SET QUOTED_IDENTIFIER ON */

    -- =============================================
    -- Author:		<Author,,Name>
    -- Create date: <Create Date,,>
    -- Description:	<Description,,>
    -- =============================================
    DELIMITER //

    CREATE PROCEDURE GlobalSurvivor()
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

    //

    DELIMITER ;


    /****** Object:  StoredProcedure [GroupDay1Death]    Script Date: 5/27/2018 10:01:51 AM ******/
    /* SET ANSI_NULLS ON */

    /* SET QUOTED_IDENTIFIER ON */

    -- =============================================
    -- Author:		<Author,,Name>
    -- Create date: <Create Date,,>
    -- Description:	<Description,,>
    -- =============================================
    DELIMITER //

    CREATE PROCEDURE GroupDay1Death(
      -- Add the parameters for the stored procedure here
      p_groupid bigint)
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
        order by pct desc limit 1;

      END;

    //

    DELIMITER ;


    /****** Object:  StoredProcedure [GroupDay1Lynch]    Script Date: 5/27/2018 10:01:51 AM ******/
    /* SET ANSI_NULLS ON */

    /* SET QUOTED_IDENTIFIER ON */

    -- =============================================
    -- Author:		<Author,,Name>
    -- Create date: <Create Date,,>
    -- Description:	<Description,,>
    -- =============================================
    DELIMITER //

    CREATE PROCEDURE GroupDay1Lynch(
      -- Add the parameters for the stored procedure here
      p_groupid bigint)
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
        having count (gp.Id) > 19) as totals
        join Player p on p.Id = totals.VictimId
        order by pct desc limit 1;

      END;

    //

    DELIMITER ;


    /****** Object:  StoredProcedure [GroupNight1Death]    Script Date: 5/27/2018 10:01:51 AM ******/
    /* SET ANSI_NULLS ON */

    /* SET QUOTED_IDENTIFIER ON */

    -- =============================================
    -- Author:		<Author,,Name>
    -- Create date: <Create Date,,>
    -- Description:	<Description,,>
    -- =============================================
    DELIMITER //

    CREATE PROCEDURE GroupNight1Death(p_groupid bigint)

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
        order by pct desc limit 1;

      END;

    //

    DELIMITER ;


    /****** Object:  StoredProcedure [GroupSurvivor]    Script Date: 5/27/2018 10:01:51 AM ******/
    /* SET ANSI_NULLS ON */

    /* SET QUOTED_IDENTIFIER ON */

    -- =============================================
    -- Author:		<Author,,Name>
    -- Create date: <Create Date,,>
    -- Description:	<Description,,>
    -- =============================================
    DELIMITER //

    CREATE PROCEDURE GroupSurvivor (p_groupid bigint )
  BEGIN
    select (survived * 100 / games) as pct, p.Name, p.TelegramId from
    ( select count (gp.Id) as games, sum( case survived when 1 then 1 else 0 end ) as survived, gp.PlayerId from GamePlayer gp
    join Game g on gp.GameId = g.Id
    where g.GroupId = p_groupid
    group by gp.PlayerId
    having count (gp.Id) > 19) as x
    join Player p on x.PlayerId = p.Id
    order by pct desc limit 1;
  END;

    //

    DELIMITER ;


    /****** Object:  StoredProcedure [PlayerMostKilled]    Script Date: 5/27/2018 10:01:51 AM ******/
    /* SET ANSI_NULLS ON */

    /* SET QUOTED_IDENTIFIER ON */

    -- =============================================
    -- Author:		<Author,,Name>
    -- Create date: <Create Date,,>
    -- Description:	<Description,,>
    -- =============================================
    DELIMITER //

    CREATE PROCEDURE PlayerMostKilled (p_pid int )
  BEGIN
    select p.Name, p.TelegramId, count (gk.Id) as times from GameKill gk
    join Player p on gk.VictimId = p.Id
    join Player p2 on gk.KillerId = p2.Id
    where p2.TelegramId = p_pid and gk.KillMethodId <> 8
    group by p.Name, p.TelegramId
    order by count (gk.Id) desc limit 1;
  END;

    //

    DELIMITER ;


    /****** Object:  StoredProcedure [PlayerMostKilledBy]    Script Date: 5/27/2018 10:01:51 AM ******/
    /* SET ANSI_NULLS ON */

    /* SET QUOTED_IDENTIFIER ON */

    -- =============================================
    -- Author:		<Author,,Name>
    -- Create date: <Create Date,,>
    -- Description:	<Description,,>
    -- =============================================
    DELIMITER //

    CREATE PROCEDURE PlayerMostKilledBy(
      -- Add the parameters for the stored procedure here
      p_pid int)
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

    //

    CREATE PROCEDURE PlayerRoles(
      -- Add the parameters for the stored procedure here
      p_pid int)
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

    //