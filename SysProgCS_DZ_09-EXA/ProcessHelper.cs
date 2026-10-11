using System.Diagnostics;

namespace SysProgCS_DZ_09_EXA;

internal static class ProcessHelper
{
    private static readonly object stopLock = new object();

    public static string NormalizeName(string? name)
    {
        name = Path.GetFileName((name ?? string.Empty).Trim().Trim('"'));

        if (name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
        {
            name = name.Substring(0, name.Length - 4);
        }

        return name;
    }

    public static bool IsOwnProcess(string name)
    {
        return string.Equals(NormalizeName(name),
            Path.GetFileNameWithoutExtension(Environment.ProcessPath),
            StringComparison.OrdinalIgnoreCase);
    }

    public static bool Start(string nameOrPath)
    {
        try
        {
            string fileName = nameOrPath.Trim().Trim('"');

            if (fileName.Length == 0)
            {
                return false;
            }

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = fileName,
                UseShellExecute = true
            };

            using Process? process = Process.Start(startInfo);
            FileHelper.WriteLog(NormalizeName(fileName) + ".exe", "Started");
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public static bool Stop(int id)
    {
        try
        {
            using Process process = Process.GetProcessById(id);
            return Stop(process);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public static bool Stop(string name, bool all = false)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        Process[] processes = Process.GetProcessesByName(NormalizeName(name));
        bool stopped = false;

        try
        {
            for (int i = 0; i < processes.Length; i++)
            {
                if (Stop(processes[i]))
                {
                    stopped = true;

                    if (!all)
                    {
                        break;
                    }
                }
            }
        }
        finally
        {
            for (int i = 0; i < processes.Length; i++)
            {
                processes[i].Dispose();
            }
        }

        return stopped;
    }

    public static bool Stop(Process process)
    {
        lock (stopLock)
        {
            try
            {
                string name = process.ProcessName;

                if (process.Id <= 4 || IsOwnProcess(name) || process.HasExited)
                {
                    return false;
                }

                process.Kill();

                if (!process.WaitForExit(2000))
                {
                    return false;
                }

                FileHelper.WriteLog(name + ".exe", "Closed");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}