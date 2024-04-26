using UnityEngine;
using UnityEngine.UI;

public class BlendShape : MonoBehaviour
{
    public SkinnedMeshRenderer avatarRenderer; // Avatar's SkinnedMeshRenderer
    public SkinnedMeshRenderer tShirtRenderer; // T-shirt's SkinnedMeshRenderer
    public Slider slider;
    private float blendShapeWeightRange = 100f; // Maximum blend shape weight (assumed to be 100)
    private int blendShapeIndex = 0; // Index of the blend shape you want to control

    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        if (avatarRenderer == null || tShirtRenderer == null)
        {
            Debug.LogWarning("Avatar's or T-shirt's SkinnedMeshRenderer not set.");
            return;
        }

        // Scale the slider value to match the blend shape weight range
        float blendShapeWeight = value * blendShapeWeightRange / slider.maxValue;

        // Update the blend shape weight for the avatar
        avatarRenderer.SetBlendShapeWeight(blendShapeIndex, blendShapeWeight);

        // Apply the same weight to the blend shape on the T-shirt
        tShirtRenderer.SetBlendShapeWeight(blendShapeIndex, blendShapeWeight);
    }
}
