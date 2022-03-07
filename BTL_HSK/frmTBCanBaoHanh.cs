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
    public partial class frmTBCanBaoHanh : Form
    {
        public frmTBCanBaoHanh()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();

        private void frmTBCanBaoHanh_Load(object sender, EventArgs e)
        {
            showDSBH();
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
                cmd.Parameters.AddWithValue("@matb", txtMaTB.Text);
                
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
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMaBH.Text =
            txtMaTB.Text =
            
            txtTinhTrang.Text =
            txtNguyenNhan.Text =
            mtbTGNhanBH.Text =
            mtbTGHetHB.Text = string.Empty;
            btnSua.Enabled = btnXoa.Enabled = false;
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
                cmd.Parameters.AddWithValue("@matb", drvTBBH["sMaThietBi"]);
                
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
                txtMaTB.Text = dgvTBBH[1, e.RowIndex].Value.ToString();
                
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
                        cmd.Parameters.AddWithValue("@matb", drvTBBH["sMaThietBi"]);
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
            SqlCommand cmd = new SqlCommand("searchTBBH", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@mabh", txtMaBH.Text);
            cmd.Parameters.AddWithValue("@matb", txtMaTB.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            

            //Load dữ liệu
            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            dgvTBBH.DataSource = dt;
            con.Close();
        }
    }
}
