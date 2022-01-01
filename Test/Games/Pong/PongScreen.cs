using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics.Modifications;

namespace Sample.Games.Pong
{
    public class PongScreen : CScreen
    {
        const int BAR_WITDH = 20;
        const int BAR_HEIGHT = 200;
        const float BAR_VELOCITY = 5F;
        const int BALL_WIDTH = 20;
        const int BALL_HEIGHT = 20;
        const float BALL_VELOCITY = 5F;

        readonly Vector2 startPosition1;
        readonly Vector2 startPosition2;
        readonly Vector2 startPositionBall;

        CKeyboardHelper keyboardInput = new CKeyboardHelper();

        CSprite bar;
        CSprite ball;

        CText pressEnterText;        
        
        Vector2 playerPosition1;
        Vector2 playerPosition2;
        Vector2 ballPosition;
        Vector2 ballDirection = Vector2.One;
        Vector2 ballVelocityIncrement = Vector2.One;
        Vector2 pressEnterTextPosition;

        int score1 = 0;
        int score2 = 0;
        
        Random random = new Random();

        CColorTransition colorModification;
        CShadow<CSprite> shadowSprite;
        CShadow<CText> shadowText;


        /// <summary>
        /// 0 = exibe a variável pressEnterText
        /// 1 = atualiza os jogadores e a bola
        /// </summary>
        byte screenState = 0;

        public PongScreen(Game game, string name = "") : base(game, name)
        {
            Viewport view = game.GraphicsDevice.Viewport;

            startPosition1 = new Vector2(BAR_WITDH, view.Height / 2 - BAR_HEIGHT / 2);
            startPosition2 = new Vector2(view.Width - BAR_WITDH * 2, view.Height / 2 - BAR_HEIGHT / 2);
            startPositionBall = new Vector2(view.Width / 2 - BALL_WIDTH / 2, view.Height / 2 - BALL_HEIGHT / 2);

            playerPosition1 = startPosition1;
            playerPosition2 = startPosition2;

            ballPosition = startPositionBall;
        }

        protected override void OnLoad()
        {
            bar = new CSprite(TextureData.GetFilledTexture(Game, BAR_WITDH, BAR_HEIGHT, Color.White));
            ball = new CSprite(TextureData.GetFilledTexture(Game, BALL_WIDTH, BALL_HEIGHT, Color.White));

            pressEnterText = new CText(Content.Load<SpriteFont>("fonts/verdana-20"), new StringBuilder("PRESS ENTER TO START"));
            pressEnterText.IsVisible = false;

            Viewport view = GraphicsDevice.Viewport;

            Rectangle bounds = pressEnterText.Bounds;
            pressEnterTextPosition = new Vector2(view.Width / 2 - bounds.Width / 2, view.Height / 2 - bounds.Height / 2);
            pressEnterTextPosition.Y += 50;

            colorModification = new CColorTransition(pressEnterText, Color.Red, false);
            shadowSprite = new CShadow<CSprite>(bar, Color.Black, new Vector2(2, 2));
            shadowText = new CShadow<CText>(pressEnterText, Color.Black, new Vector2(2, 1));
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            keyboardInput.Update(gameTime);
            
            switch(screenState)
            {
                case 0:
                    pressEnterText.IsVisible = true;

                    if (keyboardInput.Pressed(Keys.Enter))
                    {
                        screenState = 1;

                        bool toRight = random.NextBool();
                        bool toTop = random.NextBool();

                        ballDirection.X = toRight ? 1 : -1;
                        ballDirection.Y = toTop ? -1 : 1;
                    }                        

                    break;
                case 1:

                    BallUpdate();
                    Player1Update();
                    Player2Update();
                    CollisionUpdate();

                    break;
            }

            colorModification.Update(gameTime);
        }

        protected override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {            
            bar.Transform.Position2 = playerPosition1;
            shadowSprite.Draw(spriteBatch);
            bar.Draw(gameTime, spriteBatch);
            
            bar.Transform.Position2 = playerPosition2;
            shadowSprite.Draw(spriteBatch);
            bar.Draw(gameTime, spriteBatch);
            
            ball.Transform.Position2 = ballPosition;
            ball.Draw(gameTime, spriteBatch);

            if (screenState == 0)
            {
                shadowText.Draw(spriteBatch);
                pressEnterText.Transform.Position2 = pressEnterTextPosition;
                pressEnterText.Draw(gameTime, spriteBatch);
            }
        }

        void Player1Update()
        {
            if (keyboardInput.Hold(Keys.W))
                playerPosition1.Y -= BAR_VELOCITY;
            else if (keyboardInput.Hold(Keys.S))
                playerPosition1.Y += BAR_VELOCITY;           
        }

        void Player2Update()
        {
            if (keyboardInput.Hold(Keys.Up))
                playerPosition2.Y -= BAR_VELOCITY;
            else if (keyboardInput.Hold(Keys.Down))
                playerPosition2.Y += BAR_VELOCITY;
        }

        void BallUpdate()
        {
            ballPosition += ballDirection * BALL_VELOCITY * ballVelocityIncrement;
        }

        void CollisionUpdate()
        {
            Viewport view = GraphicsDevice.Viewport;

            if (playerPosition1.Y + BAR_HEIGHT > view.Height)
                playerPosition1.Y = view.Height - BAR_HEIGHT;
            if (playerPosition1.Y < view.Y)
                playerPosition1.Y = view.Y;

            if (playerPosition2.Y + BAR_HEIGHT > view.Height)
                playerPosition2.Y = view.Height - BAR_HEIGHT;
            if (playerPosition2.Y < view.Y)
                playerPosition2.Y = view.Y;

            bool isCollided = false;

            if (ballPosition.Y < view.Y)
            {
                ballPosition.Y = view.Y;                              
                isCollided = true;
            }
            else if (ballPosition.Y + BALL_HEIGHT > view.Height)
            {
                ballPosition.Y = view.Height - BALL_HEIGHT;
                isCollided = true;
            }

            if(isCollided)
            {
                ballDirection.Y *= -1F;
                ballVelocityIncrement.Y += 0.1F;
                isCollided = false;
            }

            bar.Transform.Position2 = playerPosition1;
            Rectangle bounds1 = bar.Bounds;
            bar.Transform.Position2 = playerPosition2;
            Rectangle bounds2 = bar.Bounds;

            if (ball.Bounds.Intersects(bounds1))
            {
                ballPosition.X = bounds1.X + BAR_WITDH + 1;                
                isCollided = true;
            }
            else if (ball.Bounds.Intersects(bounds2))
            {
                ballPosition.X = bounds2.X - BALL_WIDTH - 1;                
                isCollided = true;
            }

            if (isCollided)
            {
                ballDirection.X *= -1;
                ballVelocityIncrement.X += 0.25F;
            }

            Game.Window.Title = $"{ballVelocityIncrement.ToString()}";
        }

        void CheckGoal()
        {
            Viewport view = GraphicsDevice.Viewport;

            if (ballPosition.X < view.X)
            {
                score2++;
                SetDefault();
            }    
            else if(ballPosition.X > view.Width)
            {
                score1++;
                SetDefault();
            }
        }

        void SetDefault()
        {
            ballPosition = startPositionBall;
            playerPosition1 = startPosition1;
            playerPosition2 = startPosition2;
            ballVelocityIncrement = Vector2.One;
            screenState = 0;
        }
    }
}
