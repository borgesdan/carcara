using System;

namespace Microsoft.Xna.Framework
{
    public static class CRandomExtensions
    {
        /// <summary>        
        /// Return a random bool value. 
        /// </summary>
        public static bool NextBool(this Random random)
        {
            return random.Next(0, 2) > 0;
        }
    }
}