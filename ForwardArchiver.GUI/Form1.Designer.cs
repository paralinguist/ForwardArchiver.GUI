namespace ForwardArchiver.GUI
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.archive_mail = new System.Windows.Forms.Button();
            this.feedback_log = new System.Windows.Forms.TextBox();
            this.department_email = new System.Windows.Forms.TextBox();
            this.dept_email_label = new System.Windows.Forms.Label();
            this.password_label = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.gmail_label = new System.Windows.Forms.Label();
            this.gmail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.exchange_URL = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // archive_mail
            // 
            this.archive_mail.Enabled = false;
            this.archive_mail.Location = new System.Drawing.Point(15, 124);
            this.archive_mail.Name = "archive_mail";
            this.archive_mail.Size = new System.Drawing.Size(413, 23);
            this.archive_mail.TabIndex = 5;
            this.archive_mail.Text = "Enter Password Above";
            this.archive_mail.UseVisualStyleBackColor = true;
            this.archive_mail.Click += new System.EventHandler(this.archive_mail_Click);
            // 
            // feedback_log
            // 
            this.feedback_log.Enabled = false;
            this.feedback_log.Location = new System.Drawing.Point(15, 162);
            this.feedback_log.Multiline = true;
            this.feedback_log.Name = "feedback_log";
            this.feedback_log.ReadOnly = true;
            this.feedback_log.Size = new System.Drawing.Size(413, 137);
            this.feedback_log.TabIndex = 1;
            // 
            // department_email
            // 
            this.department_email.Location = new System.Drawing.Point(136, 32);
            this.department_email.Name = "department_email";
            this.department_email.Size = new System.Drawing.Size(292, 20);
            this.department_email.TabIndex = 2;
            // 
            // dept_email_label
            // 
            this.dept_email_label.AutoSize = true;
            this.dept_email_label.Location = new System.Drawing.Point(12, 35);
            this.dept_email_label.Name = "dept_email_label";
            this.dept_email_label.Size = new System.Drawing.Size(93, 13);
            this.dept_email_label.TabIndex = 3;
            this.dept_email_label.Text = "Department Email:";
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Location = new System.Drawing.Point(12, 58);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(114, 13);
            this.password_label.TabIndex = 4;
            this.password_label.Text = "Department Password:";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(136, 58);
            this.password.Name = "password";
            this.password.PasswordChar = '•';
            this.password.Size = new System.Drawing.Size(133, 20);
            this.password.TabIndex = 3;
            this.password.TextChanged += new System.EventHandler(this.password_TextChanged);
            // 
            // gmail_label
            // 
            this.gmail_label.AutoSize = true;
            this.gmail_label.Location = new System.Drawing.Point(12, 9);
            this.gmail_label.Name = "gmail_label";
            this.gmail_label.Size = new System.Drawing.Size(77, 13);
            this.gmail_label.TabIndex = 7;
            this.gmail_label.Text = "Gmail Address:";
            // 
            // gmail
            // 
            this.gmail.Location = new System.Drawing.Point(136, 6);
            this.gmail.Name = "gmail";
            this.gmail.Size = new System.Drawing.Size(292, 20);
            this.gmail.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Exchange WS URL:";
            // 
            // exchange_URL
            // 
            this.exchange_URL.Location = new System.Drawing.Point(136, 84);
            this.exchange_URL.Name = "exchange_URL";
            this.exchange_URL.Size = new System.Drawing.Size(292, 20);
            this.exchange_URL.TabIndex = 4;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 313);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.exchange_URL);
            this.Controls.Add(this.gmail_label);
            this.Controls.Add(this.gmail);
            this.Controls.Add(this.password);
            this.Controls.Add(this.password_label);
            this.Controls.Add(this.dept_email_label);
            this.Controls.Add(this.department_email);
            this.Controls.Add(this.feedback_log);
            this.Controls.Add(this.archive_mail);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.Text = "ForwardArchiver";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button archive_mail;
        private System.Windows.Forms.TextBox feedback_log;
        private System.Windows.Forms.TextBox department_email;
        private System.Windows.Forms.Label dept_email_label;
        private System.Windows.Forms.Label password_label;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label gmail_label;
        private System.Windows.Forms.TextBox gmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox exchange_URL;
    }
}

