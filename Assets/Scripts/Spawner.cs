using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    private GameObject instantiatedObject;
    public Animator characterAnimator;
    public BlendShape blendShapeScript;
    public RotateObjects rotateObjectsScript;

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
        if (characterAnimator == null || instantiatedObject == null)
            return;

        bool isWalking = characterAnimator.GetBool("isWalking");
        bool isWalking2 = characterAnimator.GetBool("isWalking2");
        bool isIdle = characterAnimator.GetBool("isIdle");
        bool isIdle2 = characterAnimator.GetBool("isIdle2");
        bool isApose = characterAnimator.GetBool("isApose");
        bool isHighHeels = characterAnimator.GetBool("isHighHeels");
        float normalizedTime = characterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;

        Animator maskAnimator = instantiatedObject.GetComponent<Animator>();

        if (maskAnimator != null)
        {
            UpdateAnimatorParameters(maskAnimator, isWalking, isWalking2, isIdle, isIdle2, isApose, isHighHeels, normalizedTime);
        }
    }

    private void UpdateAnimatorParameters(Animator maskAnimator, bool isWalking, bool isWalking2, bool isIdle, bool isIdle2, bool isApose, bool isHighHeels, float normalizedTime = 0f)
    {
        maskAnimator.SetBool("isWalking", isWalking);
        maskAnimator.SetBool("isWalking2", isWalking2);
        maskAnimator.SetBool("isIdle", isIdle);
        maskAnimator.SetBool("isIdle2", isIdle2);
        maskAnimator.SetBool("isApose", isApose);
        maskAnimator.SetBool("isHighHeels", isHighHeels);

        string animationName = GetAnimationName(isWalking, isWalking2, isIdle, isIdle2, isApose, isHighHeels);

        AnimatorStateInfo avatarStateInfo = maskAnimator.GetCurrentAnimatorStateInfo(0);

        maskAnimator.Play(avatarStateInfo.fullPathHash, -1, avatarStateInfo.normalizedTime);
    }

    private string GetAnimationName(bool isWalking, bool isWalking2, bool isIdle, bool isIdle2, bool isApose, bool isHighHeels)
    {
        if (isWalking)
            return "Walk";
        else if (isWalking2)
            return "Walk2";
        else if (isIdle)
            return "Idle";
        else if (isIdle2)
            return "Idle2";
        else if (isApose)
            return "Apose";
        else if (isHighHeels)
            return "HighHeels";
        else
            return "Apose";
    }

    public void NotifyAnimationState(bool isWalking, bool isWalking2, bool isIdle, bool isIdle2, bool isApose, bool isHighHeels)
    {
        if (instantiatedObject == null)
            return;

        Animator maskAnimator = instantiatedObject.GetComponent<Animator>();

        if (maskAnimator != null)
        {
            UpdateAnimatorParameters(maskAnimator, isWalking, isWalking2, isIdle, isIdle2, isApose, isHighHeels);
        }
    }
}
