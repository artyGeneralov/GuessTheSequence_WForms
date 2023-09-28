using System;
using System.Windows.Forms;

namespace GameSetupForm
{
    public partial class GameSetupForm : Form
    {
        public GameSetupForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            MainGameForm mainGameForm = new MainGameForm();
            mainGameForm.StartPosition = FormStartPosition.CenterScreen;
            MainGameController.SetChances(SetupButtonsController.Chances);
            mainGameForm.FormClosed += new FormClosedEventHandler(mainGameForm_FormClosed);
            mainGameForm.Show();
            this.Hide();
        }

        private void buttonChances_Click(object sender, EventArgs e)
        {
            SetupButtonsController.ChancesUpdate();
        }


        private void GameSetupForm_Load(object sender, EventArgs e)
        {
            SetupButtonsController.OnChancesUpdated += updateButtonChancesText;
            updateButtonChancesText();
        }

        private void updateButtonChancesText()
        {
            string prefix = buttonChances.Text.Split(':')[0];
            buttonChances.Text = $"{prefix}: {SetupButtonsController.Chances}";
        }

        private void GameSetupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetupButtonsController.OnChancesUpdated -= updateButtonChancesText;
        }

        private void mainGameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
