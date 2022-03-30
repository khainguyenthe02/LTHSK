using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_HSK
{
    public partial class frmRPHangHoa : Form
    {
        public frmRPHangHoa()
        {
            InitializeComponent();
        }

        private void frmRPHangHoa_Load(object sender, EventArgs e)
        {
            try
            {
                ReportDocument rpt = new ReportDocument();

                string path = string.Format("{0}\\Reports\\rpHH.rpt",
                                    Application.StartupPath);
                rpt.Load(path);

                //Cập nhật nguồn dữ liệu
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                logOnInfo.ConnectionInfo.ServerName = "KHAIPC";
                logOnInfo.ConnectionInfo.DatabaseName = "LTHSK";
                logOnInfo.ConnectionInfo.UserID = "LTHSK";
                logOnInfo.ConnectionInfo.Password = "123456";

                foreach (Table t in rpt.Database.Tables)
                {
                    t.ApplyLogOnInfo(logOnInfo);
                }

                crystalReportViewer1.ReportSource = rpt;
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
