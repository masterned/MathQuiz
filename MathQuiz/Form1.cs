using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace MathQuiz
{
    public partial class Form1 : Form
    {
        Random randomizer = new Random();

        int addend1;
        int addend2;

        int minuend;
        int subtrahend;

        int multiplicand;
        int multiplier;

        int dividend;
        int divisor;

        int timeLeft;

        /// <summary>
        /// Start the quiz by filling all of the problems
        /// and starting the timer.
        /// </summary>
        public void StartTheQuiz()
        {
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();
            sum.Value = 0;

            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        /// <summary>
        /// Check the answer to see if the user got everything right.
        /// </summary>
        /// <returns>True if the answer's correct, false otherwise.</returns>
        public bool CheckTheAnswer()
        {
            return (addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft == 5)
            {
                timeLabel.BackColor = Color.Red;
            }
            if (CheckTheAnswer())
            {
                timer1.Stop();
                MessageBox.Show("You got all the questions right!", "Congratulations!");
                startButton.Enabled = true;
                timeLabel.BackColor = DefaultBackColor;
            }
            else if (timeLeft > 0)
            {
                timeLeft -= 1;
                timeLabel.Text = $"{timeLeft} seconds";
            }
            else
            {
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
                timeLabel.BackColor = DefaultBackColor;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Casting the generic sender object to a NumericUpDown
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        private void sum_ValueChanged(object sender, EventArgs e)
        {
            if (sender is NumericUpDown && (addend1 + addend2 == sum.Value))
                SystemSounds.Exclamation.Play();
        }

        private void difference_ValueChanged(object sender, EventArgs e)
        {
            if (sender is NumericUpDown && (minuend - subtrahend == difference.Value))
                SystemSounds.Exclamation.Play();
        }

        private void product_ValueChanged(object sender, EventArgs e)
        {
            if (sender is NumericUpDown && (multiplicand * multiplier == product.Value))
                SystemSounds.Exclamation.Play();
        }

        private void quotient_ValueChanged(object sender, EventArgs e)
        {
            if (sender is NumericUpDown && (dividend / divisor == quotient.Value))
                SystemSounds.Exclamation.Play();
        }
    }
}
