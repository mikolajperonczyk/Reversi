using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Projekt_PO2_Reversi
{
    enum Colors : int
    {
        Bialy = 0,
        Czarny = 1,
        Pusty = 2,
        Dostepny = 3
    };
    public partial class Form1 : Form
    {
        readonly Gra gra;
        Thread Thread;

        public Form1()
        {
            InitializeComponent();

            bool Komp;
            DialogResult dialogResult = MessageBox.Show("Czy chcesz zagraæ z komputerem?", "Wybór Graczy", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
                Komp = true;
            else if (dialogResult == DialogResult.No)
                Komp = false;
            else Komp = false;

            int Dimension = 8; //Tylko wartoœci parzyste !!
            gra = new Gra(pictureBox1.Height, Dimension, Komp, pictureBox1, pictureBox2);


            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.Resize += new EventHandler(pictureBox1_Resize);
            pictureBox1_Resize(this, EventArgs.Empty); // Wywo³aj metodê Resize, aby ustawiæ pocz¹tkowy rozmiar i pozycjê

            pictureBox2.Anchor = AnchorStyles.None;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            this.Resize += new EventHandler(pictureBox2_Resize);
            pictureBox2_Resize(this, EventArgs.Empty); // Wywo³aj metodê Resize, aby ustawiæ pocz¹tkowy rozmiar i pozycjê




            pictureBox1.Invalidate();

            Thread = new Thread(new ThreadStart(gra.Gameplay));
            Thread.Start();


            //gra.Gameplay(Testy_Koloru);
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;
            int pictureBoxHeight = (int)(formHeight * 0.90);
            int pictureBoxWidth = pictureBoxHeight;


            pictureBox1.Size = new Size(pictureBoxWidth, pictureBoxHeight);
            pictureBox1.Location = new Point((formWidth - pictureBoxWidth) / 2, (formHeight - pictureBoxHeight) / 2);

            gra.HW = pictureBoxHeight;

            pictureBox1.Invalidate();
        }

        private void pictureBox2_Resize(object sender, EventArgs e)
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;
            int pictureBoxHeight = (int)(formHeight * 0.1);
            int pictureBoxWidth = pictureBoxHeight;


            pictureBox2.Size = new Size(pictureBoxWidth, pictureBoxHeight);
            pictureBox2.Location = new Point((formWidth - pictureBoxWidth) / 25, (formHeight - pictureBoxHeight) / 15);

            pictureBox2.Invalidate();
        }



        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            gra.ToHuman(gra.ClickToField(e));

        }

        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {
            gra.Clear(e.Graphics);
            gra.Draw(e.Graphics);
            foreach (Gracz gracz in gra.PLAYERS)
                if (gracz.Color == (int)Colors.Bialy)
                    textBox1.Text = gracz.Points.ToString();
                else
                    textBox2.Text = gracz.Points.ToString();

        }



        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            gra.ClearMove(e.Graphics);
            gra.DrawMove(e.Graphics, pictureBox2.Size.Height);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter stream;
            string serialized = gra.Serialize();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((stream = new StreamWriter(saveFileDialog1.OpenFile())) != null)
                {
                    stream.Write(serialized);
                    stream.Close();
                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void loadToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string json;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Read the contents of the file into a stream
                var fileStream = openFileDialog1.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    json = reader.ReadToEnd();
                    reader.Close();
                }
                

                Thread = null;

                gra.Deserialize(json);

                Thread = new Thread(new ThreadStart(gra.NewGameplay));
                Thread.Start();
            }
        }
    }

}

