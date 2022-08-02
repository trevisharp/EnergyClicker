//Oil Upgrade
public class OilOne : Upgrade
{
    public OilOne() : base(
        "Gotinhazinha de óleo", 
        null, 
        "Diminui o atrito da engrenagem de 95% para 70%. Ela vai parar de girar mais devagar agora.", 
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
        "Diminui o atrito da engrenagem de 70% para 50%.", 
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
        "Diminui o atrito da engrenagem de 50% para 30%.", 
        "Suuuuuaaaaveeeeee", 
        Joule.Nano(30)) { }
    
    public override void Apply(Game game)
    {
        game.Friction -= .20f;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is OilTwo);
}

public class OilFour : Upgrade
{
    public OilFour() : base(
        "quatro gotonas de óleo", 
        null, 
        "Diminui o atrito da engrenagem de 30% para 20%.", 
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
        "Diminui o atrito da engrenagem de 20% para 10%.", 
        "Isso está saindo do controle, não?", 
        Joule.Nano(600)) { }
    
    public override void Apply(Game game)
    {
        game.Friction -= .1f;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is OilFour);
}

public class OilSix : Upgrade
{
    public OilSix() : base(
        "Seis banheiras de óleo", 
        null, 
        "Diminui o atrito da engrenagem de 10% para 7%.", 
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
        "Diminui o atrito da engrenagem de 7% para 4%.", 
        "Quanto óleo você quer na sua engrenagem? Sim", 
        Joule.Micro(25.2)) { }
    
    public override void Apply(Game game)
    {
        game.Friction -= .03f;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is OilSix);
}

public class OilEight : Upgrade
{
    public OilEight() : base(
        "Oito diluvios de óleo", 
        null, 
        "Diminui o atrito da engrenagem de 4% para 2.5%.", 
        "Quanto óleo você quer na sua engrenagem? Sim", 
        Joule.Micro(201.6)) { }
    
    public override void Apply(Game game)
    {
        game.Friction -= .015f;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is OilSeven);
}

public class OilNine : Upgrade
{
    public OilNine() : base(
        "Nove mares de óleo", 
        null, 
        "Diminui o atrito da engrenagem de 2.5% para 1%.", 
        "Não eram 7 mares?", 
        Joule.Milli(1.8144)) { }
    
    public override void Apply(Game game)
    {
        game.Friction -= .015f;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is OilEight);
}

public class OilTen : Upgrade
{
    public OilTen() : base(
        "Moto perpétuo", 
        null, 
        "Diminui o atrito da engrenagem de 1% para 0,5%.", 
        "Pera... Então não precisa mais clicar?", 
        Joule.Milli(1.8144)) { }
    
    public override void Apply(Game game)
    {
        game.Friction -= .005f;
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


//Engine Upgrades
public class ImproveEngineOne : Upgrade
{
    public ImproveEngineOne() : base(
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

public class ImproveEngineTwo : Upgrade
{
    public ImproveEngineTwo() : base(
        "Engrenagem de Cobre", 
        null, 
        "Aumenta a eficiência da sua engrenagem em 10 vezes.", 
        "Cubrir o quê?", 
        Joule.Micro(100)) { }
    
    public override void Apply(Game game)
    {
        game.EnginePower *= 10;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is ImproveEngineOne);
}


//Worker Upgrades
public class WorkerOne : Upgrade
{
    public WorkerOne() : base(
        "Bob", 
        null, 
        "Contrata Bob como seu escravo. Bob gira a roda pra você... a cada 10 segundos.", 
        "Bob não liga. Bob só quer rodar", 
        Joule.Nano(1)) { }
    
    public override void Apply(Game game)
    {
        game.WorkerEfficience += 1;
    }

    public override bool Condition(Game game)
        => true;
}

public class WorkerTwo : Upgrade
{
    public WorkerTwo() : base(
        "Brad", 
        null, 
        "Contrata Brad como seu escravo. Brad gira a roda pra você a cada 10 segundos, alternando com Bob", 
        "É o Brad!", 
        Joule.Nano(2)) { }
    
    public override void Apply(Game game)
    {
        game.WorkerEfficience += 2;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is WorkerOne);
}

public class WorkerThree : Upgrade
{
    public WorkerThree() : base(
        "Billy e Jim", 
        null, 
        "Contrata Billy e Jean como seus escravos. Eles giram a roda junto de Bob e Brad.", 
        "Isso está ficando divertido", 
        Joule.Nano(6)) { }
    
    public override void Apply(Game game)
    {
        game.WorkerEfficience += 2;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is WorkerTwo);
}

public class WorkerFour : Upgrade
{
    public WorkerFour() : base(
        "Mandy", 
        null, 
        "Dobra a eficiência dos seus trabalhadores.", 
        "Mandy não roda, mas ela grita a beça", 
        Joule.Nano(24)) { }
    
    public override void Apply(Game game)
    {
        game.WorkerEfficience *= 2;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is WorkerThree);
}

public class WorkerFive : Upgrade
{
    public WorkerFive() : base(
        "Chicote", 
        null, 
        "Dobra a eficiência dos seus trabalhadores.", 
        "Mandy adorou seu novo presentinho", 
        Joule.Nano(120)) { }
    
    public override void Apply(Game game)
    {
        game.WorkerEfficience *= 2;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is WorkerFour);
}

public class WorkerSix : Upgrade
{
    public WorkerSix() : base(
        "Familia da Silva", 
        null, 
        "Dobra a eficiência dos seus trabalhadores.", 
        "É uma grande familia...", 
        Joule.Nano(720)) { }
    
    public override void Apply(Game game)
    {
        game.WorkerEfficience *= 2;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is WorkerFive);
}

public class WorkerSeven : Upgrade
{
    public WorkerSeven() : base(
        "Amanda", 
        null, 
        "Dobra a eficiência dos seus trabalhadores.", 
        "Amanda faz um cházinho que faz todo mundo trabalhar muito.",
        Joule.Micro(5)) { }
    
    public override void Apply(Game game)
    {
        game.WorkerEfficience *= 2;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is WorkerSix);
}

public class WorkerEight : Upgrade
{
    public WorkerEight() : base(
        "Material Ilícito", 
        null, 
        "Dobra a eficiência dos seus trabalhadores.", 
        "O chá da amanda está diferente... Mas melhor.", 
        Joule.Micro(40)) { }
    
    public override void Apply(Game game)
    {
        game.WorkerEfficience *= 2;
    }

    public override bool Condition(Game game)
        => game.Upgrades.Exists(u => u is WorkerSeven);
}
