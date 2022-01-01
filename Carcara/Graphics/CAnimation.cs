using System;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Representa uma animação 2D que armazena texturas e recortes.
    /// </summary>
    public class CAnimation : ICTransformable, ICDrawTransformable
    {
        double elapsedTime = 0;

        /// <summary>
        /// Obtém ou define a quantidade de itens a serem utilizados na animação.
        /// Um item pode receber uma textura e recortes, que representam um quadro a ser exibido.
        /// </summary>
        public CAnimationItem[] Items { get; set; }

        /// <summary>Obtém ou define o tempo de exibição de cada item.</summary>
        public float Time { get; set; }

        /// <summary>Obtém ou define se animação se repetirá quando chegar ao fim.</summary>
        public bool IsLooping { get; set; } = true;

        /// <summary>Obtém o item a ser utilizado no momento.</summary>
        public CAnimationItem CurrentItem { get => Items[CurrentItemIndex]; }
        
        /// <summary>Obtém o recorte a ser utilizado no momento.</summary>
        public Rectangle CurrentFrame 
        {
            get
            {
                if (CurrentItem.Frames != null)
                    return CurrentItem.Frames[CurrentFrameIndex];
                else
                    return CurrentTexture.Bounds;
            } 
        }

        /// <summary>Obtém a textura a ser utilizada no momento.</summary>
        public Texture2D CurrentTexture { get => CurrentItem.Texture; }

        /// <summary>Obtém o index do recorte do item atual.</summary>
        public int CurrentFrameIndex { get; private set; } = 0;
        /// <summary>Obtém o index atual de acesso ao vetor <see cref="Items"/>.</summary>
        public int CurrentItemIndex { get; private set; } = 0;
        
        /// <summary>Obtém se a animação finalizou após o termino do método de atualização.</summary>
        public bool IsFinished { get; private set; }

        /// <summary>Obtém ou define as propriedades de transformações.</summary>
        public CTransform Transform { get; set; } = new CTransform();
        /// <summary>Obtém ou define as propriedades de transformações para desenho.</summary>
        public CDrawTransform DrawTransform { get; set; } = new CDrawTransform();

        /// <summary>Ocorre quando um item é trocado.</summary>
        public event Action<CAnimationItem> ItemChanged;
        /// <summary>Ocorre quando um frame do item atual é trocado.</summary>
        public event Action<CAnimationItem> FrameChanged;
        /// <summary>Ocorre quando a animação chega ao fim.</summary>
        public event Action<CAnimationItem> Finished;

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="items">Define a quantidade de itens a serem utilizados na animação.</param>
        /// <param name="time">Define o tempo de exibição de cada item.</param>
        /// <param name="isLooping">Define se animação se repetirá quando chegar ao fim.</param>
        public CAnimation(CAnimationItem[] items, float time = 0.1F, bool isLooping = true)
        {
            Items = items;
            Time = time;
            IsLooping = isLooping;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="textures">Define as texturas que serão convertidas em items.</param>
        /// <param name="time">Define o tempo de exibição de cada item.</param>
        /// <param name="isLooping">Define se animação se repetirá quando chegar ao fim.</param>
        public CAnimation(Texture2D[] textures, float time = 0.1F, bool isLooping = true)            
        {
            Items = new CAnimationItem[textures.Length];

            for(int i = 0; i < textures.Length; i++)
            {
                Items[i] = new CAnimationItem(textures[i], null);
            }

            Time = time;
            IsLooping = isLooping;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public CAnimation(CAnimation source)
        {
            this.CurrentFrameIndex = source.CurrentFrameIndex;
            this.CurrentItemIndex = source.CurrentItemIndex;
            this.DrawTransform = new CDrawTransform(source.DrawTransform);
            this.IsLooping = source.IsLooping;
            this.IsFinished = source.IsFinished;
            this.Time = source.Time;
            this.Transform = new CTransform(source.Transform);
            this.elapsedTime = source.elapsedTime;
            
            this.Items = new CAnimationItem[source.Items.Length];

            for (int i = 0; i < source.Items.Length; i++)
            {
                Items[i] = new CAnimationItem(source.Items[i]);
            }

            this.Finished = source.Finished;
            this.FrameChanged = source.FrameChanged;
            this.ItemChanged = source.ItemChanged;
        }

        /// <summary>
        /// Método de atualização.
        /// </summary>
        /// <param name="gameTime">Obtém acesso aos tempos de jogo.</param>
        public void Update(GameTime gameTime)
        {
            if (Items == null)
                return;

            IsFinished = false;
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            int fIndex = CurrentFrameIndex;
            int iIndex = CurrentItemIndex;

            if(elapsedTime > Time)
            {
                elapsedTime = 0;        

                if(CurrentFrameIndex < CurrentItem.FrameCount - 1)
                {
                    CurrentFrameIndex++;
                }
                else
                {
                    CurrentItemIndex++;

                    if (CurrentItemIndex > Items.Length - 1)
                    {
                        if (IsLooping)
                        {
                            CurrentFrameIndex = 0;
                            CurrentItemIndex = 0;
                        }
                        else
                        {
                            CurrentItemIndex--;
                            CurrentFrameIndex = CurrentItem.FrameCount - 1;
                            CurrentItemIndex = Items.Length - 1;
                        }

                        Finished?.Invoke(CurrentItem);
                        IsFinished = true;
                    }
                }
            }

            if (fIndex != CurrentFrameIndex)
                FrameChanged?.Invoke(CurrentItem);
            if (iIndex != CurrentItemIndex)
                ItemChanged?.Invoke(CurrentItem);
        }

        /// <summary>
        /// Método de desenho.
        /// </summary>
        /// <param name="spriteBatch">Obtém acesso ao objeto SpriteBatch corrente.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Items == null && CurrentItem.Texture == null)
                return;
            
            CDrawArgs args = new CDrawArgs(Transform, DrawTransform);            
            args.Source = CurrentFrame;

            //spriteBatch.Draw(CurrentTexture, Vector2.Zero, CurrentFrame, Color.White);
            spriteBatch.Draw(CurrentTexture, args);
        }
    }
}