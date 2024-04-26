using UnityEngine;
using UnityEngine.UI;

public class BlendShapeTransfer : MonoBehaviour
{
    //public SkinnedMeshRenderer targetRenderer; // T-shirt's SkinnedMeshRenderer
    //public SkinnedMeshRenderer avatarRenderer; // Avatar's SkinnedMeshRenderer
    //public Slider slider;
    //public int blendShapeIndex = 0; // Index of the blend shape you want to control

    //private void Start()
    //{
    //    slider.onValueChanged.AddListener(OnSliderValueChanged);
    //}

    //private void OnSliderValueChanged(float value)
    //{
    //    if (targetRenderer == null || avatarRenderer == null)
    //    {
    //        Debug.LogWarning("T-shirt's or Avatar's SkinnedMeshRenderer not set.");
    //        return;
    //    }

    //    float avatarBlendShapeWeight = GetAvatarBlendShapeWeight();
    //    float currentTShirtBlendShapeWeight = targetRenderer.GetBlendShapeWeight(blendShapeIndex);

    //    // Calculate the difference between avatar and T-shirt blend shape weights
    //    float weightDifference = avatarBlendShapeWeight - currentTShirtBlendShapeWeight;

    //    // Set the T-shirt blend shape weight by adding the difference
    //    float newTShirtBlendShapeWeight = currentTShirtBlendShapeWeight + weightDifference;

    //    SetBlendShapeWeight(newTShirtBlendShapeWeight);
    //}


    //private float GetAvatarBlendShapeWeight()
    //{
    //    if (blendShapeIndex >= 0 && blendShapeIndex < avatarRenderer.sharedMesh.blendShapeCount)
    //    {
    //        return avatarRenderer.GetBlendShapeWeight(blendShapeIndex);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Invalid blend shape index or no blend shapes available on the avatar.");
    //        return 0f;
    //    }
    //}

    //private void SetBlendShapeWeight(float weight)
    //{
    //    if (blendShapeIndex >= 0 && blendShapeIndex < targetRenderer.sharedMesh.blendShapeCount)
    //    {
    //        targetRenderer.SetBlendShapeWeight(blendShapeIndex, weight);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Invalid blend shape index or no blend shapes available on the T-shirt.");
    //    }
    //}

    public SkinnedMeshRenderer targetRenderer;
    public SkinnedMeshRenderer avatarRenderer;
    public Slider slider;
    public int blendShapeIndex = 0;

    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        if (targetRenderer == null || avatarRenderer == null)
        {
            Debug.LogWarning("T-shirt's or Avatar's SkinnedMeshRenderer not set.");
            return;
        }

        float avatarBlendShapeWeight = GetAvatarBlendShapeWeight();
        float currentTShirtBlendShapeWeight = targetRenderer.GetBlendShapeWeight(blendShapeIndex);

        // Calculate the difference between avatar and T-shirt blend shape weights
        float weightDifference = avatarBlendShapeWeight - currentTShirtBlendShapeWeight;

        // Adjust the T-shirt blend shape weight to match the avatar's blend shape weight difference
        float newTShirtBlendShapeWeight = currentTShirtBlendShapeWeight + weightDifference;

        // Set the T-shirt blend shape weight
        SetBlendShapeWeight(newTShirtBlendShapeWeight);
    }

    private float GetAvatarBlendShapeWeight()
    {
        if (blendShapeIndex >= 0 && blendShapeIndex < avatarRenderer.sharedMesh.blendShapeCount)
        {
            return avatarRenderer.GetBlendShapeWeight(blendShapeIndex);
        }
        else
        {
            Debug.LogWarning("Invalid blend shape index or no blend shapes available on the avatar.");
            return 0f;
        }
    }

    private void SetBlendShapeWeight(float weight)
    {
        if (blendShapeIndex >= 0 && blendShapeIndex < targetRenderer.sharedMesh.blendShapeCount)
        {
            targetRenderer.SetBlendShapeWeight(blendShapeIndex, weight);
        }
        else
        {
            Debug.LogWarning("Invalid blend shape index or no blend shapes available on the T-shirt.");
        }
    }
}
