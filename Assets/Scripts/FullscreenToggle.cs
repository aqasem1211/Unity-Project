using UnityEngine;
using TMPro;

public class FullscreenToggle : MonoBehaviour
{
    public TMP_Text buttonText;

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;

        if (Screen.fullScreen)
        {
            buttonText.text = "Windowed";
        }
        else
        {
            buttonText.text = "Fullscreen";
        }
    }
}
