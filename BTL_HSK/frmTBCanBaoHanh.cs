using BTL_HSK.Reports;
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
using CrystalDecisions.CrystalReports.Engine;

namespace BTL_HSK
{
    public partial class frmTBCanBaoHanh : Form
    {
        public frmTBCanBaoHanh()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();

        private void frmTBCanBaoHanh_Load(object sender, EventArgs e)
        {
            loadCBHangHoa();
            showDSBH();
        }

        void loadCBHangHoa()
        {
            string con = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con))
            {
                var cmd = new SqlCommand("SELECT * FROM dbo.tblHangHoa", connection);
                connection.Open();
                var er = cmd.ExecuteReader();

                var dt = new DataTable();
                dt.Load(er);

                cbThietBi.DisplayMember = "sTenHang";
                cbThietBi.ValueMember = "PK_MaHang";
                cbThietBi.DataSource = dt;
                connection.Close();
            }//con
        }
        private DataTable getTBBH()
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            SqlCommand cmd = new SqlCommand("hienTBBH", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable tblTBBH = new DataTable("tblThietBiCanBaoHanh");
                da.Fill(tblTBBH);
                return tblTBBH;
            }
        }
        public void showDSBH()
        {
            using (DataTable tblTBBH = getTBBH())
            {
                DataView dv = new DataView(tblTBBH);
                dgvTBBH.AutoGenerateColumns = false;
                dgvTBBH.DataSource = dv;
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlCommand cmd = new SqlCommand("addTBBH", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@mabh", txtMaBH.Text);
                cmd.Parameters.AddWithValue("@tentb", cbThietBi.Text);
                
                cmd.Parameters.AddWithValue("@tinhtrang", txtTinhTrang.Text);
                cmd.Parameters.AddWithValue("@nguyennhan", txtNguyenNhan.Text);
                cmd.Parameters.AddWithValue("@tgbh", Convert.ToDateTime(mtbTGNhanBH.Text));
                cmd.Parameters.AddWithValue("@tghetbh", Convert.ToDateTime(mtbTGHetHB.Text));


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Thiết bị bảo hành đã thêm thành công", "Kết quả",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                showDSBH();
                btnReset_Click(sender, e);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Mã bảo hành này đã tồn tại!!", "Kết quả",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                if (ex.Message.Contains("fk_TBBH"))
                {
                    MessageBox.Show("Mã hàng này không tồn tại!!", "Kết quả",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                if (ex.Message.Contains("checkdatebh"))
                {
                    MessageBox.Show("Ngày nhận bảo hành phải trước ngày hết hạn bảo hành!!", "Kết quả",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                btnReset_Click(sender, e);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMaBH.Text =
            cbThietBi.Text =
            
            txtTinhTrang.Text =
            txtNguyenNhan.Text =
            mtbTGNhanBH.Text =
            mtbTGHetHB.Text = string.Empty;
            showDSBH();
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dvTBBH = (DataView)dgvTBBH.DataSource;
                DataRowView drvTBBH = dvTBBH[dgvTBBH.CurrentRow.Index];
                string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlCommand cmd = new SqlCommand("updateTBBH", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@mabh", drvTBBH["PK_MaBH"]);
                cmd.Parameters.AddWithValue("@tentb", drvTBBH["sTenhang"]);
                
                cmd.Parameters.AddWithValue("@tinhtrang", txtTinhTrang.Text);
                cmd.Parameters.AddWithValue("@nguyennhan", txtNguyenNhan.Text);
                cmd.Parameters.AddWithValue("@tgbh", Convert.ToDateTime(mtbTGNhanBH.Text));
                cmd.Parameters.AddWithValue("@tghetbh", Convert.ToDateTime(mtbTGHetHB.Text));


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sửa thành công", "Kết quả",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                showDSBH();
                btnReset_Click(sender, e);
            }
            catch (Exception ex)
            {

            }
        }

        private void dgvTBBH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMaBH.Text = dgvTBBH[0, e.RowIndex].Value.ToString();
                cbThietBi.Text = dgvTBBH[1, e.RowIndex].Value.ToString();
                
                txtTinhTrang.Text = dgvTBBH[2, e.RowIndex].Value.ToString();
                txtNguyenNhan.Text = dgvTBBH[3, e.RowIndex].Value.ToString();
                mtbTGNhanBH.Text = dgvTBBH[4, e.RowIndex].Value.ToString();
                mtbTGHetHB.Text = dgvTBBH[5, e.RowIndex].Value.ToString();
            }
            catch
            {

            }
            btnThem.Enabled = false;
            btnSua.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            try
            {
                DataView dvTBBH = (DataView)dgvTBBH.DataSource;
                DataRowView drvTBBH = dvTBBH[dgvTBBH.CurrentRow.Index];
                string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("delTBBH", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@mabh", drvTBBH["PK_MaBH"]);
                        cmd.Parameters.AddWithValue("@tentb", drvTBBH["sTenhang"]);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        MessageBox.Show("Xóa thành công", "Kết quả",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnReset_Click(sender, e);
                        showDSBH();
                    }
                }
            }
            catch(Exception ex)
            {

            }      
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string timkiem = "select * from tblThietBiCanBaoHanh where PK_MaBH is not null ";
            if (!string.IsNullOrEmpty(txtMaBH.Text.Trim()))
                timkiem += string.Format(" AND PK_MaBH like '%{0}%'", txtMaBH.Text);
            if (!string.IsNullOrEmpty(cbThietBi.Text.Trim()))
                timkiem += string.Format(" AND sTenhang like N'%{0}%'", cbThietBi.Text);
            if (!string.IsNullOrEmpty(mtbTGNhanBH.Text.Trim()))
                timkiem += string.Format(" AND dTGNhanBH > '{0}'", mtbTGNhanBH.Text);
            if (!string.IsNullOrEmpty(mtbTGHetHB.Text.Trim()))
                timkiem += string.Format(" AND dTGNhanBH < '{0}'", mtbTGHetHB.Text);
            con.Open();
            using (SqlCommand cmd = new SqlCommand(timkiem, con))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dgvTBBH.DataSource = dt;
                }
            }
            con.Close();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlCommand cmd = new SqlCommand("TBBHTheoTen", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ma", cbThietBi.SelectedValue);
            con.Open();
            
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    rptTBBH rpt = new rptTBBH();
                    
                    rpt.SetDataSource(dt);
                    rpt.SetParameterValue("name", cbThietBi.SelectedValue.ToString());
                


                    frmRPTBBH f = new frmRPTBBH();
                    f.crvTBBH.ReportSource = rpt;
                    
                    f.Show();

                }
            
            con.Close();

        }

        private void mtbTGNhanBH_TextChanged(object sender, EventArgs e)
        {
            DateTime d;
            DateTime.TryParse(mtbTGNhanBH.Text.Trim(),out d);

        }

        private void mtbTGHetHB_TextChanged(object sender, EventArgs e)
        {
            DateTime d;
            DateTime.TryParse(mtbTGHetHB.Text.Trim(), out d);
        }
    }
}
