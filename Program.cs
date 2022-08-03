using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

Game game = new Game();
var upgrades = Upgrade.All.ToList();

// Configurations
Configuration.EnableSTAThread();

// Graphics Attributes
int fps = 40;
int frame = 0;
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
    while (number >= 1000.0)
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
float angle = 0f;

void mainObjectRotation(Graphics g, Bitmap bmp)
{
    float frameVelocityKeep = (float)Math.Pow(1f - game.Friction, 1f / fps);
    game.EngineAngularVelocity *= frameVelocityKeep;
    if (game.EngineAngularVelocity < 1f)
        game.EngineAngularVelocity = 0;
    angle += game.EngineAngularVelocity / fps;
    
    RectangleF forceBar = new RectangleF(
        bmp.Width / 2 - mainsize / 2 + 20, 
        bmp.Height / 2 - mainsize / 2 - 30, 
        mainsize - 40, 20);
    LinearGradientBrush brush = new LinearGradientBrush(
        forceBar, Color.Yellow, Color.Red, LinearGradientMode.Horizontal);
    RectangleF currentForceBar = new RectangleF(
            forceBar.X,
            forceBar.Y,
            forceBar.Width * game.EngineAngularVelocity / game.MaxAngularVelocity,
            forceBar.Height
        );
    g.FillRectangle(brush, currentForceBar);
    g.DrawRectangle(Pens.Black, forceBar.X, forceBar.Y, forceBar.Width, forceBar.Height);
}

void drawInfo(Graphics g, Bitmap bmp)
{
    RectangleF rect;
    var production = game.GetProduction();
    var strProduction = format(production, "W");
    game.Energy += production / fps;
    var strEnergy = format(game.Energy, "J");

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
    LinearGradientBrush brush =new LinearGradientBrush(
        mainRect, Color.White, Color.Gray, LinearGradientMode.Vertical);
    g.FillRectangle(brush, mainRect);
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
    game.Click();
}


// Shop Implementation
int shopScroll = 0;
Point? shopMouseClick = null;
void drawShop(Graphics g, Bitmap bmp, Point cursor)
{
    int start = (int)(bmp.Width / 2 + mainsize / 2 + 60);
    var shopRect = new Rectangle(start, 10, bmp.Width - start - 10, (int)(bmp.Height - 20));
    LinearGradientBrush brush =new LinearGradientBrush(
        shopRect, Color.Gray, Color.White, LinearGradientMode.Vertical);
    g.FillRectangle(brush, shopRect);
    g.DrawRectangle(Pens.Black, shopRect);
    
    drawUpgrades(g, bmp, cursor, upgrades.Where(
        u => u.Condition(game) && !game.Upgrades.Contains(u))
        .OrderBy(u => u.Price));
}

void drawUpgrades(Graphics g, Bitmap bmp, Point cursor, IEnumerable<Upgrade> upgrades)
{
    int y = 20;
    int x = (int)(bmp.Width / 2 + mainsize / 2 + 60 + 10);
    int wid = bmp.Width - x - 20;
    int hei = 100;

    Upgrade purchaseUpgrade = null;

    foreach (var upgrade in upgrades)
    {
        var upgradeRect = new Rectangle(x, y, wid, hei);
        bool hasCursor = upgradeRect.Contains(cursor);
        bool canBuy = upgrade.Price <= game.Energy;

        if (shopMouseClick != null && upgradeRect.Contains(shopMouseClick.Value))
        {
            shopMouseClick = null;
            purchaseUpgrade = upgrade;
        }

        drawUpgrade(upgrade, canBuy, upgradeRect, hasCursor, x, y, wid, hei, g);

        y += hei + 10;
    }
    
    if (purchaseUpgrade != null)
        game.Purchase(purchaseUpgrade);
}

void drawUpgrade(Upgrade upgrade, bool canBuy, Rectangle upgradeRect, 
    bool hasCursor, int x, int y, int wid, int hei, Graphics g)
{

    var colorA = canBuy ? Color.FromArgb(180, 140, 100) : Color.FromArgb(90, 70, 50);
    var colorB = canBuy ? Color.FromArgb(180, 180, 100) : Color.FromArgb(90, 90, 50);

    LinearGradientBrush brush = new LinearGradientBrush(
        upgradeRect, colorA, colorB, LinearGradientMode.Horizontal);
    g.FillRectangle(brush, upgradeRect);
    if (!canBuy)
        g.DrawRectangle(Pens.Gray, upgradeRect);
    else if (hasCursor)
        g.DrawRectangle(Pens.White, upgradeRect);
    else g.DrawRectangle(Pens.Black, upgradeRect);

    var title = new Rectangle(x, y, wid, 25);
    if (!canBuy)
        g.DrawString(upgrade.Name, SystemFonts.MenuFont, Brushes.Gray, title);
    else if (hasCursor)
        g.DrawString(upgrade.Name, SystemFonts.MenuFont, Brushes.White, title);
    else g.DrawString(upgrade.Name, SystemFonts.MenuFont, Brushes.Black, title);

    var price = new Rectangle(x + 90, y + 20, wid - 90, 20);
    if (!canBuy)
        g.DrawString(format(upgrade.Price, "J"), SystemFonts.MenuFont, Brushes.DarkOrange, price);
    else g.DrawString(format(upgrade.Price, "J"), SystemFonts.MenuFont, Brushes.Orange, price);

    var commnetary = new Rectangle(x + 90, y + 40, wid - 90, 25);
    var italicFont = new Font(SystemFonts.CaptionFont, FontStyle.Italic);
    if (!canBuy)
        g.DrawString($"\"{upgrade.Commentary}\"", italicFont, Brushes.Gray, commnetary);
    else g.DrawString($"\"{upgrade.Commentary}\"", italicFont, Brushes.LightGray, commnetary);

    var mainText = new Rectangle(x + 90, y + 65, wid - 90, hei - 60);
    g.DrawString(upgrade.Text, SystemFonts.DialogFont, Brushes.Black, mainText);
}

