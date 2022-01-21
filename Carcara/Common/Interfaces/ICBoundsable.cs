namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Represents an object that exposes its boundaries on the screen.
    /// </summary>
    public interface ICBoundsable
    {
        /// <summary>
        /// Gets the object's screen bounds.
        /// </summary>
        Rectangle Bounds { get; }
    }
}
