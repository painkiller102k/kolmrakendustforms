using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace kolmrakendustforms
{
    public class PictureViewerForm : Form
    {
        PictureBox pictureBox;
        Button showButton, clearButton, bgButton, closeButton, loadButton, prevButton, nextButton;
        CheckBox stretchCheckBox;

        List<Image> images = new List<Image>();
        int currentIndex = -1;

        public PictureViewerForm()
        {
            Text = "Picture Viewer";
            Width = 800;
            Height = 600;

            pictureBox = new PictureBox()
            {
                BorderStyle = BorderStyle.Fixed3D,
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            clearButton = new Button() { Text = "Clear the picture" };
            bgButton = new Button() { Text = "Set the background color" };
            closeButton = new Button() { Text = "Close" };
            loadButton = new Button() { Text = "Load Images" };
            prevButton = new Button() { Text = "< Previous" };
            nextButton = new Button() { Text = "Next >" };
            stretchCheckBox = new CheckBox() { Text = "Stretch" };

            FlowLayoutPanel panel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Bottom,
                Height = 40
            };

            panel.Controls.Add(stretchCheckBox);
            panel.Controls.Add(loadButton);
            panel.Controls.Add(prevButton);
            panel.Controls.Add(nextButton);
            panel.Controls.Add(showButton);
            panel.Controls.Add(bgButton);
            panel.Controls.Add(clearButton);
            panel.Controls.Add(closeButton);

            Controls.Add(pictureBox);
            Controls.Add(panel);

            loadButton.Click += LoadImagesButton_Click;
            prevButton.Click += PrevButton_Click;
            nextButton.Click += NextButton_Click;
            clearButton.Click += (s, e) =>
            {
                pictureBox.Image = null;
                images.Clear();
                currentIndex = -1;
            };

            bgButton.Click += BgButton_Click;
            stretchCheckBox.CheckedChanged += (s, e) =>
                pictureBox.SizeMode = stretchCheckBox.Checked ? PictureBoxSizeMode.StretchImage : PictureBoxSizeMode.Zoom;
            closeButton.Click += (s, e) => this.Close();
        }
        private void LoadImagesButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Image Files|*.png";
                dlg.Multiselect = true;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    images.Clear();
                    foreach (var file in dlg.FileNames)
                    {
                        images.Add(Image.FromFile(file));
                    }
                    if (images.Count > 0)
                    {
                        currentIndex = 0;
                        pictureBox.Image = images[currentIndex];
                    }
                }
            }
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            if (images.Count == 0) return;
            currentIndex--;
            if (currentIndex < 0) currentIndex = images.Count - 1;
            pictureBox.Image = images[currentIndex];
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (images.Count == 0) return;
            currentIndex++;
            if (currentIndex >= images.Count) currentIndex = 0;
            pictureBox.Image = images[currentIndex];
        }


        private void BgButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    pictureBox.BackColor = dlg.Color;
            }
        }
    }
}
