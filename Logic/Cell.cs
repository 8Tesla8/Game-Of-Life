using System;

namespace Logic
{
    public enum CellState
    {
        Live = 0,
        Dead = 1,
    }

    public class Cell
    {
        public CellState State { get; private set; }

        public Cell()
        {
            State = CellState.Live;
        }

        public void Kill()
        {
            State = CellState.Dead;
        }

        public void Resurrect()
        {
            State = CellState.Live;
        }

        public override string ToString()
        {
            if (State == CellState.Live)
                return "o";
            else 
                return "X";
        }
    }

}
