using System;

namespace Microsoft.Xna.Framework
{
    public static class CVector2Extensions
    {
        /// <summary>
        /// Transform a Vector2 into a Vector3 where Z is equals to 0.
        /// </summary>
        public static Vector3 ToVector3(this Vector2 vector2) => new Vector3(vector2, 0);

        /// <summary>
        /// Transform a Vector2 into a Vector3.
        /// </summary>
        /// <param name="z">Set the z-axis value.</param>
        public static Vector3 ToVector3(this Vector2 vector2, float z) => new Vector3(vector2, z);

        /// <summary>Retorna um vetor normalizado que representa a direção de uma vetor para outro vetor.</summary>
        /// <param name="vector2">A posição atual.</param>
        /// <param name="destination">A posição de destino.</param>
        public static Vector2 Direction(this Vector2 vector2, Vector2 destination)
        {
            Vector2 direction = destination - vector2;
            direction.Normalize();
            return direction;
        }

        /// <summary>
        /// Obtém a posição rotacionada de um vetor ao informar uma coordenada de tela como origem e o grau de rotação em radianos.        
        /// </summary>
        /// <param name="vector">A posição do vetor na tela.</param>
        /// <param name="origin">Coordenada de tela que servirá como origem da rotação.</param>
        /// <param name="radians">O grau da rotação em radianos.</param>
        public static Vector2 Rotate(this Vector2 vector, Vector2 origin, float radians)
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

            var resultX = (vector.X - origin.X) * Math.Cos(radians) - (vector.Y - origin.Y) * Math.Sin(radians) + origin.X;
            var resultY = (vector.Y - origin.Y) * Math.Cos(radians) + (vector.X - origin.X) * Math.Sin(radians) + origin.Y;

            return new Vector2((float)resultX, (float)resultY);
        }
    }
}
