using System;
using Microsoft.Xna.Framework;
using MonogameTemplate.Interfaces.Content;
using MonogameTemplate.Interfaces.Graphics;
using SudokuFu.Desktop.Interfaces.Board;

namespace SudokuFu.Desktop.Game
{
    public class Board
    {
        private readonly Tile[,] _Grid;

        public Board(Vector2 tileSize, Vector2 textOffset, Single spacer, IContentService content, IRenderService renderService)
        {
            _Grid = new Tile[9, 9];

            for (Int32 y = 0; y < 9; y++)
            {
                for (Int32 x = 0; x < 9; x++)
                {
                    Single xPos = (tileSize.X * x) + spacer;
                    Single yPos = (tileSize.Y * y) + spacer;

                    Tile tile = new Tile(new Vector2(xPos, yPos), tileSize, textOffset);
                    tile.SetNumber(0);
                    tile.LoadContent(content);
                    renderService.Register(tile);
                    _Grid[x, y] = tile;
                }
            }
        }

        public Int32 GetNumber(Int32 x, Int32 y)
        {
            return _Grid[x, y].GetNumber();
        }

        public void SetNumber(Int32 x, Int32 y, Int32 num)
        {
            _Grid[x, y].SetNumber(num);
        }
    }
}
