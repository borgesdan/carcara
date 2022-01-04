using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework
{
    public interface ICDrawable
    {
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
