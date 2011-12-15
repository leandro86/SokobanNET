using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SokobanNET
{
    public partial class ChangeLevelForm : Form
    {
        public LevelCollection SelectedCollection { get; private set; }
        public int SelectedLevel { get; private set; }
        
        public ChangeLevelForm()
        {
            InitializeComponent();

            levelCollectionGrid.AutoGenerateColumns = false;
        }

        private void ChangeLevelForm_Load(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles("Levels");
            List<LevelCollection> levels = new List<LevelCollection>();

            foreach (string file in files)
            {
                levels.Add(new LevelCollection(file));
            }

            levelCollectionGrid.DataSource = levels;
            
            levelCollectionGrid.Columns["title"].DataPropertyName = "Title";
            levelCollectionGrid.Columns["copyright"].DataPropertyName = "Copyright";
            levelCollectionGrid.Columns["levels"].DataPropertyName = "NumberOfLevels";
        }

        private void levelCollectionGrid_SelectionChanged(object sender, EventArgs e)
        {
            LevelCollection selectedLevelCollection = (LevelCollection)levelCollectionGrid.CurrentRow.DataBoundItem;
            levelsGrid.Rows.Clear();

            for (int i = 1; i <= selectedLevelCollection.NumberOfLevels; i++)
            {
                levelsGrid.Rows.Add("Level " + i);
            }

            SelectedCollection = selectedLevelCollection;
            levelCollectionDescriptionText.Text = selectedLevelCollection.Description;
        }

        private void levelsGrid_SelectionChanged(object sender, EventArgs e)
        {
            SelectedLevel = levelsGrid.CurrentRow.Index + 1;
        }
    }
}
