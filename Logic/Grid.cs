using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Grid
    {
        private Cell[,] _cells = null;

        private Func<int, int, Cell>[] _neighboursCoords;
        private StringBuilder _strBuilder = new StringBuilder();

        public int Length { get; private set; } = -1;


        public Grid()
        {
            _neighboursCoords = new Func<int, int, Cell>[8];

            //upper line  
            _neighboursCoords[0] = (int row, int column) => GetCell(row - 1, column - 1);  //upLeft
            _neighboursCoords[1] = (int row, int column) => GetCell(row - 1, column);      //upMiddle
            _neighboursCoords[2] = (int row, int column) => GetCell(row - 1, column + 1);  //upRight

            //middle line 
            _neighboursCoords[3] = (int row, int column) => GetCell(row, column - 1);       //middleLeft
            _neighboursCoords[4] = (int row, int column) => GetCell(row, column + 1);       //middleRight

            //bottom line 
            _neighboursCoords[5] = (int row, int column) => GetCell(row + 1, column - 1);   //bottomLeft
            _neighboursCoords[6] = (int row, int column) => GetCell(row + 1, column);       //bottomMiddle
            _neighboursCoords[7] = (int row, int column) => GetCell(row + 1, column + 1);   //bottomRight
        }


        
        public void Create(int length)
        {
            if (length <= 1)
                throw new ArgumentException("The length of grid must be more than 1");

            Length = length;

            _cells = new Cell[Length, Length];

            for (int row = 0; row < _cells.GetLength(0); row++)
            {
                for (int column = 0; column < _cells.GetLength(1); column++)
                {
                    _cells[row, column] = new Cell();
                }
            }
        }

        public void ClearGrid()
        {            
            for (int row = 0; row < _cells.GetLength(0); row++)
            {
                for (int column = 0; column < _cells.GetLength(1); column++)
                {
                    _cells[row, column].Resurrect();
                }
            }
        }

        public Cell GetCell(int row, int column)
        {
            if (_cells == null ||
                row < 0 ||
                row >= _cells.GetLength(0) ||
                column < 0 ||
                column >= _cells.GetLength(1))
                return null;

            return _cells[row, column];
        }

        public int GetCountOfLiveingNeighbours(int row, int column)
        {
            var countLive = 0;

            for (int i = 0; i < _neighboursCoords.Length; i++)
            {
                var cell = _neighboursCoords[i].Invoke(row, column);

                if (cell == null)
                    continue;

                if (cell.State == CellState.Live)
                    countLive++;
            }

            return countLive;
        }

        public void CopyGrid(Grid grid)
        {
            for (int row = 0; row < _cells.GetLength(0); row++)
            {
                for (int column = 0; column < _cells.GetLength(1); column++)
                {
                    var cell = grid.GetCell(row, column);

                    if (cell.State == CellState.Live)
                        GetCell(row, column).Resurrect();
                    else
                        GetCell(row, column).Kill();
                }
            }
        } 

        public (int countLiveCell, int countDeadCell) GetCountOfNeighboursState(int row, int column)
        {
            var countLive = 0;
            var countDead = 0;

            for (int i = 0; i < _neighboursCoords.Length; i++)
            {
                var cell = _neighboursCoords[i].Invoke(row, column);

                if (cell == null)
                    continue;

                if (cell.State == CellState.Live)
                    countLive++;
                else
                    countDead++;
            }

            return (countLive, countDead);
        }


        public override string ToString()
        {
            for (int row = 0; row < _cells.GetLength(0); row++)
            {
                for (int column = 0; column < _cells.GetLength(1); column++)
                {
                    var cell = _cells[row, column];
                    _strBuilder.Append(cell.ToString());
                }

                _strBuilder.Append("\n");
            }

            var result = _strBuilder.ToString();

            _strBuilder.Clear();

            return result;
        }
    }
}
