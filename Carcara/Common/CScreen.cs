using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Representa uma tela base de jogo.
    /// </summary>
    public class CScreen : IDisposable
    {
        /// <summary>Obtém a classe Game do jogo.</summary>
        protected Game Game { get; }
        /// <summary>Obtém acesso ao dispositivo gráfico.</summary>
        protected GraphicsDevice GraphicsDevice { get => Game.GraphicsDevice; }
        /// <summary>Obtém acesso aos conteúdos do jogo.</summary>
        protected ContentManager Content { get => Game.Content; }
        /// <summary>Obtém ou define o nome da tela.</summary>
        public string Name { get; }
        /// <summary>Obtém ou define se a tela está habilitada a sofrer atualizações.</summary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>Obtém ou define se a tela está habilitada a ser desenhada na tela.</summary>
        public bool IsVisible { get; set; } = true;     

        /// <summary>Inicializa uma nova instância da classe.</summary> 
        /// <param name="game">A classe Game do jogo.</param>
        /// <param name="name">Define o nome da tela.</param>
        public CScreen(Game game, string name = "")
        {
            Game = game;
            Name = name;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A tela a ser copiada.</param>
        public CScreen(CScreen source)
        {
            Name = source.Name;
            IsEnabled = source.IsEnabled;
            IsVisible = source.IsVisible;
        }

        /// <summary>
        /// Atualiza a tela.
        /// </summary>
        /// <param name="gameTime">Obtém acesso aos tempos de jogo.</param>
        public void Update(GameTime gameTime) 
        {
            if (IsEnabled)
            {
                OnUpdate(gameTime);                
            }
        }

        /// <summary>
        /// Desenha a tela.
        /// </summary>
        /// <param name="gameTime">Obtém acesso aos tempos de jogo.</param>
        /// <param name="spriteBatch">O objeto SpriteBatch para desenho.</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            if (IsVisible)
            {
                OnDraw(gameTime, spriteBatch);
            }
        }

        /// <summary>
        /// Carrega as propriedades da tela definidas neste método.
        /// </summary>
        public void Load()
        {
            OnLoad();
        }

        /// <summary>
        /// Descarrega a tela sem chamar o método Dispose().
        /// </summary>
        public void Unload()
        {
            OnUnload();
        }

        /// <summary>
        /// Sobrecarregue este método para definir as atualizações da tela a ser chamado no método Update.
        /// </summary>
        /// <param name="gameTime">Obtém o acesso aos tempos de jogo.</param>
        protected virtual void OnUpdate(GameTime gameTime) { }

        /// <summary>
        /// Sobrecarregue este método para definir o desenho da tela a ser chamado no método Draw.
        /// </summary>
        /// <param name="gameTime">Obtém o acesso aos tempos de jogo.</param>
        protected virtual void OnDraw(GameTime gameTime, SpriteBatch spriteBatch) { }

        /// <summary>
        /// Sobrecarregue esse método caso deseje carregar sua tela a ser chamada no método Load().
        /// </summary>
        protected virtual void OnLoad() { }

        /// <summary>
        /// Sobrecarregue esse método caso deseje descarregar sua tela a ser chamada no método Unload().
        /// </summary>
        protected virtual void OnUnload() { }

        /// <summary>Redefine a tela ao estado padrão.</summary>
        public virtual void Reset()
        {
            IsEnabled = true;
            IsVisible = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposed)
        {
        }
    }
}