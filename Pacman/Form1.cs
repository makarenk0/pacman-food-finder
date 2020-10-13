﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Pacman
{
    public partial class Form1 : Form
    {
        public static GameBoard gameboard = new GameBoard();
        public static Food food = new Food();
        public static Pacman pacman = new Pacman();
        public static Ghost ghost = new Ghost();
        public static Player player = new Player();
        public static HighScore highscore = new HighScore();
        //public static Audio audio = new Audio();
        private static FormElements formelements = new FormElements();


        //For console
        //[DllImport("kernel32.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool AllocConsole();

        public Form1()
        {
            InitializeComponent();
            SetupGame(Pacman.LEVEL_NUMBER);
        }

        public void SetupGame(int Level)
        {
            // Create Game Board
            gameboard.CreateBoardImage(this, Level);

            // Create Board Matrix
            Tuple<int, int> PacmanStartCoordinates = gameboard.InitialiseBoardMatrix(Level);

            // Create Player
            player.CreatePlayerDetails(this);
            player.CreateLives(this);

            // Create Form Elements
            formelements.CreateFormElements(this);

            // Create High Score
            highscore.CreateHighScore(this);

            // Create Food
            food.CreateFoodImages(this);

            // Create Ghosts
            ghost.CreateGhostImage(this);

            // Create Pacman
            pacman.CreatePacmanImage(this, PacmanStartCoordinates.Item1, PacmanStartCoordinates.Item2);
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            /* //disabled 
            switch (e.KeyCode)
            {
                case Keys.Up: pacman.nextDirection = 1; pacman.MovePacman(1); break;
                case Keys.Right: pacman.nextDirection = 2; pacman.MovePacman(2); break;
                case Keys.Down: pacman.nextDirection = 3; pacman.MovePacman(3); break;
                case Keys.Left: pacman.nextDirection = 4; pacman.MovePacman(4); break;
            }
            */
        }

    }
}
