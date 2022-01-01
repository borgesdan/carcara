namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Representa um objeto com textura transformável.
    /// </summary>
    public class CSprite : ICTransformable, ICDrawTransformable, ICDrawable, ICBoundsable
    {
        /// <summary>Obtém ou define a textura para desenho.</summary>
        public Texture2D Texture { get; set; }
        /// <summary>Obtém ou define as transformações de desenho.</summary>
        public CDrawTransform DrawTransform { get; set; } = new CDrawTransform();
        /// <summary>Obtém ou define as transformações de tela.</summary>
        public CTransform Transform { get; set; } = new CTransform();
        /// <summary>Obtém ou define se o objeto é visível em tela.</summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// Obtém os limites de tela do objeto.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                Point size;

                if (DrawTransform.Destination != null)
                    size = DrawTransform.Destination.Value.Size;
                else if (DrawTransform.Source != null)
                    size = DrawTransform.Source.Value.Size;
                else
                    size = Texture.Bounds.Size;

                return CBounds.Get(Transform, (int)size.X, (int)size.Y, DrawTransform.Origin);
            }
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="texture">Define a textura para desenho.</param>
        public CSprite(Texture2D texture)
        {
            Texture = texture;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public CSprite(CSprite source)
        {
            Texture = source.Texture;
            DrawTransform = new CDrawTransform(source.DrawTransform);
            Transform = new CTransform(source.Transform);
        }

        /// <summary>
        /// Desenha a textura na tela.
        /// </summary>
        /// <param name="spriteBatch">O objeto spriteBatch para desenho.</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(Texture == null || !IsVisible)
                return;

            CDrawArgs args = new CDrawArgs(Transform, DrawTransform);

            spriteBatch.Draw(Texture, args);
        }
    }
}
