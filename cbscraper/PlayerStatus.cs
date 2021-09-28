using System;

public class PlayerStatus : Attribute
{
    public PlayerStatus(string key)
    {
        this.Key = key;
    }
    public string Key { get; set; }
}