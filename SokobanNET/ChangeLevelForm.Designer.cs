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
            this.levelsCollectionList = new System.Windows.Forms.ListBox();
            this.levelsList = new System.Windows.Forms.ListBox();
            this.levelCollectionDescriptionText = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // levelsCollectionList
            // 
            this.levelsCollectionList.FormattingEnabled = true;
            this.levelsCollectionList.Location = new System.Drawing.Point(12, 12);
            this.levelsCollectionList.Name = "levelsCollectionList";
            this.levelsCollectionList.Size = new System.Drawing.Size(238, 264);
            this.levelsCollectionList.TabIndex = 0;
            this.levelsCollectionList.SelectedIndexChanged += new System.EventHandler(this.levelsCollectionList_SelectedIndexChanged);
            // 
            // levelsList
            // 
            this.levelsList.FormattingEnabled = true;
            this.levelsList.Location = new System.Drawing.Point(256, 12);
            this.levelsList.Name = "levelsList";
            this.levelsList.Size = new System.Drawing.Size(139, 264);
            this.levelsList.TabIndex = 1;
            this.levelsList.SelectedIndexChanged += new System.EventHandler(this.levelsList_SelectedIndexChanged);
            // 
            // levelCollectionDescriptionText
            // 
            this.levelCollectionDescriptionText.Location = new System.Drawing.Point(12, 282);
            this.levelCollectionDescriptionText.Name = "levelCollectionDescriptionText";
            this.levelCollectionDescriptionText.ReadOnly = true;
            this.levelCollectionDescriptionText.Size = new System.Drawing.Size(383, 69);
            this.levelCollectionDescriptionText.TabIndex = 2;
            this.levelCollectionDescriptionText.Text = "";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(239, 356);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(320, 356);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // ChangeLevelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 391);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.levelCollectionDescriptionText);
            this.Controls.Add(this.levelsList);
            this.Controls.Add(this.levelsCollectionList);
            this.Name = "ChangeLevelForm";
            this.Text = "ChangeLevelForm";
            this.Load += new System.EventHandler(this.ChangeLevelForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox levelsCollectionList;
        private System.Windows.Forms.ListBox levelsList;
        private System.Windows.Forms.RichTextBox levelCollectionDescriptionText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}