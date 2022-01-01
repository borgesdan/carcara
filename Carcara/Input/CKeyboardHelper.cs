namespace Microsoft.Xna.Framework.Input
{
    /// <summary>Classe que gerencia e auxilia nas entradas do jogador com um teclado.</summary>
    public class CKeyboardHelper : ICInput<Keys>
    {
        /// <sumary>Obtém ou define se esta instância está disponível para ser atualizada.</sumary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>Obtém o estado atual do teclado.</summary>
        public KeyboardState State { get; private set; }
        /// <summary>Obtém o estado anterior do teclado antes da atualização.</summary>
        public KeyboardState OldState { get; private set; }

        /// <summary>
        /// Inicializa uma nova instância de KeyboardHelper.
        /// </summary>
        public CKeyboardHelper() { }

        /// <summary>
        /// Inicializa uma nova instância de KeyboardHelper.
        /// </summary>
        /// <param name="state">O estado inicializado.</param>
        public CKeyboardHelper(KeyboardState state)
        {
            State = state;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public CKeyboardHelper(CKeyboardHelper source)
        {
            IsEnabled = source.IsEnabled;
            State = source.State;
            OldState = source.OldState;
        }

        /// <summary>Atualiza os estados do teclado.</summary>
        /// <param name="gameTime">Obtém os tempos de jogo.</param>
        public void Update(GameTime gameTime)
        {
            if (IsEnabled)
            {
                OldState = State;
                State = Keyboard.GetState();
            }            
        }

        /// <summary>Verifica se a tecla está sendo pressionada.</summary>
        /// <param name="key">A tecla a ser verificada.</param>
        public bool Hold(Keys key)
        {
            return State.IsKeyDown(key);
        }

        /// <summary>Verifica se a tecla estava liberada e foi pressionada.</summary>
        /// <param name="key">A tecla a ser verificada.</param>
        public bool Pressed(Keys key)
        {
            return OldState.IsKeyUp(key) 
                && State.IsKeyDown(key);
        }        

        /// <summary>Verifica se a tecla está liberada.</summary>   
        /// <param name="key">A tecla a ser verificada.</param>
        public bool Released(Keys key)
        {
            return State.IsKeyUp(key);
        }
    }
}