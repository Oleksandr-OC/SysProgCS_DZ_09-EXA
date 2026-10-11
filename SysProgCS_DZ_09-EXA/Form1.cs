using System.Diagnostics;

namespace SysProgCS_DZ_09_EXA
{
    public partial class Form1 : Form
    {
        private AppSettings settings = new AppSettings();
        private bool checking;
        private bool updatingSettings;
        private bool exitRequested;
        private bool closing;

        public Form1()
        {
            InitializeComponent();
            LoadBlackList();
            UpdateProcesses();
            timerUpdate.Start();
            timerDetection.Start();
            buttonDetection.Text = "Stop apps detection";
            ReloadSettings();
        }

        private void buttonStart_Click(object? sender, EventArgs e)
        {
            if (!ProcessHelper.Start(textBoxInput.Text))
            {
                MessageBox.Show("Cannot start the process. Check its name or file path.");
            }

            UpdateProcesses();
            ShowLogError();
        }

        private async void buttonStop_Click(object? sender, EventArgs e)
        {
            if (GetActualSelected() is not ProcessWraper processWraper)
            {
                return;
            }

            int id = processWraper.Id;
            buttonStop.Enabled = false;
            bool stopped = await Task.Run(() => ProcessHelper.Stop(id));

            if (closing)
            {
                return;
            }

            buttonStop.Enabled = true;

            if (!stopped)
            {
                MessageBox.Show("Cannot stop the process. It may have exited or access is denied.");
            }

            UpdateProcesses();
            ShowLogError();
        }

        private void ClearProcesses()
        {
            foreach (ProcessWraper processWraper in listBoxProcesses.Items)
            {
                processWraper.GetInnerProcess().Dispose();
            }

            listBoxProcesses.Items.Clear();
            textBoxInfo.Clear();
        }

        private void UpdateProcesses()
        {
            int selectedId = GetActualSelected()?.Id ?? -1;
            int topIndex = listBoxProcesses.TopIndex;
            listBoxProcesses.BeginUpdate();

            try
            {
                ClearProcesses();
                Process[] processes = Process.GetProcesses();

                for (int i = 0; i < processes.Length; i++)
                {
                    try
                    {
                        ProcessWraper processWraper = new ProcessWraper(processes[i]);
                        listBoxProcesses.Items.Add(processWraper);

                        if (processWraper.Id == selectedId)
                        {
                            listBoxProcesses.SelectedItem = processWraper;
                        }
                    }
                    catch (Exception)
                    {
                        processes[i].Dispose();
                    }
                }

                if (topIndex >= 0 && topIndex < listBoxProcesses.Items.Count)
                {
                    listBoxProcesses.TopIndex = topIndex;
                }
            }
            finally
            {
                listBoxProcesses.EndUpdate();
            }
        }

        private void buttonUpdate_Click(object? sender, EventArgs e)
        {
            UpdateProcesses();
        }

        private void timerUpdate_Tick(object? sender, EventArgs e)
        {
            UpdateProcesses();
            ReloadSettings();
            ShowLogError();
        }

        private void listBoxProcesses_SelectedIndexChanged(object? sender, EventArgs e)
        {
            textBoxInfo.Text = GetActualSelected()?.GetProcessInfo() ?? string.Empty;
        }

        private ProcessWraper? GetActualSelected()
        {
            return listBoxProcesses.SelectedItem as ProcessWraper;
        }

