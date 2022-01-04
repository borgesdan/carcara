using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Xna.Framework
{
    public static class CBounds
    {
        /// <summary>
        /// Calcula e define os limites de um objeto 2D ao informar sua posição, escala e origem.
        /// </summary>
        /// <param name="transform">A transformação do objeto.</param>
        /// <param name="width">Informa o valor da largura do objeto.</param>
        /// <param name="height">Informa o valor da altura do objeto.</param>
        /// <param name="origin">Informa a origem para o cálculo.</param> 
        public static Rectangle Get(CTransform transform, int width, int height, Vector2 origin)
        {
            //Posição
            int x = (int)transform.X;
            int y = (int)transform.Y;
            //Escala
            float sx = transform.Xs;
            float sy = transform.Ys;
            //Origem
            float ox = origin.X;
            float oy = origin.Y;

            //Obtém uma matrix: -origin * escala * posição (excluíndo a rotação)
            Matrix m = Matrix.CreateTranslation(-ox, -oy, 0)
                * Matrix.CreateScale(sx, sy, 1)
                * Matrix.CreateTranslation(x, y, 0);

            //Os limites finais
            Rectangle rectangle = new Rectangle((int)m.Translation.X, (int)m.Translation.Y, (int)(width * sx), (int)(height * sy));
            return rectangle;
        }
    }
}