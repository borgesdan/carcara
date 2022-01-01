using System;

namespace Microsoft.Xna.Framework.Input
{
    /// <summary>
    /// Representa um tipo de entrada do usuário.
    /// </summary>
    /// <typeparam name="T">T é uma enumeração Button, MouseButtons, Keys</typeparam>
    public interface ICInput<T> where T : Enum
    {
        /// <summary>Atualiza os estados da entrada.</summary>
        /// <param name="gameTime">Obtém os tempos de jogo.</param>
        void Update(GameTime gameTime);

        /// <summary>Verifica se o botão selecionado está pressionada.</summary>     
        /// <param name="button">A entrada a ser verificada.</param>
        bool Hold(T button);

        /// <summary>Verifica se o botão selecionado estava liberado e foi pressionado.</summary>     
        /// <param name="button">A entrada a ser verificada.</param>
        bool Pressed(T button);

        /// <summary>Verifica se o botão selecionado está liberado.</summary>     
        /// <param name="button">A entrada a ser verificada.</param>
        bool Released(T button);
    }
}