using System;

namespace SudokuFu.Desktop.Interfaces.Board
{
    public interface ITile
    {
        Int32 GetNumber();

        void SetNumber(Int32 num);
    }
}
