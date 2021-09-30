using System.Text;
using System.Reflection;

class PlayerMatchStats
{
    public string Nickname { get; set; }
    public string Match { get; set; }
    public string Team { get; set; }
    public int Duration { get; set; }

    [PlayerStatus("kills")]
    public int Kills { get; set; }

    [PlayerStatus("deaths")]
    public int Deaths { get; set; }

    [PlayerStatus("assists")]
    public int Assists { get; set; }

    [PlayerStatus("totalDamageDealt")]
    public int Damage { get; set; }
    
    [PlayerStatus("totalDamageDealtToChampions")]
    public int ChampionsDamage { get; set; }

    [PlayerStatus("damageSelfMitigated")]
    public int Mitigation { get; set; }

    [PlayerStatus("damageDealtToObjectives")]
    public int ObjectiveDamage { get; set; }

    [PlayerStatus("visionScore")]
    public int VisionScore { get; set; }

    [PlayerStatus("timeCCingOthers")]
    public int CCSCore { get; set; }

    [PlayerStatus("totalHeal")]
    public int Heal { get; set; }

    [PlayerStatus("totalUnitsHealed")]
    public int TotalUnitsHealed { get; set; }

    [PlayerStatus("totalDamageTaken")]
    public int Recived { get; set; }

    [PlayerStatus("goldEarned")]
    public int Resource { get; set; }

    [PlayerStatus("totalMinionsKilled")]
    public int Farm { get; set; }

    [PlayerStatus("neutralMinionsKilled")]
    public int JungleFarm { get; set; }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        foreach (var prop in typeof(PlayerMatchStats).GetProperties())
        {
            builder.Append(prop.GetValue(this));
            builder.Append(",");
        }
        return builder.ToString();
    }
}