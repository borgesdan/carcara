using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Representa um rotacionamento de imagem estilo Mode 7 do Super Nintendo.
    /// </summary>
    public class Mode7
    {
        float radians = 0;

        Game game;
        BasicEffect effect;
        Matrix view;
        Matrix projection;
        Matrix world;
        VertexPositionColor[] vertices = new VertexPositionColor[5];

        public Mode7(Game game)
        {
            this.game = game;
            
            Viewport viewport = game.GraphicsDevice.Viewport;

            effect = new BasicEffect(game.GraphicsDevice);
            effect.VertexColorEnabled = true;

            view = Matrix.CreateLookAt(new Vector3(0, 0, 1), Vector3.Zero, Vector3.Up);
            //world = Matrix.Identity * Matrix.CreateRotationX(MathHelper.ToRadians(45));
            world = Matrix.Identity;
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), viewport.AspectRatio, 1F, 10F);

            vertices[0] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Red);
            vertices[1] = new VertexPositionColor(new Vector3(1, 1, 0), Color.Green);
            vertices[2] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Blue);
            vertices[3] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Yellow);
            vertices[4] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Red);
        }

        public void Update()
        {
            // radians += MathHelper.ToRadians(0.001F);
            world *= Matrix.CreateRotationX(MathHelper.ToRadians(0.01F));
        }

        public void Draw()
        {
            GraphicsDevice graphicsDevice = game.GraphicsDevice;
            effect.CurrentTechnique.Passes[0].Apply();
            effect.View = view;
            effect.World = world;
            effect.Projection = projection;
            
            graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, vertices, 0, 3);
        }
    }
}