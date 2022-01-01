namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Classe que armazena propriedades para desenho ao utlizar a classe SpriteBatch.
    /// </summary>
    public class CDrawArgs
    {
        /// <summary>Obtém ou define a posição do desenho.</summary>
        public Vector2 Position { get; set; } = Vector2.Zero;
        /// <summary>Obtém ou define a posição o retângulo de destino do desenho (pode ser null).</summary>
        public Rectangle? Destination { get; set; } = null;
        /// <summary>Obtém ou define o retângulo que representa a parte da imagem a ser desenhada (pode ser null).</summary>
        public Rectangle? Source { get; set; } = null;
        /// <summary>Obtém ou define a cor de desenho.</summary>
        public Color Color { get; set; } = Color.White;
        /// <summary>Obtém ou define a rotação do desenho.</summary>
        public float Rotation { get; set; } = 0f;
        /// <summary>Obtém ou define a origem para rotação e posição.</summary>
        public Vector2 Origin { get; set; } = Vector2.Zero;
        /// <summary>Obtém ou define a escala do desenho.</summary>
        public Vector2 Scale { get; set; } = Vector2.One;
        /// <summary>Obtém ou define a efeitos de espelhamento do desenho.</summary>
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;
        /// <summary>Obtém ou define a profundidade do desenho (Z index).</summary>
        public float LayerDepth { get; set; } = 0F;

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        public CDrawArgs() { }

        public CDrawArgs(Vector2 position, Color color)
        {
            Position = position;
            Color = color;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="position">Define a posição de destino.</param>
        /// <param name="color">Define a cor.</param>
        /// <param name="rotation">Define a rotação</param>
        /// <param name="origin">Define a origem para posição e rotação.</param>
        /// <param name="scale">Define a escala.</param>
        /// <param name="effects">Define os efeitos de espelhamento</param>
        /// <param name="layerDetph">Define a profundidade do desenho (z-index).</param>
        public CDrawArgs(Vector2 position, Color color, float rotation, Vector2 origin,
            Vector2 scale, SpriteEffects effects, float layerDetph)
        {
            Position = position;
            Color = color;
            Rotation = rotation;
            Origin = origin;
            Scale = scale;
            Effects = effects;
            LayerDepth = layerDetph;
        }

        public CDrawArgs(Rectangle destination, Color color)
        {
            Destination = destination;
            Color = color;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="destination">Define o retângulo de destino.</param>
        /// <param name="color">Define a cor.</param>
        /// <param name="rotation">Define a rotação</param>
        /// <param name="origin">Define a origem para posição e rotação.</param>
        /// <param name="effects">Define os efeitos de espelhamento</param>
        /// <param name="layerDetph">Define a profundidade do desenho (z-index).</param>
        public CDrawArgs(Rectangle destination, Color color, float rotation, Vector2 origin,
            SpriteEffects effects, float layerDetph)
        {
            Destination = destination;
            Color = color;
            Rotation = rotation;
            Origin = origin;
            Effects = effects;
            LayerDepth = layerDetph;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="transform">Define o objeto de transformação.</param>
        /// <param name="dTransform">Define as transformações de desenho.</param>
        public CDrawArgs(CTransform transform, CDrawTransform dTransform)
        {
            Position = transform.Position2;
            Color = dTransform.Color;
            Rotation = transform.Rotation2;
            Origin = dTransform.Origin;
            Scale = transform.Scale2;
            Effects = dTransform.Effects;
            LayerDepth = dTransform.LayerDetph;
            Destination = dTransform.Destination;
            Source = dTransform.Source;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public CDrawArgs(CDrawArgs source)
        {
            this.Source = source.Source;
            this.Destination = source.Destination;
            this.Scale = source.Scale;
            this.LayerDepth = source.LayerDepth;
            this.Scale = source.Scale;
            this.Effects = source.Effects;
            this.Position = source.Position;
            this.Origin = source.Origin;
            this.Rotation = source.Rotation;
        }
    }
}
