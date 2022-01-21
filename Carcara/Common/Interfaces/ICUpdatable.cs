namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Represents a updateble object.
    /// </summary>
    public interface ICUpdatable
    {
        /// <summary>
        /// Update the game object.
        /// </summary>
        /// <param name="gameTime">Gets the game time state.</param>
        void Update(GameTime gameTime);
    }    
}
