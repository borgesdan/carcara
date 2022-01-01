using System.Text;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Representa um objeto de texto transformável.
    /// </summary>
    public class CText : ICTransformable, ICDrawTransformable, ICBoundsable, ICDrawable
    {
        /// <summary>Obtém ou define fonte a ser utilizada para desenho.</summary>
        public SpriteFont Font { get; set; }
        /// <summary>Obtém ou define o texto a ser desenhado.</summary>
        public StringBuilder Text { get; set; }
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
                Vector2 size = Font.MeasureString(Text.ToString());
                return CBounds.Get(Transform, (int)size.X, (int)size.Y, DrawTransform.Origin);
            }
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="font">Define fonte a ser utilizada para desenho</param>
        /// <param name="text">Define o texto a ser desenhado.</param>
        public CText(SpriteFont font, StringBuilder text)
        {
            Font = font;
            Text = text;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public CText(CText source)
        {
            Font = source.Font;
            Text = new StringBuilder(source.Text.ToString());
            DrawTransform = new CDrawTransform(source.DrawTransform);
            Transform = new CTransform(source.Transform);
        }

        /// <summary>
        /// Desenha o texto na tela.
        /// </summary>
        /// <param name="spriteBatch">O objeto spriteBatch para desenho.</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Font == null
                || Text == null
                || string.IsNullOrWhiteSpace(Text.ToString())
                || !IsVisible)
            return;

            CDrawArgs args = new CDrawArgs(Transform, DrawTransform);

            spriteBatch.DrawString(Font, Text, args);
        }
    }
}