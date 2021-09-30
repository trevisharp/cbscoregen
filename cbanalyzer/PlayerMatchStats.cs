using System;
using System.Linq;
class PlayerMatchStats
{
    public string Nickname { get; set; }
    public string Match { get; set; }
    public string Team { get; set; }
    public string Duration { get; set; }
    public int Kills { get; set; }
    public int Deaths { get; set; }
    public int Assists { get; set; }
    public int Damage { get; set; }
    public int ChampionsDamage { get; set; }
    public int Mitigation { get; set; }
    public int ObjectiveDamage { get; set; }
    public int VisionScore { get; set; }
    public int CCSCore { get; set; }
    public int Heal { get; set; }
    public int TotalUnitsHealed { get; set; }
    public int Recived { get; set; }
    public int Resource { get; set; }
    public int Farm { get; set; }
    public int JungleFarm { get; set; }

    public PlayerMatchStats FromString(string str)
    {
        PlayerMatchStats result = new PlayerMatchStats();
        var x = str.Split(",", StringSplitOptions.RemoveEmptyEntries);
        this.Nickname = x[0];
        this.Match = x[1];
        this.Team = x[2];
        this.Duration = x[3];
        int i = 4;
        foreach (var prop in typeof(PlayerMatchStats).GetProperties().Skip(3))
        {
            prop.SetValue(this, int.Parse(x[i]));
            i++;
        }
        return result;
    }
}