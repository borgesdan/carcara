namespace Microsoft.Xna.Framework.Input
{
    public class CGamePad : ICInput<Buttons>
    {
        PlayerIndex currentIndex = PlayerIndex.One;

        /// <sumary>Obtém ou define se esta instância está disponível para ser atualizada.</sumary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>Obtém o estado atual do teclado.</summary>
        public GamePadState State { get; private set; }
        /// <summary>Obtém o estado anterior do teclado antes da atualização.</summary>
        public GamePadState OldState { get; private set; }
        /// <summary>Obtém ou define o index do jogador.</summary>
        public PlayerIndex PlayerIndex { get => currentIndex; set => SetIndex(value); }

        /// <summary>
        /// Inicializa uma nova instância de CGamePadHelper.
        /// </summary>
        public CGamePad() { }

        /// <summary>
        /// Inicializa uma nova instância de CGamePadHelper.
        /// </summary>
        /// <param name="index">Define o index do jogador.</param>
        public CGamePad(PlayerIndex index) 
        {
            PlayerIndex = index;
        }

        /// <summary>
        /// Inicializa uma nova instância de CGamePadHelper.
        /// </summary>
        /// <param name="state">O estado inicializado.</param>
        /// <param name="index">Define o index do jogador.</param>
        public CGamePad(GamePadState state, PlayerIndex index)
        {
            State = state;
            PlayerIndex = index;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public CGamePad(CGamePad source)
        {
            IsEnabled = source.IsEnabled;
            State = source.State;
            OldState = source.OldState;
            PlayerIndex = source.PlayerIndex;
        }

        /// <summary>Atualiza os estados do GamePad.</summary>
        /// <param name="gameTime">Obtém os tempos de jogo.</param>
        public void Update(GameTime gameTime)
        {
            if (IsEnabled)
            {
                OldState = State;
                State = GamePad.GetState(PlayerIndex);
            }
        }

        /// <summary>
        /// Redefine o index do controle.
        /// </summary>        
        public void SetIndex(PlayerIndex index)
        {
            currentIndex = index;
            State = GamePad.GetState(index);
        }

        /// <summary>Verifica se o botão selecionado está pressionado.</summary>
        public bool Hold(Buttons button)
        {
            return State.IsButtonDown(button);
        }

        /// <summary>Verifica se o botão selecionado estava liberado e foi pressionado.</summary>
        public bool Pressed(Buttons button)
        {
            return OldState.IsButtonUp(button)
                && State.IsButtonDown(button);
        }

        /// <summary>Verifica se o botão selecionado está liberado.</summary>     
        public bool Released(Buttons button)
        {
            return State.IsButtonUp(button);
        }

        /// <summary>
        /// Obtém um Vetor2 relacionado a posição do analógico esquerdo (de -1 a 1).
        /// </summary>        
        public Vector2 GetThumbLeft()
        {
            return State.ThumbSticks.Left;
        }

        /// <summary>
        /// Obtém um Vetor2 relacionado a posição do analógico direito (de -1 a 1).
        /// </summary>
        public Vector2 GetThumbRight()
        {
            return State.ThumbSticks.Right;
        }
    }
}
