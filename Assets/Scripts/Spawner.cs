using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    private GameObject instantiatedObject;
    public Animator characterAnimator;

    private bool isAvatarWalking = false;
    private float avatarAnimationTime = 0f;


    public void PrefabsInstantiate()
    {
        if (instantiatedObject != null)
        {
            Destroy(instantiatedObject);
            instantiatedObject = null;
        }
        else
        {
            isAvatarWalking = characterAnimator.GetBool("isWalking");
            avatarAnimationTime = characterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            instantiatedObject = Instantiate(prefabToInstantiate, Vector3.zero, Quaternion.identity);

            Animator pantsAnimator = instantiatedObject.GetComponent<Animator>();

            if (characterAnimator != null && pantsAnimator != null)
            {
                pantsAnimator.SetBool("isWalking", isAvatarWalking);
                pantsAnimator.SetBool("isIdle", !isAvatarWalking);

                pantsAnimator.Play("Base Layer." + (isAvatarWalking ? "Walk" : "Idle"), 0, avatarAnimationTime);
            }
        }
    }

    public void NotifyAnimationState(bool isWalking)
    {
        if (instantiatedObject != null)
        {
            Animator pantsAnimator = instantiatedObject.GetComponent<Animator>();
            if (pantsAnimator != null)
            {
                pantsAnimator.SetBool("isWalking", isWalking);
                pantsAnimator.SetBool("isIdle", !isWalking);
            }
        }
    }
}
