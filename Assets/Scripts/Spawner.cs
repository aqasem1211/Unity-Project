using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    private GameObject instantiatedObject;
    public BlendShape blendShapeScript;
    public RotateObjects rotateObjectsScript;
    private AvatarAnimation avatarAnimation;
    private MasksController masksController;



    private void Start()
    {
        avatarAnimation = FindObjectOfType<AvatarAnimation>();
        blendShapeScript = FindObjectOfType<BlendShape>();

        masksController = FindObjectOfType<MasksController>(); // Assign the MasksController here

    }

    public void PrefabsInstantiate()
    {
        if (instantiatedObject != null)
        {
            Destroy(instantiatedObject);
            rotateObjectsScript.HandleInstantiatedOrDestroyedObject(instantiatedObject);

            List<GameObject> masks = new List<GameObject>(blendShapeScript.masksPrefab);
            masks.Remove(instantiatedObject);
            blendShapeScript.masksPrefab = masks.ToArray();

            instantiatedObject = null;
        }
        else
        {
            instantiatedObject = Instantiate(prefabToInstantiate, Vector3.zero, Quaternion.identity);
            rotateObjectsScript.HandleInstantiatedOrDestroyedObject(instantiatedObject);

            blendShapeScript.avatarObject = instantiatedObject;
            SetInstantiatedObjectBlendShape();

            List<GameObject> masks = new List<GameObject>(blendShapeScript.masksPrefab);
            masks.Add(instantiatedObject);
            blendShapeScript.masksPrefab = masks.ToArray();

            ApplyAvatarAnimationState();

            SetInstantiatedObjectAnimationState();
        }
    }

    private void SetInstantiatedObjectBlendShape()
    {
        if (blendShapeScript != null && blendShapeScript.avatarObject != null)
        {
            SkinnedMeshRenderer avatarRenderer = blendShapeScript.avatarObject.GetComponentInChildren<SkinnedMeshRenderer>();
            SkinnedMeshRenderer instantiatedRenderer = instantiatedObject.GetComponentInChildren<SkinnedMeshRenderer>();

            if (avatarRenderer != null && instantiatedRenderer != null)
            {
                float currentSliderValue = blendShapeScript.slider.value;
                float blendShapeWeight = currentSliderValue * blendShapeScript.blendShapeWeightRange;
                instantiatedRenderer.SetBlendShapeWeight(blendShapeScript.blendShapeIndex, blendShapeWeight);
            }
        }
    }

    public void ApplyAvatarAnimationState()
    {
        if (avatarAnimation != null && instantiatedObject != null)
        {
            Animator avatarAnimator = avatarAnimation.avatarAnimator;
            Animator instantiatedAnimator = instantiatedObject.GetComponent<Animator>();

            if (avatarAnimator != null && instantiatedAnimator != null)
            {
                bool isAvatarXL = avatarAnimator.GetBool("isWalkingXL") ||
                                  avatarAnimator.GetBool("isIdleXL") ||
                                  avatarAnimator.GetBool("isAposeXL");

                instantiatedAnimator.SetBool("isWalking", isAvatarXL ? avatarAnimator.GetBool("isWalkingXL") : avatarAnimator.GetBool("isWalking"));
                instantiatedAnimator.SetBool("isIdle", isAvatarXL ? avatarAnimator.GetBool("isIdleXL") : avatarAnimator.GetBool("isIdle"));
                instantiatedAnimator.SetBool("isApose", isAvatarXL ? avatarAnimator.GetBool("isAposeXL") : avatarAnimator.GetBool("isApose"));
            }
        }
    }

    public void SetInstantiatedObjectAnimationState()
    {
        if (avatarAnimation != null && instantiatedObject != null)
        {
            Animator avatarAnimator = avatarAnimation.avatarAnimator;
            Animator instantiatedAnimator = instantiatedObject.GetComponent<Animator>();

            if (avatarAnimator != null && instantiatedAnimator != null)
            {
                instantiatedAnimator.SetBool("isWalking", avatarAnimator.GetBool("isWalking"));
                instantiatedAnimator.SetBool("isIdle", avatarAnimator.GetBool("isIdle"));
                instantiatedAnimator.SetBool("isApose", avatarAnimator.GetBool("isApose"));
                instantiatedAnimator.SetBool("isWalkingXL", avatarAnimator.GetBool("isWalkingXL"));
                instantiatedAnimator.SetBool("isIdleXL", avatarAnimator.GetBool("isIdleXL"));
                instantiatedAnimator.SetBool("isAposeXL", avatarAnimator.GetBool("isAposeXL"));

                AnimatorStateInfo avatarStateInfo = avatarAnimator.GetCurrentAnimatorStateInfo(0);

                instantiatedAnimator.Play(avatarStateInfo.fullPathHash, -1, avatarStateInfo.normalizedTime);
            }
        }
    }

    private void Update()
    {
        ApplyAvatarAnimationState();

    }


    public void ApplyButton()
    {
        ApplyAvatarAnimationState();
        SetInstantiatedObjectAnimationState();
        avatarAnimation.ApplyButtonClicked();
    }
}