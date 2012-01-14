using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SokobanLogic;

namespace SokobanNET
{
    public partial class ChangeLevelForm : Form
    {
        public LevelCollection SelectedCollection { get; private set; }
        public int SelectedLevel { get; private set; }

        private Sokoban _sokoban;

        private int _cellSize;
        private int _paddingX;
        private int _paddingY;

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
            levelCollectionGrid.Columns["author"].DataPropertyName = "Author";
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
            SelectedLevel = levelsGrid.CurrentRow.Index;

            LevelCollection selectedLevelCollection = (LevelCollection)levelCollectionGrid.CurrentRow.DataBoundItem;
            _sokoban.LoadLevel(selectedLevelCollection[SelectedLevel]);

            _cellSize = levelPreview.Width / Math.Max(_sokoban.Width, _sokoban.Height);
            _paddingX = (levelPreview.Width - (_sokoban.Width * _cellSize)) / 2;
            _paddingY = (levelPreview.Height - (_sokoban.Height * _cellSize)) / 2;

            levelPreview.Invalidate();
        }

        private void levelPreview_Paint(object sender, PaintEventArgs e)
        {           
            foreach (Element element in _sokoban)
            {
                Bitmap imageToDraw = null;

                switch (element.Type)
                {
                    case ElementType.Wall:
                        imageToDraw = Properties.Resources.Wall;
                        break;
                    case ElementType.Box:
                    case ElementType.BoxOnGoal:
                        imageToDraw = Properties.Resources.Box;
                        break;
                    case ElementType.Goal:
                        imageToDraw = Properties.Resources.Goal;
                        break;
                    case ElementType.Player:
                    case ElementType.PlayerOnGoal:
                        imageToDraw = Properties.Resources.Player;
                        break;
                }

                if (imageToDraw != null)
                {
                    e.Graphics.DrawImage(imageToDraw, _paddingX + element.Column * _cellSize,
                                         _paddingY + element.Row * _cellSize,
                                         _cellSize, _cellSize);
                }
            }
        }
    }
}
