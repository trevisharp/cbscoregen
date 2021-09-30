class PlayerData
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double KDA { get; set; }
    public double Resource { get; set; }
    public double DamagePerResource { get; set; }
    public double MitigationEficience { get; set; }
    public double VisionScore { get; set; }
    public double CCScorePerTime { get; set; }
    public double DeathParticipation { get; set; }
    public double Participation { get; set; }
    public double ParticipationPerResource { get; set; }
    public double FarmPerTime { get; set; }

    public override string ToString()
    {
        return base.ToString();
    }

    public static PlayerData FromPlayerMatchStats(PlayerMatchStats pms)
    {
        var result = new PlayerData();
        result.Name = pms.Nickname;
        result.KDA = (pms.Assists + pms.Kills) / (double)pms.Deaths; 

        return result;
    }
}