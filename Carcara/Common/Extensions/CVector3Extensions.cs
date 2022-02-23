namespace Microsoft.Xna.Framework
{
    public static class CVector3Extensions
    {
        /// <summary>
        /// Retorna um objeto Vector2 excluindo o eixo Z.
        /// </summary>
        public static Vector2 ToVector2(this Vector3 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }
    }
}
