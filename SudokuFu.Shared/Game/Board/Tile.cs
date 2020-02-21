using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameTemplate.Interfaces.Content;
using MonogameTemplate.Interfaces.Role;
using SudokuFu.Shared.Interfaces;

namespace SudokuFu.Desktop.Game.Board
{
    public class Tile : ITile, IRenderable, IContent
    {
        private Int32 _Number;

        private Texture2D _Texture;

        private SpriteFont _SpriteFont;

        public Tile()
        {
            
        }

        #region Implementation of ITile

        public void SetNumber(Int32 num)
        {

        }

        #endregion

        #region Implementation of IVisible

        /// <summary> Whether the <see cref="IVisible"/> should be visible</summary>
        public Boolean Visible { get; set; }

        /// <summary> Draws an <see cref="IRenderable"/> to screen </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        #endregion

        #region Implementation of IContent

        public void LoadContent(IContentService contentService)
        {

        }

        #endregion
    }
}
