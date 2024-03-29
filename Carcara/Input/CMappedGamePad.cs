﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Xna.Framework.Input
{
    /// <summary>
    /// Gerencia as entradas do usuário ao utilizar um mapeamento das teclas com relação aos botões do GamePad.
    /// </summary>
    public class CMappedGamePad : ICInput<Buttons>
    {
        /// <summary>Obtém os estados do GamePad.</summary>
        public CGamePad CurrentGamePad { get; }
        /// <summary>Obtém os estados do teclado.</summary>
        public CKeyboard CurrentKeyboard { get; }
        /// <summary>Obtém ou define o mapa de referência entre teclas e botões.</summary>
        public CKeyButtonMap Map { get; set; }
        /// <summary>Obtém ou define se o teclado pode ser utilizado mesmo com o GamePad conectado.</summary>
        public bool AlwaysEnableKeyboard { get; set; } = true;
        /// <summary>Obtém ou define o index do jogador.</summary>
        public PlayerIndex PlayerIndex { get => CurrentGamePad.PlayerIndex; set => CurrentGamePad.SetIndex(value); }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="map">O mapeamento tecla-botão (pode ser null).</param>
        public CMappedGamePad(CKeyButtonMap map) : this(new CGamePad(), new CKeyboard(), map)
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="gamePad">A instância do GamePad a ser utilizada.</param>
        /// <param name="keyboard">A instância do teclado a ser utilizada.</param>
        /// <param name="map">O mapeamento tecla-botão (pode ser null).</param>
        public CMappedGamePad(CGamePad gamePad, CKeyboard keyboard, CKeyButtonMap map)
        {
            CurrentGamePad = gamePad;
            CurrentKeyboard = keyboard;
            Map = map;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public CMappedGamePad(CMappedGamePad source)
        {
            CurrentGamePad = new CGamePad(source.CurrentGamePad);
            CurrentKeyboard = new CKeyboard(source.CurrentKeyboard);
            Map = new CKeyButtonMap(source.Map);
            AlwaysEnableKeyboard = source.AlwaysEnableKeyboard;
        }

        /// <summary>Atualiza os estados do GamePad.</summary>
        /// <param name="gameTime">Uma instância de GameTime.</param>
        public void Update(GameTime gameTime)
        {
            CurrentGamePad.Update(gameTime);
            CurrentKeyboard.Update(gameTime);
        }

        /// <summary>
        /// Redefine o index do controle.
        /// </summary>        
        public void SetIndex(PlayerIndex index)
        {            
            CurrentGamePad.SetIndex(index);
        }

        //Método de auxiliar para checar o estado dos botões e executar um comando
        //Recebo o botão do GamePad e a função do teclado a ser verificada
        private bool CheckButton(Buttons button, Predicate<Keys> function)
        {
            if (Map == null)
                return false;

            Keys? k = null;
            var dictionary = Map.GetKeyboardMap();

            if (dictionary.ContainsKey(button))
                k = dictionary[button];

            return k.HasValue 
                && function.Invoke(k.Value);
        }

        /// <summary>Verifica se o botão selecionado está pressionado.</summary>
        public bool Hold(Buttons button)
        {
            if(CurrentGamePad.State.IsConnected)
            {
                bool state = CurrentGamePad.Hold(button);
                bool cond = AlwaysEnableKeyboard && CheckButton(button, CurrentKeyboard.Hold);

                return state || cond;
            }

            return CheckButton(button, CurrentKeyboard.Hold);
        }

        /// <summary>Verifica se o botão selecionado estava liberado e foi pressionado.</summary>
        public bool Pressed(Buttons button)
        {
            if (CurrentGamePad.State.IsConnected)
            {
                bool state = CurrentGamePad.Pressed(button);
                bool cond = AlwaysEnableKeyboard && (CheckButton(button, CurrentKeyboard.Pressed));

                return state || cond;
            }

            return CheckButton(button, CurrentKeyboard.Pressed);
        }

        /// <summary>Verifica se o botão selecionado está liberado.</summary>     
        public bool Released(Buttons button)
        {
            if (CurrentGamePad.State.IsConnected)
            {
                bool state = CurrentGamePad.Released(button);
                bool cond = AlwaysEnableKeyboard && CheckButton(button, CurrentKeyboard.Released);

                return state || cond;
            }

            return CheckButton(button, CurrentKeyboard.Released);
        }

        /// <summary>
        /// Obtém um Vetor2 relacionado a posição do analógico esquerdo (de -1 a 1).
        /// </summary>     
        public Vector2 GetThumbLeft()
        {
            Vector2 thumb;

            if (CurrentGamePad.State.IsConnected)
            {
                thumb = CurrentGamePad.GetThumbLeft();

                if (thumb == Vector2.Zero && AlwaysEnableKeyboard)
                    thumb = GetLeftThumbKeyboard();
            }
            else
            {
                thumb = GetLeftThumbKeyboard();
            }

            return thumb;
        }

        private Vector2 GetLeftThumbKeyboard()
        {
            Vector2 thumb = Vector2.Zero;

            if (CheckButton(Buttons.LeftThumbstickUp, CurrentKeyboard.Hold))
                thumb.Y = 1;
            if (CheckButton(Buttons.LeftThumbstickDown, CurrentKeyboard.Hold))
                thumb.Y = -1;
            if (CheckButton(Buttons.LeftThumbstickRight, CurrentKeyboard.Hold))
                thumb.X = 1;
            if (CheckButton(Buttons.LeftThumbstickLeft, CurrentKeyboard.Hold))
                thumb.X = -1;

            return thumb;
        }

        /// <summary>
        /// Obtém um Vetor2 relacionado a posição do analógico direito (de -1 a 1).
        /// </summary>
        public Vector2 GetRightThumbState()
        {
            Vector2 thumb;

            if (CurrentGamePad.State.IsConnected)
            {
                thumb = CurrentGamePad.GetThumbRight();

                if (thumb == Vector2.Zero && AlwaysEnableKeyboard)
                    thumb = GetRightThumbKeyboard();
            }
            else
            {
                thumb = GetRightThumbKeyboard();
            }

            return thumb;
        }

        private Vector2 GetRightThumbKeyboard()
        {            
            Vector2 thumb = Vector2.Zero;

            if (CheckButton(Buttons.RightThumbstickUp, CurrentKeyboard.Hold))
                thumb.Y = 1;
            if (CheckButton(Buttons.RightThumbstickDown, CurrentKeyboard.Hold))
                thumb.Y = -1;
            if (CheckButton(Buttons.RightThumbstickRight, CurrentKeyboard.Hold))
                thumb.X = 1;
            if (CheckButton(Buttons.RightThumbstickLeft, CurrentKeyboard.Hold))
                thumb.X = -1;

            return thumb;
        }
    }
}