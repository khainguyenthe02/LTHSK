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
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            try
            {
                
                string sql = "SELECT * FROM dbo.tblNhanVien where [sTenDangNhap] = '" + txtUser.Text +
                            "' and [sMatKhau] = '" + txtMk.Text+"'";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    MessageBox.Show("Đăng nhập thành công");
                    txtMk.Text = txtUser.Text = string.Empty;
                    MDIMain mdi = new MDIMain();
                    mdi.getData(txtUser.Text);
                    txtTB.Hide();
                    mdi.Show();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác", "Đăng nhập thất bại",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (txtUser.Text.Trim().Length == 0 || txtMk.Text.Trim().Length == 0)
                {
                    txtTB.Show();
                    
                    txtTB.Text = "Vui lòng điền đầy đủ!!!!";
                    
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {
            btnDangnhap.Enabled = 
                                    (txtMk.Text.Trim().Length > 0) &&
                                    (txtUser.Text.Trim().Length > 0);

        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            txtTB.Hide();
        }
    }
}
