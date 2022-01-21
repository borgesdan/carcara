using System;

namespace Microsoft.Xna.Framework.Graphics
{ 
	/// <summary>
	/// Classe que expõe métodos de gerenciamento de texturas.
	/// </summary>
	public static class CTextureData
	{
        /// <summary>
        /// Obtém um objeto Texture2D preechido com uma cor definida.
        /// </summary>
        /// <param name="game">A instância da classe Game.</param>
        /// <param name="width">Define a largura do retângulo.</param>
        /// <param name="height">Define a altura do retângulo.</param>
        /// <param name="color">Define a cor do retângulo.</param>
        public static Texture2D GetFilledTexture(Game game, int width, int height, Color color)
        {
            Color[] data;
            Texture2D texture;

            //Inicializa a textura com o tamanho pré-definido
            texture = new Texture2D(game.GraphicsDevice, width, height);
            //Inicializa o array de cores, sendo a quantidade a multiplicação da altura e largura do retângulo.
            data = new Color[texture.Width * texture.Height];

            //Cada cor do array é setada com a cor definida do argumento.
            for (int i = 0; i < data.Length; ++i)
                data[i] = color;

            //Seta o array de cores a textura
            texture.SetData(data);

            return texture;
        }       

        /// <summary>
        /// Obtém o conteúdo de cores passando o frame (com o tamanho do data) e o Color data.
        /// </summary>
        /// <param name="data">O array de cores recebido da textura.</param>
        /// <param name="width">A largura da textura.</param>
        /// <param name="height">A altura da textura.</param>
        /// <param name="effects">Os efeitos de sprite.</param>
        public static Color[] Getdata(Color[] data, int width, int height, SpriteEffects effects)
        {
            return GetData(data, new Rectangle(0, 0, width, height), effects);
        }

        /// <summary>
        /// Obtém o conteúdo de cores passando o frame (com o tamanho do data) e o Color data.
        /// </summary>
        /// <param name="frame">O recorte da textura.</param>
        /// <param name="data">O array de cores recebido da textura.</param>
        /// <param name="effects">Os efeitos de sprite.</param>
        public static Color[] GetData(Color[] data, Rectangle frame, SpriteEffects effects)
		{
            if (effects == SpriteEffects.None)
            {
                //Se não há transformação retorna o array recebido.                
                return data;
            }
            else
            {
                //index do array data
                int index = 0;
                // O array a ser enviado no final
                Color[] final = new Color[data.Length];

                //Se está invertido horizontal e verticalmente
                if (effects == (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically))
                {
                    for (int i = data.Length; i > 0; i--)
                    {
                        final[i - 1] = data[index];
                        index++;
                    }
                }
                //Se somente horizontalmente
                else if (effects == SpriteEffects.FlipHorizontally)
                {
                    for (int i = 0; i < frame.Height; i++)
                    {
                        for (int j = 0; j < frame.Width; j++)
                        {
                            int x = (data.Length / frame.Height) * (i + 1) - j;

                            final[x - 1] = data[index];
                            index++;
                        }
                    }
                }
                //Se somente verticalmente
                else if (effects == SpriteEffects.FlipVertically)
                {
                    int length = data.Length;

                    for (int i = 0; i < frame.Height; i++)
                    {
                        length -= frame.Width;

                        for (int j = 0; j < frame.Width; j++)
                        {
                            int x = length + j;

                            final[x] = data[index];
                            index++;
                        }
                    }
                }

                return final;
            }
        }

        /// <summary>
        /// Retorna true caso os pixels do sprite A intersecta outros pixels do sprite B.
        /// </summary>
        /// <param name="boundsA">Os limites atuais do objeto 1 (posição e tamanho).</param>
        /// <param name="dataA">O array de cores recebido da textura 1.</param>
        /// <param name="boundsB">Os limites atuais do objeto 2 (posição e tamanho).</param>
        /// <param name="dataB">O array de cores recebido da textura 2.</param>
        public static bool PerPixelCollision(Rectangle boundsA, Color[] dataA, Rectangle boundsB, Color[] dataB)
        {
            bool result = false;

            int top = Math.Max(boundsA.Top, boundsB.Top);
            int bottom = Math.Min(boundsA.Bottom, boundsB.Bottom);
            int left = Math.Max(boundsA.Left, boundsB.Left);
            int right = Math.Min(boundsA.Right, boundsB.Right);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color colorOne = dataA[(x - boundsA.Left) + (y - boundsA.Top) * boundsA.Width];
                    Color colorTwo = dataB[(x - boundsB.Left) + (y - boundsB.Top) * boundsB.Width];

                    if (colorOne.A != 0 && colorTwo.A != 0)
                        result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Retorna true caso os pixels do ator A está intersectando os pixels do ator B utilizando matrizes para cálculos.
        /// </summary>
        /// <param name="transformA">A matrix de transformação do objeto A.</param>
        /// <param name="widthA">A largura do objeto A.</param>
        /// <param name="heightA">A altura do objeto A.</param>
        /// <param name="dataA">O array de cores do objeto A.</param>
        /// <param name="transformB">A matrix de transformação do objeto B.</param>
        /// <param name="widthB">A largura do objeto B.</param>
        /// <param name="heightB">A altura do objeto B.</param>
        /// <param name="dataB">O array de cores do objeto B.</param>
        public static bool PerPixelIntersects(Matrix transformA, int widthA, int heightA, Color[] dataA, Matrix transformB, int widthB, int heightB, Color[] dataB)
        {
            // Calculate a matrix which transforms from A's local space into
            // world space and then into B's local space
            Matrix transformAToB = transformA * Matrix.Invert(transformB);

            // When a point moves in A's local space, it moves in B's local space with a
            // fixed direction and distance proportional to the movement in A.
            // This algorithm steps through A one pixel at a time along A's X and Y axes
            // Calculate the analogous steps in B:
            Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
            Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

            // Calculate the top left corner of A in B's local space
            // This variable will be reused to keep track of the start of each row
            Vector2 yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

            // For each row of pixels in A
            for (int yA = 0; yA < heightA; yA++)
            {
                // Start at the beginning of the row
                Vector2 posInB = yPosInB;

                // For each pixel in this row
                for (int xA = 0; xA < widthA; xA++)
                {
                    // Round to the nearest pixel
                    int xB = (int)Math.Round(posInB.X);
                    int yB = (int)Math.Round(posInB.Y);

                    // If the pixel lies within the bounds of B
                    if (0 <= xB && xB < widthB &&
                        0 <= yB && yB < heightB)
                    {
                        // Get the colors of the overlapping pixels
                        Color colorA = dataA[xA + yA * widthA];
                        Color colorB = dataB[xB + yB * widthB];

                        // If both pixels are not completely transparent,
                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            // then an intersection has been found
                            return true;
                        }
                    }

                    // Move to the next pixel in the row
                    posInB += stepX;
                }

                // Move to the next row
                yPosInB += stepY;
            }

            // No intersection found
            return false;
        }
    }
}