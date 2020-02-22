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
        private readonly Board _Board;
        private readonly Creator _Creator;
        private readonly Unassigner _Unassigner;
        private readonly Solver _Solver;

        public GameManager(IMediator mediator)
        {
            IEventService eventService = mediator.GetInstance<IEventService>();
            IContentService contentService = mediator.GetInstance<IContentService>();
            IRenderService renderService = mediator.GetInstance<IRenderService>();

            _Board = new Board(new Vector2(100, 150), new Vector2(13, 5), 5, contentService, renderService, eventService);
            _Creator = new Creator(_Board, eventService);
            _Unassigner = new Unassigner(_Board, eventService, 3, 6);
            _Solver = new Solver(eventService);

            RunPuzzle();
        }

        private void RunPuzzle()
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
    }
}
