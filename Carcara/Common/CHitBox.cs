using System;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Representa uma caixa de colisão 2D.
    /// </summary>
    public struct CHitBox : IEquatable<CHitBox>
    {
        /// <summary>Define se a caixa recebe colisão.</summary>
        public readonly bool CanCollide;
        /// <summary>Define se a caixa recebe dano ao colidir.</summary>
        public readonly bool CanTakeDamage;
        /// <summary>Define a porcentagem do dano sofrido, de 0F a 1F.</summary>
        public readonly float DamagePercentage;
        /// <summary>Define se a caixa inflige dano ao colidir.</summary>
        public readonly bool CanInflictDamage;
        /// <summary>Define a potência do dano caso CanInflitDamage esteja definido como True.</summary>
        public readonly int DamagePower;

        /// <summary>A posição no eixo X.</v>
        public readonly int X;
        /// <summary>A posição no eixo Y.</summary>
        public readonly int Y;
        /// <summary>A largura da caixa.</summary>
        public readonly int Width;
        /// <summary>A altura da caixa.</summary>
        public readonly int Height;

        /// <summary>Define uma valor que pode ser recuperado futuramente na tag 01.</summary>
        public readonly byte T01;
        /// <summary>Define uma valor que pode ser recuperado futuramente na tag 02.</summary>
        public readonly byte T02;
        /// <summary>Define uma valor que pode ser recuperado futuramente na tag 03.</summary>
        public readonly byte T03;
        /// <summary>Define uma valor que pode ser recuperado futuramente na tag 04.</summary>
        public readonly byte T04;
        /// <summary>Define uma valor que pode ser recuperado futuramente na tag 05.</summary>
        public readonly byte T05;
        /// <summary>Define uma valor que pode ser recuperado futuramente na tag 06.</summary>
        public readonly byte T06;

        /// <summary>Obtém os limites da caixa.</summary>
        public Rectangle Bounds
        {
            get => new Rectangle(X, Y, Width, Height);
        }

        /// <summary>Obtém um objeto com os valores zerados e com os estados false.</summary>
        public CHitBox Empty
        {
            get => new CHitBox(new Rectangle());
        }

        /// <summary>
        /// Cria um novo objeto HitFrame.
        /// </summary>
        /// <param name="rectangle">O retângulo que representa a posição e o tamanho do box.</param>
        /// <param name="canCollide">Define se a caixa recebe colisão.</param>
        /// <param name="canTakeDamage">Define se a caixa recebe dano ao colidir.</param>
        /// <param name="damagePercentage">Define a porcentagem do dano sofrido, de 0F a 1F.</param>
        /// <param name="canInflictDamage">Define se a caixa inflige dano ao colidir.</param>
        /// <param name="power">Define a potência do dano caso CanInflitDamage esteja definido como True.</param>
        /// <param name="t1">Obtém ou define a Tag 01.</param>
        /// <param name="t2">Obtém ou define a Tag 02.</param>
        /// <param name="t3">Obtém ou define a Tag 03.</param>
        /// <param name="t4">Obtém ou define a Tag 04<./param>
        /// <param name="t5">Obtém ou define a Tag 05.</param>
        /// <param name="t6">Obtém ou define a Tag 06.</param>
        public CHitBox(Rectangle rectangle, bool canCollide = false, bool canTakeDamage = false, float damagePercentage = 0, bool canInflictDamage = false, int power = 0,
            byte t1 = 0, byte t2 = 0, byte t3 = 0, byte t4 = 0, byte t5 = 0, byte t6 = 0)
        {
            X = rectangle.X;
            Y = rectangle.Y;
            Width = rectangle.Width;
            Height = rectangle.Height;
            CanCollide = canCollide;
            CanTakeDamage = canTakeDamage;
            DamagePercentage = MathHelper.Clamp(damagePercentage, 0F, 1F);
            CanInflictDamage = canInflictDamage;
            DamagePower = power;

            T01 = t1;
            T02 = t2;
            T03 = t3;
            T04 = t4;
            T05 = t5;
            T06 = t6;
        }

        public bool Equals(CHitBox other)
        {
            return CanCollide == other.CanCollide &&
                   CanTakeDamage == other.CanTakeDamage &&
                   DamagePercentage == other.DamagePercentage &&
                   CanInflictDamage == other.CanInflictDamage &&
                   DamagePower == other.DamagePower &&
                   X == other.X &&
                   Y == other.Y &&
                   Width == other.Width &&
                   Height == other.Height &&
                   T01 == other.T01 &&
                   T02 == other.T02 &&
                   T03 == other.T03 &&
                   T04 == other.T04 &&
                   T05 == other.T05 &&
                   T06 == other.T06;
        }

        public override int GetHashCode()
        {
            int hashCode = -856949754;
            hashCode = hashCode * -1521134295 + CanCollide.GetHashCode();
            hashCode = hashCode * -1521134295 + CanTakeDamage.GetHashCode();
            hashCode = hashCode * -1521134295 + CanInflictDamage.GetHashCode();
            hashCode = hashCode * -1521134295 + DamagePercentage.GetHashCode();
            hashCode = hashCode * -1521134295 + DamagePower.GetHashCode();
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            hashCode = hashCode * -1521134295 + T01.GetHashCode();
            hashCode = hashCode * -1521134295 + T02.GetHashCode();
            hashCode = hashCode * -1521134295 + T03.GetHashCode();
            hashCode = hashCode * -1521134295 + T04.GetHashCode();
            hashCode = hashCode * -1521134295 + T05.GetHashCode();
            hashCode = hashCode * -1521134295 + T06.GetHashCode();
            return hashCode;
        }
    }
}