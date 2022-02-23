using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Representa um gerenciador de telas.
    /// </summary>
    public class CScreenManager : CScreenManager<CScreen> { }

    /// <summary>
    /// Representa um gerenciador de telas.
    /// </summary>
    public class CScreenManager<T> where T : ICScreen
    {
        bool isChanged = false;
         T standbyScreen = default;
         readonly List<T> screens = new List<T>();

        /// <summary>Obtém a tela ativa.</summary>
        public  T CurrentScreen { get; private set; } = default;

        /// <summary>
        /// Atualiza o gerenciador de tela.
        /// </summary>
        /// <param name="gameTime">Obtém acesso aos tempos de jogo.</param>
        public  void Update(GameTime gameTime)
        {
            CurrentScreen?.Update(gameTime);

            if (isChanged)
            {
                CurrentScreen = standbyScreen;
                isChanged = false;
                standbyScreen = default;
            }
        }

        /// <summary>
        /// Desenha a tela de jogo.
        /// </summary>
        /// <param name="gameTime">Obtém acesso aos tempos de jogo.</param>
        /// <param name="spriteBatch">O objeto SpriteBatch para desenho.</param>
        public  void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            CurrentScreen?.Draw(gameTime, spriteBatch);
        }

        /// <summary>Adiciona telas para esse gerenciador.</summary>
        /// <param name="screenList">A quantidade desejada de telas a serem adicionadas.</param>
        public  void Add(params T[] screenList)
        {
            screens.AddRange(screenList);

            if (CurrentScreen == null)
            {
                CurrentScreen = screens[0];
            }
        }

        private  void ApplyAction(CScreenAction action, T screen)
        {
            switch (action)
            {
                case CScreenAction.Load:
                    screen.Load();
                    break;
                case CScreenAction.Unload:
                    screen.Unload();
                    break;
                case CScreenAction.Reset:
                    screen.Reset();
                    break;
            }
        }

        /// <summary>Remove uma tela do gerenciador. Retorna true caso sucesso.</summary>
        /// <param name="name">Nome da tela.</param>
        public  bool Remove(string name)
        {
            T finder = Find(name);
            return screens.Remove(finder);
        }

        /// <summary>
        /// Troca a tela ativa para a tela selecionada.
        /// </summary>
        /// <param name="index">O index da tela pela ordem de adição.</param>
        /// <param name="action">Define a ação a ser executada na tela anterior após a sua troca.</param>
        public  void Change(int index, CScreenAction action)
        {
            T old = CurrentScreen;
            T finder = screens[index];

            isChanged = true;
            standbyScreen = finder;

            ApplyAction(action, old);
        }

        /// <summary>Troca a tela tiva para a tela selecionada.</summary>
        /// <param name="name">O nome da tela que será ativada.</param>
        /// <param name="action">Define a ação a ser executada com a tela anterior após a sua troca.</param>
        public  void Change(string name, CScreenAction action)
        {
            int index = screens.FindIndex(s => s.Name.Equals(name));

            if (index != -1)
                Change(index, action);
            else
                throw new NullReferenceException($"The screen {name} doesn'T exist.");
        }

        /// <summary>
        /// Troca para tela posterior a atual.
        /// </summary>
        /// <param name="action">Define a ação a ser executada com a tela anterior após a sua troca.</param>
        public  void Next(CScreenAction action)
        {
            int index = screens.FindIndex(x => x.Equals(CurrentScreen));

            if (index >= screens.Count - 1)
                index = 0;
            else
                index++;

            Change(screens[index].Name, action);
        }

        /// <summary>
        /// Troca para a tela anterior a atual.
        /// </summary>
        /// <param name="action">Define a ação a ser executada com a tela anterior após a sua troca.</param>
        public  void Back(CScreenAction action)
        {
            int index = screens.FindIndex(x => x.Equals(CurrentScreen));

            if (index <= 0)
                index = screens.Count - 1;
            else
                index--;

            Change(screens[index].Name, action);
        }

        /// <summary>Busca e retorna uma tela definida pelo nome.</summary>
        /// <param name="name">O nome da tela.</param>
        public  T Find(string name)
        {
            var s = screens.Find(x => x.Name.Equals(name));
            if (s != null)
                return s;
            else
                throw new NullReferenceException($"The screen {name} doesn'T exist.");
        }

        /// <summary>
        /// Define uma tela a ser carregada ao seu estado inicial.
        /// </summary>
        /// <param name="name">O nome da tela a ser redefinida.</param>
        public  void Reset(string name)
        {
            Find(name).Reset();
        }

        /// <summary>
        /// Chama o método Load() de uma determinada tela.
        /// </summary>
        /// <param name="index">O index no array de telas.</param>
        public  void Load(int index)
        {
            screens[index].Load();
        }

        /// <summary>
        /// Chama o método Load() de uma determinada tela.
        /// </summary>
        /// <param name="name">O nome da tela.</param>
        public  void Load(string name)
        {
            Find(name).Load();
        }

        /// <summary>
        /// Chama o método Unload() de uma determinada tela.
        /// </summary>
        /// <param name="index">O index no array de telas.</param>
        public  void Unload(int index)
        {
            screens[index].Unload();
        }

        /// <summary>
        /// Chama o método Unload() de uma determinada tela.
        /// </summary>
        /// <param name="name">O nome da tela.</param>
        public  void Unload(string name)
        {
            Find(name).Unload();
        }
    }
}
