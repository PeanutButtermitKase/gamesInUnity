using System;
using System.Drawing;
using System.Windows.Forms;

namespace AsteroidsGame
{
    public partial class MainForm : Form
    {
        private const int FormWidth  = 800;
        private const int FormHeight = 600;

        private Timer gameTimer;
        private SpaceShip SpaceShip;
        private Asteroid[] asteroids;

        public MainForm()
        {
            initAsset();
            init();
        }

        private void init()
        {
            Width = FormWidth;
            Height = FormHeight;
            DoubleBuffered = true;

            SpaceShip = new SpaceShip(FormWidth, FormHeight);
            asteroids = new Asteroid[3];
            for (int i = 0; i < asteroids.Length; i++)
                asteroids[i] = new Asteroid(FormWidth, FormHeight);

            gameTimer = new Timer();
            gameTimer.Interval = 20; // frames per second
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            SpaceShip.Update();
            foreach (Asteroid asteroid in asteroids)
                asteroid.Update();

            CheckCollisions();
            Invalidate(); // Refresh the screen
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            SpaceShip.Draw(g);
            foreach (Asteroid asteroid in asteroids)
                asteroid.Draw(g);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                SpaceShip.RotateLeft();
            else if (e.KeyCode == Keys.Right)
                SpaceShip.RotateRight();
            else if (e.KeyCode == Keys.Up)
                SpaceShip.Accelerate();
            else if (e.KeyCode == Keys.Space)
                SpaceShip.Shoot();
        }

        private void CheckCollisions()
        {
            foreach (Asteroid asteroid in asteroids)
            {
                if (SpaceShip.Bounds.IntersectsWith(asteroid.Bounds))
                {
                    gameTimer.Stop();
                    MessageBox.Show("Game Over!");
                    Close();
                }
            }
        }
    }

    public class SpaceShip
    {
        private const int SpaceShipSize = 20;
        private const int SpaceShipAcceleration = 1;
        private const int SpaceShipRotationSpeed = 5;
        private const int MaxSpeed = 10;
        private const int BulletSpeed = 10;

        private int formWidth;
        private int formHeight;

        private PointF position;
        private PointF velocity;
        private float angle;

        public RectangleF Bounds => new RectangleF(position, new SizeF(SpaceShipSize, SpaceShipSize));

        public SpaceShip(int formWidth, int formHeight)
        {
            this.formWidth = formWidth;
            this.formHeight = formHeight;

            position = new PointF(formWidth / 2 - SpaceShipSize / 2, formHeight / 2 - SpaceShipSize / 2);
            velocity = new PointF(0, 0);
            angle = 0;
        }

        public void Update()
        {
            position.X += velocity.X;
            position.Y += velocity.Y;

            WrapAroundScreen();

            // Apply drag to slow down the SpaceShip
            velocity.X *= 0.99f;
            velocity.Y *= 0.99f;
        }

        private void WrapAroundScreen()
        {
            if (position.X < 0)
                position.X = formWidth;
            else if (position.X > formWidth)
                position.X = 0;

            if (position.Y < 0)
                position.Y = formHeight;
            else if (position.Y > formHeight)
                position.Y = 0;
        }

        public void RotateLeft()
        {
            angle -= SpaceShipRotationSpeed;
        }

        public void RotateRight()
        {
            angle += SpaceShipRotationSpeed;
        }

        public void Accelerate()
        {
            float angleInRadians = (float)(angle * Math.PI / 180.0);
            velocity.X += SpaceShipAcceleration * (float)Math.Sin(angleInRadians);
            velocity.Y -= SpaceShipAcceleration * (float)Math.Cos(angleInRadians);

            // Limit the speed
            velocity.X = Math.Max(-MaxSpeed, Math.Min(MaxSpeed, velocity.X));
            velocity.Y = Math.Max(-MaxSpeed, Math.Min(MaxSpeed, velocity.Y));
        }

