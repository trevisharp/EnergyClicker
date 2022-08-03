using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;

using static System.Convert;
using static System.BitConverter;
using static System.Text.Encoding;

public class CompactSerializer
{
    public string Serialize(Game game)
    {
        List<int> data = new List<int>();

        var oilLevel = game.Upgrades.Count(
            u => u.GetType().Name.Contains("Oil"));
        data.Add(oilLevel);

        var workerLevel = game.Upgrades.Count(
            u => u.GetType().Name.Contains("Worker"));
        data.Add(workerLevel);

        var speedLevel = game.Upgrades.Count(
            u => u.GetType().Name.Contains("Speed"));
        data.Add(speedLevel);

        var engineLevel = game.Upgrades.Count(
            u => u.GetType().Name.Contains("ImproveEngine"));
        data.Add(engineLevel);
        
        byte[] binaryData = getBinaryData(data);
        int size = binaryData.Length;

        var energyData = GetBytes(game.Energy);
        for (int i = 0; i < energyData.Length; i++)
            binaryData[size - 1 - i] = energyData[i];

        var strData = ToBase64String(binaryData);
        var signedData = signData(strData);
        return signedData;
    }

    public bool Validate(string data)
    {
        string payload = data.Substring(0, data.Length / 2);
        var validateStr = signData(payload);
        return validateStr == data;
    }

    public void Deserialize(Game game, string data)
    {
        string payload = data.Substring(0, data.Length / 2);
        byte[] bytes = FromBase64String(payload);
        
        var upgrades = Upgrade.All.ToList();

        int oilLevel = bytes[0] % 16;
        addUpgrades(upgrades, "Oil", game, oilLevel);

        int workerLevel = bytes[0] / 16;
        addUpgrades(upgrades, "Worker", game, workerLevel);

        int speedLevel = bytes[1] % 16;
        addUpgrades(upgrades, "Speed", game, speedLevel);

        int improveEngineLevel = bytes[1] / 16;
        addUpgrades(upgrades, "ImproveEngine", game, improveEngineLevel);

        byte[] energyData = new byte[8];
        for (int i = 0; i < 8; i++)
            energyData[i] = bytes[bytes.Length - 1 - i];
        game.Energy = BitConverter.ToDouble(energyData);
    }

    private void addUpgrades(List<Upgrade> upgrades, string name, Game game, int level)
    {
        bool inSearch = true;
        while (inSearch)
        {
            inSearch = false;
            if (level == 0)
                return;
            for (int i = 0; i < upgrades.Count; i++)
            {
                var upgrade = upgrades[i];

                if (!upgrade.GetType().Name.Contains(name))
                    continue;
                
                if (!upgrade.Condition(game))
                    continue;
                
                game.Upgrades.Add(upgrade);
                upgrade.Apply(game);
                upgrades.RemoveAt(i);
                inSearch = true;
                level--;
                break;
            }
        }
    }

    private byte[] getBinaryData(List<int> numbers)
    {
        byte[] binaryData = new byte[48];
        int i = 0;
        foreach (var num in numbers)
        {
            byte value = (byte)(i % 2 == 0 ? num : num << 4);
            binaryData[i / 2] += value;
            i++;
        }
        return binaryData;
    }

    private string signData(string data)
    {
        using (SHA256 sha = SHA256.Create())
        {
            string secretString = getSecretString();
            string fullInfo = data + secretString;
            byte[] bytes = ASCII.GetBytes(fullInfo);
            var hash = sha.ComputeHash(bytes);
            
            var fullSignature = ToBase64String(hash).Replace("=", "");
            var signature = fillString(fullSignature, data.Length);
            return $"{data}{signature}";
        }
    }

    private string fillString(string initial, int len)
    {
        int initlen = initial.Length;
        if (initlen == len)
            return initial;
        if (initlen > len)
            return initial.Substring(0, len);
        else
        {
            if (initlen == 0)
                return string.Concat(Enumerable.Repeat("A", len));
            
            int repeat = len / initlen + 1;
            var bigStr = string.Concat(Enumerable.Repeat(initial, repeat));
            return fillString(bigStr, len);
        }
    }

    private string getSecretString()
    {
        var secret ="EnergyClickerSecret"
            + "rekilCeikooCSecret"
            + "myStrongPassword";
        var secretBytes = ASCII.GetBytes(secret);
        return ToBase64String(secretBytes);
    }
}