namespace Microsoft.Xna.Framework.Input
{
    /// <summary>Classe que auxilia no gerenciamento de entradas do mouse.</summary>
    public class CMouseHelper : ICInput<MouseButtons>
    {
        /// <sumary>Obtém ou define se esta instância está disponível para ser atualizada.</sumary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>Obtém o estado atual do mouse.</summary>
        public MouseState State { get; private set; }
        /// <summary>Obtém estado anterior do mouse.</summary>
        public MouseState OldState { get; private set; }
        /// <summary>Obtém os eventos do mouse.</summary>
        public CMouseEvents MouseEvents { get; private set; } = new CMouseEvents();

        /// <summary>Inicializa uma nova instância da classe MouseHelper.</summary>
        public CMouseHelper()
        {
            State = Mouse.GetState();
        }

        /// <summary>Inicializa uma nova instância da classe MouseHelper.</summary>
        /// <param name="state">O estado definido do mouse.</param>
        public CMouseHelper(MouseState state)
        {
            State = state;
        }

        /// <summary>
        /// Inializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public CMouseHelper(CMouseHelper source)
        {
            IsEnabled = source.IsEnabled;
            State = source.State;
            OldState = source.OldState;
            MouseEvents = new CMouseEvents(MouseEvents);
        }

        /// <summary>Atualiza o estado do mouse.</summary>
        /// <param name="gameTime">Fornece acesso aos valores de tempo do jogo.</param>
        public void Update(GameTime gameTime)
        {
            if (IsEnabled)
            {
                OldState = State;
                State = Mouse.GetState();

                MouseEvents.Update(gameTime);
            }            
        }        

        /// <summary>Checa se o botão do mouse está pressionado.</summary>
        /// <param name="button">O botão do mouse a ser checado.</param>
        public bool Hold(MouseButtons button)
        {            
             return Check(button, State) == ButtonState.Pressed;
        }

        /// <summary>Checa se o botão do mouse está liberado.</summary>
        /// <param name="button">O botão do mouse a ser checado.</param>
        public bool Released(MouseButtons button)
        {            
            return Check(button, State) == ButtonState.Released;
        }

        /// <summary>Checa se o botão do mouse foi pressionado.</summary>
        /// <param name="button">O botão do mouse a ser checado.</param>
        public bool Pressed(MouseButtons button)
        {
            return Check(button, OldState) == ButtonState.Released
                && Check(button, State) == ButtonState.Pressed;
        }        

        //Checa os estados dos botões
        private ButtonState Check(MouseButtons button, MouseState state)
        {
            if (button == MouseButtons.Left)
                return state.LeftButton;
            else if (button == MouseButtons.Right)
                return state.RightButton;
            else if (button == MouseButtons.Middle)
                return state.MiddleButton;
            else if (button == MouseButtons.X1)
                return state.XButton1;
            else
                return state.XButton2;
        }
    }
}
