using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Microsoft.Xna.Framework
{
    public static class CPointExtensions
    {
        /// <summary>
        /// Obtém a posição rotacionada de um ponto ao informar uma coordenada de tela como origem e o grau de rotação em radianos.        
        /// </summary>
        /// <param name="point">A posição do vetor na tela.</param>
        /// <param name="origin">Coordenada de tela que servirá como origem da rotação.</param>
        /// <param name="radians">O grau da rotação em radianos.</param>
        public static Point Rotate(this Point point, Vector2 origin, float radians)
        {
            return point.ToVector2().Rotate(origin, radians).ToPoint();
        }
    }
}
