using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Fornece acesso as manipulações de transformação (posição, escala e rotação) de um objeto.
    /// </summary>
    public class CTransform : IEquatable<CTransform>
    {
        /// <summary>Obtém ou define a posição nos eixos X, Y e Z.</summary>
        public Vector3 Position3 { get; set; } = Vector3.Zero;
        /// <summary>Obtém ou define a posição no eixo X.</summary>
        public float X { get { return Position3.X; } set { Position3 = new Vector3(value, Y, Z); } }
        /// <summary>Obtém ou define a posição no eixo Y.</summary>
        public float Y { get { return Position3.Y; } set { Position3 = new Vector3(X, value, Z); } }
        /// <summary>Obtém ou define a posição no eixo Z.</summary>
        public float Z { get { return Position3.Z; } set { Position3 = new Vector3(X, Y, value); } }
        /// <summary>Obtém ou define a posição através de um Vector2 ignorando o eixo Z.</summary>
        public Vector2 Position2 { get => new Vector2(Position3.X, Position3.Y); set => Position3 = new Vector3(value.X, value.Y, Position3.Z); }        

        /// <summary>Obtém ou define a escala nos eixos X, Y e Z.</summary>
        public Vector3 Scale3 { get; set; } = Vector3.One;
        /// <summary>Obtém ou define a escala no eixo X.</summary>
        public float Xs { get { return Scale3.X; } set { Scale3 = new Vector3(value, Ys, Zs); } }
        /// <summary>Obtém ou define a escala no eixo Y.</summary>
        public float Ys { get { return Scale3.Y; } set { Scale3 = new Vector3(Xs, value, Zs); } }
        /// <summary>Obtém ou define a escala no eixo Z.</summary>
        public float Zs { get { return Scale3.Z; } set { Scale3 = new Vector3(Xs, Ys, value); } }
        /// <summary>Obtém ou define a escala através de um Vector2 ignorando o eixo Z.</summary>
        public Vector2 Scale2 { get => new Vector2(Scale3.X, Scale3.Y); set => Scale3 = new Vector3(value.X, value.Y, Scale3.Z); }

        /// <summary>Obtém ou define a rotação nos eixos X, Y e Z.</summary>
        public Vector3 Rotation3 { get; set; } = Vector3.Zero;
        /// <summary>Obtém ou define a rotação no eixo X.</summary>
        public float Xr { get { return Rotation3.X; } set { Rotation3 = new Vector3(value, Yr, Zr); } }
        /// <summary>Obtém ou define a rotação no eixo Y.</summary>
        public float Yr { get { return Rotation3.Y; } set { Rotation3 = new Vector3(Xr, value, Zr); } }
        /// <summary>Obtém ou define a rotação no eixo Z.</summary>
        public float Zr { get { return Rotation3.Z; } set { Rotation3 = new Vector3(Xr, Yr, value); } }
        /// <summary>Obtém ou define a rotação em um plano 2D (obtém ou define o valor de Rz)</summary>
        public float Rotation2 { get => Zr; set => Zr = value; }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        public CTransform() { }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="position">Informa o valor da posição.</param>
        /// <param name="scale">Informa o valor da escala.</param>
        /// <param name="rotation">Informa o valor da rotação. Para um jogo 2D somente o valor no eixo Z será considerado.</param>
        public CTransform(Vector3 position, Vector3 scale, Vector3 rotation)
        {
            this.Position3 = position;
            this.Scale3 = scale;
            this.Rotation3 = rotation;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="position">Informa o valor da posição.</param>
        /// <param name="scale">Informa o valor da escala.</param>
        /// <param name="rotation">Informa o valor da rotação.</param>
        public CTransform(Vector2 position, Vector2 scale, float rotation)
            : this(new Vector3(position, 0), new Vector3(scale, 1), new Vector3(0, 0, rotation))
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância
        /// </summary>
        /// <param name="source">A instância a ser copiada</param>
        public CTransform(CTransform source)
        {
            this.Position3 = source.Position3;
            this.Scale3 = source.Scale3;
            this.Rotation3 = source.Rotation3;
        }

        /// <summary>
        /// Copia as propriedades de um objeto Transform.
        /// </summary>
        /// <param name="transform"></param>
        public void Set(CTransform transform)
        {
            this.Position3 = transform.Position3;
            this.Scale3 = transform.Scale3;
            this.Rotation3 = transform.Rotation3;
        }                

        /// <summary>
        /// Obtém um objeto valor dessa classe.
        /// </summary>
        /// <returns></returns>
        public CTransformS GetStruct()
        {
            return new CTransformS(Position3, Scale3, Rotation3);
        }

        ///<inheritdoc/>
        public override string ToString()
        {
            return string.Concat("Position: ", Position3, " ", "Scale: ", Scale3, " ", "Rotation: ", Rotation3);
        }

        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals(obj as CTransform);
        }

        ///<inheritdoc/>
        public bool Equals(CTransform other)
        {
            return other != null &&
                   Position3.Equals(other.Position3) &&
                   Scale3.Equals(other.Scale3) &&
                   Rotation3.Equals(other.Rotation3);
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = -1249237961;
            hashCode = hashCode * -1521134295 + Position3.GetHashCode();
            hashCode = hashCode * -1521134295 + Scale3.GetHashCode();
            hashCode = hashCode * -1521134295 + Rotation3.GetHashCode();
            return hashCode;
        }

        ///<inheritdoc/>
        public static bool operator ==(CTransform left, CTransform right)
        {
            return EqualityComparer<CTransform>.Default.Equals(left, right);
        }

        ///<inheritdoc/>
        public static bool operator !=(CTransform left, CTransform right)
        {
            return !(left == right);
        }
    }

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