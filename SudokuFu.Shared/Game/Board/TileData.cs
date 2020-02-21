using System;
using Microsoft.Xna.Framework;
using MonogameTemplate.Core.Logic;

namespace SudokuFu.Shared.Game.Board
{
    [Serializable]
    [DataCreation("TileData")]
    public class TileData
    {
        public Vector2 Size { get; set; }

        public Vector2 Offset { get; set; }
    }
}
