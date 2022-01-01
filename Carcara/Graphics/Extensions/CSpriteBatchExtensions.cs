using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework
{
    public static class CSpriteBatchExtensions
    {
        /// <summary>
        /// Desenha um sprite na tela. 
        /// </summary>
        /// <param name="texture">A textura a ser desenhada.</param>
        /// <param name="args">Os argumentos de desenho.</param>
        public static void Draw(this SpriteBatch spriteBatch, Texture2D texture, CDrawArgs args)
        {
            if(args.Destination == null)
            {
                spriteBatch.Draw(texture, args.Position, args.Source, args.Color, args.Rotation,
                    args.Origin, args.Scale, args.Effects, args.LayerDepth);

                return;
            }

            spriteBatch.Draw(texture, args.Destination.Value, args.Source, 
                args.Color, args.Rotation, args.Origin, args.Effects, args.LayerDepth);
        }

        /// <summary>
        /// Desenha um texto na tela.
        /// </summary>
        /// <param name="font">A fonte para desenho.</param>
        /// <param name="stringBuilder">O texto a ser desenhado.</param>
        /// <param name="args">Os argumentos de desenho.</param>
        public static void DrawString(this SpriteBatch spriteBatch, SpriteFont font, StringBuilder stringBuilder, CDrawArgs args)
        {
            spriteBatch.DrawString(font, stringBuilder, args.Position, args.Color, args.Rotation, args.Origin, args.Scale, args.Effects, args.LayerDepth);
        }
    }
}