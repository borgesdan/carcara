using System;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Representa uma animação 2D que armazena texturas e recortes.
    /// </summary>
    public class CAnimation : ICTransformable, ICDrawTransformable, ICUpdatable, ICDrawable
    {
        double elapsedTime = 0;
        float time;
        CAnimationItem[] items;

        /// <summary>Obtém ou define o tempo de exibição de cada item.</summary>
        public float Time { get => time; set => time = MathHelper.Clamp(value, 0, float.MaxValue); }

        /// <summary>Obtém ou define se animação se repetirá quando chegar ao fim.</summary>
        public bool IsLooping { get; set; } = true;

        /// <summary>Obtém o item a ser utilizado no momento.</summary>
        public CAnimationItem CurrentItem { get => items[CurrentItemIndex]; }
        
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
        /// <summary>Obtém o index atual de acesso ao vetor <see cref="items"/>.</summary>
        public int CurrentItemIndex { get; private set; } = 0;
        
        /// <summary>Obtém se a animação finalizou após o termino do método de atualização.</summary>
        public bool IsFinished { get; private set; }

        /// <summary>Obtém os limites de tela da animação.</summary>
        public Rectangle Bounds
        {
            get
            {
                if (CurrentItem == null)
                    return Rectangle.Empty;

                return CBounds.Get(Transform, CurrentFrame.Width, CurrentFrame.Height, DrawTransform.Origin);
            }
        }

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
        /// <param name="time">Define o tempo de exibição de cada item.</param>
        /// <param name="isLooping">Define se animação se repetirá quando chegar ao fim.</param>
        /// <param name="items">Define a quantidade de itens a serem utilizados na animação.</param>
        public CAnimation(float time, bool isLooping, params CAnimationItem[] items)
        {
            this.items = items;
            Time = time;
            IsLooping = isLooping;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="time">Define o tempo de exibição de cada item.</param>
        /// <param name="isLooping">Define se animação se repetirá quando chegar ao fim.</param>
        /// <param name="textures">Define as texturas que serão convertidas em items.</param>
        public CAnimation(float time, bool isLooping, params Texture2D[] textures)
        {
            items = new CAnimationItem[textures.Length];

            for(int i = 0; i < textures.Length; i++)
            {
                items[i] = new CAnimationItem(textures[i], null);
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
            this.items = source.items;
            this.Finished = source.Finished;
            this.FrameChanged = source.FrameChanged;
            this.ItemChanged = source.ItemChanged;
        }

        public CAnimationItem this[int index]
        {
            get => items[index];
            set => items[index] = value;
        }

        /// <summary>
        /// Método de atualização.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            if (items == null)
                return;

            IsFinished = false;
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            int fIndex = CurrentFrameIndex;
            int iIndex = CurrentItemIndex;

            if(elapsedTime > Time)
            {
                elapsedTime = 0;
                UpdateAnimation();
            }

            if (fIndex != CurrentFrameIndex)
                FrameChanged?.Invoke(CurrentItem);
            if (iIndex != CurrentItemIndex)
                ItemChanged?.Invoke(CurrentItem);
        }

        void UpdateAnimation()
        {
            if (CurrentFrameIndex < CurrentItem.FrameCount - 1)
            {
                CurrentFrameIndex++;
            }
            else
            {
                CurrentItemIndex++;

                if (CurrentItemIndex > items.Length - 1)
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
                        CurrentItemIndex = items.Length - 1;
                    }

                    Finished?.Invoke(CurrentItem);
                    IsFinished = true;
                }
            }
        }

        /// <summary>
        /// Método de desenho.
        /// </summary>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (items == null && CurrentItem.Texture == null)
                return;
            
            CDrawArgs args = new CDrawArgs(Transform, DrawTransform);            
            args.Source = CurrentFrame;

            //spriteBatch.Draw(CurrentTexture, Vector2.Zero, CurrentFrame, Color.White);
            spriteBatch.Draw(CurrentTexture, args);
        }

        /// <summary>
        /// Redefine o tempo corrente da animação, as proprieades <see cref="CurrentFrameIndex"/> e <see cref="CurrentItemIndex"/> para o valor 0.
        /// </summary>
        public void Reset()
        {
            elapsedTime = 0;
            CurrentFrameIndex = 0;
            CurrentItemIndex = 0;
            IsFinished = false;
        }
    }
}