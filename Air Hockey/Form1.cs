using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Air_Hockey
{
    public partial class Form1 : Form
    {
        int paddle1X = 375;
        int paddle1Y = 20;
        int player1Score = 0;

        int paddle2X = 375;
        int paddle2Y = 525;
        int player2Score = 0;

        int paddleWidth = 50;
        int paddleHeight = 50;
        int paddleSpeed = 6;

        int ballX = 390;
        int ballY = 290;
        int ballXSpeed = 9;
        int ballYSpeed = 9;
        int ballWidth = 20;
        int ballHeight = 20;

        bool wDown = false;
        bool sDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool aDown = false;
        bool dDown = false;
        bool rightArrowDown = false;
        bool leftArrowDown = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush darkblueBrush = new SolidBrush(Color.DarkBlue);
        Pen whitePen = new Pen(Color.White, 2);
        Pen grayPen = new Pen(Color.Gray, 10);
        Pen redPen = new Pen(Color.Red, 5);
        Pen bluePen = new Pen(Color.Blue, 5);

        SoundPlayer horn = new SoundPlayer(Properties.Resources._170825__santino_c__sirene_horn);
        SoundPlayer hit = new SoundPlayer(Properties.Resources._266647__eelke__hit_hockey);
        SoundPlayer wall = new SoundPlayer(Properties.Resources._324246__permagnuslindborg__hockey_ball);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            #region keydown
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                break;
                case Keys.S:
                    sDown = true;
                break;
                case Keys.Up:
                    upArrowDown = true;
                break;
                case Keys.Down:
                    downArrowDown = true;
                break;
                case Keys.A:
                    aDown = true;
                break;
                case Keys.D:
                    dDown = true;
                break;
                case Keys.Right:
                    rightArrowDown = true;
                break;
                case Keys.Left:
                    leftArrowDown = true;
                break;

            }
            #endregion
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            #region keyup
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;

            }
            #endregion
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            #region graphics
            Graphics g = this.CreateGraphics();
            e.Graphics.DrawLine(redPen, 200, 300, 600, 300);
            e.Graphics.DrawLine(bluePen, 200, 170, 600, 170);
            e.Graphics.DrawLine(bluePen, 200, 430, 600, 430);

            e.Graphics.DrawLine(grayPen, 200, 0, 200, 600);
            e.Graphics.DrawLine(grayPen, 600, 0, 600, 600);

            e.Graphics.DrawLine(grayPen, 200, 0, 325, 0);
            e.Graphics.DrawLine(grayPen, 600, 0, 475, 0);

            e.Graphics.DrawLine(grayPen, 200, 600, 325, 600);
            e.Graphics.DrawLine(grayPen, 600, 600, 475, 600);

            e.Graphics.FillEllipse(whiteBrush, ballX, ballY, ballWidth, ballHeight);
            e.Graphics.FillEllipse(blueBrush, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            e.Graphics.FillEllipse(redBrush, paddle2X, paddle2Y, paddleWidth, paddleHeight);
            #endregion
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {

            #region move ball
            //move ball
            int x = ballX;
            int y = ballY;

            ballX += ballXSpeed;
            ballY += ballYSpeed;
            #endregion

            #region move player 1
            //move player 1 
            if (wDown == true && paddle1Y > 8)
            {
                paddle1Y -= paddleSpeed;
            }

            if (sDown == true && paddle1Y < 245)
            {
                paddle1Y += paddleSpeed;
            }

            if (aDown == true && paddle1X > 207)
            {
                paddle1X -= paddleSpeed;
            }

            if (dDown == true && paddle1X < 542)
            {
                paddle1X += paddleSpeed;
            }
            #endregion

            #region move player 2
            //move player 2 
            if (upArrowDown == true && paddle2Y > 302)
            {
                paddle2Y -= paddleSpeed;
            }

            if (downArrowDown == true && paddle2Y < 540)
            {
                paddle2Y += paddleSpeed;
            }

            if (leftArrowDown == true && paddle2X > 207)
            {
                paddle2X -= paddleSpeed;
            }

            if (rightArrowDown == true && paddle2X < 542)
            {
                paddle2X += paddleSpeed;
            }
            #endregion

            #region ball colision left/right
            //ball collision with left/right
            if (ballY < 5 || ballY > 572)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 
                wall.Play();
            }
            if (ballX > 570 || ballX < 210)
            {
                ballXSpeed *= -1;
                wall.Play();
            }
            #endregion

            #region paddle collision
                //create Rectangles of objects on screen to be used for collision detection 
                Rectangle player1Rec = new Rectangle(paddle1X, paddle1Y, paddleWidth, paddleHeight);
                Rectangle player2Rec = new Rectangle(paddle2X, paddle2Y, paddleWidth, paddleHeight);
                Rectangle ballRec = new Rectangle(ballX, ballY, ballWidth, ballHeight);

                //check if ball hits either paddle. If it does change the direction 
                //and place the ball in front of the paddle hit 

                    if (player1Rec.IntersectsWith(ballRec))
                    {

                    ballX = x;
                    ballY = y;
                    ballYSpeed *= -1;

                ballX += ballXSpeed;
                ballY += ballYSpeed;

                hit.Play();

            }

                    if (player2Rec.IntersectsWith(ballRec))
                    {
                    ballX = x;
                    ballY = y;
                    ballYSpeed *= -1;

                ballX += ballXSpeed;
                ballY += ballYSpeed;

                hit.Play();
            }
                
            #endregion

            #region point scoring
            //check if either player scored a point
            if (ballY < 4 && ballX > 315 && ballX < 460)
            {

                    player2Score++;
                    p2ScoreLabel.Text = $"{player2Score}";


                ballX = 400;
                ballY = 300;

                paddle1Y = 20;
                paddle2Y = 525;
                paddle1X = 375;
                paddle2X = 375;

                horn.Play();
            }
            if (ballY > 575 && ballX > 315 && ballX < 460)
            {

                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";


                ballX = 390;
                ballY = 290;

                paddle1Y = 20;
                paddle2Y = 525;
                paddle1X = 375;
                paddle2X = 375;

                horn.Play();
            }


            if (player1Score == 3)
            {
                gameTimer.Enabled = false;
                winnerLabel.Text = $"Player 1 is the Winner!";
            }

            if (player2Score == 3)
            {
                gameTimer.Enabled = false;
                winnerLabel.Text = $"Player 2 is the Winner!";
            }
            #endregion

            Refresh();
        }
    }
}
