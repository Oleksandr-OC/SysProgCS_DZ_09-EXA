using System.ComponentModel;
using System.Diagnostics;

namespace SysProgCS_DZ_09_EXA;

internal class ProcessWraper
{
    private Process process;
    private string name;

    public int Id { get; }

    public ProcessWraper(Process process)
    {
        this.process = process;
        Id = process.Id;
        name = process.ProcessName;
    }

    public Process GetInnerProcess() => process;

    public string GetProcessInfo()
    {
        try
        {
            return $"Process Name: {name}\r\n" +
                $"ID: {Id}\r\n" +
                $"Start Time: {process.StartTime}\r\n" +
                $"Memory Usage: {process.WorkingSet64 / 1024 / 1024} MB\r\n" +
                $"Priority: {process.BasePriority}\r\n" +
                $"Threads: {process.Threads.Count}\r\n";
        }
        catch (Win32Exception)
        {
            return $"Process Name: {name}\r\nID: {Id}\r\n" +
                "Access denied to some process information.";
        }
        catch (Exception)
        {
            return "Process has been terminated.";
        }
    }

    public override string ToString() => $"{name} ({Id})";
}

