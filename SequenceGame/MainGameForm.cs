using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GameSetupForm
{
    public partial class MainGameForm : Form
    {
        public MainGameForm()
        {
            InitializeComponent();
        }

        private void buttonGuessClick(object sender, EventArgs e)
        {
            using (ColorPickForSequence colorPickForm = new ColorPickForSequence())
            {
                Button senderButton = sender as Button;
                colorPickForm.StartPosition = FormStartPosition.Manual;
                Point location = senderButton.PointToScreen(Point.Empty);
                colorPickForm.Location = new Point(location.X, location.Y + senderButton.Height);
                if (colorPickForm.ShowDialog() == DialogResult.OK)
                {
                    if (MainGameController.currentGuess.Contains((int)colorPickForm.SelectedColor.ToColorEnum()) == false)
                    {
                        ((Button)sender).BackColor = colorPickForm.SelectedColor;
                        int currentGuessPosition = int.Parse((sender as Button).Name.Split('_')[1]);
                        MainGameController.AddToCurrentGuess((int)colorPickForm.SelectedColor.ToColorEnum(), currentGuessPosition);
                        if (MainGameController.currentGuessFull())
                        {
                            toggleSubmitInRow(MainGameController.currentRow, true);
                        }
                    }
                }
            }
        }

        private void buttonSubmitClick(object sender, EventArgs e)
        {
            EGuessResults[] result = MainGameController.EvaluateGuess();
            colorResultButtons(MainGameController.currentRow, result);

            if (MainGameController.CheckWin() == true)
            {
                toggleGuessesInRow(MainGameController.currentRow);
                toggleSubmitInRow(MainGameController.currentRow, false);
                colorSolutionButtons();
            }
            else if(MainGameController.CheckWin() == false && MainGameController.currentRow != MainGameController.MaxChances)
            {
                toggleGuessesInRow(MainGameController.currentRow);
                toggleSubmitInRow(MainGameController.currentRow, false);
                MainGameController.nextRow();
                toggleGuessesInRow(MainGameController.currentRow);
                toggleSubmitInRow(MainGameController.currentRow, true);
            }
            else
            {
                toggleGuessesInRow(MainGameController.currentRow);
                toggleSubmitInRow(MainGameController.currentRow, false);
            }
            MainGameController.ClearCurrentGuess();
        }

        private void colorSolutionButtons()
        {
            for(int i = 0; i < 4; i++)
            {
                string btnSol = $"buttonSolution{i+1}";
                Button btnSolController = (Button)this.Controls[btnSol];
                EColorEnum color = (EColorEnum)MainGameController.generatedCombination[i];
                btnSolController.BackColor = color.ToColor();
            }
        }

        private void colorResultButtons(int row, EGuessResults[] results)
        {
            for(int i = 0; i < 4; i++)
            {
                string resButtonName = $"buttonResult{row}_{i+1}";
                Button resButton = (Button)this.Controls.Find(resButtonName, true)[0];
                switch (results[i]){
                    case EGuessResults.CorrectRightPos:
                        resButton.BackColor = Color.Black;
                        break;
                    case EGuessResults.CorrectWrongPos:
                        resButton.BackColor = Color.Yellow;
                        break;
                }
            }
        }

        private void toggleGuessesInRow(int row)
        {
            string panelName = $"row{row}";
            Control[] rowToEnable = this.Controls.Find(panelName, true);
            if(rowToEnable.Length > 0)
            {
                Panel panel = rowToEnable[0] as Panel;
                if(panel != null)
                {
                    foreach(Control control in panel.Controls)
                    {
                        if (control.Name.Contains("Guess"))
                        {
                            control.Enabled = !control.Enabled;
                        }
                    }
                }
            }
        }

        private void toggleSubmitInRow(int row, bool toggle)
        {
            string panelName = $"row{row}";
            Control[] rowToEnable = this.Controls.Find(panelName, true);
            if (rowToEnable.Length > 0)
            {
                Panel panel = rowToEnable[0] as Panel;
                if (panel != null)
                {
                    foreach (Control control in panel.Controls)
                    {
                        if (control.Name.Contains("Submit"))
                        {
                            control.Enabled = toggle;
                        }
                    }
                }
            }
        }

        private void MainGameForm_Load(object sender, EventArgs e)
        {
            generateRows();
            adjustWindow();
            MainGameController.GenerateSequence();
            toggleGuessesInRow(1);
        }

        private void adjustWindow()
        {
            int rowHeight = row1.Height * SetupButtonsController.Chances;
            int gapHeight = 15 * (SetupButtonsController.Chances - 1);
            int topHeight = 135;
            this.Size = (new Size(this.Width, rowHeight + gapHeight + topHeight));
        }

        private void generateRows()
        {
            for(int i = 2; i <= MainGameController.MaxChances; i++)
            {
                Panel newPanel = cloneButtonsPanel(i);
                newPanel.Location = new Point(row1.Location.X,
                                              row1.Location.Y + (newPanel.Height * (i-1))+15*(i-1));
                newPanel.Name = $"row{i}";
                this.Controls.Add(newPanel);
            }
        }

        private Panel cloneButtonsPanel(int rowNum)
        {
            Panel original = row1;
            Panel newPanel = new Panel();
            newPanel.Size = original.Size;
            foreach(Control button in original.Controls)
            {
                Button newButton = (Button)Activator.CreateInstance(button.GetType());
                newButton.Location = button.Location;
                newButton.BackColor = button.BackColor;
                newButton.Enabled = button.Enabled;
                newButton.Height = button.Height;
                newButton.Width = button.Width;
                newButton.Text = button.Text;
                newButton.Parent = newPanel;
                newButton.Font = button.Font;
                newButton.UseVisualStyleBackColor = (button as Button).UseVisualStyleBackColor;
                string btnId = button.Name.Split('_')[1];
                string newName = button.Name.Split('1')[0] + $"{rowNum}_{btnId}";
                newButton.Name = newName;
                if (newButton.Name.Contains("Guess"))
                {
                    newButton.Click += buttonGuessClick;
                }
                else if (newButton.Name.Contains("Submit"))
                {
                    newButton.Click += buttonSubmitClick;
                }
                newPanel.Controls.Add(newButton);
            }
            return newPanel;
        }
    }
}
