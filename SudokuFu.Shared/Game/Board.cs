using System;
using Microsoft.Xna.Framework;
using MonogameTemplate.Core.Transform;
using MonogameTemplate.Interfaces.Content;
using MonogameTemplate.Interfaces.Graphics;

namespace SudokuFu.Desktop.Game
{
    public class Board
    {

        private Transform2D _Transform;

        private readonly Tile[,] _Grid;

        public Board(Vector2 tileSize, Vector2 textOffset, Single spacer, IContentService content, IRenderService renderService)
        {
            _Transform = new Transform2D();

            _Transform.Position = new Vector2(70, 50);

            _Grid = new Tile[9, 9];

            for (Int32 x = 0; x < 9; x++)
            {
                for (Int32 y = 0; y < 9; y++)
                {
                    // Grid was drawing top to bottom, no idea why but had to switch the x and y here to get it to draw board in portrait :(
                    Single xPos = (tileSize.X + spacer) * y;
                    Single yPos = (tileSize.Y + spacer) * x;

                    Tile tile = new Tile(new Vector2(xPos, yPos), tileSize, textOffset);
                    tile.SetNumber(0);
                    tile.SetCoord(new Vector2(x, y));
                    tile.LoadContent(content);
                    renderService.Register(tile);
                    tile.Transform.Parent = _Transform;

                    _Grid[x, y] = tile;
                }
            }
        }

        public Int32 GetNumber(Int32 x, Int32 y)
        {
            return _Grid[x, y].GetNumber();
        }

        public void SetNumber(Int32 x, Int32 y, Int32 num, Color colour)
        {
            _Grid[x, y].SetNumber(num);
            _Grid[x, y].SetColour(colour);
        }

        public void SetColour(Color colour)
        {
            foreach (Tile tile in _Grid)
            {
                tile.SetColour(colour);
            }
        }

        public void SetAllNumbers(Int32 num)
        {
            foreach (Tile tile in _Grid)
            {
                tile.SetNumber(num);
            }
        }
    }
}
