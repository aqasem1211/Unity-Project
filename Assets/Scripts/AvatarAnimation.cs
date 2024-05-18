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
        if (avatarAnimator != null && spawners != null)
        {
            foreach (Spawner spawner in spawners)
            {
                if (spawner != null)
                {
                    spawner.NotifyAnimationState(true, false, false, false, false, false);
                }
            }
            avatarAnimator.SetBool("isWalking", true);
            avatarAnimator.SetBool("isWalking2", false);
            avatarAnimator.SetBool("isIdle", false);
            avatarAnimator.SetBool("isIdle2", false);
            avatarAnimator.SetBool("isApose", false);
            avatarAnimator.SetBool("isHighHeels", false);
        }
    }

    public void Catwalk2ButtonClicked()
    {
        if (avatarAnimator != null && spawners != null)
        {
            foreach (Spawner spawner in spawners)
            {
                if (spawner != null)
                {
                    spawner.NotifyAnimationState(false, true, false, false, false, false);
                }
            }
            avatarAnimator.SetBool("isWalking2", true);
            avatarAnimator.SetBool("isWalking", false);
            avatarAnimator.SetBool("isIdle", false);
            avatarAnimator.SetBool("isIdle2", false);
            avatarAnimator.SetBool("isApose", false);
            avatarAnimator.SetBool("isHighHeels", false);
        }
    }

    public void IdleButtonClicked()
    {
        if (avatarAnimator != null && spawners != null)
        {
            foreach (Spawner spawner in spawners)
            {
                if (spawner != null)
                {
                    spawner.NotifyAnimationState(false, false, true, false, false, false);
                }
            }
            avatarAnimator.SetBool("isIdle", true);
            avatarAnimator.SetBool("isWalking", false);
            avatarAnimator.SetBool("isWalking2", false);
            avatarAnimator.SetBool("isIdle2", false);
            avatarAnimator.SetBool("isApose", false);
            avatarAnimator.SetBool("isHighHeels", false);
        }
    }

    public void Idle2ButtonClicked()
    {
        if (avatarAnimator != null && spawners != null)
        {
            foreach (Spawner spawner in spawners)
            {
                if (spawner != null)
                {
                    spawner.NotifyAnimationState(false, false, false, true, false, false);
                }
            }
            avatarAnimator.SetBool("isIdle2", true);
            avatarAnimator.SetBool("isIdle", false);
            avatarAnimator.SetBool("isWalking", false);
            avatarAnimator.SetBool("isWalking2", false);
            avatarAnimator.SetBool("isApose", false);
            avatarAnimator.SetBool("isHighHeels", false);
        }
    }

    public void HighHeelsButtonClicked()
    {
        if (avatarAnimator != null && spawners != null)
        {
            foreach (Spawner spawner in spawners)
            {
                if (spawner != null)
                {
                    spawner.NotifyAnimationState(false, false, false, false, false, true);
                }
            }
            avatarAnimator.SetBool("isHighHeels", true);
            avatarAnimator.SetBool("isIdle", false);
            avatarAnimator.SetBool("isWalking", false);
            avatarAnimator.SetBool("isWalking2", false);
            avatarAnimator.SetBool("isIdle2", false);
            avatarAnimator.SetBool("isApose", false);
        }
    }

    public void AposeButtonClicked()
    {
        if (avatarAnimator != null && spawners != null)
        {
            foreach (Spawner spawner in spawners)
            {
                if (spawner != null)
                {
                    spawner.NotifyAnimationState(false, false, false, false, true, false);
                }
            }
            avatarAnimator.SetBool("isApose", true);
            avatarAnimator.SetBool("isIdle", false);
            avatarAnimator.SetBool("isWalking", false);
            avatarAnimator.SetBool("isWalking2", false);
            avatarAnimator.SetBool("isIdle2", false);
            avatarAnimator.SetBool("isHighHeels", false);
        }
    }
}
