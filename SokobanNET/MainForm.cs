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
        private int _currentLevel = 0;

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
            _levels = new LevelCollection("Levels\\Original.slc");
            
            _sokoban = new Sokoban();
            _sokoban.LevelCompleted += new EventHandler(sokoban_LevelCompleted);    

            ChangeLevel(_currentLevel);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (_isLevelComplete)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ChangeLevel(++_currentLevel);
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
                        ChangeLevel(++_currentLevel);
                        break;
                }
                drawingArea.Invalidate();
            }
        }

        private void drawingArea_Paint(object sender, PaintEventArgs e)
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
                        e.Graphics.DrawImage(Properties.Resources.Player, x * _cellSize, y * _cellSize);
                        break;
                    case Sokoban.Element.PlayerOnGoal:
                        e.Graphics.DrawImage(Properties.Resources.Goal, x * _cellSize, y * _cellSize);
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

        private void sokoban_LevelCompleted(object sender, EventArgs e)
        {
            drawingArea.Invalidate();

            _isLevelComplete = true;
            statusBarLabel.Text = "Level Completed! Press enter to go to the next level";
        }

        private void ChangeLevel(int levelIndex)
        {
            _sokoban.LoadLevel(_levels[levelIndex]);
            
            drawingArea.Width = _levels[levelIndex].Width * _cellSize;
            drawingArea.Height = _levels[levelIndex].Height * _cellSize;

            // some code to resize the form to fit the level size, and also to center the level in the form
            int formNewWidth = drawingArea.Width > _defaultBackgroundPanelWidth
                                   ? _defaultFormWidth + drawingArea.Width - _defaultBackgroundPanelWidth
                                   : _defaultFormWidth;
            int formNewHeight = drawingArea.Height > _defaultBackgroundPanelHeight
                                    ? _defaultFormHeight + drawingArea.Height - _defaultBackgroundPanelHeight
                                    : _defaultFormHeight;

            Size = new Size(formNewWidth + 10, formNewHeight + 10);

            int x = backgroundPanel.Size.Width / 2 - drawingArea.Size.Width / 2;
            int y = backgroundPanel.Size.Height / 2 - drawingArea.Size.Height / 2;

            drawingArea.Location = new Point(x, y);

            _isLevelComplete = false;
            statusBarLabel.Text = "";
        }
    }
}
