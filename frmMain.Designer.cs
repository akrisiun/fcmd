// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace fcmd
{
    
    #region Windows Form Designer generated code
    public partial class frmMain
    {
        private void InitializeComponent()
        {
            this.lstFiles = new System.Windows.Forms.ListBox();
            // 
            // lstFiles
            // 
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Location = new System.Drawing.Point(64, 48);
            this.lstFiles.Size = new System.Drawing.Size(368, 303);
            this.lstFiles.BackColor = System.Drawing.SystemColors.Window;
            this.lstFiles.TabIndex = 1;
			this.lstFiles.DoubleClick += new System.EventHandler(this.lstFiles_DblClick);
            // 
            // frmMain
            // 
            this.Name = "frmMain";
            this.ClientSize = new System.Drawing.Size(754, 454);
            this.Controls.Add(this.lstFiles);
            this.Text = "File Commander";
            this.Load += new System.EventHandler(this.frmMain_Load);
        }
        private System.Windows.Forms.ListBox lstFiles;
    }
    #endregion
}