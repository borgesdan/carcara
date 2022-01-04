using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Xna.Framework
{
    public static class CVector2Extensions
    {
        /// <summary>
        /// Retorna um Vetor2 como um objeto Vetor3 onde Z terá valor 0
        /// </summary>
        public static Vector3 ToVector3(this Vector2 vector2)
        {
            return new Vector3(vector2, 0);
        }
    }
}
