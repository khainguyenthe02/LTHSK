
namespace BTL_HSK
{
    partial class frmRPKH
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
            this.crvKH = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crvKH
            // 
            this.crvKH.ActiveViewIndex = -1;
            this.crvKH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvKH.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvKH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvKH.Location = new System.Drawing.Point(0, 0);
            this.crvKH.Name = "crvKH";
            this.crvKH.Size = new System.Drawing.Size(800, 450);
            this.crvKH.TabIndex = 0;
            // 
            // frmRPKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.crvKH);
            this.Name = "frmRPKH";
            this.Text = "frmRPKH";
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crvKH;
    }
}