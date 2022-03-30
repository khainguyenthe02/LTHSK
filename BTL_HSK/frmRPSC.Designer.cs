
namespace BTL_HSK
{
    partial class frmRPSC
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
            this.crvNKSC = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crvNKSC
            // 
            this.crvNKSC.ActiveViewIndex = -1;
            this.crvNKSC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvNKSC.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvNKSC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvNKSC.Location = new System.Drawing.Point(0, 0);
            this.crvNKSC.Name = "crvNKSC";
            this.crvNKSC.Size = new System.Drawing.Size(800, 450);
            this.crvNKSC.TabIndex = 0;
            // 
            // frmRPSC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.crvNKSC);
            this.Name = "frmRPSC";
            this.Text = "frmRPSC";
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crvNKSC;
    }
}