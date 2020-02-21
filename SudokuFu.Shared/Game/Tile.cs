using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameTemplate.Core.Helper;
using MonogameTemplate.Core.Transform;
using MonogameTemplate.Interfaces.Content;
using MonogameTemplate.Interfaces.Role;
using SudokuFu.Desktop.Interfaces.Board;

namespace SudokuFu.Desktop.Game
{
    public class Tile : ITile, IRenderable, IContent, ITransform
    {
        private readonly Vector2 _Size;

        private readonly Vector2 _TextOffset;

        private Int32 _Number;

        private SpriteFont _SpriteFont;

        #region Implementation of ITransform

        public Transform2D Transform { get; set; }

        #endregion

        public Tile(Vector2 position, Vector2 size, Vector2 textOffset)
        {
            Transform = new Transform2D
            {
                Position = position
            };

            _Size = size;

            _TextOffset = textOffset;

            Visible = true;
        }

        #region Implementation of ITile

        public Int32 GetNumber()
        {
            return _Number;
        }

        public void SetNumber(Int32 num)
        {
            _Number = num;
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
            if (!Visible) return;
            Texture2D whiteRect = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            whiteRect.SetData(new[] { Color.White });
            spriteBatch.Draw(whiteRect, new Vector2(Transform.AbsolutePosition.X, Transform.AbsolutePosition.Y), null,
                Color.White, 0f, Vector2.Zero, new Vector2(_Size.X, _Size.Y),
                SpriteEffects.None, 0f);
            spriteBatch.DrawString(_SpriteFont, _Number.ToString(), Transform.AbsolutePosition + _TextOffset, Color.Black);
        }

        #endregion

        #region Implementation of IContent

        public void LoadContent(IContentService contentService)
        {
            _SpriteFont = contentService.Load<SpriteFont>("Fonts/Number");
        }

        #endregion


    }
}
