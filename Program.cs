using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

double energy = 0;

// Energy Attributes
float maxAngularVelocity = 180f;
float friction = 0.75f;
float enginePower = 1f / 1000 / 1000 / 1000;


// Graphics Attributes
int fps = 40;
Font font = SystemFonts.CaptionFont;
StringFormat strFormat = new StringFormat();
strFormat.Alignment = StringAlignment.Center;
strFormat.LineAlignment = StringAlignment.Center;
string[] systems = new string[]
{
    "y", "z", "a", "f", "p", "n", "µ", "m", "", "k", "M", "G", "T", "P", "E", "Z", "Y"
};
string format(double number, string type)
{
    if (number == 0)
        return "0 " + type;
    int system = 8;
    while (number < 1.0)
    {
        number *= 1000;
        system--;
    }
    while (number > 1000.0)
    {
        number /= 1000;
        system++;
    }
    if (system < 0)
        return "0";
    if (system > 16)
        return "∞";
    return number.ToString("0.00") + " " + systems[system] + type;
}


// Main Object Implementation
Image main = null;
float mainsize = 600f;
float clickForce = 90f;
float angle = 0f;
float angularVelocity = 0f;

void mainObjectRotation(Graphics g, Bitmap bmp)
{
    float frameVelocityKeep = (float)Math.Pow(1f - friction, 1f / fps);
    angularVelocity *= frameVelocityKeep;
    if (angularVelocity < 1f)
        angularVelocity = 0;
    angle += angularVelocity / fps;
    
    RectangleF forceBar = new RectangleF(
        bmp.Width / 2 - mainsize / 2 + 20, 
        bmp.Height / 2 - mainsize / 2 - 30, 
        mainsize - 40, 20);
    LinearGradientBrush brush = new LinearGradientBrush(
        forceBar, Color.Yellow, Color.Red, LinearGradientMode.Horizontal);
    RectangleF currentForceBar = new RectangleF(
            forceBar.X,
            forceBar.Y,
            forceBar.Width * angularVelocity / maxAngularVelocity,
            forceBar.Height
        );
    g.FillRectangle(brush, currentForceBar);
    g.DrawRectangle(Pens.Black, forceBar.X, forceBar.Y, forceBar.Width, forceBar.Height);
}

void drawInfo(Graphics g, Bitmap bmp)
{
    RectangleF rect;
    var production = getProduction();
    var strProduction = format(production, "W");
    addEnergy(production / fps);
    var strEnergy = format(energy, "J");

    rect = new RectangleF(
        bmp.Width / 2 - mainsize / 2 + 20, 
        100, 
        mainsize - 40, 100);
    var subfont = new Font(FontFamily.Families[30], 30f);
    g.DrawString(strProduction, subfont, Brushes.Orange, rect, strFormat);

    rect = new RectangleF(
        bmp.Width / 2 - mainsize / 2 + 20, 
        30, 
        mainsize - 40, 100);
    var mainfont = new Font(FontFamily.Families[30], 60f);
    g.DrawString(strEnergy, mainfont, Brushes.Orange, rect, strFormat);
}

void drawEngine(Graphics g, Bitmap bmp)
{
    var center = new PointF(bmp.Width / 2, bmp.Height / 2f);
    g.TranslateTransform(center.X, center.Y);
    g.RotateTransform(angle);
    g.DrawImage(main, new RectangleF(-mainsize / 2, -mainsize / 2, mainsize, mainsize));
    g.ResetTransform();
}

void drawMainObject(Graphics g, Bitmap bmp)
{
    if (main == null)
        main = Image.FromFile("res/main.png");

    var mainRect = new Rectangle(
        (int)(bmp.Width / 2 - mainsize / 2 - 50),
        10, (int)(mainsize + 100), (int)(bmp.Height - 20));
    g.FillRectangle(Brushes.LightGray, mainRect);
    g.DrawRectangle(Pens.Black, mainRect);

    drawInfo(g, bmp);
    mainObjectRotation(g, bmp);
    drawEngine(g, bmp);
}

bool mainObjectTestClick(Point p, Bitmap bmp)
{
    var mainrect = new RectangleF(bmp.Width / 2 - mainsize / 2, 
        bmp.Height / 2 - mainsize / 2, mainsize, mainsize);
    return mainrect.Contains(p);
}

void mainObjectClick()
{
    angularVelocity += clickForce;
    if (angularVelocity > maxAngularVelocity)
        angularVelocity = maxAngularVelocity;
}


// Energy System
double getProduction()
{
    double engineProduction = angularVelocity / 360f * enginePower;
    return engineProduction;
}
void addEnergy(double gain)
{
    energy += gain;
}


// Shop Implementation
void drawShop(Graphics g, Bitmap bmp)
{

}

// Screen Implementation
void start(Action<Form> create)
{
    ApplicationConfiguration.Initialize();
    Form form = new Form();
    create(form);
    Application.Run(form);
}

start(form =>
{
    form.WindowState = FormWindowState.Maximized;
    form.FormBorderStyle = FormBorderStyle.None;

    PictureBox pb = new PictureBox();
    pb.Dock = DockStyle.Fill;
    form.Controls.Add(pb);

    Bitmap bmp = null;
    Graphics g = null;

    Timer tm = new Timer();
    tm.Interval = 1000 / fps;
    tm.Tick += delegate
    {
        g.Clear(Color.White);
        drawMainObject(g, bmp);
        drawShop(g, bmp);
        pb.Refresh();
    };

    form.Load += delegate
    {
        bmp = new Bitmap(pb.Width, pb.Height);
        g = Graphics.FromImage(bmp);
        g.Clear(Color.White);
        pb.Image = bmp;
        tm.Start();
    };

    form.KeyDown += (o, e) =>
    {
        if (e.KeyCode == Keys.Escape)
        {
            Application.Exit();
        }
    };

    pb.MouseClick += (o, e) =>
    {
        if (mainObjectTestClick(e.Location, bmp))
        {
            mainObjectClick();
        }
    };
});