using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Representa uma transição de uma cor para outra.
    /// </summary>
    public class CColorTransition : ICUpdatable
    {
        private float elapsedTime = 0;
        private bool detachWhenIsFinished;

        /// <summary>Obtém ou define o tempo em milisegundos a ser atrasada para ser aplicada a atualização da cor (default = 0).</summary>
        public float DelayTime { get; set; } = 0;
        /// <summary>Obtém ou define a cor de destino.</summary>
        public Color DestinationColor { get; set; }
        /// <summary>Obtém se a transição chegou ao fim.</summary>
        public bool IsFinished { get; private set; } = false;
        /// <summary>Obtém ou define uma objeto de jogo que implementa a interface ICDrawTransformable e que receberá os efeitos de transição.</summary>
        public ICDrawTransformable GameObject { get; set; }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="gameObject">Define uma objeto de jogo que implementa a interface ICDrawTransformable e que receberá os efeitos de transição.</param>
        /// <param name="destination">Define a cor de destino.</param>
        /// <param name="detachWhenIsFinished">Separa o game object definido deste modificador quando a operação for terminada.</param>
        public CColorTransition(ICDrawTransformable gameObject, Color destination, bool detachWhenIsFinished)
        {
            DestinationColor = destination;
            GameObject = gameObject;
            this.detachWhenIsFinished = detachWhenIsFinished;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public CColorTransition(CColorTransition source)
        {
            elapsedTime = source.elapsedTime;
            detachWhenIsFinished = source.detachWhenIsFinished;
            DelayTime = source.DelayTime;
            DestinationColor = source.DestinationColor;
            IsFinished = source.IsFinished;
            GameObject = source.GameObject;
        }

        /// <summary>
        /// Atualiza a classe.
        /// </summary>
        /// <param name="gameTime">Obtém acesso aos tempos de jogo.</param>
        public void Update(GameTime gameTime) 
        {
            if (GameObject == null)
                return;

            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            IsFinished = false;

            if (elapsedTime > DelayTime)
            {
                Color active = GameObject.DrawTransform.Color;
                Color final = DestinationColor;

                if (!final.Equals(active))
                {
                    if (active.R > final.R)
                        active.R--;
                    else if (active.R < final.R)
                        active.R++;

                    if (active.G > final.G)
                        active.G--;
                    else if (active.G < final.G)
                        active.G++;

                    if (active.B > final.B)
                        active.B--;
                    else if (active.B < final.B)
                        active.B++;

                    if (active.A > final.A)
                        active.A--;
                    else if (active.A < final.A)
                        active.A++;                    
                }

                GameObject.DrawTransform.Color = active;
                elapsedTime = 0;
                
                if (active == final)
                {
                    IsFinished = true;

                    if (detachWhenIsFinished)
                        GameObject = null;
                }
            }            

        }
        
    }
}