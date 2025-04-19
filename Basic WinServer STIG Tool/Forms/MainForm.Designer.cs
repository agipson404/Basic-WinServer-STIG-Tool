namespace Basic_WinServer_STIG_Tool
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            logTextBox1 = new RichTextBox();
            runauditbutton = new Button();
            remediatebutton = new Button();
            exportlog = new Button();
            SuspendLayout();
            // 
            // logTextBox1
            // 
            logTextBox1.Location = new Point(12, 6);
            logTextBox1.Name = "logTextBox1";
            logTextBox1.Size = new Size(780, 432);
            logTextBox1.TabIndex = 0;
            logTextBox1.Text = "";
            // 
            // runauditbutton
            // 
            runauditbutton.Location = new Point(12, 444);
            runauditbutton.Name = "runauditbutton";
            runauditbutton.Size = new Size(75, 23);
            runauditbutton.TabIndex = 1;
            runauditbutton.Text = "Run Audit";
            runauditbutton.UseVisualStyleBackColor = true;
            runauditbutton.Click += runauditbutton_Click;
            // 
            // remediatebutton
            // 
            remediatebutton.Location = new Point(93, 444);
            remediatebutton.Name = "remediatebutton";
            remediatebutton.Size = new Size(75, 23);
            remediatebutton.TabIndex = 2;
            remediatebutton.Text = "Fix Issues";
            remediatebutton.UseVisualStyleBackColor = true;
            remediatebutton.Click += remediatebutton_Click;
            // 
            // exportlog
            // 
            exportlog.Location = new Point(174, 444);
            exportlog.Name = "exportlog";
            exportlog.Size = new Size(75, 23);
            exportlog.TabIndex = 3;
            exportlog.Text = "Export Log";
            exportlog.UseVisualStyleBackColor = true;
            exportlog.Click += exportlog_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 498);
            Controls.Add(exportlog);
            Controls.Add(remediatebutton);
            Controls.Add(runauditbutton);
            Controls.Add(logTextBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Basic WinServer STIG Tool";
            Load += MainForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox logTextBox1;
        private Button runauditbutton;
        private Button remediatebutton;
        private Button exportlog;
    }
}
