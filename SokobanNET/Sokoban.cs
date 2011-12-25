using System;
using System.Collections;
using System.Collections.Generic;

namespace SokobanNET
{
    public class Sokoban : IEnumerable
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public enum MoveDirection
        {
            Up,
            Down,
            Right,
            Left
        }

        public event EventHandler LevelCompleted;
        
        private Element[][] _level;
        private Element _player;
        
        private int _goalsCount;
        private int _goalsFilled;

        private Stack<List<Element>> _movesHistory;
        
        public Sokoban(Level level)
        {
            LoadLevel(level);
        }

        public Sokoban()
        {
            _movesHistory = new Stack<List<Element>>();
        }

        public void LoadLevel(Level level)
        {
            Width = level.Width;
            Height = level.Height;

            _goalsCount = 0;
            _goalsFilled = 0;

            _level = new Element[level.Data.Length][];
            for (int row = 0; row < level.Data.Length; row++)
            {
                _level[row] = new Element[level.Data[row].Length];

                for (int column = 0; column < level.Data[row].Length; column++)
                {
                    _level[row][column] = new Element() {Row = row, Column = column};

                    switch (level.Data[row][column])
                    {
                        case '@':
                            _level[row][column].Type = ElementType.Player;
                            _player = _level[row][column];
                            break;
                        case '+':
                            _level[row][column].Type = ElementType.PlayerOnGoal;
                            _player = _level[row][column];
                            break;
                        case '#':
                            _level[row][column].Type = ElementType.Wall;
                            break;
                        case '$':
                            _level[row][column].Type = ElementType.Box;
                            break;
                        case '*':
                            _level[row][column].Type = ElementType.BoxOnGoal;
                            _goalsCount++;
                            _goalsFilled++;
                            break;
                        case '.':
                            _level[row][column].Type = ElementType.Goal;
                            _goalsCount++;
                            break;
                        case ' ':
                            _level[row][column].Type = ElementType.Floor;
                            break;
                    }
                }
            }

            _movesHistory.Clear();
        }

        public void MovePlayer(MoveDirection moveDirection)
        {
            int newPlayerRow = _player.Row;
            int newPlayerColumn = _player.Column;

            int newBoxRow = newPlayerRow;
            int newBoxColumn = newPlayerColumn;

            switch (moveDirection)
            {
                case MoveDirection.Up:
                    newPlayerRow -= 1;
                    newBoxRow -= 2;
                    break;
                case MoveDirection.Down:
                    newPlayerRow += 1;
                    newBoxRow += 2;
                    break;
                case MoveDirection.Left:
                    newPlayerColumn -= 1;
                    newBoxColumn -= 2;
                    break;
                case MoveDirection.Right:
                    newPlayerColumn += 1;
                    newBoxColumn += 2;
                    break;
            }

            bool isThereAWall = _level[newPlayerRow][newPlayerColumn].Type == ElementType.Wall;

            bool isTryingToMoveABox = _level[newPlayerRow][newPlayerColumn].Type == ElementType.Box ||
                                      _level[newPlayerRow][newPlayerColumn].Type == ElementType.BoxOnGoal;

            bool canMoveBox = isTryingToMoveABox && (_level[newBoxRow][newBoxColumn].Type == ElementType.Floor ||
                                                     _level[newBoxRow][newBoxColumn].Type == ElementType.Goal);

            if (!isThereAWall && !(isTryingToMoveABox && !canMoveBox))
            {
                List<Element> elementsList = new List<Element>()
                                                 {
                                                     new Element()
                                                         {
                                                             Type = _level[_player.Row][_player.Column].Type,
                                                             Row = _player.Row,
                                                             Column = _player.Column
                                                         },
                                                     new Element()
                                                         {
                                                             Type = _level[newPlayerRow][newPlayerColumn].Type,
                                                             Row = newPlayerRow,
                                                             Column = newPlayerColumn
                                                         }
                                                 };

                _level[_player.Row][_player.Column].Type = _level[_player.Row][_player.Column].Type ==
                                                           ElementType.PlayerOnGoal
                                                               ? ElementType.Goal
                                                               : ElementType.Floor;

                if (_level[newPlayerRow][newPlayerColumn].Type == ElementType.Goal ||
                    _level[newPlayerRow][newPlayerColumn].Type == ElementType.BoxOnGoal)
                {
                    if (_level[newPlayerRow][newPlayerColumn].Type == ElementType.BoxOnGoal)
                    {
                        _goalsFilled--;
                    }

                    _level[newPlayerRow][newPlayerColumn].Type = ElementType.PlayerOnGoal;
                }
                else
                {
                    _level[newPlayerRow][newPlayerColumn].Type = ElementType.Player;
                }

                if (isTryingToMoveABox)
                {
                    elementsList.Add(new Element()
                                         {
                                             Type = _level[newBoxRow][newBoxColumn].Type,
                                             Row = newBoxRow,
                                             Column = newBoxColumn
                                         });

                    if (_level[newBoxRow][newBoxColumn].Type == ElementType.Goal)
                    {
                        _level[newBoxRow][newBoxColumn].Type = ElementType.BoxOnGoal;
                        _goalsFilled++;

                        if (_goalsFilled == _goalsCount && LevelCompleted != null)
                        {
                            LevelCompleted(this, new EventArgs());
                        }
                    }
                    else
                    {
                        _level[newBoxRow][newBoxColumn].Type = ElementType.Box;
                    }
                }

                _player = _level[newPlayerRow][newPlayerColumn];
                _movesHistory.Push(elementsList);
            }
        }

        public void UndoMovement()
        {
            if (_movesHistory.Count > 0)
            {
                List<Element> elementsList = _movesHistory.Pop();

                foreach (var element in elementsList)
                {
                    _level[element.Row][element.Column].Type = element.Type;
                }

                _player = elementsList[0];
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (Element[] row in _level)
            {
                foreach (Element element in row)
                {
                    yield return element;
                }
            }
        }
    }
}
