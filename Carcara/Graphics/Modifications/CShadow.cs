using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Xna.Framework.Graphics.Modifications
{
    public class CShadow<T> where T : ICDrawable, ICDrawTransformable, ICTransformable
    {
        public T GameObject { get; set; }
        public Color Color { get; set; }
        public Vector2 Offset { get; set; }

        public CShadow(T gameObject, Color color, Vector2 offset)
        {
            GameObject = gameObject;
            Color = color;
            Offset = offset;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 oldPosition = GameObject.Transform.Position2;
            Color oldColor = GameObject.DrawTransform.Color;

            GameObject.Transform.Position2 = oldPosition + Offset;
            GameObject.DrawTransform.Color = Color;
            GameObject.Draw(new GameTime(), spriteBatch);

            GameObject.Transform.Position2 = oldPosition;
            GameObject.DrawTransform.Color = oldColor;
        }
    }
}