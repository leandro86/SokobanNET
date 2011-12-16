using System;
using System.Drawing;
using System.Windows.Forms;

namespace SokobanNET
{
    public partial class MainForm : Form
    {
        private LevelCollection _levels;
        private Sokoban _sokoban;

        private readonly int _cellSize = Properties.Resources.Wall.Width;
        private int _currentLevel;

        private readonly int _defaultFormWidth;
        private readonly int _defaultFormHeight;

        private readonly int _defaultBackgroundPanelWidth;
        private readonly int _defaultBackgroundPanelHeight;

        private bool _isLevelComplete;

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
            _sokoban.LevelCompleted += new EventHandler(sokoban_LevelCompleted);

            //GoToLevel(++_currentLevel);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (_isLevelComplete)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    GoToLevel(++_currentLevel);
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        _sokoban.MovePlayer(Sokoban.MoveDirection.Up);
                        break;
                    case Keys.Down:
                        _sokoban.MovePlayer(Sokoban.MoveDirection.Down);
                        break;
                    case Keys.Right:
                        _sokoban.MovePlayer(Sokoban.MoveDirection.Right);
                        break;
                    case Keys.Left:
                        _sokoban.MovePlayer(Sokoban.MoveDirection.Left);
                        break;
                    case Keys.Space:
                        // for testing...
                        GoToLevel(++_currentLevel);
                        break;
                    case Keys.Back:
                        UndoMovement();
                        break;
                }
                drawingArea.Invalidate();
            }
        }

        private void drawingArea_Paint(object sender, PaintEventArgs e)
        {
            if (_levels != null)
            {
                int y = 0;
                int x = 0;
                foreach (Sokoban.Element element in _sokoban)
                {
                    switch (element)
                    {
                        case Sokoban.Element.Wall:
                            e.Graphics.DrawImage(Properties.Resources.Wall, x * _cellSize, y * _cellSize);
                            break;
                        case Sokoban.Element.Box:
                        case Sokoban.Element.BoxOnGoal:
                            e.Graphics.DrawImage(Properties.Resources.Box, x * _cellSize, y * _cellSize);  
                            break;
                        case Sokoban.Element.Goal:
                            e.Graphics.DrawImage(Properties.Resources.Goal, x * _cellSize, y * _cellSize);
                            break;
                        case Sokoban.Element.Player:
                        case Sokoban.Element.PlayerOnGoal:
                            e.Graphics.DrawImage(Properties.Resources.Player, x * _cellSize, y * _cellSize);
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

        private void sokoban_LevelCompleted(object sender, EventArgs e)
        {
            drawingArea.Invalidate();

            _isLevelComplete = true;
            statusBarLabel.Text = "Level Completed! Press enter to go to the next level...";
        }

        private void GoToLevel(int levelNumber)
        {
            _sokoban.LoadLevel(_levels[levelNumber]);
            
            drawingArea.Width = _levels[levelNumber].Width * _cellSize;
            drawingArea.Height = _levels[levelNumber].Height * _cellSize;

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
            statusBarLabel.Text = "";

            drawingArea.Invalidate();
        }

        private void restartMenuItem_Click(object sender, EventArgs e)
        {
            GoToLevel(_currentLevel);
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void undoMenuItem_Click(object sender, EventArgs e)
        {
            UndoMovement();
        }

        private void UndoMovement()
        {
            _sokoban.UndoMovement();
            drawingArea.Invalidate();
        }

        private void changeLevelMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLevelForm changeLevelForm = new ChangeLevelForm();
            if (changeLevelForm.ShowDialog() == DialogResult.OK)
            {
                _levels = changeLevelForm.SelectedCollection;
                _currentLevel = changeLevelForm.SelectedLevel;

                GoToLevel(_currentLevel);

                drawingArea.Visible = true;
            }
        }
    }
}
