using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using System.Threading;
using System.Windows.Input;

namespace WindowsFormsApplication1
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        int Lives = 1;
        private void Form1_Load(object sender, EventArgs e)
        {
            GamePanel.Visible = false;
            Menu.Visible = true;
            if (Lives == 0)
            { 
                YouLoosePanel.Visible = true;
                Thread.Sleep(3000);
                GamePanel.Visible = false;
                Menu.Visible = true;
            }
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            Menu.Visible = false;
            GamePanel.Visible = true;
        }

        private void ScoreButton_Click(object sender, EventArgs e)
        {
            Menu.Visible = false;
            ScorePanel.Visible = true;
        }

        private void RetMenSco_Click(object sender, EventArgs e)
        {
            ScorePanel.Visible = false;
            Menu.Visible = true;
        }
        //this.Player.Location = new System.Drawing.Point(412, 605);
        int PlayerX = 412;
        int PlayerY = 605;
        bool LeftBorder = false;
        bool RightBorder = false;
        int PlayerSpeed = 15;
        bool Start = false;
        private void GamePanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                if (Start == false)
                {
                    Ball.Visible = true;
                    Start = true;
                    Game();
                }
                if (LeftBorder == true) { PlayerX = PlayerX + PlayerSpeed; }
                PlayerX = PlayerX - PlayerSpeed;
                Player.Location = new System.Drawing.Point(PlayerX, PlayerY);
                if(PlayerX - PlayerSpeed <= 0)
                {
                    LeftBorder = true;
                }
                else { LeftBorder = false; }
            }
            if (e.KeyCode == Keys.Right)
            {
                if (Start == false)
                {
                    Ball.Visible = true;
                    Start = true;
                    Game();
                }
                if (RightBorder == true) { PlayerX = PlayerX - PlayerSpeed; }
                PlayerX = PlayerX + PlayerSpeed;
                Player.Location = new System.Drawing.Point(PlayerX, PlayerY);
                if(PlayerX + PlayerSpeed >= 820)
                {
                    RightBorder = true;
                }
                else { RightBorder = false; }
            }
        }
        public void Game()
        {
            var r = new Random();
            int BallX = 476;
            int BallY = 540;
            int Speed = 1;
            int RiLeRand = r.Next(1, 2);
            bool Loose = false;
            while (Ball.Bounds.IntersectsWith(Player.Bounds) == false)
            {
                BallY = BallY + Speed;
                Ball.Location = new Point(BallX, BallY);
            }
            // X pos = X right && Y pos = Y down //
            while (Loose == false)
            {
                foreach (Control control in GamePanel.Controls)
                {
                    if (control is Panel && control != Player)
                    {
                        if (Ball.Bounds.IntersectsWith(control.Bounds) == true)
                        {
                            RiLeRand = r.Next(1, 2);
                            if (RiLeRand == 1) { DownLeft(BallX, BallY, Speed, Ball, control); }          
                            else { DownRight(BallX, BallY, Speed, Ball, control); } 
                        }
                     }
                    else if (control is Panel && control == Player)
                    {
                        if (Ball.Bounds.IntersectsWith(control.Bounds) == true)
                        {
                            RiLeRand = r.Next(1, 2);
                            if (RiLeRand == 1) { UpLeft(BallX, BallY, Speed, Ball, control); } 
                            else { UpRight(BallX, BallY, Speed, Ball, control); } 
                        }
                    }
                    else if (control is Panel && control == GamePanel)
                    {
                        if (Ball.Bounds.IntersectsWith(control.Bounds) == true)
                        {
                            Loose = true;
                        }
                    }
                    if (Loose == true)
                    {
                        break;
                        YouLoosePanel.Visible = true;
                        Thread.Sleep(3000);
                        YouLoosePanel.Visible = false;
                        Menu.Visible = true;
                        GamePanel.Visible = false;
                        Loose = false;
                        Start = false;
                    }
                }
            }
        }
        public void DownLeft(int x, int y, int s, Control ball, Control panel)
        {
            y = y + 1;
            x = x - 1;
            ball.Location = new Point(x, y);
            while (ball.Bounds.IntersectsWith(panel.Bounds) == false)
            {
                y = y + s;
                x = x - s;
                ball.Location = new Point(x, y);
            }
        }
        public void DownRight(int x, int y, int s, Control ball, Control panel)
        {
            y = y + 1;
            x = x + 1;
            ball.Location = new Point(x, y);
            while (ball.Bounds.IntersectsWith(panel.Bounds) == false)
            {
                y = y + s;
                x = x + s;
                ball.Location = new Point(x, y);
            }
        }
        public void UpLeft(int x, int y, int s, Control ball, Control panel)
        {
            y = y - 1;
            x = x - 1;
            ball.Location = new Point(x, y);
            while (ball.Bounds.IntersectsWith(panel.Bounds) == false)
            {
                y = y - s;                                                                  //need to make control == other than just Player//
                x = x - s;
                ball.Location = new Point(x, y);
            }
        }
        public void UpRight(int x, int y, int s, Control ball, Control panel)
        {
            y = y - 1;
            x = x + 1;
            ball.Location = new Point(x, y);
            while (ball.Bounds.IntersectsWith(panel.Bounds) == false)
            {
                y = y - s;
                x = x + s;
                ball.Location = new Point(x, y);
            }
        }
    }
}
