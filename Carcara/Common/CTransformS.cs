namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Fornece acesso as manipulações de transformação (posição, escala e rotação) através de uma estrutura.
    /// </summary>
    public struct CTransformS
    {
        /// <summary>Obtém a posição no eixo X.</summary>
        public float X;
        /// <summary>Obtém a posição no eixo Y.</summary>
        public float Y;
        /// <summary>Obtém a posição no eixo Z.</summary>
        public float Z;

        /// <summary>Obtém a escala no eixo X.</summary>
        public float Xs;
        /// <summary>Obtém a escala no eixo Y.</summary>
        public float Ys;
        /// <summary>Obtém a escala no eixo Z.</summary>
        public float Zs;

        /// <summary>Obtém a rotação no eixo X.</summary>
        public float Xr;
        /// <summary>Obtém a rotação no eixo Y.</summary>
        public float Yr;
        /// <summary>Obtém a rotação no eixo Z.</summary>
        public float Zr;

        /// <summary>Obtém a posição nos eixos X, Y e Z.</summary>
        public Vector3 Position => new Vector3(X, Y, Z);        
        /// <summary>Obtém a escala nos eixos X, Y e Z.</summary>
        public Vector3 Scale => new Vector3(Xs, Ys, Zs);
        /// <summary>Obtém a rotação nos eixos X, Y e Z.</summary>
        public Vector3 Rotation => new Vector3(Xr, Yr, Zr);

        /// <summary>
        /// Cria um novo objeto da estrutura.
        /// </summary>
        /// <param name="position">O valor da posição.</param>
        /// <param name="scale">O valor da escala.</param>
        /// <param name="rotation">O valor da rotação.</param>
        public CTransformS(Vector3 position, Vector3 scale, Vector3 rotation)
        {
            X = position.X;
            Y = position.Y;
            Z = position.Z;

            Xs = scale.X;
            Ys = scale.Y;
            Zs = scale.Z;

            Xr = rotation.X;
            Yr = rotation.Y;
            Zr = rotation.Z;
        }
    }
}
