using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAvatarResponseCharacteristics
{
    [JsonProperty("age")]
    public int Age { get; set; }

    [JsonProperty("skin_tone")]
    public int SkinTone { get; set; }

    [JsonProperty("eye_color")]
    public int EyeColor { get; set; }
}

public class CreateAvatarResponseEyes
{
    [JsonProperty("shape")]
    public string Shape { get; set; }

    [JsonProperty("percentage")]
    public int Percentage { get; set; }
}

public class CreateAvatarResponseFeatures
{
    [JsonProperty("mouth")]
    public CreateAvatarResponseMouth Mouth { get; set; }

    [JsonProperty("eyes")]
    public CreateAvatarResponseEyes Eyes { get; set; }

    [JsonProperty("lips")]
    public CreateAvatarResponseLips Lips { get; set; }
}

public class CreateAvatarResponseHair
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

public class CreateAvatarResponseLips
{
    [JsonProperty("shape")]
    public string Shape { get; set; }

    [JsonProperty("percentage")]
    public int Percentage { get; set; }
}

public class CreateAvatarResponseMorphs
{
    [JsonProperty("labels")]
    public List<string> Labels { get; set; }

    [JsonProperty("values")]
    public List<double> Values { get; set; }
}

public class CreateAvatarResponseMouth
{
    [JsonProperty("shape")]
    public string Shape { get; set; }

    [JsonProperty("percentage")]
    public int Percentage { get; set; }
}

public class CreateAvatarResponseRepresentation
{
    [JsonProperty("morphs")]
    public CreateAvatarResponseMorphs Morphs { get; set; }

    [JsonProperty("textures")]
    public CreateAvatarResponseTextures Textures { get; set; }
}

public class CreateAvatarResponseRoot
{
    [JsonProperty("characteristics")]
    public CreateAvatarResponseCharacteristics Characteristics { get; set; }

    [JsonProperty("hair")]
    public CreateAvatarResponseHair Hair { get; set; }

    [JsonProperty("features")]
    public CreateAvatarResponseFeatures Features { get; set; }

    [JsonProperty("representation")]
    public CreateAvatarResponseRepresentation Representation { get; set; }
}

public class CreateAvatarResponseTextures
{
    [JsonProperty("skintone_id")]
    public int SkintoneId { get; set; }

    [JsonProperty("eye_color_id")]
    public int EyeColorId { get; set; }

    [JsonProperty("head_texture")]
    public string HeadTexture { get; set; }
}

