using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace kolmrakendustforms
{
    public class MatchingGameForm : Form
    {
        TableLayoutPanel table;
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        Label firstClicked = null, secondClicked = null;
        Random random = new Random();
        bool allowClick = true;

        public MatchingGameForm()
        {
            Text = "Matching Game";
            Width = 800;
            Height = 500;
            StartPosition = FormStartPosition.CenterScreen;

            table = new TableLayoutPanel()
            {
                RowCount = 4,
                ColumnCount = 4,
                Dock = DockStyle.Fill,
                BackColor = Color.White,
            };

            for (int i = 0; i < 4; i++)
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));

            for (int j = 0; j < 4; j++)
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            AddCardsToTable();

            Controls.Add(table);
        }

        private void AddCardsToTable()
        {
            table.Controls.Clear();

            foreach (var icon in icons.OrderBy(x => random.Next()))
            {
                Label card = new Label()
                {
                    Tag = icon,
                    Font = new Font("Times New Roman", 48, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Text = ""
                };
                card.Click += Card_Click;
                table.Controls.Add(card);
            }
        }

        private void Card_Click(object sender, EventArgs e)
        {
            if (!allowClick) return;

            Label clicked = sender as Label;
            if (clicked == null) return;
            if (clicked.Text != "") return;

            clicked.Text = clicked.Tag.ToString();

            if (firstClicked == null)
            {
                firstClicked = clicked;
                return;
            }

            secondClicked = clicked;

            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;

                if (table.Controls.Cast<Label>().All(c => c.Text != ""))
                    MessageBox.Show("Palju õnne! Leidsite kõik paarid!");

                return;
            }

            allowClick = false;
            Timer timer = new Timer { Interval = 700 };
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                firstClicked.Text = "";
                secondClicked.Text = "";
                firstClicked = null;
                secondClicked = null;
                allowClick = true;
            };
            timer.Start();
        }
    }
}
