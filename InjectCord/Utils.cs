#region

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

#endregion

namespace InjectCord;

internal class Utils
{
    /// <summary>
    ///     Restarts discord client.
    /// </summary>
    public static void RestartDiscord()
    {
        Process[] discord = Process.GetProcessesByName("discord");
        if (discord.Length < 1) return;
        foreach (var proc in discord) proc.Kill();
        Process.Start(discord[0].MainModule.FileName);
    }


    /// <summary>
    ///     Returns a path of voice module that to be used for injecting.
    /// </summary>
    public static string GetModulePath()
    {
        string baseP = $"C:\\Users\\{Environment.UserName}\\AppData\\Local\\Discord";

        // find "app-" folder to get version.
        string[] versionPaths = Directory
            .GetDirectories(baseP);
        string version = versionPaths
            .FirstOrDefault(x => x.Contains("app-"));

        // get into modules and find "voice" folder.
        string modules = Path.Combine(baseP, version + "\\modules");
        string voice = Directory.GetDirectories(modules).FirstOrDefault(x => x.Contains("voice"));
        string final = voice + "\\discord_voice\\index.js";
        return (File.Exists(final) ? final : null) ?? throw new FileNotFoundException("Unable to find module path.");
    }
}