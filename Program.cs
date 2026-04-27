using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Calculator_Proj_App
{
    public partial class Application : Form
    {

        List<string> history = new List<string>();
        int cursorIndex = 0;

        public Application()
        {
            InitializeComponent();

            resultBox.Focus();
            resultBox.SelectionStart = resultBox.Text.Length;
        }

        private void UpdateDisplay()
        {
            string cleanText = resultBox.Text.Replace("|", "");

            cursorIndex = Math.Max(0, Math.Min(cursorIndex, cleanText.Length));

            resultBox.Text = cleanText.Insert(cursorIndex, "|");
        }


        private void AppendToCalculateString(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            string cleanText = resultBox.Text.Replace("|", "");

            cursorIndex = Math.Max(0, Math.Min(cursorIndex, cleanText.Length));

            cleanText = cleanText.Insert(cursorIndex, btn.Text);
            cursorIndex += btn.Text.Length;

            resultBox.Text = cleanText;
            UpdateDisplay();
        }
        


        private void ClearEntry(object sender, EventArgs e)
        {
            resultBox.Text = "";
            cursorIndex = 0;
            UpdateDisplay();
        }


        private void MoveLeft(object sender, EventArgs e)
        {
            cursorIndex--;
            UpdateDisplay();
        }


        private void MoveRight(object sender, EventArgs e)
        {
            cursorIndex++;
            UpdateDisplay();
        }


        private void ShowHistory(object sender, EventArgs e)
        {
            if (history.Count == 0)
            {
                MessageBox.Show("No history yet.");
                return;
            }

            string historyText = string.Join("\n", history);
            MessageBox.Show(historyText, "History");
        }


        private void sqrtBtn_Click(object sender, EventArgs e)
        {
            string cleanText = resultBox.Text.Replace("|", "");

            cleanText = cleanText.Insert(cursorIndex, "√");
            cursorIndex++;

            resultBox.Text = cleanText;
            UpdateDisplay();
        }


        private void leftBtn_Click(object sender, EventArgs e)
        {
            MoveLeft(sender, e);
            resultBox.Focus();
        }


        private void rightBtn_Click(object sender, EventArgs e)
        {
            MoveRight(sender, e);
            resultBox.Focus();
        }


        private void historyBtn_Click(object sender, EventArgs e)
        {
            ShowHistory(sender, e);
        }


        private void EvaluateCalculation(object sender, EventArgs e)
        {
            string expression = resultBox.Text.Replace("|", "");

            try
            {
                double evaluatedResult;


                if (expression.StartsWith("√"))
                {
                    double num = Convert.ToDouble(expression.Substring(1));
                    evaluatedResult = Math.Sqrt(num);
                }

                else if (expression.Contains("^"))
                {
                    string[] parts = expression.Split('^');

                    double baseNum = Convert.ToDouble(parts[0]);
                    double exponent = Convert.ToDouble(parts[1]);

                    evaluatedResult = Math.Pow(baseNum, exponent);
                }
                else
                {
                    var result = new DataTable();
                    evaluatedResult = Convert.ToDouble(result.Compute(expression, null));
                }


                if (double.IsInfinity(evaluatedResult) || double.IsNaN(evaluatedResult))
                {
                    MessageBox.Show("Invalid calculation.", "Error");
                    resultBox.Text = "";
                    return;
                }


                string fullExpression = expression + " = " + evaluatedResult;
                history.Insert(0, fullExpression);

                if (history.Count > 5)
                {
                    history.RemoveAt(5);
                }

                resultBox.Text = evaluatedResult.ToString();
                cursorIndex = resultBox.Text.Length;
                UpdateDisplay();
                resultBox.Focus();
            }
            catch
            {
                MessageBox.Show("Invalid expression.", "Error");
                resultBox.Text = "";
                resultBox.Focus();
            }
        }
    }
}
