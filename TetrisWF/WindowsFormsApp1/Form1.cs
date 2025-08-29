using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Ttrs

{
    public partial class Form1 : Form
    {
        private readonly GameEngine engine;
        private readonly Renderer renderer;
        private readonly MainMenu mainMenu;
        private readonly GameOverScreen gameOverScreen;
        public enum GameState
        {
            MainMenu,
            Playing,
            GameOver
        }
        private GameState state;

        public Form1()
        {
            InitializeComponent();
            engine = new GameEngine();
            renderer = new Renderer(engine);
            mainMenu = new MainMenu();
            gameOverScreen = new GameOverScreen();
            
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.KeyDown += MoveFunction;
            this.FormBorderStyle = FormBorderStyle.Sizable;

            state = GameState.MainMenu;

            var timer = new Timer();
            timer.Interval = 500;
            timer.Tick += (s, e) =>
            {
                if (state == GameState.Playing && !engine.GameOver)
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
            if (state == GameState.MainMenu)
            {
                mainMenu.Draw(e.Graphics, this.ClientSize);
                this.Invalidate();
            }
            else if (state == GameState.Playing)
            {
                renderer.Draw(e.Graphics, this.ClientSize);
                if (engine.GameOver)
                {
                    state = GameState.GameOver;
                    this.Invalidate();
                }
            }
            else if (state == GameState.GameOver)
            {
                gameOverScreen.Draw(e.Graphics, this.ClientSize);
                this.Invalidate();
            }
        }
        private void MoveFunction(object sender, KeyEventArgs e)

        {
            if (state == GameState.MainMenu)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    state = GameState.Playing;
                    engine.EngineReset(); 
                    renderer.Cheating = mainMenu.Cheating;
                    this.Invalidate();
                    return;
                }
                if(e.KeyCode == Keys.C)
                {
                    mainMenu.ToggleCheating();
                    this.Invalidate();
                    return;
                }
            }

            if (state == GameState.GameOver && e.KeyCode == Keys.Enter)
            {
                state = GameState.MainMenu;
                engine.EngineReset();
                this.Invalidate();
                return;
            }
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
                case Keys.Space:
                    engine.DropBlok();
                    break;
            }
            this.Invalidate();
        }

    }
}
