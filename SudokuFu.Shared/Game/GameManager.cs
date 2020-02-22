using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonogameTemplate.Interfaces.Content;
using MonogameTemplate.Interfaces.Events;
using MonogameTemplate.Interfaces.Graphics;
using MonogameTemplate.Interfaces.Mediation;
using SudokuFu.Desktop.Game;
using SudokuFu.Shared.Game.Puzzle;

namespace SudokuFu.Shared.Game
{
    public class GameManager
    {
        private readonly Board _BoardTop;
        private readonly Creator _CreatorTop;
        private readonly Unassigner _UnassignerTop;
        private readonly Solver _SolverTop;

        private readonly Board _BoardBtm;
        private readonly Creator _CreatorBtm;
        private readonly Unassigner _UnassignerBtm;
        private readonly Solver _SolverBtm;

        public GameManager(IMediator mediator)
        {
            IEventService eventService = mediator.GetInstance<IEventService>();
            IContentService contentService = mediator.GetInstance<IContentService>();
            IRenderService renderService = mediator.GetInstance<IRenderService>();

            _BoardTop = new Board(new Vector2(70, 50), new Vector2(100, 100), new Vector2(17, 3), 5, contentService, renderService, eventService);
            _CreatorTop = new Creator(_BoardTop, eventService);
            _UnassignerTop = new Unassigner(_BoardTop, eventService, 3, 6);
            _SolverTop = new Solver(eventService);

            RunTopPuzzle();

            //_BoardBtm = new Board(new Vector2(70, 1000), new Vector2(100, 100), new Vector2(17, 3), 5, contentService, renderService, eventService);
            //_CreatorBtm = new Creator(_BoardBtm, eventService);
            //_UnassignerBtm = new Unassigner(_BoardBtm, eventService, 3, 6);
            //_SolverBtm = new Solver(eventService);

            //RunBtmPuzzle();
        }

        private void RunTopPuzzle()
        {
            Task.Run(() =>
            {
                _CreatorTop.Create(() =>
                {
                    _UnassignerTop.Run(() =>
                    {
                        _SolverTop.Solve(_BoardTop, RunTopPuzzle);
                    });
                });
            });
        }

        private void RunBtmPuzzle()
        {
            Task.Run(() =>
            {
                (_CreatorBtm).Create(() =>
                {
                    _UnassignerBtm.Run(() =>
                    {
                        _SolverBtm.Solve(_BoardBtm, RunBtmPuzzle);
                    });
                });
            });
        }
    }
}
