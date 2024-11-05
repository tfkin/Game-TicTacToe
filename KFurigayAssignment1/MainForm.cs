// MainForm.cs
// Assignment 1 that is a tic-tac-toe game.
// Revision History: $Kin Furigay, 2023.09.26: Created. -- >

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KFurigayAssignment1
{
    public partial class MainForm : Form
    {
        // Declares variables
        bool turn = true; // true = X turn, false = O turn
        int turnCount = 0; // to count the turns taken, to check out whether it’s a draw when turnCount = 9
        PictureBox[] pbArray;
        string[] boardState = new string[9];
        public MainForm()
        {
            InitializeComponent();  
            InitializeGame();
        }

        // Initializes or resets the game to its default state.
        private void InitializeGame()
        {
            turn = true; // X starts
            turnCount = 0;
            boardState = new string[9]; // Resetting the boardState
            if (pbArray == null)
                pbArray = new PictureBox[] { pbTL, pbTC, pbTR, pbML, pbMC, pbMR, pbBL, pbBC, pbBR };

            foreach (var pb in pbArray)
            {
                pb.Image = null;
                pb.Enabled = true;
                pb.Click -= Pb_Click; // Unsubscribe before subscribing to avoid multiple subscriptions
                pb.Click += Pb_Click; // adding a common click event handler for all PictureBoxes
            }
            UpdateTurnLabel();
        }

        // Handles the Click event for all PictureBox controls.
        private void Pb_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender; 
            int index = Array.IndexOf(pbArray, pb);
            if (turn) // If it's X's turn
            {   
                pb.Image = Properties.Resources.X;
                boardState[index] = "X";
            }
            else // If it's O's turn
            {
                pb.Image = Properties.Resources.O;
                boardState[index] = "O";
            }

            turn = !turn; // Switch turns
            pb.Enabled = false; // Disable the clicked PictureBox
            turnCount++;
            CheckWinner();
            UpdateTurnLabel();
        }

        // Updates the turn label to display which player's turn it is.
        private void UpdateTurnLabel()
        {
            if (turn) // If it's X's turn
                lblTurn.Text = "TURN: Player 1 (X)";
            else // If it's O's turn
                lblTurn.Text = "TURN: Player 2 (O)";
        }

        // Checks if there is a winner or a draw and displays a message accordingly.
        private void CheckWinner()
        {
            string winnerPlayer = CheckRows() ?? CheckColumns() ?? CheckDiagonals();

            if (winnerPlayer != null)
            {
                lblTurn.Text = "";
                MessageBox.Show($"{winnerPlayer} Wins!", "Game Over");
                InitializeGame();
            }
            else if (turnCount == 9)
            {
                lblTurn.Text = "";
                MessageBox.Show("It's a draw!", "Game Result");
                InitializeGame();
            }
        }

        // Checks each row to see if there is a winner.
        private string CheckRows()
        {
            for (int i = 0; i < 9; i += 3)
            {
                if (boardState[i] != null && boardState[i] == boardState[i + 1] && boardState[i + 1] == boardState[i + 2])
                    return boardState[i];
            }
            return null;
        }

        // Checks each column to see if there is a winner.
        private string CheckColumns()
        {
            for (int i = 0; i < 3; i++)
            {
                if (boardState[i] != null && boardState[i] == boardState[i + 3] && boardState[i + 3] == boardState[i + 6])
                    return boardState[i];
            }
            return null;
        }

        // Checks the diagonals to see if there is a winner.
        private string CheckDiagonals()
        {
            if ((boardState[0] != null && boardState[0] == boardState[4] && boardState[4] == boardState[8]) ||
                (boardState[2] != null && boardState[2] == boardState[4] && boardState[4] == boardState[6]))
                return boardState[4];

            return null;
        }
    }
}
