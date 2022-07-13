
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
            this.DgvTimesheets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvTimesheets.Location = new System.Drawing.Point(12, 70);
            this.DgvTimesheets.Name = "DgvTimesheets";
            this.DgvTimesheets.RowTemplate.Height = 25;
            this.DgvTimesheets.Size = new System.Drawing.Size(391, 217);
            this.DgvTimesheets.TabIndex = 5;
            // 
            // FrmTestDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 299);
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
    }
}
