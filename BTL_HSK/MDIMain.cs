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
    public partial class MDIMain : Form
    {
        public MDIMain()
        {
            InitializeComponent();
        }

        //kiểm tra xem form con đó đã hiển thị hay chưa
        private bool checkExistForm(string name)
        {
            bool check = false;
            foreach (Form frm in this.MdiChildren)
            {
                check = true;
                break;
            }
            return check;

        }
        //check xem form đó đã active chưa
        private void ActiveChildForm(string name) {
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm.Name == name)
                    {
                        frm.Activate();
                        break;
                    }
                }
        }
       

        private void khoHàngHóaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!checkExistForm("frmRPHangHoa"))
            {
                frmRPHangHoa frmRPHangHoa = new frmRPHangHoa();
                frmRPHangHoa.MdiParent = this;
                gbMain.Hide();
                frmRPHangHoa.Show();
            }
            else
            {
                ActiveChildForm("frmRPHangHoa");
            }
        }


        

        private void MDIMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void HangHoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!checkExistForm("frmHangHoa"))
            {
                frmHangHoa frmHangHoa = new frmHangHoa();
                frmHangHoa.MdiParent = this;
                gbMain.Hide();
                frmHangHoa.Show();
            }
            else
            {
                ActiveChildForm("frmHangHoa");
            }
        }

        private void quảnLíTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!checkExistForm("frmQLTK"))
            {
                frmQLTK frmQLTK = new frmQLTK();
                frmQLTK.MdiParent = this;
                gbMain.Hide();
                
                frmQLTK.Show();
            }
            else
            {
                ActiveChildForm("frmQLTK");
            }
        }

        private void MDIMain_Load(object sender, EventArgs e)
        {
            
        }

        public void getData(string txtUser)
        {
            txtUsermain.Text = txtUser;
        }

        private void CTHDtoolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (!checkExistForm("frmHangBan"))
            {
                frmHangBan f = new frmHangBan();
                f.MdiParent = this;
                gbMain.Hide();
                f.Show();
            }
            else
            {
                ActiveChildForm("frmhangBan");
            }

        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!checkExistForm("frmKhachHang"))
            {
                frmKhachHang f = new frmKhachHang();
                f.MdiParent = this;
                gbMain.Hide();
                f.Show();
            }
            else
            {
                ActiveChildForm("frmKhachHang");
            }

        }

        private void HDtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!checkExistForm("frmHoaDon"))
            {
                frmHoaDon f = new frmHoaDon();
                f.MdiParent = this;
                gbMain.Hide();
                f.Show();
            }
            else
            {
                ActiveChildForm("frmHoaDon");
            }
        }

        private void nhậtKýSửaChữaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!checkExistForm("frmTBSC"))
            {
                frmTBSC f = new frmTBSC();
                f.MdiParent = this;
                gbMain.Hide();
                f.Show();
            }
            else
            {
                ActiveChildForm("frmTBSC");
            }
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
