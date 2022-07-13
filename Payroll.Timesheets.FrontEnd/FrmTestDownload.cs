﻿using Payroll.Timesheets.FrontEnd.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payroll.Timesheets.FrontEnd
{
    public partial class FrmTestDownload : Form
    {
        private TimesheetDownloadController DownloadController;
        private TimesheetController Controller;


        public FrmTestDownload()
        {
            InitializeComponent();


            DownloadController = new();
            DownloadController.PageDownload += Controller_PageDownloadSucceeded;
            DownloadController.DownloadStarted += Controller_DownloadStarted;
            DownloadController.DownloadEnded += DownloadController_DownloadEnded; ;

            Controller = new();
        }

        private void FrmTestDownload_Load(object sender, EventArgs e)
        {
            DgvTimesheets.DataSource = Controller.List();
        }

        private void DownloadController_DownloadEnded(object sender, int TotalPages)
        {
            Pb1.Maximum = 0;
            Pb1.Value = 0;
        }

        private void Controller_DownloadStarted(object sender, int TotalPages)
        {
            Pb1.Maximum = TotalPages;
            Pb1.Value = 0;
        }

        private void Controller_PageDownloadSucceeded(object sender, int Page)
        {
            Pb1.Value = Page;
        }


        private void BtnRun_Click(object sender, EventArgs e)
        {
            _ = DownloadController.StartDownload(DtCutoffDate.Value, CbPayrollCode.Text, CbBankCategory.Text);
        }
    }
}
