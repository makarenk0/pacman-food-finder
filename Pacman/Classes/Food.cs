using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman
{
    public class Food
    {
        public KeyValuePair<PictureBox, int>[,] FoodImage = new KeyValuePair<PictureBox, int>[30,27];
        public int Amount = 0;

        private const int FoodScore = 10;
        private const int SuperFoodScore = 50;

        public void CreateFoodImages(Form formInstance)
        {
            for (int y = 0; y < Form1.gameboard.Matrix.GetLength(0); y++)
            {
                for (int x = 0; x < Form1.gameboard.Matrix.GetLength(1); x++)
                {
                    if (Form1.gameboard.Matrix[y,x] == 1 || Form1.gameboard.Matrix[y, x] == 2)
                    {
                        FoodImage[y, x] = new KeyValuePair<PictureBox, int>(new PictureBox(), 0);
                        FoodImage[y, x].Key.Name = "FoodImage" + Amount.ToString();
                        FoodImage[y, x].Key.SizeMode = PictureBoxSizeMode.AutoSize;
                        FoodImage[y, x].Key.Location = new Point(x * 16 - 1, y * 16 + 47);
                        if (Form1.gameboard.Matrix[y,x] == 1)
                        {
                            FoodImage[y, x].Key.Image = Properties.Resources.Block_1;
                            Amount++;
                        }
                        else
                        {
                            FoodImage[y, x].Key.Image = Properties.Resources.Block_2;
                        }
                        formInstance.Controls.Add(FoodImage[y, x].Key);
                        FoodImage[y, x].Key.BringToFront();
                    }
                }
            }
        }

        public void CreateGreenFoodImage(int x ,int y, Form1 formInstance, int iterateNum)
        {
            if(x < 27 && y < 30 && FoodImage[y, x].Equals(new KeyValuePair<PictureBox, int>()))
            {
                FoodImage[y, x] = new KeyValuePair<PictureBox, int>(new PictureBox(), iterateNum);
                FoodImage[y, x].Key.Name = "FoodImage" + Amount.ToString();
                FoodImage[y, x].Key.SizeMode = PictureBoxSizeMode.AutoSize;
                FoodImage[y, x].Key.Location = new Point(x * 16 - 1, y * 16 + 47);
                
                FoodImage[y, x].Key.Image = Properties.Resources.Block_1;
               
                
                formInstance.Controls.Add(FoodImage[y, x].Key);
                FoodImage[y, x].Key.BringToFront();
            }
            
        }

        public void CreateRedFoodImage(int x, int y, Form1 formInstance, int iterateNum)
        {
            if (x < 27 && y < 30 && FoodImage[y, x].Equals(new KeyValuePair<PictureBox, int>()))
            {
                FoodImage[y, x] = new KeyValuePair<PictureBox, int>(new PictureBox(), iterateNum);
                FoodImage[y, x].Key.Name = "FoodImage" + Amount.ToString();
                FoodImage[y, x].Key.SizeMode = PictureBoxSizeMode.AutoSize;
                FoodImage[y, x].Key.Location = new Point(x * 16 - 1, y * 16 + 47);

                FoodImage[y, x].Key.Image = Properties.Resources.Block_3;


                formInstance.Controls.Add(FoodImage[y, x].Key);
                FoodImage[y, x].Key.BringToFront();
            }

        }

        public void DeleteOneFoodImage(int x, int y, Form1 formInstance, int iterateNum)
        {
            if (x < 27 && y < 30 && FoodImage[y, x].Value == iterateNum)
            {
                formInstance.Controls.Remove(FoodImage[y, x].Key);
                FoodImage[y, x] = new KeyValuePair<PictureBox, int>();
            }

        }

        public void EatFood(int x, int y)
        {
            // Eat food
            FoodImage[x, y].Key.Visible = false;
            Form1.gameboard.Matrix[x, y] = 0;
            Form1.player.UpdateScore(FoodScore);
            Amount--;
            if (Amount < 1) { Form1.player.LevelComplete(); }
            //Form1.audio.Play(1);
        }

        public void EatSuperFood(int x, int y)
        {
            // Eat food
            FoodImage[x, y].Key.Visible = false;
            Form1.gameboard.Matrix[x, y] = 0;
            Form1.player.UpdateScore(SuperFoodScore);
            Form1.ghost.ChangeGhostState();
        }
    }
}
