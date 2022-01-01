using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Providência acesso as tranformações de desenho na classe SpriteBatch.
    /// </summary>
    public class CDrawTransform
    {
        /// <summary>
        /// Obtém ou define a cor de desenho.
        /// </summary>
        public Color Color { get; set; } = Color.White;
        /// <summary>
        /// Obtém ou define a origem para posição e rotação.
        /// </summary>
        public Vector2 Origin { get; set; } = Vector2.Zero;
        /// <summary>
        /// Obtém ou define os efeitos de espelhamento.
        /// </summary>
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;
        /// <summary>
        /// Obtém ou define a profundidade do desenho (z-index).
        /// </summary>
        public float LayerDetph { get; set; } = 0;
        /// <summary>Obtém ou define a posição o retângulo de destino do desenho (pode ser null).</summary>
        public Rectangle? Destination { get; set; } = null;
        /// <summary>Obtém ou define o retângulo que representa a parte da imagem a ser desenhada (pode ser null).</summary>
        public Rectangle? Source { get; set; } = null;

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        public CDrawTransform()
        {

        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public CDrawTransform(CDrawTransform source)
        {
            Color = source.Color;
            Origin = source.Origin;
            Effects = source.Effects;
            LayerDetph = source.LayerDetph;
            Destination = source.Destination;
            Source = source.Source;
        }
    }
}
