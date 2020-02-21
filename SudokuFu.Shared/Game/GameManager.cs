using System;
using Microsoft.Xna.Framework;
using MonogameTemplate.Interfaces.Content;
using MonogameTemplate.Interfaces.Graphics;
using MonogameTemplate.Interfaces.Mediation;
using SudokuFu.Desktop.Game;

namespace SudokuFu.Shared.Game
{
    public class GameManager
    {
        private IMediator _Mediator;
        private Board _Board;


        public GameManager(IMediator mediator)
        {
            _Mediator = mediator;
            _Board = new Board(new Vector2(100, 150), new Vector2(10, 10), 10, _Mediator.GetInstance<IContentService>(), _Mediator.GetInstance<IRenderService>());
        }

        public void Start()
        {
            // TODO Get a SCENE and start a GAME STATE
        }

    }
}
