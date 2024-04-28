using UnityEngine;
using UnityEngine.UI;

public class BlendShape : MonoBehaviour
{
    public GameObject avatarObject;
    public GameObject[] masksPrefab;
    public Slider slider;
    public float blendShapeWeightRange;
    public int blendShapeIndex;

    private SkinnedMeshRenderer avatarRenderer;

    private void Start()
    {
        if (slider == null)
        {
            return;
        }

        slider.onValueChanged.AddListener(OnSliderValueChanged);
        InitializeMasksRenderer();
    }

    private void InitializeMasksRenderer()
    {
        if (avatarObject == null)
        {
            return;
        }

        avatarRenderer = avatarObject.GetComponentInChildren<SkinnedMeshRenderer>();
        if (avatarRenderer == null)
        {
            return;
        }

        foreach (var maskPrefab in masksPrefab)
        {
            SkinnedMeshRenderer maskRenderer = maskPrefab.GetComponentInChildren<SkinnedMeshRenderer>();
            if (maskRenderer != null)
            {
                maskRenderer.SetBlendShapeWeight(blendShapeIndex, 0);
            }
        }
    }

    public void OnSliderValueChanged(float value)
    {
        if (avatarRenderer == null)
        {
            return;
        }

        float blendShapeWeight = value * blendShapeWeightRange;

        avatarRenderer.SetBlendShapeWeight(blendShapeIndex, blendShapeWeight);

        foreach (var maskPrefab in masksPrefab)
        {
            SkinnedMeshRenderer maskRenderer = maskPrefab.GetComponentInChildren<SkinnedMeshRenderer>();
            if (maskRenderer != null)
            {
                maskRenderer.SetBlendShapeWeight(blendShapeIndex, blendShapeWeight);
            }
        }
    }
}
