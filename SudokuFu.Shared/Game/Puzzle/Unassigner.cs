using System;
using System.Collections.Generic;
using System.Threading;
using MonogameTemplate.Interfaces.Events;
using MonogameTemplate.Interfaces.Timers;
using SudokuFu.Desktop.Game;

namespace SudokuFu.Shared.Game.Puzzle
{
    public class Unassigner
    {
        private readonly Random _Rand = new Random(Environment.TickCount);
        private readonly Board _Board;
        private readonly Int32 _MinSwaps;
        private readonly Int32 _MaxSwaps;
        private ITimedCallbackFactory _TimedCallbackFactory;
        private IEventService _EventService;
        private const Int32 SLEEP = 0;

        public Unassigner(Board board, IEventService eventService, Int32 min, Int32 max, ITimedCallbackFactory timedCallbackFactory)
        {
            _Board = board;
            _EventService = eventService;
            _MinSwaps = min;
            _MaxSwaps = max;
            _TimedCallbackFactory = timedCallbackFactory;
        }

        public void Run(Action onComplete)
        {
            const Int32 QUADRANT_SIZE = 3;

            for (Int32 quadrantX = 0; quadrantX < 9; quadrantX += QUADRANT_SIZE)
            {
                for (Int32 quadrantY = 0; quadrantY < 9; quadrantY += QUADRANT_SIZE)
                {
                    List<Int32> quadrantValues = new List<Int32>();

                    for (Int32 x = 0; x < QUADRANT_SIZE; x++)
                    {
                        for (Int32 y = 0; y < QUADRANT_SIZE; y++)
                        {
                            quadrantValues.Add(_Board.GetNumber(quadrantX + x, quadrantY + y));
                        }
                    }

                    Int32 amountReplaced = ReplaceValues(ref quadrantValues, _MinSwaps, _MaxSwaps);

                    Int32 valueIndex = 0;

                    for (Int32 x = 0; x < QUADRANT_SIZE; x++)
                    {
                        for (Int32 y = 0; y < QUADRANT_SIZE; y++)
                        {
                            _Board.SetNumber(quadrantX + x, quadrantY + y, quadrantValues[valueIndex++]);
                        }
                    }

                    Thread.Sleep(100);

                    // TODO _Printer.Print(_Grid, SLEEP, $"Quadrant: ({quadrantX}, {quadrantY}) had {amountReplaced} values unassigned");
                }
            }

            onComplete?.Invoke();
        }

        private Int32 ReplaceValues(ref List<Int32> values, Int32 min, Int32 max)
        {
            if (min <= 0)
            {
                min = 0;
            }
            if (max >= 8)
            {
                max = 8;
            }

            List<Int32> indices = new List<Int32>() { 0,1,2,3,4,5,6,7,8 };

            Int32 amount = _Rand.Next(min, max);

            for (Int32 i = 0; i < amount; i++)
            {
                Int32 randomIndex = _Rand.Next(0, indices.Count);
                Int32 valueIndex = indices[randomIndex];

                values[valueIndex] = 0;

                indices.Remove(valueIndex);
            }

            return amount;
        }
    }
}
