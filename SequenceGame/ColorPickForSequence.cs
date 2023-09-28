using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameSetupForm
{

    public partial class ColorPickForSequence : Form
    {
        public Color SelectedColor { get; private set; }
        public ColorPickForSequence()
        {
            InitializeComponent();
        }

        private void ColorPickForSequence_Load(object sender, EventArgs e)
        {
            assignColorsToButtons();
        }

        private void assignColorsToButtons()
        {
            int colorVal = 1;
            for (int i = 1; i <= 8; i++)
            {
                Button button = (Button)this.Controls["button" + i];
                if(button != null)
                {
                    EColorEnum colorEnum = (EColorEnum)colorVal;
                    button.BackColor = colorEnum.ToColor();
                    colorVal++;
                }
            }
        }

        private void colorButtonClicked(object sender, EventArgs e)
        {
            SelectedColor = (sender as Button).BackColor;
            DialogResult = DialogResult.OK;
        }
    }
}
