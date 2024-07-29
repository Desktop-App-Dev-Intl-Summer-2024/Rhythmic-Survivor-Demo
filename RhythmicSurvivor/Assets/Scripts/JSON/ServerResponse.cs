using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ServerResponse
{
    public int statusCode { get; set; }
    public string statusMessage { get; set; }
    public PlayerData dataChanged { get; set; }
    public List<PlayerData> gamesData { get; set; }
}
