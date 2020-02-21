using System.Threading.Tasks;
using Microsoft.Xna.Framework;
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
        private readonly IMediator _Mediator;
        private readonly Board _Board;
        private readonly Creator _Creator;
        private readonly Unassigner _Unassigner;
        private readonly Solver _Solver;
        private readonly InfoText _InfoText;

        public GameManager(IMediator mediator)
        {
            _Mediator = mediator;
            IEventService eventService = _Mediator.GetInstance<IEventService>();

            _Board = new Board(new Vector2(100, 150), new Vector2(12, 5), 5, _Mediator.GetInstance<IContentService>(), _Mediator.GetInstance<IRenderService>());
            _Creator = new Creator(_Board, eventService);
            _Unassigner = new Unassigner(_Board, eventService, 3, 6);
            _Solver = new Solver(eventService);

            _InfoText = new InfoText(eventService);
            _InfoText.LoadContent(_Mediator.GetInstance<IContentService>());
            _Mediator.GetInstance<IRenderService>().Register(_InfoText);

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

        public void Start()
        {
            // TODO Get a SCENE and start a GAME STATE
        }

    }
}
