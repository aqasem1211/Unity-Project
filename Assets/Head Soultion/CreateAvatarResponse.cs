using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Characteristics
{
    [JsonProperty("age")]
    public int Age { get; set; }

    [JsonProperty("skin_tone")]
    public int SkinTone { get; set; }

    [JsonProperty("eye_color")]
    public int EyeColor { get; set; }
}

public class Data
{
    [JsonProperty("characteristics")]
    public Characteristics Characteristics { get; set; }

    [JsonProperty("hair")]
    public Hair Hair { get; set; }

    [JsonProperty("features")]
    public Features Features { get; set; }

    [JsonProperty("representation")]
    public Representation Representation { get; set; }
}

public class Error
{
    [JsonProperty("errors")]
    public object Errors { get; set; }

    [JsonProperty("message")]
    public object Message { get; set; }
}

public class Eyes
{
    [JsonProperty("shape")]
    public string Shape { get; set; }

    [JsonProperty("percentage")]
    public int Percentage { get; set; }
}

public class Features
{
    [JsonProperty("mouth")]
    public Mouth Mouth { get; set; }

    [JsonProperty("eyes")]
    public Eyes Eyes { get; set; }

    [JsonProperty("lips")]
    public Lips Lips { get; set; }
}

public class Hair
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("length")]
    public int Length { get; set; }

    [JsonProperty("color_str")]
    public string ColorStr { get; set; }

    [JsonProperty("color_hex")]
    public string ColorHex { get; set; }
}

public class Lips
{
    [JsonProperty("shape")]
    public string Shape { get; set; }

    [JsonProperty("percentage")]
    public int Percentage { get; set; }
}

public class Morphs
{
    [JsonProperty("labels")]
    public List<string> Labels { get; set; }

    [JsonProperty("values")]
    public List<double> Values { get; set; }
}

public class Mouth
{
    [JsonProperty("shape")]
    public string Shape { get; set; }

    [JsonProperty("percentage")]
    public int Percentage { get; set; }
}

public class Representation
{
    [JsonProperty("morphs")]
    public Morphs Morphs { get; set; }

    [JsonProperty("textures")]
    public Textures Textures { get; set; }
}

public class Root
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("data")]
    public Data Data { get; set; }

    [JsonProperty("error")]
    public Error Error { get; set; }
}

public class Textures
{
    [JsonProperty("skintone_id")]
    public int SkintoneId { get; set; }

    [JsonProperty("eye_color_id")]
    public int EyeColorId { get; set; }

    [JsonProperty("head_texture")]
    public string HeadTexture { get; set; }
}

