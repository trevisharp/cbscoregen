using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;


var list = await GetPlayeMatchStats("ESPORTSTMNT03_2054910");

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
        pms.Team = names[id].Split(' ')[0];
        pms.Nickname = names[id].Split(' ')[1];
        
        for (int i = 2; i < stats.Length; i++)
        {
            
        }

        playersstats = playersstats.ScopeSkipSearch("<tr>", "</tr>");
        list.Add(pms);
    }

    return list;
}