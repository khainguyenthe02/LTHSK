
namespace BTL_HSK
{
    partial class frmQLTK
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClose = new System.Windows.Forms.Button();
            this.btlCapnhat = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNhaplaiMK = new System.Windows.Forms.Label();
            this.txtNhaplai = new System.Windows.Forms.TextBox();
            this.btnMKmoi = new System.Windows.Forms.Label();
            this.txtMKmoi = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textMk2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTendangnhap2 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(368, 207);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Thoát";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btlCapnhat
            // 
            this.btlCapnhat.Location = new System.Drawing.Point(276, 207);
            this.btlCapnhat.Name = "btlCapnhat";
            this.btlCapnhat.Size = new System.Drawing.Size(75, 23);
            this.btlCapnhat.TabIndex = 13;
            this.btlCapnhat.Text = "Cập nhật";
            this.btlCapnhat.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNhaplaiMK);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.txtNhaplai);
            this.groupBox1.Controls.Add(this.btlCapnhat);
            this.groupBox1.Controls.Add(this.btnMKmoi);
            this.groupBox1.Controls.Add(this.txtMKmoi);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textMk2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTendangnhap2);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(659, 312);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin tài khoản";
            // 
            // btnNhaplaiMK
            // 
            this.btnNhaplaiMK.AutoSize = true;
            this.btnNhaplaiMK.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhaplaiMK.Location = new System.Drawing.Point(172, 140);
            this.btnNhaplaiMK.Name = "btnNhaplaiMK";
            this.btnNhaplaiMK.Size = new System.Drawing.Size(83, 19);
            this.btnNhaplaiMK.TabIndex = 0;
            this.btnNhaplaiMK.Text = "Nhập lại:";
            // 
            // txtNhaplai
            // 
            this.txtNhaplai.Location = new System.Drawing.Point(276, 139);
            this.txtNhaplai.Name = "txtNhaplai";
            this.txtNhaplai.Size = new System.Drawing.Size(268, 20);
            this.txtNhaplai.TabIndex = 1;
            // 
            // btnMKmoi
            // 
            this.btnMKmoi.AutoSize = true;
            this.btnMKmoi.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMKmoi.Location = new System.Drawing.Point(124, 107);
            this.btnMKmoi.Name = "btnMKmoi";
            this.btnMKmoi.Size = new System.Drawing.Size(131, 19);
            this.btnMKmoi.TabIndex = 0;
            this.btnMKmoi.Text = "Mật khẩu mới:";
            // 
            // txtMKmoi
            // 
            this.txtMKmoi.Location = new System.Drawing.Point(276, 108);
            this.txtMKmoi.Name = "txtMKmoi";
            this.txtMKmoi.Size = new System.Drawing.Size(268, 20);
            this.txtMKmoi.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(161, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Mật khẩu:";
            // 
            // textMk2
            // 
            this.textMk2.Location = new System.Drawing.Point(276, 74);
            this.textMk2.Name = "textMk2";
            this.textMk2.Size = new System.Drawing.Size(268, 20);
            this.textMk2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(116, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên đăng nhập:";
            // 
            // txtTendangnhap2
            // 
            this.txtTendangnhap2.Location = new System.Drawing.Point(276, 43);
            this.txtTendangnhap2.Name = "txtTendangnhap2";
            this.txtTendangnhap2.Size = new System.Drawing.Size(268, 20);
            this.txtTendangnhap2.TabIndex = 1;
            // 
            // frmQLTK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 361);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmQLTK";
            this.Text = "Quản lý tài khoản";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btlCapnhat;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textMk2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTendangnhap2;
        private System.Windows.Forms.TextBox txtNhaplai;
        private System.Windows.Forms.Label btnNhaplaiMK;
        private System.Windows.Forms.TextBox txtMKmoi;
        private System.Windows.Forms.Label btnMKmoi;
    }
}