using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    private GameObject instantiatedObject;
    public Animator characterAnimator;
    public BlendShape blendShapeScript;

    public void PrefabsInstantiate()
    {
        if (instantiatedObject != null)
        {
            Destroy(instantiatedObject);

            List<GameObject> masks = new List<GameObject>(blendShapeScript.masksPrefab);
            masks.Remove(instantiatedObject);
            blendShapeScript.masksPrefab = masks.ToArray();

            instantiatedObject = null;
        }
        else
        {
            instantiatedObject = Instantiate(prefabToInstantiate, Vector3.zero, Quaternion.identity);

            blendShapeScript.avatarObject = instantiatedObject;

            SetInstantiatedObjectBlendShape();

            List<GameObject> masks = new List<GameObject>(blendShapeScript.masksPrefab);
            masks.Add(instantiatedObject);
            blendShapeScript.masksPrefab = masks.ToArray();

            SetAnimationParameters();

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


    private void SetAnimationParameters()
    {
        if (characterAnimator != null && instantiatedObject != null)
        {
            bool isWalking = characterAnimator.GetBool("isWalking");
            float normalizedTime = characterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            Animator maskAnimator = instantiatedObject.GetComponent<Animator>();

            if (maskAnimator != null)
            {
                maskAnimator.SetBool("isWalking", isWalking);
                maskAnimator.SetBool("isIdle", !isWalking);
                maskAnimator.Play("Base Layer." + (isWalking ? "Walk" : "Idle"), 0, normalizedTime);
            }
        }
    }

    public void NotifyAnimationState(bool isWalking)
    {
        if (instantiatedObject != null)
        {
            Animator maskAnimator = instantiatedObject.GetComponent<Animator>();
            if (maskAnimator != null)
            {
                maskAnimator.SetBool("isWalking", isWalking);
                maskAnimator.SetBool("isIdle", !isWalking);
            }
        }
    }
}

