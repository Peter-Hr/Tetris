using System.Windows.Forms;

namespace Ttrs

{
    public partial class Form1 : Form
    {
        private readonly GameEngine engine;
        private readonly Renderer renderer;
        private readonly MainMenu mainMenu;
        private readonly GameOverScreen gameOverScreen;
        private readonly Leaderboard leaderboard;
        public enum GameState
        {
            MainMenu,
            Playing,
            GameOver,
            Leaderboard
        }
        private GameState state;
        private Timer gameTimer;

        public Form1()
        {
            InitializeComponent();
            engine = new GameEngine();
            renderer = new Renderer(engine);
            mainMenu = new MainMenu();
            gameOverScreen = new GameOverScreen();
            leaderboard = new Leaderboard();

            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.KeyDown += MoveFunction;
            this.FormBorderStyle = FormBorderStyle.Sizable;

            state = GameState.MainMenu;

            gameTimer = new Timer();
            gameTimer.Interval = 500;
            gameTimer.Tick += (s, e) =>
            {
                if (state == GameState.Playing && !engine.GameOver)
                {
                    engine.MoveBlokDown();
                    this.Invalidate();
                    AdjustSpeed();
                }
            };
            gameTimer.Start();
        }
        private void AdjustSpeed()
        {
            int score = engine.GetTtlScore();

            int newInterval = 500 - (score / 100) * 50;

            if (newInterval < 100)
                newInterval = 100;

            if (gameTimer.Interval != newInterval)
                gameTimer.Interval = newInterval;
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
                gameOverScreen.Draw(e.Graphics, this.ClientSize, engine.GetTtlScore().ToString());
                leaderboard.AddScore(mainMenu.PlayerName, engine.GetTtlScore());
                this.Invalidate();
            }
            else if (state == GameState.Leaderboard)
            {
                leaderboard.Draw(e.Graphics, this.ClientSize);
                this.Invalidate();
            }
        }
        private void MoveFunction(object sender, KeyEventArgs e)

        {
            if (state == GameState.MainMenu)
            {
                if (!mainMenu.enteringName)
                {
                    if(e.KeyCode == Keys.Enter)
                    {
                        state = GameState.Playing;
                        engine.EngineReset();
                        renderer.Cheating = mainMenu.Cheating;
                        this.Invalidate();
                        return;

                    }
                    if (e.KeyCode == Keys.L)
                    {
                        state = GameState.Leaderboard;
                        this.Invalidate();
                        return;
                    }
                }
                mainMenu.HandleKey(e.KeyCode);
                this.Invalidate();
                return;
            }

            if (state == GameState.GameOver && e.KeyCode == Keys.Enter)
            {
                state = GameState.MainMenu;
                engine.EngineReset();
                this.Invalidate();
                return;
            }
            if (state == GameState.Leaderboard && e.KeyCode == Keys.Escape)
            {
                state = GameState.MainMenu;
                this.Invalidate();
                return;
            }
            if (state == GameState.Playing)
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
                    case Keys.Space:
                        engine.DropBlok();
                        break;
                }
            }
            this.Invalidate();
        }

    }
}
