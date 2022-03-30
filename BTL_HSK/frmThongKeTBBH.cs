using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_HSK
{
    public partial class frmThongKeTBBH : Form
    {
        public frmThongKeTBBH()
        {
            InitializeComponent();
        }
        private void frmThongKeTBBH_Load(object sender, EventArgs e)
        {
            

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            mtbNgayBD.Text = mtbNgayKT.Text = string.Empty;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            DataView dvTBBH = (DataView)dgvTBBH.DataSource;

            //frmRPTBBH rptbbh = Program.FindOpenForm("frmRPTBBH") as frmRPTBBH;





        }
        public string mtbBD { get { return mtbNgayBD.Text; } }

        private void btnHien_Click(object sender, EventArgs e)
        {
            string con = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(con))
            {
                using (SqlCommand cmd = new SqlCommand("BHTheoTg", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tgbd", mtbNgayBD.Text);
                    cmd.Parameters.AddWithValue("@tgkt", mtbNgayKT.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dgvTBBH.AutoGenerateColumns = false;
                        dgvTBBH.DataSource = dt;
                    }
                }//cmd
            }//con
        }
    }
}
