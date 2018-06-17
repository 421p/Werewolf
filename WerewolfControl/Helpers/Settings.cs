using System;
using System.Collections.Generic;

namespace Werewolf_Control.Helpers
{
    internal static class Settings
    {

        public static int Port = 9050; //9050-@werewolfbot 
        public static int AdminPort = 9060;

        public static string TcpSecret => Environment.MachineName.GetHashCode().ToString();
        public static readonly long PersianSupportChatId = -1001059174638;
        public static readonly long MainChatId = -1001049529775; //Beta group
        public static readonly long SupportChatId = -1001060486754; //@werewolfsupport
        public static readonly long PrimaryChatId = -1001030085238; //@werewolfgame
        public static readonly string DevChannelId = "@werewolfdev"; //@werewolfdev
        public static readonly long VeteranChatId = -1001094614730;
        public static readonly string VeteranChatLink = "werewolfvets";
        public static readonly string VeteranChatUrl = $"https://t.me/{VeteranChatLink}";
        public static readonly long TranslationChatId = -1001074012132;
        public static readonly long AdminChatId = -1001094155678;
        public static readonly long ErrorGroup = 268253251; // @athened

           /// <summary>
           ///     How many games are allowed for any given node
           /// </summary>
           public static int MaxGamesPerNode = 60;

           /// <summary>
           ///     How many games on each node before starting a new node (to be added later)
           /// </summary>
          
        public static int NewNodeThreshhold = 30;
        public static int ShutDownNodesAt = 15;

        public static int MaxPlayers = 35,
            
            TimeDay = 60,
            TimeNight = 90,
            TimeLynch = 90,

            PlayerCountSeerCursed = 6,
            PlayerCountHarlot = 7,
            PlayerCountBeholderChance = 8,
            PlayerCountSecondWolf = 9,
            PlayerCountGunner = 9,
            PlayerCountTraitor = 10,
            PlayerCountGuardianAngel = 11,
            PlayerCountDetective = 12,
            PlayerCountApprenticeSeer = 13,
            PlayerCountCultist = 15,
            PlayerCountThirdWolf = 16,
            PlayerCountWildChild = 17,
            PlayerCountFoolChance = 18,
            PlayerCountMasons = 21,
            PlayerCountSecondCultist = 22,
            MaxGames = 80,
            TannerChance = 40,
            FoolChance = 20,
            BeholderChance = 50,
            SeerConversionChance = 40,
            GuardianAngelConversionChance = 60,
            DetectiveConversionChance = 70,
            CursedConversionChance = 60,
            HarlotConversionChance = 70,
            SorcererConversionChance = 40,
            BlacksmithConversionChance = 75,
            HarlotDiscoverCultChance = 50,
            ChanceDetectiveCaught = 40,

            GameJoinTime = 180,
            MaxExtend = 180;
    }
}