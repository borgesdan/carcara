using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Representa um objeto que implementa a propriedade de transformação de desenho.
    /// </summary>
    public interface ICDrawTransformable
    {
        /// <summary>
        /// Obtém ou define o acesso as transformações de desenho.
        /// </summary>
        CDrawTransform DrawTransform { get; set; }
    }
}
