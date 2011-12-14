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

        private static Dictionary<string, string> _moves;
        private Stack<List<Tuple<Element, int, int>>> _movesHistory;
        
        public Sokoban(Level level)
        {
            LoadLevel(level);
        }

        public Sokoban()
        {
            _movesHistory = new Stack<List<Tuple<Element, int, int>>>();
        }

        static Sokoban()
        {
            /* i decided to keep possible movements in a dictionary, instead of coding a bunch of ifs to handle
            * the logic. For example, the first entry reads: (KEY) --> if i'm the player, and where i want to 
            * move there is a floor, (VALUE) --> then where was the player put a floor, and where was the floor
            * put the player */
            _moves = new Dictionary<string, string>()
                         {
                             {"@ ", " @"},
                             {"@$ ", " @$"},
                             {"@.", " +"},
                             {"+.", ".+"},
                             {"+ ", ".@"},
                             {"@$.", " @*"},
                             {"@*.", " +*"},
                             {"+*.", ".+*"},
                             {"+* ", ".+$"},
                             {"+$ ", ".@$"},
                             {"+$.", ".@*"}
                         };
        }

        public void LoadLevel(Level level)
        {
            Width = level.Width;
            Height = level.Height;

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
                }
            }
        }

        public void MovePlayer(MoveDirection moveDirection)
        {
            int newPlayerX = _playerX;
            int newPlayerY = _playerY;

            /* i keep track of the position two elements beyond the current player's position in case i want to
             * move a box, so i can check if there is another box or wall next to the box that will prevent me 
             * to do that */
            int posAheadX = _playerX;
            int posAheadY = _playerY;

            switch (moveDirection)
            {
                case MoveDirection.Up:
                    newPlayerY = _playerY - 1;
                    posAheadY = newPlayerY - 1;
                    break;
                case MoveDirection.Down:
                    newPlayerY = _playerY + 1;
                    posAheadY = newPlayerY + 1;
                    break;
                case MoveDirection.Left:
                    newPlayerX = _playerX - 1;
                    posAheadX = newPlayerX - 1;
                    break;
                case MoveDirection.Right:
                    newPlayerX = _playerX + 1;
                    posAheadX = newPlayerX + 1;
                    break;
            }

            Element playerPositionElement = _level[_playerY][_playerX];
            Element newPlayerPositionElement = _level[newPlayerY][newPlayerX];
            
            /* i build the key for the move: the element at the current player position, the element at where i want
             * to move, and the element next to it in case it's a Box or a BoxOnGoal, since if it's some of those two
             * elements, i need to check if i would be able to move the box or not */
            string moveInput = ((char)playerPositionElement).ToString() + (char)newPlayerPositionElement;
            if (newPlayerPositionElement == Element.Box || newPlayerPositionElement == Element.BoxOnGoal)
            {
                moveInput += ((char)_level[posAheadY][posAheadX]).ToString();
            }

            // if there's no key in the movement's hashtable, i know it's not a valid move
            if (_moves.ContainsKey(moveInput))
            {
                /* i save in the stack the elements (with the positions) that are going to be affected by the
                 * move, so i can go back to this previous state if i do an undo action */
                List<Tuple<Element, int, int>> elementsList = new List<Tuple<Element, int, int>>();
                elementsList.Add(new Tuple<Element, int, int>((Element)moveInput[0], _playerY, _playerX));
                elementsList.Add(new Tuple<Element, int, int>((Element)moveInput[1], newPlayerY, newPlayerX));
                
                string moveOutput = _moves[moveInput];
                
                /* the first char in the value associated with the key tells me what element i need to put in the
                 * current player's position, since the player is moving. The next char tells me the element that goes
                 * in the position i want to move, and it's going to be a Player or a PlayerOnGoal. The last char, 
                 * represents what happen when i move a box: is it still a Box or now it's a BoxOnGoal? */
                _level[_playerY][_playerX] = (Element)(moveOutput[0]);
                _level[newPlayerY][newPlayerX] = (Element)(moveOutput[1]);    

                if (moveOutput.Length == 3)
                {
                    _level[posAheadY][posAheadX] = (Element)(moveOutput[2]);
                    // if the move affected three cells, then i need to save this element too
                    elementsList.Add(new Tuple<Element, int, int>((Element)moveInput[2], posAheadY, posAheadX));
                }

                _movesHistory.Push(elementsList);

                _playerX = newPlayerX;
                _playerY = newPlayerY;

                // this is the only way i can turn a Box in a BoxOnGoal
                if (moveInput == "@$.")
                {
                    _goalsFilled++;

                    if (_goalsFilled == _goalsCount && LevelCompleted != null)
                    {
                        LevelCompleted(this, new EventArgs());
                    }
                }
                // and this the only way to go from a BoxOnGoal to a Box
                else if (moveInput == "+* ")
                {
                    _goalsFilled--;
                }
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
