using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCutRequest_NEW
{
    [JsonProperty("userId")]
    public int UserId { get; set; }

    [JsonProperty("itemsCount")]
    public int ItemsCount { get; set; }

    [JsonProperty("pageNumber")]
    public int PageNumber { get; set; }

    [JsonProperty("cut")]
    public int Cut { get; set; }

    [JsonProperty("colors")]
    public List<int> Colors { get; set; }
}


