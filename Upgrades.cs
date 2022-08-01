public class OilUpgrade : Upgrade
{
    public OilUpgrade() : base(
        "Gotinha de óleo", 
        null, 
        "Diminui o atrito da engrenagem de 80% para 55%", 
        "Um pouco de óleo não faz mal a ninguém", 
        Joule.Nano(5)) { }
    
    public override void Apply(Game game)
    {
        game.Friction -= .25f;
    }

    public override bool Condition(Game game)
        => true;
}