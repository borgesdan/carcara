using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Xna.Framework
{
    public static class CRandomExtensions
    {
        /// <summary>
        /// Retorna um valor boleano aleatório.
        /// </summary>
        public static bool NextBool(this Random random)
        {
            return random.Next(0, 2) > 0;
        }
    }
}