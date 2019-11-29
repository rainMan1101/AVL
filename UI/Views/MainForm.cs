using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AVL.UI.Views
{
    public partial class MainForm : Form, IView
    {
        //  Область для рисования дерева
        private PictureBox _pictureBox;

        private Decimal _value;

        public MainForm()
        {
            InitializeComponent();
            _pictureBox = pictureBox1;

            /*            Событие изименения режима             */
            radioButton3.CheckedChanged += (sender, e) => ModeChanged?.Invoke(sender, e);
            radioButton4.CheckedChanged += (sender, e) => ModeChanged?.Invoke(sender, e);
            radioButton5.CheckedChanged += (sender, e) => ModeChanged?.Invoke(sender, e);
            checkBox1.CheckedChanged += (sender, e) => ModeChanged?.Invoke(sender, e);
            checkBox2.CheckedChanged += (sender, e) => ModeChanged?.Invoke(sender, e);

            button2.Click += (sender, args) =>
            {
                if (Decimal.TryParse(textBox6.Text, out _value)) AddEvent?.Invoke(sender, args);
            };

            button6.Click += (sender, args) =>
            {
                if (Decimal.TryParse(textBox6.Text, out _value)) DeleteEvent?.Invoke(sender, args);
            };

            button5.Click += (sender, args) =>
            {
                if (Decimal.TryParse(textBox6.Text, out _value)) FindEvent?.Invoke(sender, args);
            };

            button3.Click += (sender, args) => FillingAddEvent?.Invoke(sender, args);

            button7.Click += (sender, args) => Traversal?.Invoke(sender, args);
        }


        public EDrawNodeMode DrawNodeMode
        {
            get
            {
                if (radioButton3.Checked) return EDrawNodeMode.CircleMode;
                if (radioButton4.Checked) return EDrawNodeMode.SquareMode;
                return EDrawNodeMode.NotNodeMode;
            }
        }

        public bool DrawValues => checkBox2.Checked;  //DrawBinaryCodes

        public bool DrawHeight => checkBox1.Checked;  //DrawProbability

        public PictureBox DrawWindow => _pictureBox;

        public Decimal Value => _value;

        public string TraversalList { set => textBox2.Text = value; }
        public int CountSteps { set => textBox1.Text = "" + value; }

        public event EventHandler AddEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler FindEvent;
        public event EventHandler FillingAddEvent;
        public event EventHandler Traversal;


        public event EventHandler ModeChanged;

        public event EventHandler DrawClick;

        public event EventHandler FullScreenModeClick;
        

        //  Открытие окна увеличения дерева
        private void button4_Click(object sender, EventArgs e)
        {
            FullScreenForm fullScreenForm = new FullScreenForm();

            fullScreenForm.FormClosed += FullScreenFormClosed;
            fullScreenForm.WindowState = FormWindowState.Maximized;
            _pictureBox = fullScreenForm.DrawWindow;
            fullScreenForm.Show();

            // For Redraw window
            DrawClick?.Invoke(sender, e);
            FullScreenModeClick?.Invoke(sender, e);
        }


        // Закрытие окна увеличения дерева   
        private void FullScreenFormClosed(object sender, EventArgs e)
        {
            _pictureBox = pictureBox1;
        }
    }
}
