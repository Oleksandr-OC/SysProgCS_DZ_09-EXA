using Microsoft.Win32;

namespace SysProgCS_DZ_09_EXA;

internal static class StartupHelper
{
    private const string keyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
    private const string valueName = "SysProgCS_DZ_09-EXA";

    public static void Update(bool enabled)
    {
        using RegistryKey key = Registry.CurrentUser.CreateSubKey(keyPath);

        if (enabled)
        {
            string command = "\"" + Application.ExecutablePath + "\"";

            if (!string.Equals(key.GetValue(valueName) as string, command,
                StringComparison.OrdinalIgnoreCase))
            {
                key.SetValue(valueName, command);
            }
        }
        else
        {
            key.DeleteValue(valueName, false);
        }
    }
}
