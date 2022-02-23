namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Fornece acesso a métodos para cálculo dos limites de tela de um objeto de jogo.
    /// </summary>
    public static class CBounds
    {
        /// <summary>
        /// Calcula e retorna os limites de um objeto 2D ao informar sua posição, escala e origem.
        /// </summary>
        /// <param name="transform">A transformação do objeto.</param>
        /// <param name="width">Informa o valor da largura do objeto.</param>
        /// <param name="height">Informa o valor da altura do objeto.</param>
        /// <param name="origin">Informa a origem para o cálculo.</param> 
        public static Rectangle Get(CTransform transform, int width, int height, Vector2 origin)
        {
            return Get(transform.Position2, transform.Scale2, width, height, origin);
        }

        /// <summary>
        /// Calcula e retorna os limites de um objeto 2D ao informar sua posição, escala e origem.
        /// </summary>
        /// <param name="transform">A transformação do objeto.</param>
        /// <param name="width">Informa o valor da largura do objeto.</param>
        /// <param name="height">Informa o valor da altura do objeto.</param>
        /// <param name="origin">Informa a origem para o cálculo.</param> 
        public static Rectangle Get(CTransformS transform, int width, int height, Vector2 origin)
        {
            return Get(transform.Position.ToVector2(), transform.Scale.ToVector2(), width, height, origin); ;
        }

        /// <summary>
        /// Calcula e retorna os limites de um objeto 2D ao informar sua posição, escala e origem.
        /// </summary>
        /// <param name="position">A posição de tela do objeto de jogo.</param>
        /// <param name="scale">A escala do objeto de jogo.</param>
        /// <param name="width">Informa o valor da largura do objeto.</param>
        /// <param name="height">Informa o valor da altura do objeto.</param>
        /// <param name="origin">Informa a origem para o cálculo.</param> 
        public static Rectangle Get(Vector2 position, Vector2 scale, int width, int height, Vector2 origin)
        {
            //Posição
            int x = (int)position.X;
            int y = (int)position.Y;
            //Escala
            float sx = scale.X;
            float sy = scale.Y;
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