using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Representa um gerenciador de telas do jogo.
    /// </summary>
    public static class CScreenManager
    {
        static bool changed = false;
        static CScreen standbyScreen = null;
        
        /// <summary>Obtém ou define a lista de telas.</summary>
        public static List<CScreen> Screens { get; set; } = new List<CScreen>();        
        /// <summary>Obtém a tela ativa.</summary>
        public static CScreen CurrentScreen { get; private set; } = null;               

        /// <summary>
        /// Atualiza o gerenciador de tela.
        /// </summary>
        /// <param name="gameTime">Obtém acesso aos tempos de jogo.</param>
        public static void Update(GameTime gameTime)
        {
            if (changed)
            {
                CurrentScreen = standbyScreen;
                changed = false;
                standbyScreen = null;
            }

            CurrentScreen?.Update(gameTime);
        }

        /// <summary>
        /// Desenha a tela de jogo.
        /// </summary>
        /// <param name="gameTime">Obtém acesso aos tempos de jogo.</param>
        /// <param name="spriteBatch">O objeto SpriteBatch para desenho.</param>
        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            CurrentScreen?.Draw(gameTime, spriteBatch);
        }            

        /// <summary>
        /// Define uma tela a ser carregada ao seu estado inicial.
        /// </summary>
        /// <param name="name">O nome da tela a ser redefinida.</param>
        public static void Reset(string name)
        {
            Find(name).Reset();            
        }

        /// <summary>Adiciona telas para esse gerenciador.</summary>
        /// <param name="screenList">A quantidade desejada de telas a serem adicionadas.</param>
        public static void Add(params CScreen[] screenList)
        {
            Screens.AddRange(screenList);

            if (CurrentScreen == null)
            {
                CurrentScreen = Screens[0];
            }            
        }

        /// <summary>Remove uma tela do gerenciador. Retorna true caso sucesso.</summary>
        /// <param name="name">Nome da tela.</param>
        public static bool Remove(string name)
        {
            CScreen finder = Find(name);
            return Screens.Remove(finder);
        }

        /// <summary>
        /// Troca a tela ativa para a tela selecionada.
        /// </summary>
        /// <param name="index">O index da tela pela ordem de adição.</param>
        /// <param name="reset">
        /// Defina true caso deseje que o gerenciador chame o método Reset()
        /// da tela ativa após a troca de tela.
        /// </param>
        public static void Change(int index, bool reset)
        {
            CScreen old = CurrentScreen;
            CScreen finder = Screens[index];

            changed = true;
            standbyScreen = finder;

            if (reset)
                old.Reset();
        }

        /// <summary>Troca a tela tiva para a tela selecionada.</summary>
        /// <param name="name">O nome da tela que será ativada.</param>
        /// <param name="reset">True se deseja que o gerenciador chame o método Reset() da tela atual.</param>
        public static void Change(string name, bool reset)
        {
            int index = Screens.FindIndex(s => s.Name.Equals(name));

            if (index != -1)
                Change(index, reset);
            else
                throw new NullReferenceException($"The screen {name} doesn't exist.");
        }

        /// <summary>
        /// Troca para próxima tela da lista.
        /// </summary>
        /// <param name="reset">True se deseja que o gerenciador chame o método Reset() da tela atual.</param>
        public static void Next(bool reset)
        {
            int index = Screens.FindIndex(x => x.Equals(CurrentScreen));

            if (index >= Screens.Count - 1)
                index = 0;
            else
                index++;

            Change(Screens[index].Name, reset);
        }

        /// <summary>
        /// Troca para a tela anterior da lista.
        /// </summary>
        /// <param name="reset">True se deseja que o gerenciador chame o método Reset() da tela atual.</param>
        public static void Back(bool reset)
        {
            int index = Screens.FindIndex(x => x.Equals(CurrentScreen));

            if (index <= 0)
                index = Screens.Count - 1;
            else
                index--;

            Change(Screens[index].Name, reset);
        }

        /// <summary>Busca e retorna uma tela definida pelo nome.</summary>
        /// <param name="name">O nome da tela.</param>
        public static CScreen Find(string name)
        {
            var s = Screens.Find(x => x.Name.Equals(name));
            if (s != null)
                return s;
            else
                throw new NullReferenceException($"The screen {name} doesn't exist.");
        }      
        
        /// <summary>
        /// Chama o método Load() de uma determinada tela.
        /// </summary>
        /// <param name="index">O index no array de telas.</param>
        public static void Load(int index)
        {
            Screens[index].Load();
        }

        /// <summary>
        /// Chama o método Load() de uma determinada tela.
        /// </summary>
        /// <param name="name">O nome da tela.</param>
        public static void Load(string name)
        {
            Find(name).Load();
        }

        /// <summary>
        /// Chama o método Unload() de uma determinada tela.
        /// </summary>
        /// <param name="index">O index no array de telas.</param>
        public static void Unload(int index)
        {
            Screens[index].Unload();
        }

        /// <summary>
        /// Chama o método Unload() de uma determinada tela.
        /// </summary>
        /// <param name="name">O nome da tela.</param>
        public static void Unload(string name)
        {
            Find(name).Unload();
        }
    }
}
