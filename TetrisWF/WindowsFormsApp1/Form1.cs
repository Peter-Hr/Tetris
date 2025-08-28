using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ttrs

{
    public partial class Form1 : Form
    {
        private readonly GameEngine engine;
        private readonly Renderer renderer;

        public Form1()
        {
            InitializeComponent();
            engine = new GameEngine();
            renderer = new Renderer(engine, 30);
            
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.KeyDown += MoveFunction;
            

            var timer = new Timer();
            timer.Interval = 500;
            timer.Tick += (s, e) =>
            {
                if (!engine.GameOver)
                {
                    engine.MoveBlokDown();
                    this.Invalidate();
                }
            };
            timer.Start();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            renderer.Draw(e.Graphics);
        }
        private void MoveFunction(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    engine.RotateCWIfPossible();
                    break;
                case Keys.Left:
                    engine.MoveLeftIfPossible();
                    break;
                case Keys.Right:
                    engine.MoveRightIfPossible();
                    break;
                case Keys.Up:
                    engine.RotateCCWIfPossible();
                    break;
            }
        }

    }
}
