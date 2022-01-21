namespace Microsoft.Xna.Framework
{
    public static class CRectangleExtensions
    {
        /// <summary>
        /// Retorna o valor necessário para voltar a posição anterior de uma intersecção entre dois retângulos
        /// </summary>
        public static Point Subtract(this Rectangle rectangle, Rectangle other)
        {
            Rectangle rcr = Rectangle.Intersect(rectangle, other);
            Point sub = Point.Zero;

            if (rcr == Rectangle.Empty)
                return sub;            

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
                if (rectangle.Right > other.Left && rectangle.Right < other.Right)
                {
                    sub.X -= rectangle.Right - other.Left;
                }
                else if (rectangle.Left < other.Right && rectangle.Left > other.Left)
                {
                    sub.X -= rectangle.Left - other.Right;
                }
            }
            //O contrário é uma colisão vertical.
            if (rcr.Width > rcr.Height)
            {
                if (rectangle.Bottom > other.Top && rectangle.Bottom < other.Bottom)
                {
                    sub.Y -= rectangle.Bottom - other.Top;
                }
                else if (rectangle.Top < other.Bottom && rectangle.Top > other.Top)
                {
                    sub.Y -= rectangle.Top - other.Bottom;
                }
            }

            return sub;
        }

        /// <summary>
        /// Transform the rectangle to a polygon object, with the values [0]left-top, [1] right-top, [2] right-bottom e [3]left-bottom.
        /// </summary>
        public static CPoly ToPolygon(this Rectangle rectangle)
        {
            CPoly polygon = new CPoly(4);
            return ToPolygon(rectangle, polygon);
        }

        /// <summary>
        /// Transform the rectangle to a polygon object, with the values [0]left-top, [1] right-top, [2] right-bottom e [3]left-bottom.
        /// </summary>
        /// <param name="rectangle">The rectangle object.</param>
        /// <param name="polygon">The polygon that will receive the coordinates of the rectangle.</param>
        public static CPoly ToPolygon(this Rectangle rectangle, CPoly polygon)
        {
            Vector2 v1 = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 v2 = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 v3 = new Vector2(rectangle.Right, rectangle.Bottom);
            Vector2 v4 = new Vector2(rectangle.Left, rectangle.Bottom);

            polygon.Set(v1, v2, v3, v4);

            return polygon;
        }

        /// <summary>
        /// Get the rotation of the rectangle as an array with the values [0]left-top, [1] right-top, [2] right-bottom e [3]left-bottom..
        /// </summary>
        /// <param name="rectangle">The rectangle object.</param>
        /// <param name="origin">The origin the origin value for the rotation.</param>
        /// <param name="radians">The rotation value in radians.</param>
        public static Point[] GetRotationAsArray(this Rectangle rectangle, Vector2 origin, float radians)
        {
            Vector2 finalOrigin;
            finalOrigin.X = rectangle.X + origin.X;
            finalOrigin.Y = rectangle.Y + origin.Y;

            Point left_top = new Point(rectangle.Left, rectangle.Top);
            Point right_top = new Point(rectangle.Right, rectangle.Top);
            Point right_bottom = new Point(rectangle.Right, rectangle.Bottom);
            Point left_bottom = new Point(rectangle.Left, rectangle.Bottom);

            //Top-Left
            Point p1 = left_top.Rotate(finalOrigin, radians);
            //Top-Right
            Point p2 = right_top.Rotate(finalOrigin, radians);
            //Bottom-Right
            Point p3 = right_bottom.Rotate(finalOrigin, radians);
            //Bottom-Left
            Point p4 = left_bottom.Rotate(finalOrigin, radians);

            return new Point[] { p1, p2, p3, p4 };
        }

        /// <summary>
        /// Get the rotation of the rectangle as an polygon with the values [0]left-top, [1] right-top, [2] right-bottom e [3]left-bottom..
        /// </summary>
        /// <param name="rectangle">The rectangle object.</param>
        /// <param name="origin">The origin the origin value for the rotation.</param>
        /// <param name="radians">The rotation value in radians.</param>
        public static CPoly GetRotationAsPolygon(this Rectangle rectangle, Vector2 origin, float radians)
        {
            CPoly poly = new CPoly(4);
            return GetRotationAsPolygon(rectangle, origin, radians, poly);
        }

        /// <summary>
        /// Get the rotation of the rectangle as an array with the values [0]left-top, [1] right-top, [2] right-bottom e [3]left-bottom..
        /// </summary>
        /// <param name="rectangle">The rectangle object.</param>
        /// <param name="origin">The origin the origin value for the rotation.</param>
        /// <param name="radians">The rotation value in radians.</param>
        /// <param name="polygon">The polygon that will receive the rotated coordinates of the rectangle.</param>
        public static CPoly GetRotationAsPolygon(this Rectangle rectangle, Vector2 origin, float radians, CPoly polygon)
        {            
            Point[] points = GetRotationAsArray(rectangle, origin, radians);
            
            Vector2[] vertices = new Vector2[points.Length];
            vertices[0] = points[0].ToVector2();
            vertices[1] = points[1].ToVector2();
            vertices[2] = points[2].ToVector2();
            vertices[3] = points[3].ToVector2();

            polygon.Set(vertices[0], vertices[1], vertices[2], vertices[3]);

            return polygon;
        }
    }
}
