public class OilUpgrade : Upgrade
{
    public OilUpgrade() : base(
        "Gotinhazinha de óleo", 
        null, 
        "Diminui o atrito da engrenagem de 80% para 55%. Ela vai parar de girar mais devagar agora.", 
        "Um pouco de óleo não faz mal a ninguém", 
        Joule.Nano(5)) { }
    
    public override void Apply(Game game)
    {
        game.Friction -= .25f;
    }

    public override bool Condition(Game game)
        => true;
}

public class ImproveEngineUpgrade : Upgrade
{
    public ImproveEngineUpgrade() : base(
        "Engrenagem de latão", 
        null, 
        "Aumenta a eficiência da sua engrenagem em 10 vezes.", 
        "Isso não pode ser ruim", 
        Joule.Nano(50)) { }
    
    public override void Apply(Game game)
    {
        game.EnginePower *= 10;
    }

    public override bool Condition(Game game)
        => true;
}

public class SecondOilUpgrade : Upgrade
{
    public SecondOilUpgrade() : base(
        "Duas gotinhas de óleo", 
        null, 
        "Diminui o atrito da engrenagem de 55% para 35%. Ela vai parar de girar mais devagar agora.", 
        "Tão fluído como um fidget spinner paraguaio", 
        Joule.Nano(25)) { }
    
    public override void Apply(Game game)
    {
        game.Friction -= .20f;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is OilUpgrade);
}