void tryBuyUpgrade(Point p, Bitmap bmp)
{
    int start = (int)(bmp.Width / 2 + mainsize / 2 + 60);
    var shopRect = new Rectangle(start, 10, bmp.Width - start - 10, (int)(bmp.Height - 20));
    if (!shopRect.Contains(p))
        return;
    shopMouseClick = p;
}


// Options Implementation
bool optionsOpened = false;
Bitmap optionsTexture = null;
bool OptmouseInDown = false;
bool Optclick = false;
void drawButtonAndScreen(Graphics g, Bitmap bmp, Point cursor, bool down)
{
    if (OptmouseInDown && !down)
    {
        Optclick = true;
        OptmouseInDown = false;
    }
    else if (!OptmouseInDown && down)
    {
        Optclick = false;
        OptmouseInDown = true;
    }
    else Optclick = false;

    var optionsRect = new Rectangle(10, 10, 150, 40);
    if (optionsOpened)
    {   
        g.FillRectangle(Brushes.LightGray, optionsRect);
        g.DrawRectangle(Pens.Black, optionsRect);
        g.DrawString("Opções", SystemFonts.CaptionFont, Brushes.Black, optionsRect, strFormat);
        drawOptionsScreen(g, bmp, cursor, down);
    }
    else
    {
        if (optionsRect.Contains(cursor))
        {
            g.FillRectangle(Brushes.DarkGray, optionsRect);
            g.DrawRectangle(Pens.Black, optionsRect);
            g.DrawString("Opções", SystemFonts.CaptionFont, Brushes.White, optionsRect, strFormat);

            if (Optclick)
            {
                optionsOpened = true;
            }
        }
        else
        {
            g.FillRectangle(Brushes.LightGray, optionsRect);
            g.DrawRectangle(Pens.Black, optionsRect);
            g.DrawString("Opções", SystemFonts.CaptionFont, Brushes.Black, optionsRect, strFormat);
        }
    }
}
void drawOptionsScreen(Graphics g, Bitmap bmp, Point cursor, bool down)
{
    if (optionsTexture == null)
        createTexture();
    
    var pageRect = new Rectangle(50, 50, bmp.Width - 100, bmp.Height - 100);
    TextureBrush brush = new TextureBrush(optionsTexture);
    g.FillRectangle(brush, pageRect);
    g.DrawRectangle(Pens.Yellow, pageRect);

    addOptionButton(g, "Salvar", new Rectangle(60, 60, 150, 50), cursor, () =>
    {
        game.Save();
    });

    addOptionButton(g, "Carregar", new Rectangle(60, 120, 150, 50), cursor, () =>
    {
        game.Load();
        upgrades = Upgrade.All.Where(
            u => !game.Upgrades.Exists(
                v => u.GetType() == v.GetType()))
            .ToList();
    });

    if (Optclick && !pageRect.Contains(cursor))
    {
        optionsOpened = false;
    }
}
void createTexture()
{
    optionsTexture = Image.FromFile("res/opttexture.png") as Bitmap;
}
void addOptionButton(Graphics g, string text, Rectangle rect,
    Point cursor, Action onClick)
{
    if (rect.Contains(cursor))
    {
        g.FillRectangle(Brushes.White, rect);
        g.DrawRectangle(Pens.Black, rect);
        g.DrawString(text, SystemFonts.CaptionFont, Brushes.Black, rect, strFormat);

        if (Optclick)
        {
            onClick();
        }
    }
    else
    {
        g.FillRectangle(Brushes.Black, rect);
        g.DrawRectangle(Pens.White, rect);
        g.DrawString(text, SystemFonts.CaptionFont, Brushes.White, rect, strFormat);
    }
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

    Point cursor = Point.Empty;
    bool cursorDown = false;

    Timer tm = new Timer();
    tm.Interval = 1000 / fps;
    tm.Tick += delegate
    {
        frame++;
        game.Work(frame);
        g.Clear(Color.White);
        drawMainObject(g, bmp);
        drawShop(g, bmp, cursor);
        drawButtonAndScreen(g, bmp, cursor, cursorDown);
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
        if (optionsOpened)
            return;
        if (mainObjectTestClick(e.Location, bmp))
        {
            mainObjectClick();
        }
        tryBuyUpgrade(e.Location, bmp);
    };

    pb.MouseMove += (o, e) =>
    {
        cursor = e.Location;
    };

    pb.MouseDown += delegate
    {
        cursorDown = true;
    };

    pb.MouseUp += delegate
    {
        cursorDown = false;
    };
});