        private void LoadBlackList()
        {
            try
            {
                foreach (string name in FileHelper.LoadBlackList())
                {
                    listBoxBlackList.Items.Add(name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot load blacklist.json: " + ex.Message);
            }
        }

        private List<string> GetBlackList()
        {
            List<string> names = new List<string>();

            foreach (string name in listBoxBlackList.Items)
            {
                names.Add(name);
            }

            return names;
        }

        private void buttonAdd_Click(object? sender, EventArgs e)
        {
            string name = ProcessHelper.NormalizeName(textBoxBlackList.Text);

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Enter a process name.");
                return;
            }

            if (ProcessHelper.IsOwnProcess(name))
            {
                MessageBox.Show("This program cannot be added to the black list.");
                return;
            }

            List<string> names = GetBlackList();

            if (names.Contains(name, StringComparer.OrdinalIgnoreCase))
            {
                MessageBox.Show("This process is already in the black list.");
                return;
            }

            try
            {
                names.Add(name);
                FileHelper.SaveBlackList(names);
                listBoxBlackList.Items.Add(name);
                textBoxBlackList.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot save the black list: " + ex.Message);
            }
        }

        private void buttonRemove_Click(object? sender, EventArgs e)
        {
            int index = listBoxBlackList.SelectedIndex;

            if (index < 0)
            {
                return;
            }

            try
            {
                List<string> names = GetBlackList();
                names.RemoveAt(index);
                FileHelper.SaveBlackList(names);
                listBoxBlackList.Items.RemoveAt(index);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot save the black list: " + ex.Message);
            }
        }

        private void buttonDetection_Click(object? sender, EventArgs e)
        {
            if (timerDetection.Enabled)
            {
                timerDetection.Stop();
                buttonDetection.Text = "Start apps detection";
            }
            else
            {
                timerDetection.Start();
                buttonDetection.Text = "Stop apps detection";
                timerDetection_Tick(this, EventArgs.Empty);
            }
        }

        private async void timerDetection_Tick(object? sender, EventArgs e)
        {
            if (checking || listBoxBlackList.Items.Count == 0)
            {
                return;
            }

            checking = true;
            List<string> names = GetBlackList();

            try
            {
                bool stopped = await Task.Run(() =>
                {
                    bool result = false;

                    foreach (string name in names)
                    {
                        if (ProcessHelper.Stop(name, true))
                        {
                            result = true;
                        }
                    }

                    return result;
                });

                if (!closing && stopped)
                {
                    UpdateProcesses();
                    ShowLogError();
                }
            }
            catch (Exception ex)
            {
                if (!closing)
                {
                    labelStatus.Text = "Detection error: " + ex.Message;
                }
            }
            finally
            {
                checking = false;
            }
        }

        private void buttonLog_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(FileHelper.LogPath))
                {
                    File.WriteAllText(FileHelper.LogPath, string.Empty);
                }

                OpenFile(FileHelper.LogPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot open the log: " + ex.Message);
            }
        }

        private void buttonSettings_Click(object? sender, EventArgs e)
        {
            try
            {
                OpenFile(FileHelper.SettingsPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot open settings.json: " + ex.Message);
            }
        }

        private void OpenFile(string path)
        {
            using Process? process = Process.Start(new ProcessStartInfo
            {
                FileName = "notepad.exe",
                Arguments = "\"" + path + "\"",
                UseShellExecute = true
            });
        }

        private void ReloadSettings()
        {
            try
            {
                settings = FileHelper.LoadSettings();
                UpdateSettingsControls();
                StartupHelper.Update(settings.AutoStart);

                if (settings.RestartOnClose)
                {
                    Watchdog.Start();
                }
                else
                {
                    Watchdog.Stop();
                }

                labelStatus.Text = "Processes: " + listBoxProcesses.Items.Count +
                    (timerDetection.Enabled ? " | Detection is on" : " | Detection is off");
            }
            catch (Exception ex)
            {
                labelStatus.Text = "Settings error: " + ex.Message;
            }
        }

        private void UpdateSettingsControls()
        {
            updatingSettings = true;
            checkBoxAutoStart.Checked = settings.AutoStart;
            checkBoxRestart.Checked = settings.RestartOnClose;
            updatingSettings = false;
        }

        private void checkBoxSettings_CheckedChanged(object? sender, EventArgs e)
        {
            if (updatingSettings)
            {
                return;
            }

            try
            {
                FileHelper.SaveSettings(new AppSettings
                {
                    AutoStart = checkBoxAutoStart.Checked,
                    RestartOnClose = checkBoxRestart.Checked
                });
                ReloadSettings();
            }
            catch (Exception ex)
            {
                UpdateSettingsControls();
                MessageBox.Show("Cannot save settings: " + ex.Message);
            }
        }

        private void ShowLogError()
        {
            if (FileHelper.LogError.Length > 0)
            {
                labelStatus.Text = FileHelper.LogError;
            }
        }

        private void buttonExit_Click(object? sender, EventArgs e)
        {
            exitRequested = true;
            Close();
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            closing = true;
            timerUpdate.Stop();
            timerDetection.Stop();

            if (exitRequested || e.CloseReason == CloseReason.WindowsShutDown ||
                !settings.RestartOnClose)
            {
                Watchdog.Stop();
            }
        }
    }
}
