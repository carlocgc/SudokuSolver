using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameTemplate.Interfaces.Content;
using MonogameTemplate.Interfaces.Events;
using MonogameTemplate.Interfaces.Role;

namespace SudokuFu.Shared.Game
{
    public class InfoText : IContent, IRenderable
    {
        private SpriteFont _InfoSpriteFont;
        private String _Text;

        public InfoText(IEventService eventService)
        {
            eventService.Register("PuzzleInfo", OnPuzzleUpdated);
        }

        private void OnPuzzleUpdated(IEvent obj)
        {
            _Text = obj.Data;
        }

        #region Implementation of IContent

        public void LoadContent(IContentService contentService)
        {
            _InfoSpriteFont = contentService.Load<SpriteFont>("Fonts/Info");
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
            Vector2 size = _InfoSpriteFont.MeasureString(_Text);

            Vector2 position = new Vector2(540 - (size.X / 2), 1600);

            spriteBatch.DrawString(_InfoSpriteFont, _Text, position, Color.Red);
        }

        #endregion
    }
}
