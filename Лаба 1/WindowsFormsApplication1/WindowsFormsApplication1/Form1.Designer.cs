namespace WindowsFormsApplication1
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MainCodeTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ButtonOfProceed = new System.Windows.Forms.Button();
            this.FinalResultsTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            resources.ApplyResources(this.fileToolStripMenuItem1, "fileToolStripMenuItem1");
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem1});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            // 
            // openFileToolStripMenuItem1
            // 
            resources.ApplyResources(this.openFileToolStripMenuItem1, "openFileToolStripMenuItem1");
            this.openFileToolStripMenuItem1.Name = "openFileToolStripMenuItem1";
            this.openFileToolStripMenuItem1.Click += new System.EventHandler(this.openFileToolStripMenuItem1_Click);
            // 
            // MainCodeTextBox
            // 
            resources.ApplyResources(this.MainCodeTextBox, "MainCodeTextBox");
            this.MainCodeTextBox.Name = "MainCodeTextBox";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            // 
            // ButtonOfProceed
            // 
            resources.ApplyResources(this.ButtonOfProceed, "ButtonOfProceed");
            this.ButtonOfProceed.Name = "ButtonOfProceed";
            this.ButtonOfProceed.UseVisualStyleBackColor = true;
            this.ButtonOfProceed.Click += new System.EventHandler(this.ButtonOfProceed_Click);
            // 
            // FinalResultsTextBox
            // 
            resources.ApplyResources(this.FinalResultsTextBox, "FinalResultsTextBox");
            this.FinalResultsTextBox.Name = "FinalResultsTextBox";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FinalResultsTextBox);
            this.Controls.Add(this.ButtonOfProceed);
            this.Controls.Add(this.MainCodeTextBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TextBox MainCodeTextBox;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button ButtonOfProceed;
        private System.Windows.Forms.TextBox FinalResultsTextBox;
    }
}

