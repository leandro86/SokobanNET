namespace SokobanNET
{
    partial class ChangeLevelForm
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
            this.levelCollectionDescriptionText = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.levelCollectionGrid = new System.Windows.Forms.DataGridView();
            this.levelsGrid = new System.Windows.Forms.DataGridView();
            this.levelPreview = new System.Windows.Forms.PictureBox();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.levels = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.levelCollectionGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // levelCollectionDescriptionText
            // 
            this.levelCollectionDescriptionText.Location = new System.Drawing.Point(12, 282);
            this.levelCollectionDescriptionText.Name = "levelCollectionDescriptionText";
            this.levelCollectionDescriptionText.ReadOnly = true;
            this.levelCollectionDescriptionText.Size = new System.Drawing.Size(553, 63);
            this.levelCollectionDescriptionText.TabIndex = 2;
            this.levelCollectionDescriptionText.Text = "";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(409, 351);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(490, 351);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // levelCollectionGrid
            // 
            this.levelCollectionGrid.AllowUserToAddRows = false;
            this.levelCollectionGrid.AllowUserToDeleteRows = false;
            this.levelCollectionGrid.AllowUserToResizeRows = false;
            this.levelCollectionGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.levelCollectionGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.levelCollectionGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.title,
            this.author,
            this.levels});
            this.levelCollectionGrid.Location = new System.Drawing.Point(12, 12);
            this.levelCollectionGrid.MultiSelect = false;
            this.levelCollectionGrid.Name = "levelCollectionGrid";
            this.levelCollectionGrid.ReadOnly = true;
            this.levelCollectionGrid.RowHeadersVisible = false;
            this.levelCollectionGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.levelCollectionGrid.Size = new System.Drawing.Size(415, 264);
            this.levelCollectionGrid.TabIndex = 5;
            this.levelCollectionGrid.SelectionChanged += new System.EventHandler(this.levelCollectionGrid_SelectionChanged);
            // 
            // levelsGrid
            // 
            this.levelsGrid.AllowUserToAddRows = false;
            this.levelsGrid.AllowUserToDeleteRows = false;
            this.levelsGrid.AllowUserToResizeRows = false;
            this.levelsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.levelsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.levelsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.levelsGrid.Location = new System.Drawing.Point(433, 12);
            this.levelsGrid.MultiSelect = false;
            this.levelsGrid.Name = "levelsGrid";
            this.levelsGrid.ReadOnly = true;
            this.levelsGrid.RowHeadersVisible = false;
            this.levelsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.levelsGrid.Size = new System.Drawing.Size(132, 126);
            this.levelsGrid.TabIndex = 6;
            this.levelsGrid.SelectionChanged += new System.EventHandler(this.levelsGrid_SelectionChanged);
            // 
            // levelPreview
            // 
            this.levelPreview.BackgroundImage = global::SokobanNET.Properties.Resources.Floor;
            this.levelPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.levelPreview.Location = new System.Drawing.Point(431, 142);
            this.levelPreview.Name = "levelPreview";
            this.levelPreview.Size = new System.Drawing.Size(134, 134);
            this.levelPreview.TabIndex = 7;
            this.levelPreview.TabStop = false;
            this.levelPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.levelPreview_Paint);
            // 
            // title
            // 
            this.title.HeaderText = "Title";
            this.title.Name = "title";
            this.title.ReadOnly = true;
            this.title.Width = 200;
            // 
            // author
            // 
            this.author.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.author.HeaderText = "Author";
            this.author.Name = "author";
            this.author.ReadOnly = true;
            // 
            // levels
            // 
            this.levels.HeaderText = "Levels";
            this.levels.Name = "levels";
            this.levels.ReadOnly = true;
            this.levels.Width = 50;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.FillWeight = 149.2386F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Level";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // ChangeLevelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 384);
            this.Controls.Add(this.levelPreview);
            this.Controls.Add(this.levelsGrid);
            this.Controls.Add(this.levelCollectionGrid);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.levelCollectionDescriptionText);
            this.Name = "ChangeLevelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChangeLevelForm";
            this.Load += new System.EventHandler(this.ChangeLevelForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.levelCollectionGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox levelCollectionDescriptionText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView levelCollectionGrid;
        private System.Windows.Forms.DataGridView levelsGrid;
        private System.Windows.Forms.PictureBox levelPreview;
        private System.Windows.Forms.DataGridViewTextBoxColumn title;
        private System.Windows.Forms.DataGridViewTextBoxColumn author;
        private System.Windows.Forms.DataGridViewTextBoxColumn levels;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    }
}