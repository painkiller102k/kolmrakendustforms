using kolmrakendustforms;
using System;
using System.Windows.Forms;

namespace kolmrakendustforms
{
    public class MainForm : Form
    {
        public MainForm()
        {
            Text = "Peamenüü";
            Width = 400;
            Height = 300;
            StartPosition = FormStartPosition.CenterScreen;

            Button btnViewer = new Button()
            {
                Text = "Picture Viewer",
                Top = 50,
                Left = 100,
                Width = 200
            };

            Button btnQuiz = new Button()
            {
                Text = "Math Quiz",
                Top = 100,
                Left = 100,
                Width = 200
            };

            Button btnMatch = new Button()
            {
                Text = "Matching Game",
                Top = 150,
                Left = 100,
                Width = 200
            };

            btnViewer.Click += (s, e) => new PictureViewerForm().ShowDialog();
            btnQuiz.Click += (s, e) => new MathQuizForm().ShowDialog();
            btnMatch.Click += (s, e) => new MatchingGameForm().ShowDialog();
 

            Controls.Add(btnViewer);
            Controls.Add(btnQuiz);
            Controls.Add(btnMatch);
        }
    }
}
