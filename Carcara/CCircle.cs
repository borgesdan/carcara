namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Representa um cículo 2D.
    /// </summary>
    public struct CCircle
    {        
        /// <summary>Obtém o raio do círculo.</summary>
        public float Radius;
        /// <summary>Obtém a posição do centro do círculo no eixo X.</summary>
        public float X;
        /// <summary>Obtém a posição do centro do círculo no eixo Y.</summary>
        public float Y;
        
        /// <summary>Obtém a posição do centro do círculo.</summary>
        public Vector2 Center => new Vector2(X, Y);

        /// <summary>
        /// Cria um novo objeto Circle.
        /// </summary>
        /// <param name="center">A posição do centro do círculo.</param>
        /// <param name="radius">O raio do círculo.A posição do centro do círculo.</param>
        public CCircle(Vector2 center, float radius)
        {
            X = center.X;
            Y = center.Y;
            Radius = radius;
        }

        /// <summary>
        /// Determina se o círculo intersecta outro círculo.
        /// </summary>
        public bool Intersects(CCircle other)
        {
            return Vector2.Distance(Center, other.Center) < Radius + other.Radius;
        }

        /// <summary>
        /// Determina se o círculo intersecta um retângulo.
        /// </summary>
        public bool Intersects(Rectangle rectangle)
        {
            Vector2 v = new Vector2(MathHelper.Clamp(Center.X, rectangle.Left, rectangle.Right),
                                    MathHelper.Clamp(Center.Y, rectangle.Top, rectangle.Bottom));

            Vector2 direction = Center - v;
            float distanceSquared = direction.LengthSquared();

            return ((distanceSquared > 0) && (distanceSquared < Radius * Radius));
        }
    }
}
