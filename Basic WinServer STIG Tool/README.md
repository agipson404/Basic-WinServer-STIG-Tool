# 🛡️ Basic WinServer STIG Tool
 Created By: Arthur Gipson

**Basic WinServer STIG Tool** is a basic C# desktop application that audits and remediates key Windows Server security settings based on DISA STIG compliance. Built as a simple user-friendly GUI, it’s designed for system administrators who need a simple way to validate system hardening requirements.

---

## ✅ Features

- Graphical user interface (WinForms) — easy for non-cyber skill admins
- One-click system audit of:
  - Password complexity enforcement
  - RDP Network Level Authentication
  - Remote Desktop status
  - Guest account status (via `net user` command. I chose this to be compatible with .NET 8.0)
- Remediation of non-compliant items with one button
- Timestamped logs
- Export logs to `output/` folder as `.txt` files
- Manifest-enforced **administrator privileges**

---

## 💻 Requirements

- Windows Server 2016 / 2019 / 2022 (or Windows 10/11 for demo/testing)
- .NET 8.0 Runtime (WinForms Desktop)
- Must be **run as Administrator**

---

## 🚀 How to Use

1. **Right-click → Run as Administrator**
2. Click **Run Audit** to view current security configurations
3. Review output in the log area
4. Click **Fix Issues** to apply secure settings
5. Click **Export Log** to save audit results to:
   ```
   ./output/Audit-Report-YYYY-MM-DD_HHMM.txt
   ```

---

## 📋 Example Output

```
[2025-04-19 15:42:13] Password Complexity: COMPLIANT (Value: 1)
[2025-04-19 15:42:13] RDP NLA: NON-COMPLIANT (Value: 0)
[2025-04-19 15:42:13] Remote Desktop: NON-COMPLIANT (Enabled)
[2025-04-19 15:42:13] Guest Account: COMPLIANT (Disabled) (Checked via 'net user')
```

---

## 🔒 STIG Controls Mapped

| Control                        | STIG ID   | Status       |
|-------------------------------|-----------|--------------|
| Password Complexity           | V-36437   | ✅ Implemented |
| RDP Network Level Auth        | V-93315   | ✅ Implemented |
| Remote Desktop Disabled       | V-93321   | ✅ Implemented |
| Guest Account Disabled        | V-36443   | ✅ Implemented |

---

## 📁 Project Structure

```
Basic WinServer STIG Tool/
├── Forms/                      # UI layout and logic
├── output/                     # Exported audit logs
├── Program.cs                  # App entry point
├── App.manifest                # UAC: requireAdministrator
├── README.md                   # Project documentation
└── LICENSE                     # MIT License
```

---

## 👨‍💻 Author

Created by [https://github.com/agipson404/]  

---

## 📌 Improvements for actual production

- Add more STIG checks (services, audit policy, firewall)
- Add compliance score or pass/fail summary
- Add the ability to see what changes would be made before applying them
- Add support for automated auditing

