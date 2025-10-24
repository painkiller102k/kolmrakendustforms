using System;
using System.Windows.Forms;

namespace kolmrakendustforms
{
    public class MathQuizForm : Form
    {
        Label timeLabel;
        System.Windows.Forms.Timer timer;
        int timeLeft;

        Random random = new Random();
        int addLeft, addRight, subLeft, subRight, mulLeft, mulRight, divLeft, divRight;

        NumericUpDown addAns, subAns, mulAns, divAns;
        Button startButton;

        public MathQuizForm()
        {
            Text = "Math Quiz";
            Width = 400;
            Height = 300;
            StartPosition = FormStartPosition.CenterScreen;

            timeLabel = new Label() { Text = "Time Left", Top = 10, Left = 20, Width = 200 };
            startButton = new Button() { Text = "Start the quiz", Top = 200, Left = 100, Width = 200 };
            startButton.Click += StartButton_Click;

            addAns = CreateRow(50, "+", out Label addLabel);
            subAns = CreateRow(80, "-", out Label subLabel);
            mulAns = CreateRow(110, "×", out Label mulLabel);
            divAns = CreateRow(140, "÷", out Label divLabel);

            Controls.Add(timeLabel);
            Controls.Add(startButton);
            Controls.Add(addLabel);
            Controls.Add(subLabel);
            Controls.Add(mulLabel);
            Controls.Add(divLabel);
            Controls.Add(addAns);
            Controls.Add(subAns);
            Controls.Add(mulAns);
            Controls.Add(divAns);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }

        private NumericUpDown CreateRow(int top, string op, out Label label)
        {
            int leftNum = random.Next(10, 50);
            int rightNum = random.Next(1, 10);

            switch (op)
            {
                case "+": addLeft = leftNum; addRight = rightNum; break;
                case "-": subLeft = leftNum; subRight = rightNum; break;
                case "×": mulLeft = leftNum; mulRight = rightNum; break;
                case "÷":
                    divRight = rightNum;
                    divLeft = divRight * random.Next(1, 10);
                    leftNum = divLeft;
                    break;
            }

            label = new Label() { Text = $"{leftNum} {op} {rightNum} =", Top = top, Left = 20, Width = 100 };
            return new NumericUpDown() { Top = top, Left = 130, Width = 60 };
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            addAns = CreateRow(50, "+", out Label addLabel);
            subAns = CreateRow(80, "-", out Label subLabel);
            mulAns = CreateRow(110, "×", out Label mulLabel);
            divAns = CreateRow(140, "÷", out Label divLabel);

            Controls.Clear();
            Controls.Add(timeLabel);
            Controls.Add(startButton);
            Controls.Add(addLabel);
            Controls.Add(subLabel);
            Controls.Add(mulLabel);
            Controls.Add(divLabel);
            Controls.Add(addAns);
            Controls.Add(subAns);
            Controls.Add(mulAns);
            Controls.Add(divAns);

            timeLeft = 30;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                timeLabel.Text = $"Aeg on : {timeLeft} sec";
            }
            else
            {
                timer.Stop();
                CheckAnswers();
            }
        }

        private void CheckAnswers()
        {
            int correct = 0;
            string result = "";

            if (addAns.Value == addLeft + addRight)
            {
                correct++;
                result += $"Addition: Correct\n";
            }
            else
                result += $"Addition: Wrong ({addLeft}+{addRight}={addLeft + addRight}, your answer={addAns.Value})\n";

            if (subAns.Value == subLeft - subRight)
            {
                correct++;
                result += $"Subtraction: Correct\n";
            }
            else
                result += $"Subtraction: Wrong ({subLeft}-{subRight}={subLeft - subRight}, your answer={subAns.Value})\n";

            if (mulAns.Value == mulLeft * mulRight)
            {
                correct++;
                result += $"Multiplication: Correct\n";
            }
            else
                result += $"Multiplication: Wrong ({mulLeft}×{mulRight}={mulLeft * mulRight}, your answer={mulAns.Value})\n";

            if (divAns.Value == divLeft / divRight)
            {
                correct++;
                result += $"Division: Correct\n";
            }
            else
                result += $"Division: Wrong ({divLeft}÷{divRight}={divLeft / divRight}, your answer={divAns.Value})\n";

            result += $"\nTotal correct: {correct}/4";
            MessageBox.Show(result, "Quiz Result");
        }
    }
}
