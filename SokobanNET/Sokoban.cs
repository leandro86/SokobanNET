using System;
using System.Collections;
using System.Collections.Generic;

namespace SokobanNET
{
    public class Sokoban : IEnumerable
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public enum Element
        {
            EndRow,
            Player = '@',
            Wall = '#',
            Floor = ' ',
            Goal = '.',
            Box = '$',
            BoxOnGoal = '*',
            PlayerOnGoal = '+'
        }

        public enum MoveDirection
        {
            Up,
            Down,
            Right,
            Left
        }

        public event EventHandler LevelCompleted;
        
        private Element[][] _level;

        // this variables always keep track of the current player's position
        private int _playerX;
        private int _playerY;

        private int _goalsCount;
        private int _goalsFilled;

        private Stack<List<Tuple<Element, int, int>>> _movesHistory;
        
        public Sokoban(Level level)
        {
            LoadLevel(level);
        }

        public Sokoban()
        {
            _movesHistory = new Stack<List<Tuple<Element, int, int>>>();
        }

        public void LoadLevel(Level level)
        {
            Width = level.Width;
            Height = level.Height;

            _goalsCount = 0;
            _goalsFilled = 0;

            _level = new Element[level.Data.Length][];
            for (int i = 0; i < level.Data.Length; i++)
            {
                _level[i] = new Element[level.Data[i].Length];

                for (int j = 0; j < level.Data[i].Length; j++)
                {
                    _level[i][j] = (Element)level.Data[i][j];

                    if (_level[i][j] == Element.Player)
                    {
                        _playerY = i;
                        _playerX = j;
                    }
                    else if (_level[i][j] == Element.Goal)
                    {
                        _goalsCount++;
                    }
                    else if (_level[i][j] == Element.BoxOnGoal)
                    {
                        _goalsCount++;
                        _goalsFilled++;
                    }
                }
            }

            _movesHistory.Clear();
        }

        public void MovePlayer(MoveDirection moveDirection)
        {
            int newPlayerX = _playerX;
            int newPlayerY = _playerY;
            int newBoxX = _playerX;
            int newBoxY = _playerY;           

            switch (moveDirection)
            {
                case MoveDirection.Up:
                    newPlayerY = _playerY - 1;
                    newBoxY = newPlayerY - 1;
                    break;
                case MoveDirection.Down:
                    newPlayerY = _playerY + 1;
                    newBoxY = newPlayerY + 1;
                    break;
                case MoveDirection.Left:
                    newPlayerX = _playerX - 1;
                    newBoxX = newPlayerX - 1;
                    break;
                case MoveDirection.Right:
                    newPlayerX = _playerX + 1;
                    newBoxX = newPlayerX + 1;
                    break;
            }

            bool isThereAWall = _level[newPlayerY][newPlayerX] == Element.Wall;
            bool isTryingToMoveABox = _level[newPlayerY][newPlayerX] == Element.Box ||
                                      _level[newPlayerY][newPlayerX] == Element.BoxOnGoal;
            bool canMoveBox = isTryingToMoveABox && (_level[newBoxY][newBoxX] == Element.Floor ||
                                                     _level[newBoxY][newBoxX] == Element.Goal);

            if (isThereAWall || (isTryingToMoveABox && !canMoveBox))
            {
                return;
            }
            else
            {
                List<Tuple<Element, int, int>> elementsList = new List<Tuple<Element, int, int>>();
                elementsList.Add(new Tuple<Element, int, int>(_level[_playerY][_playerX], _playerY, _playerX));
                elementsList.Add(new Tuple<Element, int, int>(_level[newPlayerY][newPlayerX], newPlayerY, newPlayerX));              
                
                _level[_playerY][_playerX] = _level[_playerY][_playerX] == Element.PlayerOnGoal
                                                 ? Element.Goal
                                                 : Element.Floor;

                if (_level[newPlayerY][newPlayerX] == Element.Goal || _level[newPlayerY][newPlayerX] == Element.BoxOnGoal)
                {
                    if (_level[newPlayerY][newPlayerX] == Element.BoxOnGoal)
                    {
                        _goalsFilled--;
                    }

                    _level[newPlayerY][newPlayerX] = Element.PlayerOnGoal;
                }
                else
                {
                    _level[newPlayerY][newPlayerX] = Element.Player;
                }

                if (isTryingToMoveABox)
                {
                    elementsList.Add(new Tuple<Element, int, int>(_level[newBoxY][newBoxX], newBoxY, newBoxX));

                    if (_level[newBoxY][newBoxX] == Element.Goal)
                    {
                        _level[newBoxY][newBoxX] = Element.BoxOnGoal;
                        _goalsFilled++;

                        if (_goalsFilled == _goalsCount && LevelCompleted != null)
                        {
                            LevelCompleted(this, new EventArgs());
                        }
                    }
                    else
                    {
                        _level[newBoxY][newBoxX] = Element.Box;
                    }
                }

                _movesHistory.Push(elementsList);

                _playerY = newPlayerY;
                _playerX = newPlayerX;
            }
        }

        public void UndoMovement()
        {
            if (_movesHistory.Count > 0)
            {
                List<Tuple<Element, int, int>> elementsList = _movesHistory.Pop();

                foreach (var element in elementsList)
                {
                    _level[element.Item2][element.Item3] = element.Item1;
                }

                // the first element in the list is going to contain always the player information
                _playerX = elementsList[0].Item3;
                _playerY = elementsList[0].Item2;
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < _level.Length; i++)
            {
                for (int j = 0; j < _level[i].Length; j++)
                {
                    yield return _level[i][j];
                }
                
                /* the client needs to know where the current row ends, so he can increment y, and begin to
                 * draw the next row. The alternative would be to return some kind of struct with the element type,
                 * and the coordinates for drawing */
                yield return Element.EndRow;
            }
        }
    }
}
