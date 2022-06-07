#region

using System;
using System.IO;
using System.Net;
using System.Text;

#endregion

namespace InjectCord;

public class InjectCord
{
    /// <summary>
    ///     Injects a custom JavaScript code into client.
    /// </summary>
    /// <param name="JS">JavaScript code to be injected.</param>
    /// <param name="ApplyNow">The client should be restarted now to take effects?</param>
    /// <returns>Whether injection is success or not.</returns>
    public static bool Inject(string JS, bool ApplyNow = true)
    {
        string Path = Utils.GetModulePath();
        bool Success = true;

        #region Write to index.js

        using (FileStream Stream = File.Open(Path, FileMode.Append, FileAccess.Write))
        {
            byte[] bytes = Encoding.UTF8.GetBytes(JS);

            try
            {
                Stream.Write(bytes, 0, bytes.Length);
            }
            catch
            {
                Success = false;
            }
            finally
            {
                Stream.Flush();
                Stream.Close();
                Stream.Dispose();
            }
        }

        if (!Success) throw new Exception("Unable to write into discord modules. The directory may protected.");

        #endregion

        if (ApplyNow) Utils.RestartDiscord();

        return Success;
    }

    /// <summary>
    ///     Reverts module file into default state from backup storage.
    /// </summary>
    /// <returns>Whether UnInject is success.</returns>
    public static bool Revert()
    {
        // Download backup code of modules\\discord_voice
        string SafeCode = new WebClient()
            .DownloadString(
                "https://gist.githubusercontent.com/arshx86/e251b0f3bce5170ad216acf408b0caec/raw/Modules%255C%255CDiscord_Voice.js");

        string Path = Utils.GetModulePath(); // Where we was injected?
        try
        {
            File.Delete(Path);
        }
        catch
        {
        }

        bool Success = true;

        using (FileStream Stream = File.OpenWrite(Path))
        {
            byte[] bytes = Encoding.UTF8.GetBytes(SafeCode);
            Stream.Seek(0, SeekOrigin.End);
            try
            {
                Stream.Write(bytes, 0, bytes.Length);
            }
            catch
            {
                Success = false;
            }
            finally
            {
                Stream.Flush();
                Stream.Close();
                Stream.Dispose();
            }
        }

        Utils.RestartDiscord(); // Restart required to apply.
        return Success;
    }
}