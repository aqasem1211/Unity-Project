using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[System.Serializable]
public class TextureSets
{
    public string name;
    
    public Material[] maskMaterial;
    public Texture[] baseColorTextures;
    public Texture normalMap;
    public Texture roughnessMap;
    public Texture metallicMap;
}
                                           //***for testing***
public class MasksSelector : MonoBehaviour
{
    public TextureSets[] textureSets;
    public GameObject[] maskObject;

    protected int textureSetIndex = 0;
    protected int baseColorTextureIndex = 0;


    //wool
    protected int woolTextures;
    protected int woolColorWhite = 0, woolColorGreen = 1, woolColorRed = 2, woolColorBlue = 3, woolColorBrown = 4,
                  woolColorOrange = 5, woolColorPink = 6, woolColorPurple = 7, woolColorYellow = 8, woolColorNude = 9,
                  woolColorGold = 10, woolColorSilver = 11, woolColorCopper = 12;

    //linen
    protected int linenTextures;
    protected int linenColorWhite = 0, linenColorGreen = 1, linenColorRed = 2, linenColorBlue = 3, linenColorBrown = 4,
                  linenColorOrange = 5, linenColorPink = 6, linenColorPurple = 7, linenColorYellow = 8, linenColorNude = 9,
                  linenColorGold = 10, linenColorSilver = 11, linenColorCopper = 12;

    //Cashmere
    protected int cashmereTextures;
    protected int cashmereColorWhite = 0, cashmereColorGreen = 1, cashmereColorRed = 2, cashmereColorBlue = 3, cashmereColorBrown = 4,
                  cashmereColorOrange = 5, cashmereColorPink = 6, cashmereColorPurple = 7, cashmereColorYellow = 8, cashmereColorNude = 9,
                  cashmereColorGold = 10, cashmereColorSilver = 11, cashmereColorCopper = 12;

    //Corduroy
    protected int corduroyTextures;
    protected int corduroyColorWhite = 0, corduroyColorGreen = 1, corduroyColorRed = 2, corduroyColorBlue = 3, corduroyColorBrown = 4,
                  corduroyColorOrange = 5, corduroyColorPink = 6, corduroyColorPurple = 7, corduroyColorYellow = 8, corduroyColorNude = 9,
                  corduroyColorGold = 10, corduroyColorSilver = 11, corduroyColorCopper = 12;

    //Sequins Cotton
    protected int sequinsTextures;
    protected int sequinsColorWhite = 0, sequinsColorGreen = 1, sequinsColorRed = 2, sequinsColorBlue = 3, sequinsColorBrown = 4,
                  sequinsColorOrange = 5, sequinsColorPink = 6, sequinsColorPurple = 7, sequinsColorYellow = 8, sequinsColorNude = 9,
                  sequinsColorGold = 10, sequinsColorSilver = 11, sequinsColorCopper = 12;

    //cotton
    protected int cottonTextures;
    protected int cottonColorWhite = 0, cottonColorGreen = 1, cottonColorRed = 2, cottonColorBlue = 3, cottonColorBrown = 4,
                  cottonColorOrange = 5, cottonColorPink = 6, cottonColorPurple = 7, cottonColorYellow = 8, cottonColorNude = 9,
                  cottonColorGold = 10, cottonColorSilver = 11, cottonColorCopper = 12;


    public void ChangeTexture()
    {

        TextureSets selectedSet = textureSets[textureSetIndex];

        foreach (Material maskMaterial in selectedSet.maskMaterial)
        {
            maskMaterial.mainTexture = selectedSet.baseColorTextures[baseColorTextureIndex];
            maskMaterial.SetTexture("_BumpMap", selectedSet.normalMap);
            maskMaterial.SetTexture("_RoughnessMap", selectedSet.roughnessMap);
            maskMaterial.SetTexture("_MetallicMap", selectedSet.metallicMap);
        }

        for (int i = 0; i < maskObject.Length; i++)
        {
            if (maskObject[i] != null && i < selectedSet.maskMaterial.Length)
            {
                maskObject[i].GetComponent<Renderer>().material = selectedSet.maskMaterial[i];
            }
        }
    }
}
 

