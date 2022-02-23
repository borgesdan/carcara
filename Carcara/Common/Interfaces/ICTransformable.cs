namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Represents a transformable object.
    /// </summary>
    public interface ICTransformable
    {
        /// <summary>
        /// Gets or sets the game object transform.
        /// </summary>
        CTransform Transform { get; set; }
    }

    public interface ICTransformableS
    {
        /// <summary>
        /// Gets or sets the game object transform.
        /// </summary>
        CTransformS Transform { get; set; }
    }
}