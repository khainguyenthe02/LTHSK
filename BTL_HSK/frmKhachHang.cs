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
    public partial class frmKhachHang : Form
    {
        public frmKhachHang()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();
        private DataTable getKh()
        {
            string con = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(con))
            {
                //Câu lệnh Truy Vấn SQL
                string sqlSelect = "SELECT * FROM tblKhachHang";
                using (SqlCommand cmd = new SqlCommand(sqlSelect, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable tbl = new DataTable("tblKhachHang");
                        da.Fill(tbl);
                        return tbl;
                    }
                }//cmd
            }//conn

        }
        public void showDSKH()
        {
            using (DataTable tblKhacHang = getKh())
            {
                DataView dvHH = new DataView(tblKhacHang);
                dgvKhachhang.AutoGenerateColumns = false;
                dgvKhachhang.DataSource = dvHH;

                
            }

        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            showDSKH();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string con = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(con))
                {
                    using (SqlCommand cmd = new SqlCommand("addKH", conn))
                    {
                        string gt = (rbNam.Checked == true) ? "Nam" : "Nữ";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@makh", txtMaKH.Text);
                        cmd.Parameters.AddWithValue("@hoten", txtHoten.Text);
                        cmd.Parameters.AddWithValue("@sdt", txtDT.Text);
                        cmd.Parameters.AddWithValue("@dc", txtDiaChi.Text);

                        cmd.Parameters.AddWithValue("@ns", Convert.ToDateTime(mtbNgaySinh.Text));
                        cmd.Parameters.AddWithValue("@gt", gt);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Thêm thành công"
                          , "Kết quả"
                          , MessageBoxButtons.OK
                          , MessageBoxIcon.Information);
                        btnReset_Click(sender, e);
                        showDSKH();

                    }
                }
            }// try
            catch(Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Mã khách hàng đã tồn tại!!", "Kết quả",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMaKH.Text = txtHoten.Text = txtDT.Text = txtDiaChi.Text = mtbNgaySinh.Text
                = string.Empty;
            txtMaKH.Focus();
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            showDSKH();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataView dvKH = (DataView)dgvKhachhang.DataSource;
            DataRowView drvKH = dvKH[dgvKhachhang.CurrentRow.Index];
            string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("updateKH", cnn))
                {
                    string gt = (rbNam.Checked == true) ? "Nam" : "Nữ";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@makh", drvKH["PK_MaKH"]);
                    cmd.Parameters.AddWithValue("@hoten", txtHoten.Text);
                    cmd.Parameters.AddWithValue("@sdt", txtDT.Text);
                    cmd.Parameters.AddWithValue("@dc", txtDiaChi.Text);

                    cmd.Parameters.AddWithValue("@ns", Convert.ToDateTime(mtbNgaySinh.Text));
                    cmd.Parameters.AddWithValue("@gt", gt);

                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                    MessageBox.Show("Sửa khách hàng thành công", "Kết quả",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnReset_Click(sender, e);
                    showDSKH();
                }
            }
        }

        private void dgvKhachhang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            txtMaKH.Text = dgvKhachhang[0, e.RowIndex].Value.ToString();
            txtHoten.Text = dgvKhachhang[1, e.RowIndex].Value.ToString();
            if (dgvKhachhang[2, e.RowIndex].Value.ToString() == "Nam")
            {
                rbNam.Checked = true;
            }
            if (dgvKhachhang[2, e.RowIndex].Value.ToString() == "Nữ")
            { rbNu.Checked = true; }
            mtbNgaySinh.Text = dgvKhachhang[3, e.RowIndex].Value.ToString();
            txtDT.Text = dgvKhachhang[4, e.RowIndex].Value.ToString();
            txtDiaChi.Text = dgvKhachhang[5, e.RowIndex].Value.ToString();
            btnThem.Enabled = false;
            btnSua.Enabled =  btnXoa.Enabled = true;

        }

        private void txtMaKH_TextChanged(object sender, EventArgs e)
        {
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled =
                                (txtMaKH.Text.Trim().Length > 0)
                              && (txtHoten.Text.Trim().Length > 0)
                              ;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dvKH = (DataView)dgvKhachhang.DataSource;
                DataRowView drvKH = dvKH[dgvKhachhang.CurrentRow.Index];
                string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("delKH", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@makh", drvKH["PK_MaKH"]);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        MessageBox.Show("Xóa thành công", "Kết quả",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnReset_Click(sender, e);
                        showDSKH();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("REFERENCE"))
                    MessageBox.Show("Khách hàng có dữ liệu liên quan"
                      , "Kết quả"
                      , MessageBoxButtons.OK
                      , MessageBoxIcon.Error);
                else
                    MessageBox.Show("Đã có lỗi xảy ra!"
                      , "Kết quả"
                      , MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
            }

        }//

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string timkiem = "SELECT * FROM dbo.tblKhachHang WHERE PK_MaKH IS NOT NULL ";
            if (!string.IsNullOrEmpty(txtMaKH.Text.Trim()))
                timkiem += string.Format(" AND PK_MaKH like '{0}'", txtMaKH.Text);
            if (!string.IsNullOrEmpty(txtHoten.Text.Trim()))
                timkiem += string.Format(" AND sTenKH like N'%{0}%'", txtHoten.Text);
            //if (!string.IsNullOrEmpty(mtbNgaySinh.Text.Trim()))
            //    timkiem += string.Format(" AND dNgaySinh > '{0}'", mtbNgaySinh.Text);
            string gt = (rbNam.Checked == true) ? "Nam" : "Nữ";
            if (gt != null)
                timkiem += string.Format(" AND sGioiTinh like N'%{0}%'", gt);
           
            if (!string.IsNullOrEmpty(txtDT.Text.Trim()))
                timkiem += string.Format(" AND sSDT like '{0}'", txtDT.Text);
            if (!string.IsNullOrEmpty(txtDiaChi.Text.Trim()))
                timkiem += string.Format(" AND sDiaChi like N'%{0}%'", txtDiaChi.Text);

            con.Open();
            using (SqlCommand cmd = new SqlCommand(timkiem, con))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable("tblKhachHang");
                    sda.Fill(dt);
                    dgvKhachhang.AutoGenerateColumns = false;
                    dgvKhachhang.DataSource = dt;
                }
            }
            con.Close();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlCommand cmd = new SqlCommand("rpKH", con);
            cmd.CommandType = CommandType.StoredProcedure;
            string gt = (rbNam.Checked == true) ? "Nam" : "Nữ";
            cmd.Parameters.AddWithValue("@gt", gt);
            con.Open();

            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                rpKH rpt = new rpKH();

                rpt.SetDataSource(dt);
                rpt.SetParameterValue("gt", gt);



                frmRPKH f = new frmRPKH();
                f.crvKH.ReportSource = rpt;

                f.Show();

            }

            con.Close();
        }
    }
}
