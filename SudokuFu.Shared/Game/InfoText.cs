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
        private readonly Vector2 _Position;
        private SpriteFont _InfoSpriteFont;
        private String _Text;

        public InfoText(IEventService eventService, Vector2 position)
        {
            eventService.Register("PuzzleInfo", OnPuzzleUpdated);
            _Position = position;
        }

        private void OnPuzzleUpdated(IEvent obj)
        {
            _Text = obj.Data;
        }

        #region Implementation of IContent

        public void LoadContent(IContentService contentService)
        {
            _InfoSpriteFont = contentService.Load<SpriteFont>("Info");
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
            spriteBatch.DrawString(_InfoSpriteFont, _Text, _Position, Color.Red);
        }

        #endregion
    }
}
