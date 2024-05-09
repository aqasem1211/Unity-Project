using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarAnimation : MonoBehaviour
{
    public Animator avatarAnimator;
    public Animator[] additionalAnimators;
    private Spawner[] spawners;


    private void Start()
    {
        avatarAnimator = GetComponent<Animator>();
        if (avatarAnimator == null)
        {
            avatarAnimator = FindObjectOfType<Animator>();
        }

        additionalAnimators = GetComponentsInChildren<Animator>();

        spawners = FindObjectsOfType<Spawner>();

        BlendShape blendShape = FindObjectOfType<BlendShape>();
        if (blendShape != null)
        {
            blendShape.slider.onValueChanged.AddListener(OnSliderValueChanged);
        }
        UpdateAnimationParameters();
    }


    private void OnSliderValueChanged(float value)
    {
        UpdateAnimationParameters();
    }

    public void UpdateAnimationParameters()
    {
        if (avatarAnimator == null)
        {
            return;
        }

        float sliderValue = FindObjectOfType<BlendShape>().slider.value;
        bool isXL = sliderValue >= 0.5f;
        AnimatorStateInfo stateInfo = avatarAnimator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("isWalking") && isXL)
        {
            avatarAnimator.SetBool("isWalkingXL", true);
        }
        else
        {
            avatarAnimator.SetBool("isWalkingXL", false);
        }

        if (stateInfo.IsName("isIdle") && isXL)
        {
            avatarAnimator.SetBool("isIdleXL", true);
        }
        else
        {
            avatarAnimator.SetBool("isIdleXL", false);
        }

        if (stateInfo.IsName("isApose") && isXL)
        {
            avatarAnimator.SetBool("isAposeXL", true);
        }
        else
        {
            avatarAnimator.SetBool("isAposeXL", false);
        }

        foreach (Spawner spawner in spawners)
        {
            spawner.ApplyAvatarAnimationState();
        }
    }

    public void CatwalkButtonClicked()
    {
        if (avatarAnimator != null)
        {
            bool isXL = FindObjectOfType<BlendShape>().slider.value >= 0.5f;
            bool isIdle = false;
            SetAnimationParameters(isWalking: true, isIdle: isIdle, isApose: false, isNormal: true, isXL: isXL);

            foreach (Spawner spawner in spawners)
            {
                spawner.SetInstantiatedObjectAnimationState();
                spawner.ApplyAvatarAnimationState();
            }
        }
    }

    public void IdleButtonClicked()
    {
        if (avatarAnimator != null)
        {
            bool isXL = FindObjectOfType<BlendShape>().slider.value >= 0.5f;
            bool isIdle = true;
            SetAnimationParameters(isWalking: false, isIdle: isIdle, isApose: false, isNormal: true, isXL: isXL);

            foreach (Spawner spawner in spawners)
            {
                spawner.SetInstantiatedObjectAnimationState();
                spawner.ApplyAvatarAnimationState();
            }
        }
    }

    public void AposeButtonClicked()
    {
        bool isXL = FindObjectOfType<BlendShape>().slider.value >= 0.5f;
        bool isIdle = false;
        SetAnimationParameters(isWalking: false, isIdle: isIdle, isApose: true, isNormal: true, isXL: isXL);

        foreach (Spawner spawner in spawners)
        {
            spawner.SetInstantiatedObjectAnimationState();
            spawner.ApplyAvatarAnimationState();
        }
    }

    private void SetAnimationParameters(bool isWalking, bool isIdle, bool isApose, bool isNormal, bool isXL)
    {
        float sliderValue = FindObjectOfType<BlendShape>().slider.value;

        if (sliderValue >= 0.5f)
        {
            avatarAnimator.SetBool("isWalkingXL", isWalking && isXL);
            avatarAnimator.SetBool("isIdleXL", isIdle && isXL);
            avatarAnimator.SetBool("isAposeXL", isApose && isXL);

            avatarAnimator.SetBool("isWalking", false);
            avatarAnimator.SetBool("isIdle", false);
            avatarAnimator.SetBool("isApose", false);
        }
        else
        {
            avatarAnimator.SetBool("isWalking", isWalking && isNormal);
            avatarAnimator.SetBool("isIdle", isIdle && isNormal);
            avatarAnimator.SetBool("isApose", isApose && isNormal);

            avatarAnimator.SetBool("isWalkingXL", false);
            avatarAnimator.SetBool("isIdleXL", false);
            avatarAnimator.SetBool("isAposeXL", false);
        }
    }
    private void ForceSetAnimationParameters(bool isWalking, bool isIdle, bool isApose, bool isNormal, bool isXL)
    {
        if (isXL)
        {
            avatarAnimator.SetBool("isWalkingXL", isWalking);
            avatarAnimator.SetBool("isIdleXL", isIdle);
            avatarAnimator.SetBool("isAposeXL", isApose);
        }
        else
        {
            avatarAnimator.SetBool("isWalking", isWalking && isNormal);
            avatarAnimator.SetBool("isIdle", isIdle && isNormal);
            avatarAnimator.SetBool("isApose", isApose && isNormal);
        }
    }

    public void ApplyButtonClicked()
    {
        Debug.Log("ApplyButtonClicked() called");
        bool isIdle = false;
        ForceSetAnimationParameters(isWalking: false, isIdle: isIdle, isApose: true, isNormal: true, isXL: true);
    }



}
