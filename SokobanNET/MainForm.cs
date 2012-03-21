using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SokobanLogic;

namespace SokobanNET
{
    public partial class MainForm : Form
    {
        private LevelCollection _levelCollection;
        private Sokoban _sokoban;

        private readonly int _cellSize = Properties.Resources.Wall.Width;
        private int _currentLevel;

        private readonly int _defaultFormWidth;
        private readonly int _defaultFormHeight;

        private readonly int _defaultBackgroundPanelWidth;
        private readonly int _defaultBackgroundPanelHeight;

        private bool _isLevelComplete;
        private bool _isLastLevel;

        public MainForm()
        {
            InitializeComponent();

            _defaultFormWidth = Width;
            _defaultFormHeight = Height;

            _defaultBackgroundPanelWidth = backgroundPanel.Width;
            _defaultBackgroundPanelHeight = backgroundPanel.Height;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _sokoban = new Sokoban();
            _sokoban.LevelCompleted += new EventHandler(OnLevelCompleted);

            //GoToLevel(++_currentLevel);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (_levelCollection == null)
            {
                return;
            }
            
            if (_isLevelComplete)
            {
                if (!_isLastLevel && e.KeyCode == Keys.Enter)
                {
                    GoToLevel(_currentLevel + 1);
                }
            }
            else
            {
                bool hasMoved = false;   
             
                switch (e.KeyCode)
                {
                    case Keys.Up:    
                        hasMoved = _sokoban.MovePlayer(Sokoban.MoveDirection.Up);
                        break;
                    case Keys.Down:
                        hasMoved = _sokoban.MovePlayer(Sokoban.MoveDirection.Down);
                        break;
                    case Keys.Right:
                        hasMoved = _sokoban.MovePlayer(Sokoban.MoveDirection.Right);
                        break;
                    case Keys.Left:
                        hasMoved = _sokoban.MovePlayer(Sokoban.MoveDirection.Left);
                        break;
                    case Keys.Space:
                        // for testing...
                        GoToLevel(_currentLevel + 1);
                        break;
                    case Keys.Back:
                        UndoMovement();
                        break;
                }

                if (hasMoved)
                {
                    undoMenuItem.Enabled = true;
                    undoButton.Enabled = true;
                }

                drawingArea.Invalidate();
            }
        }

        private void drawingArea_Paint(object sender, PaintEventArgs e)
        {
            if (_levelCollection != null)
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
                        e.Graphics.DrawImage(imageToDraw, element.Column * _cellSize, element.Row * _cellSize, _cellSize,
                                             _cellSize);
                    }
                }
            }
        }

        private void OnLevelCompleted(object sender, EventArgs e)
        {
            _isLevelComplete = true;
            undoMenuItem.Enabled = false;
            undoButton.Enabled = false;

            if (_currentLevel + 1 == _levelCollection.NumberOfLevels)
            {
                statusLabel.Text = "Level Completed! No more levels in the collection.";
                _isLastLevel = true;
            }
            else
            {
                statusLabel.Text = "Level Completed! Press enter to go to the next level...";
            }            

            drawingArea.Invalidate();
        }

        private void GoToLevel(int levelNumber)
        {           
            _sokoban.LoadLevel(_levelCollection[levelNumber]);
            _currentLevel = levelNumber;
            
            drawingArea.Width = _levelCollection[levelNumber].Width * _cellSize;
            drawingArea.Height = _levelCollection[levelNumber].Height * _cellSize;

            // some code to resize the form to fit the level size, and also to center the level in the form
            int formNewWidth = drawingArea.Width > _defaultBackgroundPanelWidth
                                   ? _defaultFormWidth + drawingArea.Width - _defaultBackgroundPanelWidth + 10
                                   : _defaultFormWidth;
            int formNewHeight = drawingArea.Height > _defaultBackgroundPanelHeight
                                    ? _defaultFormHeight + drawingArea.Height - _defaultBackgroundPanelHeight + 10
                                    : _defaultFormHeight;

            Size = new Size(formNewWidth, formNewHeight);
            //CenterToScreen();

            int x = backgroundPanel.Size.Width / 2 - drawingArea.Size.Width / 2;
            int y = backgroundPanel.Size.Height / 2 - drawingArea.Size.Height / 2;
            drawingArea.Location = new Point(x, y);

            _isLevelComplete = false;

            statusLabel.Text = "Playing";
            levelCollectionLabel.Text = _levelCollection.Title;
            levelLabel.Text = string.Format("{0} of {1}", _currentLevel + 1, _levelCollection.NumberOfLevels);
            
            restartMenuItem.Enabled = true;
            restartButton.Enabled = true;
            undoMenuItem.Enabled = false;
            undoButton.Enabled = false;

            drawingArea.Invalidate();
            drawingArea.Visible = true;            
        }

        private void restartMenuItem_Click(object sender, EventArgs e)
        {
            RestartLevel();
        }

        private void RestartLevel()
        {
            GoToLevel(_currentLevel);
        }

        private void undoMenuItem_Click(object sender, EventArgs e)
        {
            UndoMovement();
        }

        private void UndoMovement()
        {
            _sokoban.UndoMovement();
            undoMenuItem.Enabled = _sokoban.MovesHistoryCount > 0;
            undoButton.Enabled = _sokoban.MovesHistoryCount > 0;

            drawingArea.Invalidate();
        }

        private void changeLevelMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLevelForm changeLevelForm = new ChangeLevelForm();
            if (changeLevelForm.ShowDialog() == DialogResult.OK)
            {
                _levelCollection = changeLevelForm.SelectedCollection;
                GoToLevel(changeLevelForm.SelectedLevel);
            }
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            RestartLevel();
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            UndoMovement();
        }
    }
}
