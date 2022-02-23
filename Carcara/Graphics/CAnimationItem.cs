using System;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Representa um objeto de desenho para a classe <see cref="CAnimation"/>.
    /// </summary>
    public class CAnimationItem
    {
        /// <summary>
        /// Obtém ou define a textura para desenho.
        /// </summary>
        public Texture2D Texture { get; set; }
        /// <summary>
        /// Obtém ou define os recortes da textura.
        /// </summary>
        public Rectangle[] Frames { get; set; }

        /// <summary>
        /// Obtém o número de recortes da textura.
        /// </summary>
        public int FrameCount
        {
            get
            {
                if (Frames == null)
                    return 0;
                else
                    return Frames.Length;
            }
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="texture">A textura para desenho.</param>
        /// <param name="frames">Os recortes da textura (pode ser nulo).</param>
        public CAnimationItem(Texture2D texture, params Rectangle[] frames)
        {
            Texture = texture;
            Frames = frames;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="game">Informa a classe Game principal do jogo.</param>
        /// <param name="path">Informa o caminho da textura a ser utilizada.</param>
        /// <param name="frames">Informa os recortes da textura (pode ser nulo).</param>
        public CAnimationItem(Game game, string path, params Rectangle[] frames)
        {
            Texture = game.Content.Load<Texture2D>(path);
            Frames = frames;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public CAnimationItem(CAnimationItem source)
        {
            Texture = source.Texture;
            Array.Copy(source.Frames, this.Frames, source.Frames.Length);
        }
    }
}
