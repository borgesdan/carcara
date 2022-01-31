using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Xna.Framework.Input
{
    /// <summary>
    /// Contém os valores de um evento de um comando.
    /// </summary>
    public class CCommandEventArgs : EventArgs
    {
        /// <summary>
        /// Obtém acesso aos tempos de jogo.
        /// </summary>
        public GameTime GameTime { get; }        

        public CCommandEventArgs(GameTime gameTime)
        {
            GameTime = gameTime;
        }
    }
}