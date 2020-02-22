using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Xna.Framework;
using MonogameTemplate.Core.Events;
using MonogameTemplate.Interfaces.Events;
using SudokuFu.Desktop.Game;

namespace SudokuFu.Shared.Game.Puzzle
{
    public class Solver
    {
        private const Int32 INNER_GRID_SIZE = 3;

        private readonly IEventService _EventService;

        private Board _Board;

        private Stopwatch _Stopwatch;

        public Solver(IEventService eventService)
        {
            _EventService = eventService;

            _Stopwatch = new Stopwatch();
        }

        /// <summary>
        /// Solves any solvable sudoku board using backtracking
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public Boolean Solve(Board board, Action onComplete)
        {
            _Board = board;

            if (!_Stopwatch.IsRunning)
            {
                _Stopwatch.Start();
            }

            Int32 row = 0;
            Int32 col = 0;

            // Find the next unassigned location
            if (!FindUnassignedLocation(board, ref row, ref col))
            {
                // No empty spaces found, the puzzle is solved

                _Stopwatch.Stop();

                Event ev3 = new Event
                {
                    Name = "PuzzleInfo",
                    Data = $"SOLVED : {_Stopwatch.Elapsed:c}"
                };
                _EventService.Trigger(ev3);

                _Stopwatch.Reset();

                Thread.Sleep(1000);

                onComplete?.Invoke();

                return true;
            }

            // Try the next number in the current space
            for (Int32 num = 1; num <= 9; num++)
            {
                // Can the number be place
                if (IsValid(board, row, col, num))
                {
                    // Yes number can be placed
                    _Board.SetNumber(row, col, num, Color.Green);

                    Event ev1 = new Event
                    {
                        Name = "PuzzleInfo",
                        Data = $"SOLVING"
                    };
                    _EventService.Trigger(ev1);

                    Thread.Sleep(150);
                    _Board.SetColour(Color.White);

                    // recursively try the next empty space on the board
                    if (Solve(board, onComplete))
                    {
                        // Next space filled

                        return true;
                    }

                    // If the next number cannot be assigned a value then un-assign the current number
                    _Board.SetNumber(row, col, 0, Color.Red);

                    Event ev2 = new Event
                    {
                        Name = "PuzzleInfo",
                        Data = $"BACKTRACKING"
                    };
                    _EventService.Trigger(ev2);

                    Thread.Sleep(150);
                    _Board.SetColour(Color.White);
                }

                // No number cannot be placed
            }

            // Exhausted all attempts to fill this space, backtrack up the recursion to the previously filled space
            return false;
        }

        /// <summary>
        /// Finds the first unassigned space on the board and sets the passed in ref integers to its coordinates
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private Boolean FindUnassignedLocation(Board board, ref Int32 row, ref Int32 col)
        {
            for (Int32 i = 0; i < 9; i++)
            {
                for (Int32 j = 0; j < 9; j++)
                {
                    if (board.GetNumber(i, j) == 0)
                    {
                        row = i;
                        col = j;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check if the given number can be placed at the given location considering the constraints of sudoku
        /// No duplicate number in row, column or quadrant
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        private Boolean IsValid(Board board, Int32 row, Int32 col, Int32 num)
        {
            // Check the row and column for validity
            for (Int32 i = 0; i < 9; i++)
            {
                if (board.GetNumber(row, i) == num || board.GetNumber(i, col) == num)
                {
                    return false;
                }
            }

            // Find the quadrant of the board we are placing the number
            Int32 quadrantStartRow = 0;
            Int32 quadrantStartCol = 0;

            for (Int32 j = INNER_GRID_SIZE; j <= Math.Max(row, col); j += INNER_GRID_SIZE)
            {
                if (j <= row)
                {
                    quadrantStartRow += INNER_GRID_SIZE;
                }

                if (j <= col)
                {
                    quadrantStartCol += INNER_GRID_SIZE;
                }
            }

            // Check the quadrant for validity
            for (Int32 j = quadrantStartRow; j < quadrantStartRow + INNER_GRID_SIZE; j++)
            {
                for (Int32 k = quadrantStartCol; k < quadrantStartCol + INNER_GRID_SIZE; k++)
                {
                    if (board.GetNumber(j, k) == num)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
