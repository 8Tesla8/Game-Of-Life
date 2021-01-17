using System;
using System.Text;

namespace Logic
{
    public class Game
    {
        private Grid _currentGenerationGrid = new Grid();
        private Grid _nextGenerationGrid = new Grid();
        private Glider _glider = new Glider();


        public long GenerationCount { get; private set; } = 0;


        public void CreateGrid(int gridLength)
        {
            GenerationCount = 0;


            if (gridLength == _currentGenerationGrid.Length)
            {
                _currentGenerationGrid.ClearGrid();
                _nextGenerationGrid.ClearGrid();
            }
            else
            {
                _currentGenerationGrid.Create(gridLength);
                _nextGenerationGrid.Create(gridLength);
            }
        }

        public void NextGeneration()
        {
            if (GenerationCount == 0)
            {
                _currentGenerationGrid.ClearGrid();
                PutGliderIntoTheGrid();

                GenerationCount++;
                return;
            }


            for (int row = 0; row < _currentGenerationGrid.Length; row++)
            {
                for (int column = 0; column < _currentGenerationGrid.Length; column++)
                {
                    var cell = _currentGenerationGrid.GetCell(row, column);
                    var countLiveCell = _currentGenerationGrid.GetCountOfLiveingNeighbours(row, column);

                    if (cell.State == CellState.Live)
                    {
                        //rule 2
                        if (countLiveCell == 2 || countLiveCell == 3)
                        {
                            _nextGenerationGrid.GetCell(row, column).Resurrect();
                        }
                        else if (
                            countLiveCell < 2 || //rule 1
                            countLiveCell > 3)   //rule  3
                        {
                            _nextGenerationGrid.GetCell(row, column).Kill();
                        }
                    }
                    else if(cell.State == CellState.Dead &&
                        countLiveCell == 3) //rule 4
                    {
                        _nextGenerationGrid.GetCell(row, column).Resurrect();
                    }
                }
            }

            _currentGenerationGrid.CopyGrid(_nextGenerationGrid);

            GenerationCount++;
        }


        public string ShowGrid()
        {
            return _currentGenerationGrid.ToString();
        }


        private void PutGliderIntoTheGrid()
        {
            //var gliderCoord = _glider.GetGliderSpaceshipCoord(_currentGenerationGrid.Length);
            var gliderCoord = _glider.GetRandomGliderCoord(_currentGenerationGrid.Length);

            foreach (var (row, column) in gliderCoord)
            {
                var cell =_currentGenerationGrid.GetCell(row, column);
                cell.Kill();
            }
        }
    }
}
