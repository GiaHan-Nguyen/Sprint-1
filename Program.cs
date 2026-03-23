using System;
using System.Data;

namespace Calculator_Proj_App
{
    public partial class Application : Form
    {
        public Application()
        {
            InitializeComponent();
        }

        private void AppendToCalculateString(object sender, EventArgs e)
        {
            Button invokedBtn = sender as Button;
            if (invokedBtn != null)
            {
                resultBox.Text += invokedBtn.Text;
            }
        }

        private void ClearEntry(object sender, EventArgs e)
        {
            resultBox.Text = string.Empty;
        }

        private void EvaluateCalculation(object sender, EventArgs e)
        {
            string expression = resultBox.Text;
            var result = new DataTable();
            try
            {
                double evaluatedResult = Convert.ToDouble(result.Compute(expression, null));
                if (double.IsInfinity(evaluatedResult) || double.IsNaN(evaluatedResult))
                {
                    MessageBox.Show("Expression was evaluated to be undefined.", "Evaluation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    resultBox.Text = string.Empty;
                    return;
                }
                resultBox.Text = evaluatedResult.ToString();
            }
            catch (System.Data.SyntaxErrorException)
            {
                MessageBox.Show("Expression was not valid.", "Syntax Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                resultBox.Text = string.Empty;
            }
        }
    }
}
