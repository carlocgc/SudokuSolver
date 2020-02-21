using System;
using Microsoft.Xna.Framework;
using MonogameTemplate.Interfaces.States;

namespace SudokuFu.Shared.Game.States
{
    public class CreateState : IState
    {
        #region Implementation of IState

        public void Enter()
        {

        }

        public void Exit()
        {

        }

        #endregion

        #region Implementation of IUpdateable

        public void Update(GameTime gameTime)
        {

        }

        public Boolean Enabled { get; }
        public Int32 UpdateOrder { get; }
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        #endregion

        #region Implementation of IDisposable

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            
        }

        #endregion
    }
}
