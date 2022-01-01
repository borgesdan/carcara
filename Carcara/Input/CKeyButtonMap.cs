using System.Collections.Generic;
using System.Xml;
using System;

namespace Microsoft.Xna.Framework.Input
{
    /// <summary>Define um mapa do teclado em referência aos botões do GamePad.</summary>
    public class CKeyButtonMap
    {
        public Keys? DPadUp { get; set; } = null;
        public Keys? DPadDown { get; set; } = null;
        public Keys? DPadRight { get; set; } = null;
        public Keys? DPadLeft { get; set; } = null;
        public Keys? X { get; set; } = null;
        public Keys? Y { get; set; } = null;
        public Keys? A { get; set; } = null;
        public Keys? B { get; set; } = null;
        public Keys? LeftTrigger { get; set; } = null;
        public Keys? RightTrigger { get; set; } = null;
        public Keys? LeftShoulder { get; set; } = null;
        public Keys? RightShoulder { get; set; } = null;
        public Keys? RightStick { get; set; } = null;
        public Keys? RightThumbStickUp { get; set; } = null;
        public Keys? RightThumbStickDown { get; set; } = null;
        public Keys? RightThumbStickRight { get; set; } = null;
        public Keys? RightThumbStickLeft { get; set; } = null;
        public Keys? LeftStick { get; set; } = null;
        public Keys? LeftThumbStickUp { get; set; } = null;
        public Keys? LeftThumbStickDown { get; set; } = null;
        public Keys? LeftThumbStickRight { get; set; } = null;
        public Keys? LeftThumbStickLeft { get; set; } = null;
        public Keys? Start { get; set; } = null;
        public Keys? Back { get; set; } = null;
        public Keys? BigButton { get; set; } = null;


        /// <summary>
        /// Obtém um mapeamento padrão da classe.
        /// AXBY(NumPad4, NumPad5, NumPad6, NumPad0);
        /// DPad(Up, Down, Right, Left)
        /// RightStick(Q, W, S, D, A)
        /// LeftStick(Y, U, J, K, H)
        /// Shoulder(LeftShift, RightShift)
        /// Trigger(LeftControl, RightControl)
        /// StartBackBig(Enter, Escape, Home)
        /// </summary>
        public static CKeyButtonMap Default
        {
            get
            {
                CKeyButtonMap map = new CKeyButtonMap();
                map.SetAXBY(Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad0);
                map.SetDPad(Keys.Up, Keys.Down, Keys.Right, Keys.Left);
                map.SetRightStick(Keys.Q, Keys.W, Keys.S, Keys.D, Keys.A);
                map.SetLeftStick(Keys.Y, Keys.U, Keys.J, Keys.K, Keys.H);
                map.SetShoulder(Keys.LeftShift, Keys.RightShift);
                map.SetTrigger(Keys.LeftControl, Keys.RightControl);
                map.SetStartBackBig(Keys.Enter, Keys.Escape, Keys.Home);

                return map;
            }
        }

        /// <summary>Inicializa uma nova instância da classe.</summary>
        public CKeyButtonMap() { }

