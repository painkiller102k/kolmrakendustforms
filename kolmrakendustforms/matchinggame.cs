using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace kolmrakendustforms
{
    public class MatchingGameForm : Form
    {
        TableLayoutPanel table = new() { Dock = DockStyle.Fill, BackColor = Color.LightSlateGray };
        Panel topPanel = new() { Height = 50, Dock = DockStyle.Top, BackColor = Color.LightGray };

        Button restartButton = new() { Text = "Restart", Left = 10, Width = 100, Top = 10 };
        Button iconSetButton = new() { Text = "Kasuta loomad 🐾", Left = 120, Width = 160, Top = 10 };
        Button difficultyButton = new() { Text = "Lihtne / Raske", Left = 290, Width = 140, Top = 10 };

        List<string> icons = new() { "!", "N", ",", "k", "b", "v", "w", "z", "x", "y", "m", "t", "u", "o", "p" };
        List<string> loomad = new() { "🐶", "🐱", "🐸", "🐻", "🦊", "🐭", "🐼", "🐰", "🦁", "🐯", "🐨", "🐵", "🐔", "🐧", "🐦" };

        List<string> currentIcons;
        Label firstClicked = null, secondClicked = null;
        Random random = new();
        bool allowClick = true;

        int rows = 4, columns = 4;

        public MatchingGameForm()
        {
            Text = "Matching Game";
            Width = 800;
            Height = 600;
            StartPosition = FormStartPosition.CenterScreen;

            currentIcons = icons;

            Controls.Add(table);
            Controls.Add(topPanel);
            topPanel.Controls.AddRange(new Control[] { restartButton, iconSetButton, difficultyButton });

            restartButton.Click += (s, e) => RestartGame();
            iconSetButton.Click += (s, e) => ToggleIconSet();
            difficultyButton.Click += (s, e) => ToggleDifficulty();

            CreateTable();
            AddCardsToTable();
        }

        void CreateTable()
        {
            table.Controls.Clear();
            table.RowStyles.Clear();
            table.ColumnStyles.Clear();

            table.RowCount = rows;
            table.ColumnCount = columns;

            for (int i = 0; i < rows; i++)
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rows));
            for (int j = 0; j < columns; j++)
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / columns));
        }

        void AddCardsToTable()
        {
            table.Controls.Clear();

            int totalCards = rows * columns;
            if (totalCards % 2 != 0)
            {
                // На случай, если кол-во карточек нечётное, уменьшаем на 1, чтобы была чётность
                totalCards--;
            }

            int pairs = totalCards / 2;
            var iconsToUse = currentIcons.Take(pairs).ToList();

            var cardIcons = iconsToUse.Concat(iconsToUse).OrderBy(_ => random.Next()).ToList();

            foreach (var icon in cardIcons)
            {
                var card = new Label
                {
                    Tag = icon,
                    Font = new Font("Segoe UI Emoji", 36, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    BackColor = Color.AliceBlue,
                    BorderStyle = BorderStyle.FixedSingle,
                    Text = ""
                };
                card.Click += Card_Click;
                table.Controls.Add(card);
            }

            // Если карточек меньше чем ячеек - добавим пустые панели, чтобы сетка не "ломалась"
            while (table.Controls.Count < rows * columns)
            {
                table.Controls.Add(new Panel { BackColor = Color.LightSlateGray });
            }
        }

        void Card_Click(object sender, EventArgs e)
        {
            if (!allowClick) return;
            var clicked = sender as Label;
            if (clicked == null || clicked.Text != "") return;

            clicked.Text = clicked.Tag.ToString();

            if (firstClicked == null)
            {
                firstClicked = clicked;
                return;
            }

            secondClicked = clicked;

            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked.BackColor = secondClicked.BackColor = Color.LightGreen;
                firstClicked = secondClicked = null;

                if (table.Controls.OfType<Label>().All(c => c.Text != ""))
                    MessageBox.Show("Palju õnne! Leidsite kõik paarid!");
            }
            else
            {
                allowClick = false;
                var timer = new Timer { Interval = 700 };
                timer.Tick += (s, args) =>
                {
                    timer.Stop();
                    firstClicked.Text = secondClicked.Text = "";
                    firstClicked = secondClicked = null;
                    allowClick = true;
                };
                timer.Start();
            }
        }

        void RestartGame()
        {
            firstClicked = secondClicked = null;
            allowClick = true;
            CreateTable();
            AddCardsToTable();
        }

        void ToggleIconSet()
        {
            if (currentIcons.SequenceEqual(icons))
            {
                currentIcons = loomad;
                iconSetButton.Text = "Kasuta sümbolid";
            }
            else
            {
                currentIcons = icons;
                iconSetButton.Text = "Kasuta loomad 🐾";
            }
            RestartGame();
        }

        void ToggleDifficulty()
        {
            if (rows == 4)
            {
                rows = 5;
                columns = 6;
            }
            else
            {
                rows = columns = 4;
            }
            RestartGame();
        }
    }
}
