using Payroll.Timesheets.FrontEnd.Controller;
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
        }

        private void FrmTestDownload_Load(object sender, EventArgs e)
        {
            DownloadController = new();
            DownloadController.PageDownloadSucceeded += Controller_PageDownloadSucceeded1;
            DownloadController.DownloadStarted += Controller_DownloadStarted;


            Controller = new();
            DgvTimesheets.DataSource = Controller.List();
        }

        private void Controller_DownloadStarted(object sender, int TotalPages)
        {
            Pb1.Maximum = TotalPages;
            Pb1.Value = 0;
        }

        private void Controller_PageDownloadSucceeded1(object sender, int Page)
        {
            Pb1.Value = Page;
        }


        private void BtnRun_Click(object sender, EventArgs e)
        {
            _ = DownloadController.StartDownload(DtCutoffDate.Value, CbPayrollCode.Text, CbBankCategory.Text);
        }
    }
}