        /// <summary>
        /// Inicializa uma nova instância da classe definindo o mapa do tecla-botão. Valores podem ser nulos.
        /// </summary>
        /// <param name="dpadup">Direcional digital para cima.</param>
        /// <param name="dpaddown">Direcional digital para baixo.</param>
        /// <param name="dpadright">Direcional digital para direita.</param>
        /// <param name="dpadleft">Direcional digital para esquerda.</param>
        /// <param name="x">Botão X do GamePad.</param>
        /// <param name="y">Botão Y do GamePad.</param>
        /// <param name="a">Botão A do GamePad.</param>
        /// <param name="b">Botão B do GamePad.</param>
        /// <param name="lefttrigger">Gatilho esquerdo [LT].</param>
        /// <param name="righttrigger">Gatilho direito [RT].</param>
        /// <param name="leftshouder">Botão de ombro esquerdo. [LB].</param>
        /// <param name="rightshoulder">Botão de ombro direito [RB].</param>
        /// <param name="rightstick">Analógico direito.</param>
        /// <param name="rightthumbstickup">Analógico direito para cima.</param>
        /// <param name="rightthumbstickdown">Analógico direito para baixo.</param>
        /// <param name="rightthumbstickright">Analógico direito para direita.</param>
        /// <param name="rightthumbstickleft">Analógico direito para esquerda.</param>
        /// <param name="leftstick">Analógico esquerdo.</param>
        /// <param name="leftthumbstickup">Analógico esquerdo para cima.</param>
        /// <param name="leftthumbstickdown">Analógico esquerdo para baixo.</param>
        /// <param name="leftthumbstickright">Analógico esquerdo para direita.</param>
        /// <param name="leftthumbstickleft">Analógico esquerdo para esquerda.</param>
        /// <param name="start">Botão Start.</param>
        /// <param name="back">Botão Back.</param>
        /// <param name="bigbutton">Botão BigButton.</param>
        public CKeyButtonMap(Keys? dpadup, Keys? dpaddown, Keys? dpadright, Keys? dpadleft,
                            Keys? x, Keys? y, Keys? a, Keys? b,
                            Keys? lefttrigger, Keys? righttrigger, Keys? leftshouder, Keys? rightshoulder,
                            Keys? rightstick, Keys? rightthumbstickup, Keys? rightthumbstickdown,
                            Keys? rightthumbstickright, Keys? rightthumbstickleft,
                            Keys? leftstick, Keys? leftthumbstickup, Keys? leftthumbstickdown,
                            Keys? leftthumbstickright, Keys? leftthumbstickleft,
                            Keys? start, Keys? back, Keys? bigbutton)
        {
            DPadUp = dpadup;
            DPadDown = dpaddown;
            DPadRight = dpadright;
            DPadLeft = dpadleft;

            X = x;
            Y = y;
            A = a;
            B = b;

            LeftTrigger = lefttrigger;
            RightTrigger = righttrigger;
            LeftShoulder = leftshouder;
            RightShoulder = rightshoulder;

            RightStick = rightstick;
            RightThumbStickUp = rightthumbstickup;
            RightThumbStickDown = rightthumbstickdown;
            RightThumbStickRight = rightthumbstickright;
            RightThumbStickLeft = rightthumbstickleft;

            LeftStick = leftstick;
            LeftThumbStickUp = leftthumbstickup;
            LeftThumbStickDown = leftthumbstickdown;
            LeftThumbStickRight = leftthumbstickright;
            LeftThumbStickLeft = leftthumbstickleft;

            Start = start;
            Back = back;
            BigButton = bigbutton;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public CKeyButtonMap(CKeyButtonMap source)
        {
            DPadUp = source.DPadUp;
            DPadDown = source.DPadDown;
            DPadRight = source.DPadRight;
            DPadLeft = source.DPadLeft;

            X = source.X;
            Y = source.Y;
            A = source.A;
            B = source.B;

            LeftTrigger = source.LeftTrigger;
            RightTrigger = source.RightTrigger;
            LeftShoulder = source.LeftShoulder;
            RightShoulder = source.RightShoulder;

            RightStick = source.RightStick;
            RightThumbStickUp = source.RightThumbStickUp;
            RightThumbStickDown = source.RightThumbStickDown;
            RightThumbStickRight = source.RightThumbStickRight;
            RightThumbStickLeft = source.RightThumbStickLeft;

            LeftStick = source.LeftStick;
            LeftThumbStickUp = source.LeftThumbStickUp;
            LeftThumbStickDown = source.LeftThumbStickDown;
            LeftThumbStickRight = source.LeftThumbStickRight;
            LeftThumbStickLeft = source.LeftThumbStickLeft;

            Start = source.Start;
            Back = source.Back;
            BigButton = source.BigButton;
        }

        /// <summary>
        /// Define o mapa dos direcionais digitais.
        /// </summary>
        /// <param name="dpadup">Direcional digital para cima.</param>
        /// <param name="dpaddown">Direcional digital para baixo.</param>
        /// <param name="dpadright">Direcional digital para direita.</param>
        /// <param name="dpadleft">Direcional digital para esquerda.</param>
        public CKeyButtonMap SetDPad(Keys? dpadup, Keys? dpaddown, Keys? dpadright, Keys? dpadleft)
        {
            DPadUp = dpadup;
            DPadDown = dpaddown;
            DPadRight = dpadright;
            DPadLeft = dpadleft;

            return this;
        }

        /// <summary>
        /// Define o mapa dos botões X, Y, A e B.
        /// </summary>
        /// <param name="x">Botão X do GamePad.</param>
        /// <param name="y">Botão Y do GamePad.</param>
        /// <param name="a">Botão A do GamePad.</param>
        /// <param name="b">Botão B do GamePad.</param>
        public CKeyButtonMap SetAXBY(Keys? x, Keys? y, Keys? a, Keys? b)
        {
            X = x;
            Y = y;
            A = a;
            B = b;

            return this;
        }

        /// <summary>
        /// Define o mapa dos gatilhos [RT] e [LT].
        /// </summary>
        /// <param name="lefttrigger">Gatilho esquerdo [LT].</param>
        /// <param name="righttrigger">Gatilho direito [RT].</param>
        public CKeyButtonMap SetTrigger(Keys? lefttrigger, Keys? righttrigger)
        {
            LeftTrigger = lefttrigger;
            RightTrigger = righttrigger;

            return this;
        }

        /// <summary>
        /// Define o mapa dos botões de ombro [RB] e [LB]
        /// </summary>
        /// <param name="leftshouder">Botão de ombro esquerdo. [LB].</param>
        /// <param name="rightshoulder">Botão de ombro direito [RB].</param>
        public CKeyButtonMap SetShoulder(Keys? leftshouder, Keys? rightshoulder)
        {
            LeftShoulder = leftshouder;
            RightShoulder = rightshoulder;

            return this;
        }

        /// <summary>
        /// Define o mapa do direcional analógico direito.
        /// </summary>
        /// <param name="rightstick">Analógico direito.</param>
        /// <param name="rightthumbstickup">Analógico direito para cima.</param>
        /// <param name="rightthumbstickdown">Analógico direito para baixo.</param>
        /// <param name="rightthumbstickright">Analógico direito para direita.</param>
        /// <param name="rightthumbstickleft">Analógico direito para esquerda.</param>
        public CKeyButtonMap SetRightStick(Keys? rightstick, Keys? rightthumbstickup,
            Keys? rightthumbstickdown, Keys? rightthumbstickright, Keys? rightthumbstickleft)
        {
            RightStick = rightstick;
            RightThumbStickUp = rightthumbstickup;
            RightThumbStickDown = rightthumbstickdown;
            RightThumbStickRight = rightthumbstickright;
            RightThumbStickLeft = rightthumbstickleft;

            return this;
        }

        /// <summary>
        /// Define o mapa do direcional analógico esquerdo.
        /// </summary>
        /// <param name="leftstick">Analógico esquerdo.</param>
        /// <param name="leftthumbstickup">Analógico esquerdo para cima.</param>
        /// <param name="leftthumbstickdown">Analógico esquerdo para baixo.</param>
        /// <param name="leftthumbstickright">Analógico esquerdo para direita.</param>
        /// <param name="leftthumbstickleft">Analógico esquerdo para esquerda.</param>
        public CKeyButtonMap SetLeftStick(Keys? leftstick, Keys? leftthumbstickup,
            Keys? leftthumbstickdown, Keys? leftthumbstickright, Keys? leftthumbstickleft)
        {
            LeftStick = leftstick;
            LeftThumbStickUp = leftthumbstickup;
            LeftThumbStickDown = leftthumbstickdown;
            LeftThumbStickRight = leftthumbstickright;
            LeftThumbStickLeft = leftthumbstickleft;

            return this;
        }

        /// <summary>
        /// Define o mapa dos botões START, BACK e BIGBUTTON.
        /// </summary>
        /// <param name="start">Botão Start.</param>
        /// <param name="back">Botão Back.</param>
        /// <param name="bigbutton">Botão BigButton.</param>
        public CKeyButtonMap SetStartBackBig(Keys? start, Keys? back, Keys? bigbutton)
        {
            Start = start;
            Back = back;
            BigButton = bigbutton;

            return this;
        }

        /// <summary>Obtém um dicionário com o mapa do teclado em referência ao GamePad.</summary>
        public Dictionary<Buttons, Keys?> GetKeyboardMap()
        {
            Dictionary<Buttons, Keys?> dictionary = new Dictionary<Buttons, Keys?>
            {
                { Buttons.A, A },
                { Buttons.B, B },
                { Buttons.X, X },
                { Buttons.Y, Y },

                { Buttons.LeftShoulder, LeftShoulder },
                { Buttons.RightShoulder, RightShoulder },
                { Buttons.LeftTrigger, LeftTrigger },
                { Buttons.RightTrigger, RightTrigger },
                { Buttons.LeftStick, LeftStick },
                { Buttons.RightStick, RightStick },

                { Buttons.Back, Back },
                { Buttons.Start, Start },
                { Buttons.BigButton, BigButton },

                { Buttons.DPadDown, DPadDown },
                { Buttons.DPadUp, DPadUp },
                { Buttons.DPadLeft, DPadLeft },
                { Buttons.DPadRight, DPadRight },

                { Buttons.LeftThumbstickDown, LeftThumbStickDown },
                { Buttons.LeftThumbstickLeft, LeftThumbStickLeft },
                { Buttons.LeftThumbstickRight, LeftThumbStickRight },
                { Buttons.LeftThumbstickUp, LeftThumbStickUp },

                { Buttons.RightThumbstickDown, RightThumbStickDown },
                { Buttons.RightThumbstickLeft, RightThumbStickLeft },
                { Buttons.RightThumbstickRight, RightThumbStickRight },
                { Buttons.RightThumbstickUp, RightThumbStickUp }
            };

            return dictionary;
        }
    }        
}