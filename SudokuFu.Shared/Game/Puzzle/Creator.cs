using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using MonogameTemplate.Core.Events;
using MonogameTemplate.Interfaces.Events;
using SudokuFu.Desktop.Game;

namespace SudokuFu.Shared.Game.Puzzle
{
    public class Creator
    {
        private readonly Random _Rand = new Random(Environment.TickCount);
        private readonly Int32[] _Shifts = {3, 3, 1, 3, 3, 1, 3, 3};
        private readonly Int32[] _Indices = {0, 3, 6};
        private readonly Board _Board;
        private readonly IEventService _EventService;

        public Creator(Board board, IEventService eventService)
        {
            _Board = board;
            _EventService = eventService;
        }

        public void Create(Action onComplete)
        {
            SendEvent("CLEARED");
            _Board.SetAllNumbers(0);
            Thread.Sleep(500);
            _Board.SetColour(Color.White);
            SendEvent("SEEDING");

            List<Int32> unused = new List<Int32> {1,2,3,4,5,6,7,8,9};

            for (Int32 i = 0; i < 9; i++)
            {
                Int32 index = _Rand.Next(0, unused.Count);
                _Board.SetNumber(0, i, unused[index], Color.SkyBlue);
                unused.Remove(_Board.GetNumber(0, i));

                Thread.Sleep(200);
                _Board.SetColour(Color.White);
            }

            for (Int32 i = 1; i < 9; i++)
            {
                ShiftAndFillRow(_Board, i, _Shifts[i - 1]);
            }

            ShuffleRowsAndColumns(_Board, _Rand.Next(10, 20));

            onComplete?.Invoke();
        }

        /// <summary>
        /// takes the previous rows values and shifts them over by the given number of spaces horizontally
        /// </summary>
        /// <param name="grid"> The grid to shift the rows of </param>
        /// <param name="row"> The row to shift </param>
        /// <param name="shift"></param>
        private void ShiftAndFillRow(Board board, Int32 row, Int32 shift)
        {
            SendEvent("SHIFTING");

            for (Int32 i = 0; i < 9; i++)
            {
                Int32 index = (i + shift) % 9;

                _Board.SetNumber(row, i, _Board.GetNumber(row - 1, index), Color.SkyBlue);
            }


            Thread.Sleep(300);
            _Board.SetColour(Color.White);
        }


        /// <summary>
        /// Shuffles the puzzle quadrants to disguise the pattern
        /// </summary>
        /// <param name="grid"> Grid to shuffle </param>
        /// <param name="shuffles"> Shuffles to make </param>
        private void ShuffleRowsAndColumns(Board board, Int32 shuffles)
        {
            SendEvent("SHUFFLING");

            List<Int32> unused = _Indices.ToList();

            for (Int32 i = 0; i < shuffles; i++)
            {
                Double chance = _Rand.NextDouble();
                Boolean rowShuffle = chance > 0.5;

                Int32 indexA = unused[_Rand.Next(unused.Count)];
                unused.Remove(indexA);

                Int32 indexB = unused[_Rand.Next(unused.Count)];
                unused.Remove(indexB);

                if (unused.Count <= 1)
                {
                    unused = _Indices.ToList();
                }

                for (Int32 j = 0; j < 3; j++)
                {
                    for (Int32 x = 0; x < 9; x++)
                    {
                        if (rowShuffle)
                        {
                            Int32 temp = _Board.GetNumber(indexA + j, x);
                            _Board.SetNumber(indexA + j, x, _Board.GetNumber(indexB + j, x), Color.MonoGameOrange);
                            _Board.SetNumber(indexB + j, x, temp, Color.Yellow);
                        }
                        else
                        {
                            Int32 temp = _Board.GetNumber(x, indexA + j);
                            _Board.SetNumber(x, indexA + j, _Board.GetNumber(x, indexB + j), Color.MonoGameOrange);
                            _Board.SetNumber(x, indexB + j, temp, Color.Yellow);
                        }
                    }
                }

                Thread.Sleep(200);
                _Board.SetColour(Color.White);
            }
        }

        private void SendEvent(String message)
        {
            Event ev = new Event
            {
                Name = "PuzzleInfo",
                Data = $"{message}"
            };
            _EventService.Trigger(ev);
        }
    }
}
