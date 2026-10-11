namespace SysProgCS_DZ_09_EXA
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 2 && args[0] == "--watch" &&
                int.TryParse(args[1], out int id))
            {
                Watchdog.Watch(id);
                return;
            }

            using Mutex mutex = new Mutex(true, @"Local\SysProgCS_DZ_09_EXA", out bool firstStart);

            if (!firstStart)
            {
                return;
            }

            Application.Run(new Form1());
        }
    }
}
