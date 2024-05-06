using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarAnimation : MonoBehaviour
{
    public Animator avatarAnimator;
    public Animator[] additionalAnimators;
    public Spawner[] spawners;

    private void Start()
    {
        avatarAnimator = GetComponent<Animator>();
        additionalAnimators = GetComponentsInChildren<Animator>();
    }

    public void CatwalkButtonClicked()
    {
        if (avatarAnimator != null)
        {
            avatarAnimator.SetBool("isWalking", true);
            avatarAnimator.SetBool("isIdle", false);

            foreach (Spawner spawner in spawners)
            {
                if (spawner != null)
                    spawner.NotifyAnimationState(true);
            }
        }

    }

    public void IdleButtonClicked()
    {
        if (avatarAnimator != null)
        {
            avatarAnimator.SetBool("isIdle", true);
            avatarAnimator.SetBool("isWalking", false);

            foreach (Spawner spawner in spawners)
            {
                if (spawner != null)
                    spawner.NotifyAnimationState(false);
            }
        }
    }

    public void IsAPoseClicked()
    {
        if(avatarAnimator != null)
        {
            avatarAnimator.SetBool("isAPose", true);
            avatarAnimator.SetBool("isIdle", false);
            avatarAnimator.SetBool("isWalking", false);

            //spawner.NotifyAnimationState(false);

        }

    }
}
