using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameTemplate.Core.Transform;
using MonogameTemplate.Interfaces.Content;
using MonogameTemplate.Interfaces.Events;
using MonogameTemplate.Interfaces.Role;

namespace SudokuFu.Shared.Game
{
    public class InfoText : IContent, IRenderable
    {
        private SpriteFont _InfoSpriteFont;

        private Transform2D _Transform;

        private String _Text;

        public InfoText(IEventService eventService, Vector2 position, Transform2D parent = null)
        {
            eventService.Register("PuzzleInfo", OnPuzzleUpdated);

            _Transform = new Transform2D();

            if (parent != null)
            {
                _Transform.Parent = parent;
            }

            _Transform.Position = position;
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

            Vector2 position = new Vector2(_Transform.AbsolutePosition.X - (size.X / 2), _Transform.AbsolutePosition.Y);

            spriteBatch.DrawString(_InfoSpriteFont, _Text, position, Color.Red);
        }

        #endregion
    }
}
