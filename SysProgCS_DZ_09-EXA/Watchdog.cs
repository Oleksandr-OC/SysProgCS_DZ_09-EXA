using System.Diagnostics;

namespace SysProgCS_DZ_09_EXA;

internal static class Watchdog
{
    private static Process? watcher;
    private static EventWaitHandle? stopEvent;

    private static string GetEventName(int id)
    {
        return @"Local\SysProgCS_DZ_09_EXA_Stop_" + id;
    }

    public static void Start()
    {
        if (watcher != null && !watcher.HasExited)
        {
            return;
        }

        watcher?.Dispose();
        stopEvent?.Dispose();
        stopEvent = new EventWaitHandle(false, EventResetMode.ManualReset,
            GetEventName(Environment.ProcessId));
        stopEvent.Reset();

        watcher = Process.Start(new ProcessStartInfo
        {
            FileName = Application.ExecutablePath,
            Arguments = "--watch " + Environment.ProcessId,
            WorkingDirectory = AppContext.BaseDirectory,
            UseShellExecute = false
        });
    }

    public static void Stop()
    {
        stopEvent?.Set();

        if (watcher != null)
        {
            watcher.WaitForExit(2000);
            watcher.Dispose();
            watcher = null;
        }

        stopEvent?.Dispose();
        stopEvent = null;
    }

    public static void Watch(int id)
    {
        using EventWaitHandle exitSignal = new EventWaitHandle(false,
            EventResetMode.ManualReset, GetEventName(id));

        try
        {
            using Process mainProcess = Process.GetProcessById(id);

            while (!mainProcess.HasExited)
            {
                if (exitSignal.WaitOne(500))
                {
                    return;
                }
            }
        }
        catch (ArgumentException)
        {
        }

        if (exitSignal.WaitOne(0))
        {
            return;
        }

        try
        {
            if (FileHelper.LoadSettings().RestartOnClose)
            {
                using Process? process = Process.Start(new ProcessStartInfo
                {
                    FileName = Application.ExecutablePath,
                    WorkingDirectory = AppContext.BaseDirectory,
                    UseShellExecute = true
                });
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}
