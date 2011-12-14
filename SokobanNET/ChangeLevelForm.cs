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
        }

        private void ChangeLevelForm_Load(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles("Levels");

            foreach (string file in files)
            {
                LevelCollection levelCollection = new LevelCollection(file);
                levelsCollectionList.Items.Add(levelCollection);

                levelCollectionDescriptionText.Text = levelCollection.Description;
            }
        }

        private void levelsCollectionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LevelCollection selectedLevelCollection = (LevelCollection)levelsCollectionList.SelectedItem;
            levelsList.Items.Clear();

            for (int i = 1; i <= selectedLevelCollection.NumberOfLevels; i++)
            {
                levelsList.Items.Add("Level " + i);
            }

            SelectedCollection = selectedLevelCollection;
        }

        private void levelsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedLevel = levelsList.SelectedIndex + 1;
        }
    }
}
