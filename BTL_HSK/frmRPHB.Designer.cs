
namespace BTL_HSK
{
    partial class frmRPHB
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
            this.crvHB = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crvHB
            // 
            this.crvHB.ActiveViewIndex = -1;
            this.crvHB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvHB.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvHB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvHB.Location = new System.Drawing.Point(0, 0);
            this.crvHB.Name = "crvHB";
            this.crvHB.Size = new System.Drawing.Size(800, 450);
            this.crvHB.TabIndex = 0;
            // 
            // frmRPHB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.crvHB);
            this.Name = "frmRPHB";
            this.Text = "frmRPHB";
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crvHB;
    }
}