using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAvatarRequestRoot
{
    [JsonProperty("image")]
    public string Image { get; set; }
}

