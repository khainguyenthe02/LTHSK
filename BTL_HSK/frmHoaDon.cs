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

namespace BTL_HSK
{
    public partial class frmHoaDon : Form
    {
        public frmHoaDon()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();
        void loadCBNV()
        {
            string con = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con))
            {
                var cmd = new SqlCommand("SELECT * FROM dbo.tblNhanVien", connection);
                connection.Open();
                var er = cmd.ExecuteReader();

                var dt = new DataTable();
                dt.Load(er);

                cbNV.DisplayMember = "sTenNV";
                cbNV.ValueMember = "PK_MaNV";
                cbNV.DataSource = dt;
                connection.Close();
            }//con
        }
        void loadCBKH()
        {
            string con = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con))
            {
                var cmd = new SqlCommand("SELECT * FROM dbo.tblKhachHang", connection);
                connection.Open();
                var er = cmd.ExecuteReader();

                var dt = new DataTable();
                dt.Load(er);

                cbKH.DisplayMember = "sTenKH";
                cbKH.ValueMember = "PK_MaKH";
                cbKH.DataSource = dt;
                connection.Close();
            }//con
        }
        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            loadCBNV();
            loadCBKH();
            showDSBH();
        }
        private DataTable getTBBH()
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            SqlCommand cmd = new SqlCommand("prdshd", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable tblTBBH = new DataTable("tblHoaDonBanHang");
                da.Fill(tblTBBH);
                return tblTBBH;
            }
        }
        public void showDSBH()
        {
            using (DataTable tblTBBH = getTBBH())
            {
                DataView dv = new DataView(tblTBBH);
                dgvHD.AutoGenerateColumns = false;
                dgvHD.DataSource = dv;
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string gt = (rbYes.Checked == true) ? "Rồi" : "Chưa";
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlCommand cmd = new SqlCommand("addHD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ma", txtMaHD.Text);
                cmd.Parameters.AddWithValue("@tttt", gt);
                cmd.Parameters.AddWithValue("@makh", cbKH.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@manv", cbNV.SelectedValue.ToString());
                
                cmd.Parameters.AddWithValue("@tglap", Convert.ToDateTime(mtbTGLap.Text));
                


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Hóa đơn đã thêm thành công", "Kết quả",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                showDSBH();
                btnReset_Click(sender, e);
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Mã hóa đơn đã tồn tại!!", "Kết quả",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMaHD.Text = cbKH.Text = cbNV.Text = mtbTGLap.Text = string.Empty;
            txtMaHD.Focus();
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            showDSBH();
        }

        private void txtMaHD_TextChanged(object sender, EventArgs e)
        {
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = txtMaHD.Text.Trim().Length > 0;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string gt = (rbYes.Checked == true) ? "Rồi" : "Chưa";
                DataView dvTBBH = (DataView)dgvHD.DataSource;
                DataRowView drvTBBH = dvTBBH[dgvHD.CurrentRow.Index];
               
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlCommand cmd = new SqlCommand("updateHD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ma", drvTBBH["PK_MaHoaDon"]);
                cmd.Parameters.AddWithValue("@tttt", gt);
                cmd.Parameters.AddWithValue("@makh", cbKH.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@manv", cbNV.SelectedValue.ToString());

                cmd.Parameters.AddWithValue("@tglap", Convert.ToDateTime(mtbTGLap.Text));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sửa thành công", "Kết quả",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                showDSBH();
                btnReset_Click(sender, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaHD.Text = dgvHD[0, e.RowIndex].Value.ToString();
            cbKH.Text = dgvHD[1, e.RowIndex].Value.ToString();
            cbNV.Text = dgvHD[2, e.RowIndex].Value.ToString();
            if (dgvHD[5, e.RowIndex].Value.ToString() == "Rồi")
            {
                rbYes.Checked = true;
            }
            if (dgvHD[5, e.RowIndex].Value.ToString() == "Chưa")
            { rbNo.Checked = true; }
            mtbTGLap.Text = dgvHD[3, e.RowIndex].Value.ToString();
            
            btnThem.Enabled = false;
            btnSua.Enabled = btnXoa.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dvTBBH = (DataView)dgvHD.DataSource;
                DataRowView drvTBBH = dvTBBH[dgvHD.CurrentRow.Index];
                string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("delHD", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ma", drvTBBH["PK_MaHoaDon"]);

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
            catch (Exception ex)
            {
                if (ex.Message.Contains("REFERENCE"))
                    MessageBox.Show("Hóa đơn có dữ liệu liên quan"
                      , "Kết quả"
                      , MessageBoxButtons.OK
                      , MessageBoxIcon.Error);
               
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string timkiem = " SELECT * FROM dbo.dshd where PK_MaHoaDon is not null ";
            if (!string.IsNullOrEmpty(txtMaHD.Text.Trim()))
                timkiem += string.Format(" AND PK_MaHoaDon like '%{0}%'", txtMaHD.Text);
            if (!string.IsNullOrEmpty(cbKH.Text.Trim()))
                timkiem += string.Format(" AND sMaKH like '%{0}%'", cbKH.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(cbNV.Text.Trim()))
                timkiem += string.Format(" AND sMaNVLap like '%{0}%'", cbNV.SelectedValue.ToString());
            string gt = (rbYes.Checked == true) ? "Rồi" : "Chưa";
            if (gt != null)
                timkiem += string.Format(" AND bTTThuTien like N'%{0}%'", gt);
            con.Open();
            using (SqlCommand cmd = new SqlCommand(timkiem, con))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dgvHD.DataSource = dt;
                }
            }
            con.Close();

        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlCommand cmd = new SqlCommand("rpDSHD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@ma", txtMaHD.Text);
            con.Open();

            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                rpHD rpt = new rpHD();

                rpt.SetDataSource(dt);
                rpt.SetParameterValue("mahd", txtMaHD.Text);



                frmRPHD f = new frmRPHD();
                f.crvHD.ReportSource = rpt;

                f.Show();

            }

            con.Close();
        }
    }
}
