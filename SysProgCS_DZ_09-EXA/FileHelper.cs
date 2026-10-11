using System.Text;
using System.Text.Json;

namespace SysProgCS_DZ_09_EXA;

internal static class FileHelper
{
    public static string BlackListPath = Path.Combine(AppContext.BaseDirectory, "blacklist.json");
    public static string SettingsPath = Path.Combine(AppContext.BaseDirectory, "settings.json");
    public static string LogPath = Path.Combine(AppContext.BaseDirectory, "process_log.txt");
    public static string LogError { get; private set; } = string.Empty;

    private static readonly object logLock = new object();
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    public static List<string> LoadBlackList()
    {
        if (!File.Exists(BlackListPath))
        {
            SaveBlackList(new List<string>());
        }

        List<string> names = JsonSerializer.Deserialize<List<string>>(
            File.ReadAllText(BlackListPath)) ?? new List<string>();
        List<string> result = new List<string>();

        foreach (string name in names)
        {
            string processName = ProcessHelper.NormalizeName(name);

            if (processName.Length > 0 &&
                !result.Contains(processName, StringComparer.OrdinalIgnoreCase) &&
                !ProcessHelper.IsOwnProcess(processName))
            {
                result.Add(processName);
            }
        }

        return result;
    }

    public static void SaveBlackList(List<string> names)
    {
        SaveJson(BlackListPath, JsonSerializer.Serialize(names, options));
    }

    public static AppSettings LoadSettings()
    {
        if (!File.Exists(SettingsPath))
        {
            SaveSettings(new AppSettings());
        }

        return JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(SettingsPath))
            ?? throw new InvalidDataException("settings.json is empty.");
    }

    public static void SaveSettings(AppSettings settings)
    {
        SaveJson(SettingsPath, JsonSerializer.Serialize(settings, options));
    }

    private static void SaveJson(string path, string json)
    {
        File.WriteAllText(path + ".tmp", json, Encoding.UTF8);
        File.Move(path + ".tmp", path, true);
    }

    public static void WriteLog(string name, string action)
    {
        lock (logLock)
        {
            try
            {
                string line = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} | {name} | {action}";
                File.AppendAllText(LogPath, line + Environment.NewLine, Encoding.UTF8);
                LogError = string.Empty;
            }
            catch (Exception ex)
            {
                LogError = "Cannot write the log: " + ex.Message;
            }
        }
    }
}