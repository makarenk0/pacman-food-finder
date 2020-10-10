using Pacman.Classes;
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
    public class Pacman
    {
        // Initialise variables
        public int xCoordinate = 0;
        public int yCoordinate = 0;
        private int xStart = 0;
        private int yStart = 0;
        public int currentDirection = 0;
        public int nextDirection = 0;
        public PictureBox PacmanImage = new PictureBox();
        private ImageList PacmanImages = new ImageList(); 
        private Timer timer = new Timer();

        private Form1 _formInstance;

        private int imageOn = 0;

        Graph searcher;
        bool foundStartPoint = false;

        public Pacman()
        {
            timer.Interval = 20;     // SET SPEED
            timer.Enabled = true;
            timer.Tick += new EventHandler(timer_Tick);

            PacmanImages.Images.Add(Properties.Resources.Pacman_1_0);
            PacmanImages.Images.Add(Properties.Resources.Pacman_1_1);
            PacmanImages.Images.Add(Properties.Resources.Pacman_1_2);
            PacmanImages.Images.Add(Properties.Resources.Pacman_1_3);

            PacmanImages.Images.Add(Properties.Resources.Pacman_2_0);
            PacmanImages.Images.Add(Properties.Resources.Pacman_2_1);
            PacmanImages.Images.Add(Properties.Resources.Pacman_2_2);
            PacmanImages.Images.Add(Properties.Resources.Pacman_2_3);

            PacmanImages.Images.Add(Properties.Resources.Pacman_3_0);
            PacmanImages.Images.Add(Properties.Resources.Pacman_3_1);
            PacmanImages.Images.Add(Properties.Resources.Pacman_3_2);
            PacmanImages.Images.Add(Properties.Resources.Pacman_3_3);

            PacmanImages.Images.Add(Properties.Resources.Pacman_4_0);
            PacmanImages.Images.Add(Properties.Resources.Pacman_4_1);
            PacmanImages.Images.Add(Properties.Resources.Pacman_4_2);
            PacmanImages.Images.Add(Properties.Resources.Pacman_4_3);

            PacmanImages.ImageSize = new Size(27,28);
            

        }

        public void CreatePacmanImage(Form1 formInstance, int StartXCoordinate, int StartYCoordinate)
        {
            // Create Pacman Image
            xStart = StartXCoordinate;
            yStart = StartYCoordinate;
            PacmanImage.Name = "PacmanImage";
            PacmanImage.SizeMode = PictureBoxSizeMode.AutoSize;
            Set_Pacman();
            formInstance.Controls.Add(PacmanImage);
            PacmanImage.BringToFront();
            _formInstance = formInstance;


        }

        public void MovePacman(int direction)
        {
            // Move Pacman
            bool CanMove = check_direction(nextDirection);
            if (!CanMove) { CanMove = check_direction(currentDirection); direction = currentDirection; } else { direction = nextDirection; }
            if (CanMove) { currentDirection = direction; }

            if (CanMove)
            {
                switch (direction)
                {
                    case 1: PacmanImage.Top -= 16; yCoordinate--; break;  
                    case 2: PacmanImage.Left += 16; xCoordinate++; break; 
                    case 3: PacmanImage.Top += 16; yCoordinate++; break; 
                    case 4: PacmanImage.Left -= 16; xCoordinate--; break; 
                }
                currentDirection = direction;
                UpdatePacmanImage();
                CheckPacmanPosition();
                Form1.ghost.CheckForPacman();
            }
        }

        private void CheckPacmanPosition()
        {
            // Check Pacmans position
            switch (Form1.gameboard.Matrix[yCoordinate, xCoordinate])
            {
                case 1: Form1.food.EatFood(yCoordinate, xCoordinate); break;
                case 2: 
                    Form1.food.EatSuperFood(yCoordinate, xCoordinate);
                    timer.Stop();
                    break;  //modified to stop
            }
        }

        private void UpdatePacmanImage()
        {
            // Update Pacman image
            PacmanImage.Image = PacmanImages.Images[((currentDirection - 1) * 4) + imageOn];
            imageOn++;
            if (imageOn > 3) { imageOn = 0; }
        }

        private bool check_direction(int direction)
        {
            // Check if pacman can move to space
            switch (direction)
            {
                case 1: return direction_ok(xCoordinate, yCoordinate - 1);
                case 2: return direction_ok(xCoordinate + 1, yCoordinate);
                case 3: return direction_ok(xCoordinate, yCoordinate + 1);
                case 4: return direction_ok(xCoordinate - 1, yCoordinate);
                default: return false;
            }
        }

        private bool direction_ok(int x, int y)
        {
            // Check if board space can be used
            if (x < 0) { xCoordinate = 27; PacmanImage.Left = 429; return true ; }
            if (x > 27) { xCoordinate = 0; PacmanImage.Left = -5; return true; }
            if (Form1.gameboard.Matrix[y, x] < 4) { return true; } else { return false; }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Keep moving pacman

            

            Explore();
           

            MovePacman(currentDirection);

            if (foundStartPoint)
            {
                if (searcher._goingBack)
                {
                    Form1.food.DeleteOneFoodImage(xCoordinate, yCoordinate, _formInstance, searcher._current.Count + 1);
                    Form1.pacman.PacmanImage.BringToFront();
                }
                else
                {
                    Form1.food.CreateOneFoodImage(xCoordinate, yCoordinate, _formInstance, searcher._current.Count);
                    Form1.pacman.PacmanImage.BringToFront();
                }
            }

        }

        public void Set_Pacman()
        {
            // Place Pacman in board
            PacmanImage.Image = Properties.Resources.Pacman_2_1;
            currentDirection = 0;
            nextDirection = 0;
            xCoordinate = xStart;
            yCoordinate = yStart;
            PacmanImage.Location = new Point(xStart * 16 - 3, yStart * 16 + 43);
        }


        //My methods
        private bool CheckIfInVerticeOfGraph()
        {
            int amount = GetFreeDirections().Count;
            return amount > 2;
        }

        private List<short> GetFreeDirections()
        {
            List<short> directions = new List<short>();
            for(short i = 1; i<5; i++)
            {
                if (check_direction(i))
                {
                    directions.Add(i);
                }
            }
            return directions;
        }

        private void Explore()
        {
            if (CheckIfInVerticeOfGraph())
            {
                if (!foundStartPoint)
                {
                    searcher = new Graph(GetFreeDirections(), 2);
                    foundStartPoint = true;
                    TakeALook();
                }
                else
                {
                    bool buf = searcher._goingBack;
                    TakeALook();
                    Form1.food.DeleteOneFoodImage(xCoordinate, yCoordinate, _formInstance, searcher._current.Count+1);
                    
                }     
            }
            else if (!check_direction(currentDirection))
            {
                currentDirection = TurnToFreeSpace(currentDirection);
            }
        }


        private int TurnToFreeSpace(int cur)
        {
            if(cur == 2 || cur == 3)
            {
                return check_direction(cur - 1) ? cur - 1 : cur + 1;
            }
            else if (cur == 1)
            {
                return check_direction(2) ? 2 : 4;
            }
            return check_direction(1) ? 1 : 3;
        }


        private void TakeALook()
        {
            List<short> dirs = GetFreeDirections();
            dirs.Remove(searcher.Opposite((short)currentDirection));

            currentDirection = searcher.Go((short)currentDirection, dirs);
        }


    }
}
