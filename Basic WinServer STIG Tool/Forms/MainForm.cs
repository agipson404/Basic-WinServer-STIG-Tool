using Microsoft.Win32;
using System.Diagnostics;


namespace Basic_WinServer_STIG_Tool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public void LogMessage(string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string entry = $"[{timestamp}] {message}";
            logTextBox1.AppendText(entry + Environment.NewLine);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void runauditbutton_Click(object sender, EventArgs e)
        {
            LogMessage("==== STIG Audit Started ====");

            // 1. Password Complexity (LimitBlankPasswordUse)
            try
            {
                int pwComplex = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Lsa", "LimitBlankPasswordUse", -1);
                bool compliant = pwComplex == 1;
                LogMessage($"Password Complexity: {(compliant ? "COMPLIANT" : "NON-COMPLIANT")} (Value: {pwComplex})");
            }
            catch { LogMessage("Password Complexity: ERROR"); }

            // 2. RDP NLA (UserAuthentication)
            try
            {
                int rdpNLA = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp", "UserAuthentication", -1);
                bool compliant = rdpNLA == 1;
                LogMessage($"RDP NLA: {(compliant ? "COMPLIANT" : "NON-COMPLIANT")} (Value: {rdpNLA})");
            }
            catch { LogMessage("RDP NLA: ERROR"); }

            // 3. Remote Desktop (fDenyTSConnections)
            try
            {
                int rdpStatus = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Terminal Server", "fDenyTSConnections", -1);
                bool compliant = rdpStatus == 1; // 1 = Deny connections
                LogMessage($"Remote Desktop: {(compliant ? "COMPLIANT (Disabled)" : "NON-COMPLIANT (Enabled)")} (Value: {rdpStatus})");
            }
            catch { LogMessage("Remote Desktop: ERROR"); }

            // 4. Guest Account
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c net user Guest",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    bool isEnabled = output.Contains("Account active") && output.Contains("Yes");
                    LogMessage($"Guest Account: {(isEnabled ? "NON-COMPLIANT (Enabled)" : "COMPLIANT (Disabled)")} (Checked via 'net user')");
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Guest Account: ERROR - {ex.Message}");
            }


            LogMessage("==== Audit Complete ====");
        }

        private void remediatebutton_Click(object sender, EventArgs e)
        {
            LogMessage("==== Remediation Started ====");

            // 1. Enforce Password Complexity
            try
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Lsa", "LimitBlankPasswordUse", 1, RegistryValueKind.DWord);
                LogMessage("✔️ Enforced Password Complexity (LimitBlankPasswordUse = 1)");
            }
            catch (Exception ex)
            {
                LogMessage($"❌ Failed to enforce Password Complexity: {ex.Message}");
            }

            // 2. Enable RDP Network Level Authentication
            try
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp", "UserAuthentication", 1, RegistryValueKind.DWord);
                LogMessage("✔️ Enabled RDP NLA (UserAuthentication = 1)");
            }
            catch (Exception ex)
            {
                LogMessage($"❌ Failed to enable RDP NLA: {ex.Message}");
            }

            // 3. Disable Remote Desktop
            try
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Terminal Server", "fDenyTSConnections", 1, RegistryValueKind.DWord);
                LogMessage("✔️ Disabled Remote Desktop (fDenyTSConnections = 1)");
            }
            catch (Exception ex)
            {
                LogMessage($"❌ Failed to disable Remote Desktop: {ex.Message}");
            }

            // 4. Disable Guest Account
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c net user Guest /active:no",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(psi))
                {
                    process.WaitForExit();
                    LogMessage("✔️ Disabled Guest account via 'net user Guest /active:no'");
                }
            }
            catch (Exception ex)
            {
                LogMessage($"❌ Failed to disable Guest account: {ex.Message}");
            }


            LogMessage("==== Remediation Complete ====");
        }

        private void exportlog_Click(object sender, EventArgs e)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output");
                string filePath = Path.Combine(folderPath, $"Audit-Report-{timestamp}.txt");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                File.WriteAllText(filePath, logTextBox1.Text);
                LogMessage($"✔️ Log exported to {filePath}");
                MessageBox.Show($"Log saved to:\n{filePath}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LogMessage($"❌ Failed to export log: {ex.Message}");
                MessageBox.Show("Log export failed.\n\n" + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
