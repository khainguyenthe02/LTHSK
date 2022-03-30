
namespace BTL_HSK
{
    partial class frmRPTBBH
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
            this.crvTBBH = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.rpTBBH1 = new BTL_HSK.Reports.rptTBBH();
            this.SuspendLayout();
            // 
            // crvTBBH
            // 
            this.crvTBBH.ActiveViewIndex = -1;
            this.crvTBBH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvTBBH.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvTBBH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvTBBH.Location = new System.Drawing.Point(0, 0);
            this.crvTBBH.Name = "crvTBBH";
            this.crvTBBH.Size = new System.Drawing.Size(724, 421);
            this.crvTBBH.TabIndex = 0;
            // 
            // frmRPTBBH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 421);
            this.Controls.Add(this.crvTBBH);
            this.Name = "frmRPTBBH";
            this.Text = "frmRPTBBH";
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crvTBBH;
        private Reports.rptTBBH rpTBBH1;
    }
}