using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Graphics
{
    public interface ICDrawTransformable
    {
        CDrawTransform DrawTransform { get; set; }
    }
}
