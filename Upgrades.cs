public class OilOne : Upgrade
{
    public OilOne() : base(
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

public class OilTwo : Upgrade
{
    public OilTwo() : base(
        "Duas gotinhas de óleo", 
        null, 
        "Diminui o atrito da engrenagem de 55% para 35%.", 
        "Tão fluído como um fidget spinner paraguaio", 
        Joule.Nano(10)) { }
    
    public override void Apply(Game game)
    {
        game.Friction -= .20f;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is OilOne);
}

public class OilThree : Upgrade
{
    public OilThree() : base(
        "Três gotas de óleo", 
        null, 
        "Diminui o atrito da engrenagem de 35% para 20%.", 
        "Suuuuuaaaaveeeeee", 
        Joule.Nano(30)) { }
    
    public override void Apply(Game game)
    {
        game.Friction -= .15f;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is OilTwo);
}

public class OilFour : Upgrade
{
    public OilFour() : base(
        "quatro gotonas de óleo", 
        null, 
        "Diminui o atrito da engrenagem de 20% para 10%.", 
        "Alguém está escutando a música do roda a roda?", 
        Joule.Nano(120)) { }
    
    public override void Apply(Game game)
    {
        game.Friction -= .1f;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is OilThree);
}

public class OilFive : Upgrade
{
    public OilFive() : base(
        "Um balde de óleo", 
        null, 
        "Diminui o atrito da engrenagem de 10% para 5%.", 
        "Isso está saindo do controle, não?", 
        Joule.Nano(600)) { }
    
    public override void Apply(Game game)
    {
        game.Friction -= .05f;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is OilFour);
}

public class OilSix : Upgrade
{
    public OilSix() : base(
        "Seis banheiras", 
        null, 
        "Diminui o atrito da engrenagem de 5% para 2%.", 
        "Proibido fumar perto da engrenagem", 
        Joule.Micro(3.6)) { }
    
    public override void Apply(Game game)
    {
        game.Friction -= .03f;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is OilFive);
}

public class OilSeven : Upgrade
{
    public OilSeven() : base(
        "Sete piscinas de óleo", 
        null, 
        "Diminui o atrito da engrenagem de 2% para 1%.", 
        "Quanto óleo você quer na sua engrenagem? Sim", 
        Joule.Micro(25.2)) { }
    
    public override void Apply(Game game)
    {
        game.Friction -= .01f;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is OilSix);
}

public class OilEight : Upgrade
{
    public OilEight() : base(
        "Oito diluvios de óleo", 
        null, 
        "Diminui o atrito da engrenagem de 1% para 0.5%.", 
        "Quanto óleo você quer na sua engrenagem? Sim", 
        Joule.Micro(201.6)) { }
    
    public override void Apply(Game game)
    {
        game.Friction -= .005f;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is OilSeven);
}

public class OilNine : Upgrade
{
    public OilNine() : base(
        "Nove mares de óleo", 
        null, 
        "Diminui o atrito da engrenagem de 0.5% para 0.2%.", 
        "Não eram 7 mares?", 
        Joule.Milli(1.8144)) { }
    
    public override void Apply(Game game)
    {
        game.Friction -= .003f;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is OilEight);
}

public class OilTen : Upgrade
{
    public OilTen() : base(
        "Moto perpétuo", 
        null, 
        "Diminui o atrito da engrenagem de 0.2% para 0%.", 
        "Pera... Então não precisa mais clicar?", 
        Joule.Milli(1.8144)) { }
    
    public override void Apply(Game game)
    {
        game.Friction -= .002f;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is OilNine);
}

public class SpeedOne : Upgrade
{    public SpeedOne() : base(
        "Eixo de cerâmica", 
        null, 
        "Dobra a velocidade máxima da sua engrenagem que poderá ter até 1 rpm.", 
        "RPM? Rodoviária Policia Militar?", 
        Joule.Nano(10)) { }
    
    public override void Apply(Game game)
    {
        game.MaxAngularVelocity *= 2;
    }

    public override bool Condition(Game game)
        => true;
}

public class SpeedTwo : Upgrade
{    
    public SpeedTwo() : base(
        "Eixo de acrílico", 
        null, 
        "Dobra a velocidade máxima da sua engrenagem que poderá ter até 2 rpm.", 
        "Não rode muito pra não quebrar", 
        Joule.Nano(50)) { }
    
    public override void Apply(Game game)
    {
        game.MaxAngularVelocity *= 2;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is SpeedOne);
}

public class SpeedThree : Upgrade
{    
    public SpeedThree() : base(
        "Eixo de ossos de vaca", 
        null, 
        "Dobra a velocidade máxima da sua engrenagem que poderá ter até 3 rpm.", 
        "Cruel... Porém eficiente", 
        Joule.Nano(250)) { }
    
    public override void Apply(Game game)
    {
        game.MaxAngularVelocity *= 2;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is SpeedThree);
}

public class ImproveEngine : Upgrade
{
    public ImproveEngine() : base(
        "Engrenagem de latão", 
        null, 
        "Aumenta a eficiência da sua engrenagem em 10 vezes.", 
        "Isso não pode ser ruim", 
        Joule.Nano(100)) { }
    
    public override void Apply(Game game)
    {
        game.EnginePower *= 10;
    }

    public override bool Condition(Game game)
        => true;
}

public class WorkerOne : Upgrade
{
    public WorkerOne() : base(
        "Bob", 
        null, 
        "Contrata Bob como seu escravo. Bob gira a roda pra você... a cada 10 segundos.", 
        "Bob não liga. Bob só quer rodar", 
        Joule.Nano(50)) { }
    
    public override void Apply(Game game)
    {
        game.WorkerEfficience = 1;
    }

    public override bool Condition(Game game)
        => true;
}