namespace komyuter.simulator
{
    partial class frmPrepareTrip
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tripDate = new System.Windows.Forms.DateTimePicker();
            this.tripStart = new System.Windows.Forms.DateTimePicker();
            this.tripEnd = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtMonitor = new System.Windows.Forms.TextBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.chkLRT1 = new System.Windows.Forms.CheckBox();
            this.chkLRT2 = new System.Windows.Forms.CheckBox();
            this.chkMRT3 = new System.Windows.Forms.CheckBox();
            this.chkPNR = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start Time";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "End Time";
            // 
            // tripDate
            // 
            this.tripDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.tripDate.Location = new System.Drawing.Point(71, 27);
            this.tripDate.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            this.tripDate.MinDate = new System.DateTime(2020, 4, 1, 0, 0, 0, 0);
            this.tripDate.Name = "tripDate";
            this.tripDate.Size = new System.Drawing.Size(92, 20);
            this.tripDate.TabIndex = 2;
            this.tripDate.Value = new System.DateTime(2020, 4, 15, 21, 9, 6, 0);
            // 
            // tripStart
            // 
            this.tripStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.tripStart.Location = new System.Drawing.Point(71, 53);
            this.tripStart.Name = "tripStart";
            this.tripStart.Size = new System.Drawing.Size(92, 20);
            this.tripStart.TabIndex = 3;
            // 
            // tripEnd
            // 
            this.tripEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.tripEnd.Location = new System.Drawing.Point(71, 79);
            this.tripEnd.Name = "tripEnd";
            this.tripEnd.Size = new System.Drawing.Size(92, 20);
            this.tripEnd.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Date";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(334, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 60);
            this.button1.TabIndex = 6;
            this.button1.Text = "1. Prepare Trip";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtMonitor
            // 
            this.txtMonitor.Location = new System.Drawing.Point(11, 163);
            this.txtMonitor.Multiline = true;
            this.txtMonitor.Name = "txtMonitor";
            this.txtMonitor.ReadOnly = true;
            this.txtMonitor.Size = new System.Drawing.Size(445, 447);
            this.txtMonitor.TabIndex = 8;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(11, 623);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(445, 23);
            this.progressBar.TabIndex = 10;
            // 
            // chkLRT1
            // 
            this.chkLRT1.AutoSize = true;
            this.chkLRT1.Location = new System.Drawing.Point(26, 21);
            this.chkLRT1.Name = "chkLRT1";
            this.chkLRT1.Size = new System.Drawing.Size(53, 17);
            this.chkLRT1.TabIndex = 11;
            this.chkLRT1.Text = "LRT1";
            this.chkLRT1.UseVisualStyleBackColor = true;
            // 
            // chkLRT2
            // 
            this.chkLRT2.AutoSize = true;
            this.chkLRT2.Location = new System.Drawing.Point(26, 47);
            this.chkLRT2.Name = "chkLRT2";
            this.chkLRT2.Size = new System.Drawing.Size(53, 17);
            this.chkLRT2.TabIndex = 12;
            this.chkLRT2.Text = "LRT2";
            this.chkLRT2.UseVisualStyleBackColor = true;
            // 
            // chkMRT3
            // 
            this.chkMRT3.AutoSize = true;
            this.chkMRT3.Location = new System.Drawing.Point(26, 73);
            this.chkMRT3.Name = "chkMRT3";
            this.chkMRT3.Size = new System.Drawing.Size(56, 17);
            this.chkMRT3.TabIndex = 13;
            this.chkMRT3.Text = "MRT3";
            this.chkMRT3.UseVisualStyleBackColor = true;
            // 
            // chkPNR
            // 
            this.chkPNR.AutoSize = true;
            this.chkPNR.Location = new System.Drawing.Point(26, 99);
            this.chkPNR.Name = "chkPNR";
            this.chkPNR.Size = new System.Drawing.Size(49, 17);
            this.chkPNR.TabIndex = 14;
            this.chkPNR.Text = "PNR";
            this.chkPNR.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.chkPNR);
            this.groupBox2.Controls.Add(this.chkLRT1);
            this.groupBox2.Controls.Add(this.chkMRT3);
            this.groupBox2.Controls.Add(this.chkLRT2);
            this.groupBox2.Location = new System.Drawing.Point(8, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(122, 141);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Route";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 121);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "No Check = ALL";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tripDate);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.tripStart);
            this.groupBox3.Controls.Add(this.tripEnd);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(143, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(178, 141);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Schedule";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(334, 88);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 60);
            this.button2.TabIndex = 17;
            this.button2.Text = "2. Process Trip Update";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmPrepareTrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(470, 659);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.txtMonitor);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrepareTrip";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Prepare Trip";
            this.Load += new System.EventHandler(this.frmPrepareTrip_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker tripDate;
        private System.Windows.Forms.DateTimePicker tripStart;
        private System.Windows.Forms.DateTimePicker tripEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtMonitor;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.CheckBox chkLRT1;
        private System.Windows.Forms.CheckBox chkLRT2;
        private System.Windows.Forms.CheckBox chkMRT3;
        private System.Windows.Forms.CheckBox chkPNR;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label9;
    }
}