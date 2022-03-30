using System;

using System.Data;

using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace BTL_HSK
{
    public partial class frmHangHoa : Form
    {
        public frmHangHoa()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();
        private void frmHangHoa_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            loadCBNhomHang();
            showDSHH();
        }
        private void frmHangHoa_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        // Combobox DS nhóm hàng
        void loadCBNhomHang()
        {
            string con = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(con))
            {
                var cmd = new SqlCommand("SELECT * FROM dbo.tblNhomHang", connection);
                connection.Open();
                var er = cmd.ExecuteReader();

                var dt = new DataTable();
                dt.Load(er);

                cbNhomHang.DisplayMember = "sTenNhomHang";
                cbNhomHang.DataSource = dt;
                connection.Close();
            }//con
        }
        //Lấy DSHH từ SQL
        private DataTable getHH()
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            SqlCommand cmd = new SqlCommand("hienHH", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable tblHH = new DataTable("tblHangHoa");
                da.Fill(tblHH);
                return tblHH;
            }
        }
        public void showDSHH()
        {
            using (DataTable tblHH = getHH())
            {
                DataView dv = new DataView(tblHH);
                dgvHangHoa.AutoGenerateColumns = false;
                dgvHangHoa.DataSource = dv;
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlCommand cmd = new SqlCommand("addDSHH", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@mahang", txtMaHang.Text);
                cmd.Parameters.AddWithValue("@tenhang", txtTenHang.Text);
                cmd.Parameters.AddWithValue("@mausac", txtMauSac.Text);
                cmd.Parameters.AddWithValue("@soluong", txtSoLuong.Text);
                cmd.Parameters.AddWithValue("@giaban", txtGiaBan.Text);
                cmd.Parameters.AddWithValue("@hsx", txtHSX.Text);
                cmd.Parameters.AddWithValue("@tennhom", cbNhomHang.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Thêm mặt hàng thành công", "Kết quả",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                showDSHH();
                btnReset_Click(sender, e);
            }
            catch (Exception ex)
            {
                

                if (ex.Message.Contains("PRIMARY KEY")) {
                    MessageBox.Show("Mã hàng hóa đã tồn tại!!", "Kết quả",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                if (ex.Message.Contains("FOREIGN KEY")) {
                    MessageBox.Show("Mã nhóm hàng không tồn tại!!", "Kết quả",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                btnReset_Click(sender, e);
            }

        }//end Thêm

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMaHang.Text =
            txtTenHang.Text =
            txtMauSac.Text =
            txtSoLuong.Text =
            txtGiaBan.Text =
            txtHSX.Text =
            cbNhomHang.Text = string.Empty;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            showDSHH();

        }

        private void txtMaHang_TextChanged(object sender, EventArgs e)
        {
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled =
                                (txtMaHang.Text.Trim().Length > 0)
                              && (txtTenHang.Text.Trim().Length > 0)
                              ;
        }

        private void dgvHangHoa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMaHang.Text = dgvHangHoa[0, e.RowIndex].Value.ToString();
                txtTenHang.Text = dgvHangHoa[1, e.RowIndex].Value.ToString();
                txtMauSac.Text = dgvHangHoa[2, e.RowIndex].Value.ToString();
                txtSoLuong.Text = dgvHangHoa[3, e.RowIndex].Value.ToString();
                txtGiaBan.Text = dgvHangHoa[4, e.RowIndex].Value.ToString();
                txtHSX.Text = dgvHangHoa[5, e.RowIndex].Value.ToString();
                cbNhomHang.Text = dgvHangHoa[6, e.RowIndex].Value.ToString();
            }
            catch (Exception ex)
            {

            }
            btnThem.Enabled = false;
            btnSua.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dvHH = (DataView)dgvHangHoa.DataSource;
                DataRowView drvHH = dvHH[dgvHangHoa.CurrentRow.Index];
                string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("delHH", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@mahang", drvHH["PK_MaHang"]);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        MessageBox.Show("Xóa thành công", "Kết quả",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnReset_Click(sender, e);
                        showDSHH();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("REFERENCE"))
                    MessageBox.Show("Hàng hóa có dữ liệu liên quan"
                      , "Kết quả"
                      , MessageBoxButtons.OK
                      , MessageBoxIcon.Information);
                else
                    MessageBox.Show("Đã có lỗi xảy ra!"
                      , "Kết quả"
                      , MessageBoxButtons.OK
                      , MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dvHH = (DataView)dgvHangHoa.DataSource;
                DataRowView drvHH = dvHH[dgvHangHoa.CurrentRow.Index];
                string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("updateHH", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@mahang", drvHH["PK_MaHang"]);
                        cmd.Parameters.AddWithValue("@tenhang", txtTenHang.Text);
                        cmd.Parameters.AddWithValue("@mausac", txtMauSac.Text);
                        cmd.Parameters.AddWithValue("@soluong", txtSoLuong.Text);
                        cmd.Parameters.AddWithValue("@giaban", txtGiaBan.Text);
                        cmd.Parameters.AddWithValue("@hsx", txtHSX.Text);
                        cmd.Parameters.AddWithValue("@tennhom", cbNhomHang.Text);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        MessageBox.Show("Sửa mặt hàng thành công", "Kết quả",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnReset_Click(sender, e);
                        showDSHH();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string timkiem = "select * from tblHangHoa where PK_MaHang is not null";
            
            if (!string.IsNullOrEmpty(txtTenHang.Text.Trim()))
                timkiem += string.Format(" AND sTenHang like N'%{0}%'", txtTenHang.Text);
            if (!string.IsNullOrEmpty(txtMauSac.Text.Trim()))
                timkiem += string.Format(" AND sMauSac like N'%{0}%'", txtMauSac.Text);
            if (!string.IsNullOrEmpty(txtSoLuong.Text.Trim()))
                timkiem += string.Format(" AND iSoLuong > {0}", Int32.Parse(txtSoLuong.Text));
            if (!string.IsNullOrEmpty(txtGiaBan.Text.Trim()))
                timkiem += string.Format(" AND fGiaBan > {0}", float.Parse(txtGiaBan.Text));
            if (!string.IsNullOrEmpty(txtHSX.Text.Trim()))
                timkiem += string.Format(" AND sHangSX like N'%{0}%'", txtHSX.Text);
            if (!string.IsNullOrEmpty(cbNhomHang.Text.Trim()))
                timkiem += string.Format(" AND sTenNhomHang like '%{0}%'", cbNhomHang.Text);
            con.Open();
            using (SqlCommand cmd = new SqlCommand(timkiem, con))
            {
                using (SqlDataAdapter sda  = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dgvHangHoa.DataSource = dt;
                }
            }
            con.Close();
            
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            frmRPHangHoa frmRPHangHoa = new frmRPHangHoa();
                       
            frmRPHangHoa.Show();
        }
    }

}
