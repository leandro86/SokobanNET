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

        private Sokoban _sokoban;
        private int _cellSize;

        public ChangeLevelForm()
        {
            InitializeComponent();

            _sokoban = new Sokoban();
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

            LevelCollection selectedLevelCollection = (LevelCollection)levelCollectionGrid.CurrentRow.DataBoundItem;
            _sokoban.LoadLevel(selectedLevelCollection[SelectedLevel - 1]);

            _cellSize = levelPreview.Width / Math.Max(_sokoban.Width, _sokoban.Height);

            levelPreview.Invalidate();
        }

        private void levelPreview_Paint(object sender, PaintEventArgs e)
        {           
            int y = 0;
            int x = 0;
            foreach (Sokoban.Element element in _sokoban)
            {
                switch (element)
                {
                    case Sokoban.Element.Wall:
                        e.Graphics.DrawImage(Properties.Resources.Wall, x * _cellSize, y * _cellSize, _cellSize, _cellSize);
                        break;
                    case Sokoban.Element.Box:
                    case Sokoban.Element.BoxOnGoal:
                        e.Graphics.DrawImage(Properties.Resources.Box, x * _cellSize, y * _cellSize, _cellSize, _cellSize);  
                        break;
                    case Sokoban.Element.Goal:
                        e.Graphics.DrawImage(Properties.Resources.Goal, x * _cellSize, y * _cellSize, _cellSize, _cellSize);
                        break;
                    case Sokoban.Element.Player:
                    case Sokoban.Element.PlayerOnGoal:
                        e.Graphics.DrawImage(Properties.Resources.Player, x * _cellSize, y * _cellSize, _cellSize, _cellSize);
                        break;
                    case Sokoban.Element.EndRow:
                        x = -1;
                        y++;
                        break;
                }
                x++;
            }
        }
    }
}
