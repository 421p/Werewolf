using Microsoft.Win32;

namespace RegistrySaver
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var record = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)
                .OpenSubKey("SOFTWARE\\Werewolf", true);
            
            record.SetValue(args[0], args[1]);
        }
    }
}