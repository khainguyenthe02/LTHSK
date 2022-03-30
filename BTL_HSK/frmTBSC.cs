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
    public partial class frmTBSC : Form
    {
        public frmTBSC()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();
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

        private void frmTBSC_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            loadCBHangHoa();
            loadCBNV();
            showDSBH();
        }
        private DataTable getTBBH()
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            SqlCommand cmd = new SqlCommand("hienNKSC", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable tblTBBH = new DataTable("tblNhatKiSuaChua");
                da.Fill(tblTBBH);
                return tblTBBH;
            }
        }
        public void showDSBH()
        {
            using (DataTable tblTBBH = getTBBH())
            {
                DataView dv = new DataView(tblTBBH);
                dgvSC.AutoGenerateColumns = false;
                dgvSC.DataSource = dv;
            }

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtGQ.Text = txtMaBH.Text = txtNguyenNhan.Text =
            mtbTGSua.Text= mtbTGTra.Text=    cbNV.Text = cbThietBi.Text = string.Empty;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            showDSBH();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlCommand cmd = new SqlCommand("addSC", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ma", txtMaBH.Text);
                cmd.Parameters.AddWithValue("@mahang", cbThietBi.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@manv", cbNV.SelectedValue.ToString());

                cmd.Parameters.AddWithValue("@gq", txtGQ.Text);
                cmd.Parameters.AddWithValue("@nn", txtNguyenNhan.Text);
                cmd.Parameters.AddWithValue("@tgsua", Convert.ToDateTime(mtbTGSua.Text));
                cmd.Parameters.AddWithValue("@tgtra", Convert.ToDateTime(mtbTGTra.Text));


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Nhật ký đã thêm thành công", "Kết quả",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                showDSBH();
                btnReset_Click(sender, e);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Mã phiếu này đã tồn tại!!", "Kết quả",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                MessageBox.Show(ex.Message);
            }
        }

        private void txtMaBH_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvSC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaBH.Text = dgvSC[0, e.RowIndex].Value.ToString();
            cbThietBi.Text = dgvSC[1, e.RowIndex].Value.ToString();
            cbNV.Text = dgvSC[4, e.RowIndex].Value.ToString();
            mtbTGTra.Text = dgvSC[6, e.RowIndex].Value.ToString();
            mtbTGSua.Text = dgvSC[5, e.RowIndex].Value.ToString();
            txtGQ.Text = dgvSC[3, e.RowIndex].Value.ToString();
            txtNguyenNhan.Text = dgvSC[2, e.RowIndex].Value.ToString();
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dvTBBH = (DataView)dgvSC.DataSource;
                DataRowView drvTBBH = dvTBBH[dgvSC.CurrentRow.Index];
                string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlCommand cmd = new SqlCommand("updateSC", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ma", drvTBBH["PK_MaSC"]);
                cmd.Parameters.AddWithValue("@mahang", cbThietBi.SelectedValue.ToString());

                cmd.Parameters.AddWithValue("@gq", txtGQ.Text);
                cmd.Parameters.AddWithValue("@nn", txtNguyenNhan.Text);
                cmd.Parameters.AddWithValue("@manv", cbNV.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@tgsua", Convert.ToDateTime(mtbTGSua.Text));
                cmd.Parameters.AddWithValue("@tgtra", Convert.ToDateTime(mtbTGTra.Text));


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
                if (ex.Message.Contains("DataRelation"))
                {
                    MessageBox.Show("Bạn không được phép thay đổi thiết bị này!!", "Kết quả",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dvTBBH = (DataView)dgvSC.DataSource;
                DataRowView drvTBBH = dvTBBH[dgvSC.CurrentRow.Index];
                string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("delSC", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ma", drvTBBH["PK_MaSC"]);
                        
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

            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string timkiem = "select * from dbo.dstbsc where PK_MaSC is not null ";
            if (!string.IsNullOrEmpty(txtMaBH.Text.Trim()))
                timkiem += string.Format(" AND PK_MaSC like '%{0}%'", txtMaBH.Text);
            if (!string.IsNullOrEmpty(cbThietBi.Text.Trim()))
                timkiem += string.Format(" AND sMaHang like '%{0}%'", cbThietBi.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(cbNV.Text.Trim()))
                timkiem += string.Format(" AND sMaNVSua like '%{0}%'", cbNV.SelectedValue.ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand(timkiem, con))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dgvSC.DataSource = dt;
                }
            }
            con.Close();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlCommand cmd = new SqlCommand("rpNKSC", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ma", txtMaBH.Text);
            con.Open();

            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                rpNKSC rpt = new rpNKSC();

                rpt.SetDataSource(dt);
                rpt.SetParameterValue("masc", txtMaBH.Text);



                frmRPSC f = new frmRPSC();
                f.crvNKSC.ReportSource = rpt;

                f.Show();

            }

            con.Close();
        }
    }
}