        public void Shoot()
        {
            // Create a new bullet with the SpaceShip's position and velocity
            Bullet bullet = new Bullet(position, velocity, angle, BulletSpeed);
            // TODO: Add bullet to the game
        }

        public void Draw(Graphics g)
        {
            PointF[] points = new PointF[4];
            float angleInRadians = (float)(angle * Math.PI / 180.0);
            points[0] = new PointF(position.X + SpaceShipSize / 2 * (float)Math.Sin(angleInRadians), position.Y - SpaceShipSize / 2 * (float)Math.Cos(angleInRadians));
            points[1] = new PointF(position.X + SpaceShipSize / 2 * (float)Math.Sin(angleInRadians + Math.PI), position.Y - SpaceShipSize / 2 * (float)Math.Cos(angleInRadians + Math.PI));
            points[2] = new PointF(position.X - SpaceShipSize / 2 * (float)Math.Sin(angleInRadians), position.Y + SpaceShipSize / 2 * (float)Math.Cos(angleInRadians));
            points[3] = new PointF(position.X - SpaceShipSize / 2 * (float)Math.Sin(angleInRadians + Math.PI), position.Y + SpaceShipSize / 2 * (float)Math.Cos(angleInRadians + Math.PI));

            g.DrawPolygon(Pens.White, points);
        }
    }

    public class Asteroid
    {
        private const int AsteroidSize = 40;
        private const int AsteroidSpeed = 5;

        private int formWidth;
        private int formHeight;

        private PointF position;
        private PointF velocity;

        public RectangleF Bounds => new RectangleF(position, new SizeF(AsteroidSize, AsteroidSize));

        public Asteroid(int formWidth, int formHeight)
        {
            this.formWidth = formWidth;
            this.formHeight = formHeight;

            Random random = new Random();
            position = new PointF(random.Next(formWidth - AsteroidSize), random.Next(formHeight - AsteroidSize));
            velocity = new PointF((float)(random.NextDouble() - 0.5) * AsteroidSpeed, (float)(random.NextDouble() - 0.5) * AsteroidSpeed);
        }

        public void Update()
        {
            position.X += velocity.X;
            position.Y += velocity.Y;

            WrapAroundScreen();
        }

        private void WrapAroundScreen()
        {
            if (position.X < 0)
                position.X = formWidth;
            else if (position.X > formWidth)
                position.X = 0;

            if (position.Y < 0)
                position.Y = formHeight;
            else if (position.Y > formHeight)
                position.Y = 0;
        }

        public void Draw(Graphics g)
        {
            g.DrawEllipse(Pens.Red, position.X, position.Y, AsteroidSize, AsteroidSize);
        }
    }

public class Bullet
{
    private const int BulletSize = 5;

    private PointF position;
    private PointF velocity;
    private float angle;
    private int speed;

    public RectangleF Bounds => new RectangleF(position, new SizeF(BulletSize, BulletSize));

    public Bullet(PointF SpaceShipPosition, PointF SpaceShipVelocity, float SpaceShipAngle, int bulletSpeed)
    {
        position = new PointF(SpaceShipPosition.X, SpaceShipPosition.Y);
        velocity = new PointF(SpaceShipVelocity.X, SpaceShipVelocity.Y);
        angle = SpaceShipAngle;
        speed = bulletSpeed;

        // Move the bullet's initial position to the front of the SpaceShip
        float angleInRadians = (float)(angle * Math.PI / 180.0);
        position.X += 20 * (float)Math.Sin(angleInRadians);
        position.Y -= 20 * (float)Math.Cos(angleInRadians);

        // Add velocity in the direction the SpaceShip is facing
        velocity.X += speed * (float)Math.Sin(angleInRadians);
        velocity.Y -= speed * (float)Math.Cos(angleInRadians);
    }

    public void Update()
    {
        position.X += velocity.X;
        position.Y += velocity.Y;
    }

    public void Draw(Graphics g)
    {
        g.FillEllipse(Brushes.Yellow, position.X, position.Y, BulletSize, BulletSize);
    }
}