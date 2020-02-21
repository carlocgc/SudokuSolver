using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonogameTemplate.Core.Timers;
using MonogameTemplate.Interfaces.Content;
using MonogameTemplate.Interfaces.Events;
using MonogameTemplate.Interfaces.Graphics;
using MonogameTemplate.Interfaces.Mediation;
using MonogameTemplate.Interfaces.Timers;
using SudokuFu.Desktop.Game;
using SudokuFu.Shared.Game.Puzzle;

namespace SudokuFu.Shared.Game
{
    public class GameManager
    {
        private IMediator _Mediator;
        private Board _Board;
        private Creator _Creator;
        private Unassigner _Unassigner;
        private Solver _Solver;

        public GameManager(IMediator mediator)
        {
            _Mediator = mediator;

            IEventService eventService = _Mediator.GetInstance<IEventService>();
            ITimedCallbackFactory timedCallbackFactory = _Mediator.GetInstance<ITimedCallbackFactory>();

            _Board = new Board(new Vector2(100, 150), new Vector2(12, 5), 5, _Mediator.GetInstance<IContentService>(), _Mediator.GetInstance<IRenderService>());
            _Creator = new Creator(_Board, eventService, timedCallbackFactory);
            _Unassigner = new Unassigner(_Board, eventService, 3, 6, timedCallbackFactory);
            _Solver = new Solver(eventService, timedCallbackFactory);

            RunPuzzle();
        }

        public void RunPuzzle()
        {
            Task.Run(() =>
            {
                _Creator.Create(() =>
                {
                    _Unassigner.Run(() =>
                    {
                        _Solver.Solve(_Board, RunPuzzle);
                    });
                });
            });
        }

        public void Start()
        {
            // TODO Get a SCENE and start a GAME STATE
        }

    }
}
