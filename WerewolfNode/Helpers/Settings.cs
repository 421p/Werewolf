using System.Collections.Generic;

namespace Werewolf_Node
{
    internal static class Settings
    {
        public static string ServerIP = "127.0.0.1";
        public static int Port = 9050;

        internal static readonly List<string> VillagerDieImages = new List<string> {"CgADBAADZaIAAssaZAfzhuNOpDt-vwI"};
        internal static readonly List<string> WolfWin = new List<string> {"CgADBAAD154AAogbZAfp5Hft43Lk7QI"};
        internal static readonly List<string> WolvesWin = new List<string> {"CgADBAADcZ8AAtAbZAefTVxIGtPZRAI"};
        internal static readonly List<string> VillagersWin = new List<string> {"CgADBAADQp0AAnsdZAdYOLXiOPhbxQI"};
        internal static readonly List<string> NoWinner = new List<string>();
        internal static readonly List<string> StartGame = new List<string> {"CgADBAADIKAAAm8cZAedB1JRO61k9wI"};
        internal static readonly List<string> StartChaosGame = new List<string> {"CgADBAADIKAAAm8cZAedB1JRO61k9wI"};
        internal static readonly List<string> TannerWin = new List<string> {"CgADBAADdLYAAlEaZAdMSg1kY38i7AI"};
        internal static readonly List<string> CultWins = new List<string> {"CgADBAADYdYAAqcZZAexv1WYVAk45gI"};
        internal static readonly List<string> SerialKillerWins = new List<string> {"CgADAgADswMAAg_GSEg5ccSG_o-YXQI"};
        internal static readonly List<string> LoversWin = new List<string> {"CgADBAADA-QAAl0cZAcY8AGMr7Ba_QI"};

        public static int

            MinPlayers = 4,

            MaxPlayers = 35,
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
            PlayerCountDoppelGanger = 22,
            PlayerCountCupid = 23,
            PlayerCountHunter = 24,
            PlayerCountSerialKiller = 25,
            PlayerCountSecondCultist = 26,
            MaxGames = 80,
            TannerChance = 40,
            FoolChance = 20,
            BeholderChance = 50,
            SeerConversionChance = 40,
            GuardianAngelConversionChance = 60,
            DetectiveConversionChance = 70,
            CursedConversionChance = 60,
            HarlotConversionChance = 70,
            HarlotDiscoverCultChance = 50,
            ChanceDetectiveCaught = 40,
            HunterConversionChance = 50,
            HunterKillCultChance = 50,
            HunterKillWolfChanceBase = 30,
            SerialKillerConversionChance = 20,
            AlphaWolfConversionChance = 20,
            SorcererConversionChance = 40,
            BlacksmithConversionChance = 75,
            OracleConversionChance = 50,
            SandmanConversionChance = 60,
            
            GameJoinTime = 180,
            MaxJoinTime = 300;


        public static long MainChatId = -1001030085238;
        public static long VeteranChatId = -1001094614730;
        public static string VeteranChatLink = "werewolfvets";

        public static bool RandomLynch = false;
    }
}