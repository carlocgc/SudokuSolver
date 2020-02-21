using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonogameTemplate.Core.Debugging.Loggers;
using MonogameTemplate.Core.Logic;

namespace SudokuFu.Desktop
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class DesktopGame : GameCore
    {
        public DesktopGame()
        {
            _GraphicsDeviceManager.PreferredBackBufferWidth = 576;
            _GraphicsDeviceManager.PreferredBackBufferHeight = 1024;
        }

        protected override Assembly[] GetActiveAssemblies()
        {
            return new Assembly[]
            {
                Assembly.GetAssembly(typeof(GameCore)),
            };
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO _InputService = _Mediator.RegisterService<IInputService, GamePadInputService>(new GamePadInputService(_UpdateService));

            _Logger.AddLogger(new DesktopTextLogger()).Start();

            base.LoadContent();

            // TODO Start core game instance
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
