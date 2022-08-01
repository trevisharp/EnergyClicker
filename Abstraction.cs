using System.Collections.Generic;
using System.Reflection;
using System.Drawing;
using System;

public class Game
{
    public List<Upgrade> Upgrades { get; private set; } = new List<Upgrade>();

    public double Energy { get; set; } = 0;

    public float MaxAngularVelocity { get; set; } = 180f;
    public float Friction { get; set; } = 0.8f;
    public float EnginePower { get; set; } = 1f / 1000 / 1000 / 1000;

    public bool Purchase(Upgrade upgrade)
    {
        if (this.Energy < upgrade.Price)
            return false;
        
        this.Energy -= upgrade.Price;
        upgrade.Apply(this);
        this.Upgrades.Add(upgrade);
        return true;
    }
}

public abstract class Upgrade
{
    public static IEnumerable<Upgrade> All
    {
        get
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.BaseType != typeof(Upgrade))
                    continue;
                var constructor = type.GetConstructor(new Type[0]);
                yield return constructor.Invoke(null) as Upgrade;
            }
        }
    }

    public Upgrade(string name, string image, string text, string commentary, double value)
    {
        this.Name = name;
        this.ImageName = image;
        this.Price = value;
        this.Text = text;
        this.Commentary = commentary;
    }

    public string Name { get; set; }
    public string ImageName { get; set; }
    public string Text { get; set;}
    public string Commentary { get; set; }
    public double Price { get; set; }

    private Image image = null;
    public Image Image
    {
        get
        {
            if (ImageName == null)
                return null;
            
            if (image == null)
                image = Image.FromFile(ImageName);
            
            return image;
        }
    }

    public abstract void Apply(Game game);
    public abstract bool Condition(Game game);
}