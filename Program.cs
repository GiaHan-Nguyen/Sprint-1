using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Calculator_Proj_App
{
    public partial class Application : Form
    {
        
        List<string> history = new List<string>();

        public Application()
        {
            InitializeComponent();

            resultBox.Focus(); 
            resultBox.SelectionStart = resultBox.Text.Length;
        }

        
        private void AppendToCalculateString(object sender, EventArgs e)
        {
            Button invokedBtn = sender as Button;

            if (invokedBtn != null)
            {
                int cursorPos = resultBox.SelectionStart;

                resultBox.Text = resultBox.Text.Insert(cursorPos, invokedBtn.Text);
                resultBox.SelectionStart = cursorPos + invokedBtn.Text.Length;

                resultBox.Focus();
            }
        }

        
        private void ClearEntry(object sender, EventArgs e)
        {
            resultBox.Text = string.Empty;
        }

        
        private void MoveLeft(object sender, EventArgs e)
        {
            if (resultBox.SelectionStart > 0)
            {
                resultBox.SelectionStart--;
            }

            resultBox.SelectionLength = 0;
            resultBox.Focus();
        }

        
        private void MoveRight(object sender, EventArgs e)
        {
            if (resultBox.SelectionStart < resultBox.Text.Length)
            {
                resultBox.SelectionStart++;
            }

            resultBox.SelectionLength = 0; 
            resultBox.Focus();
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
            int cursorPos = resultBox.SelectionStart;

            resultBox.Text = resultBox.Text.Insert(cursorPos, "√");
            resultBox.SelectionStart = cursorPos + 1;

            resultBox.Focus();
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
            string expression = resultBox.Text;

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

