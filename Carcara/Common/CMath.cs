using System;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Providencia acesso a cálculos matemáticos.
    /// </summary>
    public static class CMath
    {
        /// <summary>
        /// Retorna um vetor normalizado que representa a direção de uma posição para outra.
        /// </summary>
        /// <param name="position">A posição atual.</param>
        /// <param name="destination">A posição a ser alcançada</param>
        public static Vector2 Direction(Vector2 position, Vector2 destination)
        {
            Vector2 direction = destination - position;
            direction.Normalize();
            return direction;
        }

        /// <summary>
        /// Obtém a posição de um ponto ao informar a origem e o grau de rotação.        
        /// </summary>
        /// <param name="point">A posição do ponto na tela.</param>
        /// <param name="origin">A coordenada na tela que será o pivô para rotação, a origem.</param>
        /// <param name="radians">O grau da rotação em radianos.</param>
        public static Point GetRotation(Point point, Vector2 origin, float radians)
        {
            return GetRotation(point.ToVector2(), origin, radians).ToPoint();
        }

        /// <summary>
        /// Obtém a posição de um vetor ao informar a origem e o grau de rotação.        
        /// </summary>
        /// <param name="point">A posição do vetor na tela.</param>
        /// <param name="origin">A coordenada na tela que será o pivô para rotação, a origem.</param>
        /// <param name="radians">O grau da rotação em radianos.</param>
        public static Vector2 GetRotation(Vector2 point, Vector2 origin, float radians)
        {
            // Cálculo retirado de: http://www.inf.pucrs.br/~pinho/CG/Aulas/Vis2d/Instanciamento/Instanciamento.htm
            //
            // Fórmula:
            //
            // xf = (xo - xr) * cos(@) - (yo - yr) * sin(@) + xr
            // yf = (yo - yr) * cos(@) + (xo - xr) * sin(@) + yr
            //
            // (xo, yo) = Ponto que você deseja rotacionar
            // (xr, yr) = Ponto em que você vai rotacionar o ponto acima(no seu caso o centro do retangulo)
            // (xf, yf) = O novo local do ponto rotacionado
            // @ = Angulo de rotação

            var resultX = (point.X - origin.X) * Math.Cos(radians) - (point.Y - origin.Y) * Math.Sin(radians) + origin.X;
            var resultY = (point.Y - origin.Y) * Math.Cos(radians) + (point.X - origin.X) * Math.Sin(radians) + origin.Y;

            return new Vector2((float)resultX, (float)resultY);
        }

        /// <summary>
        /// Retorna o valor necessário para voltar a posição anterior da intersecção entre dois retângulos.
        /// </summary>
        public static Vector2 Subtract(Rectangle rectangleA, Rectangle rectangleB)
        {
            Rectangle rcr = Rectangle.Intersect(rectangleA, rectangleB);
            Vector2 sub = Vector2.Zero;

            //Lógica de colisão entre retângulos

            //Se na intersecção entre os retângulos
            //A altura é maior que a largura da intersecção,
            //Então significa que foi uma colisão lateral.
            if (rcr.Height > rcr.Width)
            {
                //Verificamos o limite.
                //Se a ponta direita é maior que a ponta esquerda do outro retângulo
                //e essa ponta está dentro do outro retângulo.
                //Então encontramos o valor de subtração.
                //A lógica serve para o restante.
                if (rectangleA.Right > rectangleB.Left && rectangleA.Right < rectangleB.Right)
                {
                    sub.X -= rectangleA.Right - rectangleB.Left;
                }
                else if (rectangleA.Left < rectangleB.Right && rectangleA.Left > rectangleB.Left)
                {
                    sub.X -= rectangleA.Left - rectangleB.Right;
                }
            }
            //O contrário é uma colisão vertical.
            if (rcr.Width > rcr.Height)
            {
                if (rectangleA.Bottom > rectangleB.Top && rectangleA.Bottom < rectangleB.Bottom)
                {
                    sub.Y -= rectangleA.Bottom - rectangleB.Top;
                }
                else if (rectangleA.Top < rectangleB.Bottom && rectangleA.Top > rectangleB.Top)
                {
                    sub.Y -= rectangleA.Top - rectangleB.Bottom;
                }
            }

            return sub;
        }
    }
}
