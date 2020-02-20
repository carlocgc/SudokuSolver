using System.Reflection;
using Microsoft.Xna.Framework;
using MonogameTemplate.Core.Input;
using MonogameTemplate.Core.Logic;
using MonogameTemplate.Interfaces.Input;

namespace SudokuFu.Android
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class AndroidGame : GameCore
    {
        public AndroidGame()
        {
            _GraphicsDeviceManager.IsFullScreen = true;
            _GraphicsDeviceManager.PreferredBackBufferWidth = 1920;
            _GraphicsDeviceManager.PreferredBackBufferHeight = 1080;
            _GraphicsDeviceManager.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
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
            base.LoadContent();
            // TODO: use this.Content to load your game content here

            if (_InputService == null)
            {
                _InputService = _Mediator.RegisterService<IInputService, TouchInputService>(new TouchInputService(_EventService, _UpdateService));
            }
        }
    }
}
