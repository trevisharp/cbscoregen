using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

StreamReader reader = new StreamReader("matchlist.txt");
StreamWriter writer = new StreamWriter("data.csv");

int count = 0;
while (!reader.EndOfStream)
{
    string line = reader.ReadLine();
    try
    {
        var list = await GetPlayeMatchStats(line);
        foreach (var player in list)
            writer.WriteLine(player.ToString());
        count++;
        Console.WriteLine($"Complete #{count}: " + line);
    }
    catch
    {
        Console.WriteLine("Error in: " + line);
    }
}

writer.Close();
reader.Close();

async Task<List<PlayerMatchStats>> GetPlayeMatchStats(string RiotPlatformGameId)
{
    var list = new List<PlayerMatchStats>();

    var client = new HttpClient();
    client.BaseAddress = new Uri("https://lol.fandom.com/wiki/");
    var response = await client.GetAsync($"V4_data:{RiotPlatformGameId}");
    var data = await response.Content.ReadAsStringAsync();
    var table = data
        .ScopeSearch("<table", "</table>")
        .ScopeSkipSearch("<tr>", "</tr>", 11);
    var playersstats = table.ScopeSearch("<table", "</table>");
    var idnamekey = table
        .ScopeSkipSearch("<tr>", "</tr>")
        .ScopeSearch("<table", "</table>")
        .Split("</tr>");
    string[] names = new string[10];
    for (int i = 0; i < 50; i += 5)
    {
        var name = idnamekey[i + 1]
            .ScopeSearch("<td class", "</td>");
        name = name.Substring(19, name.Length - 19 - 2);
        names[i / 5] = name;
    }

    for (int k = 0; k < 10; k++)
    {
        var stats = playersstats.ScopeSearch("<tr>", "</tr>")
            .ScopeSearch("<tbody", "</tbody>").Split("</tr>");
        var id = int.Parse(stats[0].Substring(51, stats[0].Length - 51 - 5)) - 1;

        PlayerMatchStats pms = new PlayerMatchStats();
        pms.Match = RiotPlatformGameId;
        pms.Team = names[id].Split(' ')[0];
        pms.Nickname = names[id].Split(' ')[1];

        var props = typeof(PlayerMatchStats).GetProperties();

        foreach (var prop in props)
        {
            var att = prop.GetCustomAttribute<PlayerStatus>();
            if (att != null)
            {
                for (int i = 2; i < stats.Length; i++)
                {
                    if (stats[i].Contains(">" + att.Key + "<"))
                    {
                        var value = stats[i]
                            .ScopeSearch("<td class=\"value\">", "</td>");
                        value = value
                            .Substring(18, value.Length - 18 - 1);
                        prop.SetValue(pms, int.Parse(value));
                    }
                }
            }
        }

        playersstats = playersstats.ScopeSkipSearch("<tr>", "</tr>");
        list.Add(pms);
    }

    return list;
}