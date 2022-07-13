
namespace Payroll.Timesheets.FrontEnd
{
    partial class FrmTestDownload
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
            this.DtCutoffDate = new System.Windows.Forms.DateTimePicker();
            this.BtnRun = new System.Windows.Forms.Button();
            this.CbPayrollCode = new System.Windows.Forms.ComboBox();
            this.CbBankCategory = new System.Windows.Forms.ComboBox();
            this.Pb1 = new System.Windows.Forms.ProgressBar();
            this.DgvTimesheets = new System.Windows.Forms.DataGridView();
            this.clDownload = new System.Windows.Forms.DataGridViewButtonColumn();
            this.EEId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clCutoffId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clPayrollCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clBankCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTotalHours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTotalOT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTotalRDOT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTotalHOT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTotalND = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTotalTardy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clIsConfirmed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clPage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTimesheetId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDateCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgvTimesheets)).BeginInit();
            this.SuspendLayout();
            // 
            // DtCutoffDate
            // 
            this.DtCutoffDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtCutoffDate.Location = new System.Drawing.Point(12, 12);
            this.DtCutoffDate.Name = "DtCutoffDate";
            this.DtCutoffDate.Size = new System.Drawing.Size(116, 23);
            this.DtCutoffDate.TabIndex = 0;
            // 
            // BtnRun
            // 
            this.BtnRun.Location = new System.Drawing.Point(328, 11);
            this.BtnRun.Name = "BtnRun";
            this.BtnRun.Size = new System.Drawing.Size(75, 23);
            this.BtnRun.TabIndex = 1;
            this.BtnRun.Text = "Run";
            this.BtnRun.UseVisualStyleBackColor = true;
            this.BtnRun.Click += new System.EventHandler(this.BtnRun_Click);
            // 
            // CbPayrollCode
            // 
            this.CbPayrollCode.FormattingEnabled = true;
            this.CbPayrollCode.Items.AddRange(new object[] {
            "P1A",
            "P10A",
            "P11A",
            "P5A",
            "P7A"});
            this.CbPayrollCode.Location = new System.Drawing.Point(134, 12);
            this.CbPayrollCode.Name = "CbPayrollCode";
            this.CbPayrollCode.Size = new System.Drawing.Size(91, 23);
            this.CbPayrollCode.TabIndex = 2;
            this.CbPayrollCode.Text = "P1A";
            // 
            // CbBankCategory
            // 
            this.CbBankCategory.FormattingEnabled = true;
            this.CbBankCategory.Items.AddRange(new object[] {
            "ATM1",
            "ATM2",
            "CCARD",
            "CHK"});
            this.CbBankCategory.Location = new System.Drawing.Point(231, 12);
            this.CbBankCategory.Name = "CbBankCategory";
            this.CbBankCategory.Size = new System.Drawing.Size(91, 23);
            this.CbBankCategory.TabIndex = 3;
            this.CbBankCategory.Text = "CHK";
            // 
            // Pb1
            // 
            this.Pb1.Location = new System.Drawing.Point(12, 41);
            this.Pb1.Name = "Pb1";
            this.Pb1.Size = new System.Drawing.Size(213, 23);
            this.Pb1.TabIndex = 4;
            // 
            // DgvTimesheets
            // 
            this.DgvTimesheets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvTimesheets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvTimesheets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clDownload,
            this.EEId,
            this.clCutoffId,
            this.clPayrollCode,
            this.clBankCategory,
            this.clTotalHours,
            this.clTotalOT,
            this.clTotalRDOT,
            this.clTotalHOT,
            this.clTotalND,
            this.clTotalTardy,
            this.clIsConfirmed,
            this.clPage,
            this.clTimesheetId,
            this.clDateCreated});
            this.DgvTimesheets.Location = new System.Drawing.Point(12, 70);
            this.DgvTimesheets.Name = "DgvTimesheets";
            this.DgvTimesheets.RowTemplate.Height = 25;
            this.DgvTimesheets.Size = new System.Drawing.Size(1055, 217);
            this.DgvTimesheets.TabIndex = 5;
            this.DgvTimesheets.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvTimesheets_CellContentClick);
            // 
            // clDownload
            // 
            this.clDownload.HeaderText = "Action";
            this.clDownload.Name = "clDownload";
            this.clDownload.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clDownload.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clDownload.Text = "Download";
            this.clDownload.UseColumnTextForButtonValue = true;
            // 
            // EEId
            // 
            this.EEId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.EEId.DataPropertyName = "EEID";
            this.EEId.HeaderText = "EEId";
            this.EEId.Name = "EEId";
            this.EEId.Width = 54;
            // 
            // clCutoffId
            // 
            this.clCutoffId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.clCutoffId.DataPropertyName = "CutoffId";
            this.clCutoffId.HeaderText = "Cutoff Id";
            this.clCutoffId.Name = "clCutoffId";
            this.clCutoffId.ReadOnly = true;
            this.clCutoffId.Width = 79;
            // 
            // clPayrollCode
            // 
            this.clPayrollCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.clPayrollCode.DataPropertyName = "PayrollCode";
            this.clPayrollCode.HeaderText = "PayrollCode";
            this.clPayrollCode.Name = "clPayrollCode";
            this.clPayrollCode.ReadOnly = true;
            this.clPayrollCode.Width = 96;
            // 
            // clBankCategory
            // 
            this.clBankCategory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.clBankCategory.DataPropertyName = "BankCategory";
            this.clBankCategory.HeaderText = "BankCategory";
            this.clBankCategory.Name = "clBankCategory";
            this.clBankCategory.ReadOnly = true;
            this.clBankCategory.Width = 106;
            // 
            // clTotalHours
            // 
            this.clTotalHours.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.clTotalHours.DataPropertyName = "TotalHours";
            this.clTotalHours.HeaderText = "Total Hours";
            this.clTotalHours.Name = "clTotalHours";
            this.clTotalHours.ReadOnly = true;
            this.clTotalHours.Width = 92;
            // 
            // clTotalOT
            // 
            this.clTotalOT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.clTotalOT.DataPropertyName = "TotalOT";
            this.clTotalOT.HeaderText = "Total OT";
            this.clTotalOT.Name = "clTotalOT";
            this.clTotalOT.ReadOnly = true;
            this.clTotalOT.Width = 74;
            // 
            // clTotalRDOT
            // 
            this.clTotalRDOT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.clTotalRDOT.DataPropertyName = "TotalRDOT";
            this.clTotalRDOT.HeaderText = "Total RD_OT";
            this.clTotalRDOT.Name = "clTotalRDOT";
            this.clTotalRDOT.ReadOnly = true;
            this.clTotalRDOT.Width = 94;
            // 
            // clTotalHOT
            // 
            this.clTotalHOT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.clTotalHOT.DataPropertyName = "TotalHOT";
            this.clTotalHOT.HeaderText = "Total H_OT";
            this.clTotalHOT.Name = "clTotalHOT";
            this.clTotalHOT.ReadOnly = true;
            this.clTotalHOT.Width = 88;
            // 
            // clTotalND
            // 
            this.clTotalND.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.clTotalND.DataPropertyName = "TotalND";
            this.clTotalND.HeaderText = "Total ND";
            this.clTotalND.Name = "clTotalND";
            this.clTotalND.ReadOnly = true;
            this.clTotalND.Width = 77;
            // 
            // clTotalTardy
            // 
            this.clTotalTardy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.clTotalTardy.DataPropertyName = "TotalTardy";
            this.clTotalTardy.HeaderText = "Total Tardy";
            this.clTotalTardy.Name = "clTotalTardy";
            this.clTotalTardy.ReadOnly = true;
            this.clTotalTardy.Width = 88;
            // 
            // clIsConfirmed
            // 
            this.clIsConfirmed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.clIsConfirmed.DataPropertyName = "IsConfirmed";
            this.clIsConfirmed.HeaderText = "Confirmed";
            this.clIsConfirmed.Name = "clIsConfirmed";
            this.clIsConfirmed.ReadOnly = true;
            this.clIsConfirmed.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clIsConfirmed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clIsConfirmed.Width = 89;
            // 
            // clPage
            // 
            this.clPage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.clPage.DataPropertyName = "Page";
            this.clPage.HeaderText = "Page";
            this.clPage.Name = "clPage";
            this.clPage.ReadOnly = true;
            this.clPage.Width = 58;
            // 
            // clTimesheetId
            // 
            this.clTimesheetId.DataPropertyName = "TimesheetId";
            this.clTimesheetId.HeaderText = "Timesheet Id";
            this.clTimesheetId.Name = "clTimesheetId";
            this.clTimesheetId.ReadOnly = true;
            this.clTimesheetId.Visible = false;
            // 
            // clDateCreated
            // 
            this.clDateCreated.DataPropertyName = "DateCreated";
            this.clDateCreated.HeaderText = "DateCreated";
            this.clDateCreated.Name = "clDateCreated";
            this.clDateCreated.ReadOnly = true;
            this.clDateCreated.Visible = false;
            // 
            // FrmTestDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 299);
            this.Controls.Add(this.DgvTimesheets);
            this.Controls.Add(this.Pb1);
            this.Controls.Add(this.CbBankCategory);
            this.Controls.Add(this.CbPayrollCode);
            this.Controls.Add(this.BtnRun);
            this.Controls.Add(this.DtCutoffDate);
            this.Name = "FrmTestDownload";
            this.Text = "FrmTestDownload";
            this.Load += new System.EventHandler(this.FrmTestDownload_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvTimesheets)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DtCutoffDate;
        private System.Windows.Forms.Button BtnRun;
        private System.Windows.Forms.ComboBox CbPayrollCode;
        private System.Windows.Forms.ComboBox CbBankCategory;
        private System.Windows.Forms.ProgressBar Pb1;
        private System.Windows.Forms.DataGridView DgvTimesheets;
        private System.Windows.Forms.DataGridViewButtonColumn clDownload;
        private System.Windows.Forms.DataGridViewTextBoxColumn EEId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clCutoffId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clPayrollCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn clBankCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTotalHours;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTotalOT;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTotalRDOT;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTotalHOT;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTotalND;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTotalTardy;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clIsConfirmed;
        private System.Windows.Forms.DataGridViewTextBoxColumn clPage;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTimesheetId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDateCreated;
    }
}
