using System;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Estrura que enumera as direções de um jogo 2D.
    /// </summary>
    [Flags]
    public enum CDirection2D : byte
    {
        Up = 2,
        Down = 4,
        Right = 8,
        Left = 16
    }
}
