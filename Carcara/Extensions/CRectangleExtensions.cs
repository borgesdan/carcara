using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Xna.Framework
{
    public static class CRectangleExtensions
    {
        /// <summary>
        /// Converte o objeto retângulo em objeto polígono. 
        /// O polígono retornado terá os vertices [0]left-top, [1] right-top, [2] right-bottom e [3]left-bottom.
        /// </summary>
        /// <param name="rectangle">O retângulo a ser rotacionado.</param>
        public static CPolyForm ToPolygon(this Rectangle rectangle)
        {
            Vector2 v1 = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 v2 = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 v3 = new Vector2(rectangle.Right, rectangle.Bottom);
            Vector2 v4 = new Vector2(rectangle.Left, rectangle.Bottom);

            return new CPolyForm(v1, v2, v3, v4);
        }

        /// <summary>
        /// Converte o objeto retângulo em objeto polígono. 
        /// O polígono retornado terá os vertices [0]left-top, [1] right-top, [2] right-bottom e [3]left-bottom.
        /// </summary>
        /// <param name="rectangle">O retângulo a ser rotacionado.</param>
        /// <param name="polygon">O polígono a ser adicionado os pontos do retângulo.</param>
        public static CPolyForm ToPolygon(this Rectangle rectangle, CPolyForm polygon)
        {
            Vector2 v1 = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 v2 = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 v3 = new Vector2(rectangle.Right, rectangle.Bottom);
            Vector2 v4 = new Vector2(rectangle.Left, rectangle.Bottom);

            polygon.Set(v1, v2, v3, v4);

            return polygon;
        }

        /// <summary>
        /// Obtém a posição de um retângulo rotacionado ao informar a origem e o grau de rotação.
        /// O polígono retornado terá os vertices [0]left-top, [1] right-top, [2] right-bottom e [3]left-bottom.
        /// </summary>
        /// <param name="rectangle">O retângulo a ser rotacionado.</param>
        /// <param name="origin">A coordenada na tela que será o pivô para rotação, a origem.</param>
        /// <param name="radians">O grau da rotação em radianos.</param>
        public static CPolyForm GetRotation(this Rectangle rectangle, Vector2 origin, float radians)
        {
            //Top-Left
            Vector2 p1 = CMath.GetRotation(new Vector2(rectangle.Left, rectangle.Top), origin, radians);
            //Top-Right
            Vector2 p2 = CMath.GetRotation(new Vector2(rectangle.Right, rectangle.Top), origin, radians);
            //Bottom-Right
            Vector2 p3 = CMath.GetRotation(new Vector2(rectangle.Right, rectangle.Bottom), origin, radians);
            //Bottom-Left
            Vector2 p4 = CMath.GetRotation(new Vector2(rectangle.Left, rectangle.Bottom), origin, radians);

            return new CPolyForm(p1, p2, p3, p4);
        }

        /// <summary>
        /// Obtém a posição de um retângulo rotacionado ao informar a origem e o grau de rotação.
        /// O polígono retornado terá os vertices [0]left-top, [1] right-top, [2] right-bottom e [3]left-bottom.
        /// </summary>
        /// <param name="rectangle">O retângulo a ser rotacionado.</param>
        /// <param name="origin">A coordenada na tela que será o pivô para rotação, a origem.</param>
        /// <param name="radians">O grau da rotação em radianos.</param>
        /// <param name="polygon">A instância do polígono a ser adicionado o retângulo.</param>
        public static CPolyForm GetRotation(this Rectangle rectangle, Vector2 origin, float radians, CPolyForm polygon)
        {
            //Top-Left
            Vector2 p1 = CMath.GetRotation(new Vector2(rectangle.Left, rectangle.Top), origin, radians);
            //Top-Right
            Vector2 p2 = CMath.GetRotation(new Vector2(rectangle.Right, rectangle.Top), origin, radians);
            //Bottom-Right
            Vector2 p3 = CMath.GetRotation(new Vector2(rectangle.Right, rectangle.Bottom), origin, radians);
            //Bottom-Left
            Vector2 p4 = CMath.GetRotation(new Vector2(rectangle.Left, rectangle.Bottom), origin, radians);

            polygon.Set(p1, p2, p3, p4);

            return polygon;
        }
    }
}
