using System.Collections.Generic;

namespace Werewolf_Node
{
    static class Settings
    {
#if DEBUG
        public static string ServerIP = "127.0.0.1";
#else
        public static string ServerIP = "127.0.0.1";

#endif
#if DEBUG
        public static int Port = 9049;
#elif RELEASE
        public static int Port = 9050;
#elif RELEASE2
        public static int Port = 9051;
#elif BETA
        public static int Port = 9052;
#endif


#if RELEASE2
        internal static List<string> VillagerDieImages =
 new List<string> { "BQADAwAD2QEAAnQXsQeU2FMN-2D3GgI", "BQADAwADggADdBexB1_X0udQaRs7Ag", "BQADBAADWAMAAt4cZAcXTtE-UCQXxAI" }; //1
        internal static List<string> WolfWin =
 new List<string> { "BQADAwADgQADdBexB5kx2CsSNDp2Ag", "BQADAwADgAADdBexBx7XahnW5XBsAg" };
        internal static List<string> WolvesWin =
 new List<string> { "BQADBAADlwMAAtgaZAeaog1gGP_fkwI", "BQADBAADcAMAAn8ZZAfjUfLaMGoxzgI" };
        internal static List<string> VillagersWin = new List<string> { "BQADAwADgwADdBexB90OD5PHXLAuAg" };
        internal static List<string> NoWinner =
 new List<string> { "BQADBAAD8QgAAqIeZAeLBjBE4l0LSAI", "BQADBAADuAMAAlUXZAfHXDmd504z5AI" };
        internal static List<string> StartGame =
 new List<string> { "BQADAwADhAADdBexB7b36d3MSPzDAg", "BQADBAADwg0AAu0XZAcwCVhaZgAB_CsC" };
        internal static List<string> StartChaosGame =
 new List<string> { "BQADAwAD1wEAAnQXsQeswdJwV9BIyQI", "BQADAwAD2AEAAnQXsQeGw_-A8E7DLwI" }; //2
        internal static List<string> TannerWin =
 new List<string> { "BQADBAADQwgAAuQaZAcjXuF_tkE3JwI", "BQADBAAD_gMAAtgaZAf2YeVX6mXnUQI" };
        internal static List<string> CultWins =
 new List<string> { "BQADBAADWAMAAosYZAcuRvZYBpQmXwI", "BQADBAADHwsAAgUYZAcUTEIahD8XSQI" };
        internal static List<string> SerialKillerWins =
 new List<string> { "BQADBAADdQMAAsEcZAd7skaRqoWKzQI", "BQADBAADmgMAArgcZAdPyqayfRT6bQI", "BQADBAADOAQAAqUXZAeeuV5vjRd6QAI", "BQADBAADKwMAAsQZZAfwd2_EAeeOTgI" };
        internal static List<string> LoversWin =
 new List<string> { "BQADBAAD8hUAAhYYZAeHmbRRzioXXQI", "BQADBAADYAMAAkMdZAfR4qo8c95FGgI" };
#elif RELEASE
        internal static List<string> VillagerDieImages = new List<string> {"CgADBAADZaIAAssaZAfzhuNOpDt-vwI"};
        internal static List<string> WolfWin = new List<string> {"CgADBAAD154AAogbZAfp5Hft43Lk7QI"};
        internal static List<string> WolvesWin = new List<string> {"CgADBAADcZ8AAtAbZAefTVxIGtPZRAI"};
        internal static List<string> VillagersWin = new List<string> {"CgADBAADQp0AAnsdZAdYOLXiOPhbxQI"};
        internal static List<string> NoWinner = new List<string> { };
        internal static List<string> StartGame = new List<string> {"CgADBAADIKAAAm8cZAedB1JRO61k9wI"};
        internal static List<string> StartChaosGame = new List<string> {"CgADBAADIKAAAm8cZAedB1JRO61k9wI"};
        internal static List<string> TannerWin = new List<string> {"CgADBAADdLYAAlEaZAdMSg1kY38i7AI"};
        internal static List<string> CultWins = new List<string> {"CgADBAADYdYAAqcZZAexv1WYVAk45gI"};
        internal static List<string> SerialKillerWins = new List<string> {"CgADAgADswMAAg_GSEg5ccSG_o-YXQI"};
        internal static List<string> LoversWin = new List<string> {"CgADBAADA-QAAl0cZAcY8AGMr7Ba_QI"};
#else
        public static List<string> VillagerDieImages =
 new List<string> { "BQADAwADggADdBexBxVNNy-rt--bAg", "BQADBAADWAMAAt4cZAfbY0WobzNPwAI", "BQADBAADKgMAAoMbZAc7Ldme4T3DKQI" };
        public static List<string> WolfWin =
 new List<string> { "BQADAwADgQADdBexBzrFBt-CBlhbAg", "BQADAwADgAADdBexB88vVl1RuLb3Ag" };
        public static List<string> WolvesWin =
 new List<string> { "BQADBAADcAMAAn8ZZAfjilsAAeijzEAC", "BQADBAADlwMAAtgaZAcKX7eF4AgXCAI" };
        public static List<string> VillagersWin = new List<string> { "BQADAwADgwADdBexB5XubJT7w_zDAg" };
        public static List<string> NoWinner =
 new List<string> { "BQADBAAD8QgAAqIeZAdO5PeO55YsOQI", "BQADBAADuAMAAlUXZAePrr-YU3PDJwI" };
        public static List<string> StartGame =
 new List<string> { "BQADBAADwg0AAu0XZAdw1sAIIH6xQQI", "BQADAwADhAADdBexByVGjSOQUSx_Ag" };
        public static List<string> StartChaosGame =
 new List<string> { "BQADBAAD7wYAAgcYZAfk95HeMjOEfgI", "BQADBAAD_wcAAiUYZAcehPF7vHGFXAI" };
        public static List<string> TannerWin =
 new List<string> { "BQADBAAD_gMAAtgaZAeTCzBjKyXi6wI", "BQADBAADQwgAAuQaZAcBXBZ1bAUmwQI" };
        public static List<string> CultWins =
 new List<string> { "BQADBAADWAMAAosYZAfwfixffVnZywI", "BQADBAADHwsAAgUYZAfm4J7Dr5HpJQI" };
        public static List<string> SerialKillerWins =
 new List<string> { "BQADBAADdQMAAsEcZAf2I8Sj2kPcNQI", "BQADBAADmgMAArgcZAebNN10T84w9AI", "BQADBAADKwMAAsQZZAflRhJNO_knQAI", "BQADBAADOAQAAqUXZAcKgmVLwfIHvAI" };
        public static List<string> LoversWin =
 new List<string> { "BQADBAADYAMAAkMdZAf0_rs89KCyDAI", "BQADBAAD8hUAAhYYZAcV2T0l7f-lJQI" };
#endif

        public static int
#if DEBUG
            MinPlayers = 1,

#else
            MinPlayers = 5,
#endif
            MaxPlayers = 35,
            TimeDay = 60,
            TimeNight = 90,
            TimeLynch = 90,
#if !DEBUG
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
#else
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
#endif
            GameJoinTime = 180,
            MaxJoinTime = 300;


#if DEBUG
//public static long MainChatId = -134703013;
        public static long MainChatId = -1001049529775; //Beta group
#else
        public static long MainChatId = -1001030085238;
#endif
        public static long VeteranChatId = -1001094614730;
        public static string VeteranChatLink = "werewolfvets";

        public static bool RandomLynch = false;
    }
}