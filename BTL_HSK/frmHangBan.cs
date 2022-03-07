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
    public partial class frmHangBan : Form
    {
        public frmHangBan()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlCommand cmd = new SqlCommand("addHB", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@mahd", txtMaHoaDon.Text);
                cmd.Parameters.AddWithValue("@mahang", txtMaHang.Text);
                cmd.Parameters.AddWithValue("@sl", txtSoLuong.Text);
                cmd.Parameters.AddWithValue("@giaban", txtGiaBan.Text);
                cmd.Parameters.AddWithValue("@tgmua", Convert.ToDateTime(mtbNgayBan.Text));
                cmd.Parameters.AddWithValue("@tgbh", Convert.ToDateTime(mtbTGBaoHanh.Text));


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Thêm mặt hàng thành công", "Kết quả",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                showDSHB();
                btnReset_Click(sender, e);
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("Subquery returned more than 1 value"))
                {
                    MessageBox.Show("Hóa đơn đã có mặt hàng này!!", "Kết quả",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                if (ex.Message.Contains("checksl"))
                {
                    MessageBox.Show("Ngày mua phải trước ngày bảo hành!!", "Kết quả",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                if (ex.Message.Contains("fk_hhb"))
                {
                    MessageBox.Show("Hóa đơn này không tồn tại!!", "Kết quả",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                if (ex.Message.Contains("checkhb"))
                {
                    MessageBox.Show("Số lượng hàng trong kho không đủ!!", "Kết quả",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void frmHangBan_Load(object sender, EventArgs e)
        {            
            showDSHB();
        }
        private DataTable getHB()
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            SqlCommand cmd = new SqlCommand("hienHB", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable tblHB = new DataTable("tblHangBan");
                da.Fill(tblHB);
                return tblHB;
            }
        }

        public void showDSHB()
        {
            using (DataTable tblHB = getHB())
            {
                DataView dv = new DataView(tblHB);
                dgvHangBan.AutoGenerateColumns = false;
                dgvHangBan.DataSource = dv;
            }
            con.Close();
        }


        private void frmHangBan_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMaHang.Text =
            txtMaHoaDon.Text =
            txtSoLuong.Text =
            txtGiaBan.Text =
            mtbNgayBan.Text =
            mtbTGBaoHanh.Text = string.Empty;
            txtMaHoaDon.Focus();
        }

        private void dgvHangBan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMaHoaDon.Text = dgvHangBan[0, e.RowIndex].Value.ToString();
                txtMaHang.Text = dgvHangBan[1, e.RowIndex].Value.ToString();
                txtSoLuong.Text = dgvHangBan[2, e.RowIndex].Value.ToString();
                txtGiaBan.Text = dgvHangBan[3, e.RowIndex].Value.ToString();
                mtbNgayBan.Text = dgvHangBan[4, e.RowIndex].Value.ToString();
                mtbTGBaoHanh.Text = dgvHangBan[5, e.RowIndex].Value.ToString();
                
            }
            catch (Exception ex)
            {

            }
            btnThem.Enabled = false;
            btnSua.Enabled = true;

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dvHB = (DataView)dgvHangBan.DataSource;
                DataRowView drvHB = dvHB[dgvHangBan.CurrentRow.Index];
                string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("updateHB", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@mahd", drvHB["PK_MaHoaDon"]);
                        cmd.Parameters.AddWithValue("@mahang", drvHB["sMaHang"]);
                        cmd.Parameters.AddWithValue("@sl", txtSoLuong.Text);

                        cmd.Parameters.AddWithValue("@giaban", txtGiaBan.Text);
                        cmd.Parameters.AddWithValue("@tgmua", mtbNgayBan.Text);
                        cmd.Parameters.AddWithValue("@tgbh", mtbTGBaoHanh.Text);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        MessageBox.Show("Sửa mặt hàng bán ra thành công", "Kết quả",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                        showDSHB();
                        btnReset_Click(sender, e);
                        showDSHB();
                    }
                }
            }catch (Exception ex)
            {
                if (ex.Message.Contains("checkhb"))
                {
                    MessageBox.Show("Số lượng hàng trong kho không đủ!!", "Kết quả",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                if (ex.Message.Contains("checksl"))
                {
                    MessageBox.Show("Ngày mua phải trước ngày bảo hành!!", "Kết quả",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataView dvHB = (DataView)dgvHangBan.DataSource;
            DataRowView drvHB = dvHB[dgvHangBan.CurrentRow.Index];
            string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("delHB", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mahd", drvHB["PK_MaHoaDon"]);
                    cmd.Parameters.AddWithValue("@mahang", drvHB["sMaHang"]);

                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                    MessageBox.Show("Xóa mặt hàng bán ra thành công", "Kết quả",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    showDSHB();
                    btnReset_Click(sender, e);
                    showDSHB();
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("searchHB", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mahd", txtMaHoaDon.Text);
            cmd.Parameters.AddWithValue("@mahang", txtMaHang.Text);

            con.Open();
            cmd.ExecuteNonQuery();

            //Load dữ liệu
            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            dgvHangBan.DataSource = dt;
            con.Close();
        }
    }
}
