namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Representa uma projeção de câmera no desenho 2D.
    /// </summary>
    public class CCamera2D
    {
        private Matrix transformMatrix = Matrix.Identity;
        private Vector2 position = Vector2.Zero;
        private float rotation = 0F;
        private float zoom = 1F;
        private Vector2 offset = Vector2.Zero;        

        /// <summary>Obtém ou define as coordenadas de tela do ponto de deslocamento do zoom e a origem de rotação.</summary>
        public Vector2 Offset 
        {
            get => offset;
            set
            {
                offset = value;
            }
        }

        /// <summary>Obtém o zoom da câmera.</summary>
        public float Zoom => zoom;

        /// <summary>Obtém a rotação da câmera.</summary>   
        public float Rotation => rotation;

        /// <summary>Obtém a Matrix a ser usada no método SpriteBatch.Begin(transformMatrix).</summary>        
        public Matrix TransformMatrix => transformMatrix;

        /// <summary>Obtém ou define a posição da câmera.</summary>
        public Vector2 Position { get => position; set => SetPosition(value);
        }
        /// <summary>Obtém ou define a posição no eixo X da câmera.</summary>
        public float X { get => Position.X; set => SetPosition(new Vector2(value, position.Y)); }
        
        /// <summary>Obtém ou define a posição no eixo Y da câmera.</summary>
        public float Y { get => Position.Y; set => SetPosition(new Vector2(position.X, value)); }

        /// <summary>
        /// Inicializa uma nova instâcia da classe.
        /// </summary>
        public CCamera2D() { }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        public CCamera2D(Vector2 position)
        {
            Position = position;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        public CCamera2D(Vector2 position, float zoom, float rotation)
        {
            Position = position;
            this.zoom = zoom;
            this.rotation = rotation;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public CCamera2D(CCamera2D source)
        {
            this.offset = source.offset;
            this.zoom = source.zoom;
            this.rotation = source.rotation;
            this.transformMatrix = source.transformMatrix;
            this.position = source.position;
        }

        private void SetPosition(Vector2 newPosition)
        {
            var diff = newPosition - position;
            Move(diff);
        }        

        /// <summary>Movimenta a câmera no sentido especificado.</summary>
        /// <param name="amount">O valor a ser movida a câmera.</param>
        public void Move(Vector2 amount)
        {
            transformMatrix *= Matrix.CreateTranslation(-amount.ToVector3());
            position += amount;
        }

        /// <summary>
        /// Rotaciona o câmera ao informar um determinado grau em radianos e tem como pivô (origem) a propriedade Offset.
        /// </summary>
        /// <param name="radians"></param>
        public void Rotate(float radians)
        {
            transformMatrix *= Matrix.CreateTranslation(X + -Offset.X, Y + -Offset.Y, 0)
                    * Matrix.CreateRotationZ(radians)
                    * Matrix.CreateTranslation(-X + Offset.X, -Y + Offset.Y, 0);

            rotation += radians;
        }

        /// <summary>
        /// Aplica zoom na câmera ao utilzar como pivô a propriedade Offset.
        /// </summary>
        /// <param name="zoom">O incremento do zoom.</param>
        public void ZoomIn(float zoom)
        {
            transformMatrix *= Matrix.CreateTranslation(X + -Offset.X, Y + -Offset.Y, 0)
                    * Matrix.CreateScale(zoom)
                    * Matrix.CreateTranslation(-X + Offset.X, -Y + Offset.Y, 0);

            this.zoom += zoom;
        }        

        /// <summary>
        /// Foca a câmera em um determinado ator de jogo.
        /// </summary>   
        /// <param name="bounds">Os limites do objeto.</param>
        public void Focus(Rectangle bounds, Viewport view)
        {
            int x = bounds.Center.X - view.Width / 2;
            int y = bounds.Center.Y - view.Height / 2;
            Vector2 focus = new Vector2(x, y);

            SetPosition(focus);
        }        
    }
}
