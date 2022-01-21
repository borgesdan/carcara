using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Represents a drawable object.
    /// </summary>
    public interface ICDrawable
    {
        /// <summary>
        /// Draw the game object.
        /// </summary>
        /// <param name="gameTime">Gets the game time state.</param>
        /// <param name="spriteBatch">The SpriteBatch object for drawing.</param>
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
