using UnityEngine;
using UnityEngine.UI;

public class XLAnimationButton : MonoBehaviour
{
    public AvatarAnimation avatarAnimation;

    private void Start()
    {
        
        avatarAnimation = FindObjectOfType<AvatarAnimation>();

        
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick() // force the xl versionn anim
    {
        
        if (avatarAnimation != null)
        {
            avatarAnimation.ApplyButtonClicked();
        }
        else
        {
            Debug.LogError("AvatarAnimation component not found.");
        }
    }
}
