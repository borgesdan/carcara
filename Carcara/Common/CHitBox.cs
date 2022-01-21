using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Microsoft.Xna.Framework
{
    public enum CHitBoxType : byte 
    {
        None,
        Defensive,
        Offensive
    }

    /// <summary>
    /// Representa uma caixa de colisão básica que expõe propriedades e métodos de verificação.
    /// </summary>
    public struct CHitBox
    {
        /// <summary>Define o retângulo de marcação.</summary>
        public Rectangle Box;
        /// <summary>Define se a caixa pode colidir com outra caixa.</summary>
        public bool CanCollide;
        /// <summary>Define se a caixa pode receber dano ao colidir com outra caixa.</summary>
        public bool CanTakeDamage;
        /// <summary>Define a porcentagem do dano a ser recebido pelo caixa, de 0F a 1F.</summary>
        public float DamagePercentage;
        /// <summary>Define se a caixa inflige dano ao colidir.</summary>
        public bool CanInflictDamage;
        /// <summary>Define a potência do dano caso CanInflitDamage esteja definido como True.</summary>
        public float DamagePower;
        /// <summary>Define o tipo da caixa de colisão.</summary>
        public CHitBoxType BoxType;

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="box">Define o retângulo de marcação.</param>
        public CHitBox(Rectangle box) : this(box, true, true, 1F, false, 0, CHitBoxType.None) { }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="box">Define o retângulo de marcação.</param>
        /// <param name="canCollide">Define se a caixa pode colidir com outra caixa.</param>
        /// <param name="canTakeDamage">Define se a caixa pode receber dano ao colidir com outra caixa.</param>
        /// <param name="damagePercent">Define a porcentagem do dano a ser recebido pelo caixa, de 0F a 1F.</param>
        /// <param name="canInflictDamage">Define a potência do dano caso CanInflitDamage esteja definido como True.</param>
        /// <param name="damagePower">Define valores adicionais que podem ser recuperados posteriormente em tags.</param>
        /// <param name="type">Define o tipo da caixa de colisão</param>
        public CHitBox(Rectangle box, bool canCollide, bool canTakeDamage, float damagePercent, bool canInflictDamage, float damagePower, CHitBoxType type)
        {
            Box = box;
            CanCollide = canCollide;
            CanTakeDamage = canTakeDamage;
            DamagePercentage = damagePercent;
            DamagePower = damagePower;
            CanInflictDamage = canInflictDamage;
            BoxType = type;
        }

        /// <summary>
        /// Verifica e retorna true caso esta caixa intersecta outra caixa.
        /// </summary>
        public bool Intersects(CHitBox other)
        {
            return this.CanCollide && other.CanCollide
                && other.Box.Intersects(this.Box);
        }

        /// <summary>
        /// Verifica e retorna true caso esta caixa intersecta outra caixa e as duas sejam do mesmo tipo determinado.
        /// </summary>
        public bool Intersects(CHitBox other, CHitBoxType type)
        {
            return Intersects(other)
                && this.BoxType == type
                && other.BoxType == type;
        }

        /// <summary>
        /// Verifica se esta caixa intersecta outra caixa (se falso retorna 0) 
        /// e retorna o dano relacionado pelo cálculo do DamagePower * DamagePercentage.
        /// </summary>
        public float GetDamage(CHitBox other)
        {
            if(Intersects(other) 
                && this.CanTakeDamage && other.CanInflictDamage)
            {
                float percent = MathHelper.Clamp(DamagePercentage, 0, 1);
                return other.DamagePower * percent;
            }

            return 0;
        }

        /// <summary>
        /// Verifica se esta caixa intersecta outra caixa (se falso retorna 0),
        /// e se esta caixa é do tipo Defensive e a outra é do tipo Offensive,
        /// e retorna o dano relacionado pelo cálculo do DamagePower * DamagePercentage.
        /// </summary>
        public float GetDamageByType(CHitBox other)
        {
            if (Intersects(other)
                && this.CanTakeDamage 
                && other.CanInflictDamage
                && this.BoxType == CHitBoxType.Defensive 
                && other.BoxType == CHitBoxType.Offensive)
            {
                float percent = MathHelper.Clamp(DamagePercentage, 0, 1);
                return other.DamagePower * percent;
            }

            return 0;
        }
    }
}