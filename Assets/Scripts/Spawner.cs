using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MasksController;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    private GameObject instantiatedObject;
    public Animator characterAnimator;
    public GameObject objectToStart;

    private bool isAvatarWalking = false;
    private float avatarAnimationTime = 0f;

    private void Start()
    {
        instantiatedObject = Instantiate(objectToStart, Vector3.zero, Quaternion.identity);

    }

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

            Animator maskAnimator = instantiatedObject.GetComponent<Animator>();

            if (characterAnimator != null && maskAnimator != null)
            {
                maskAnimator.SetBool("isWalking", isAvatarWalking);
                maskAnimator.SetBool("isIdle", !isAvatarWalking);

                maskAnimator.Play("Base Layer." + (isAvatarWalking ? "Walk" : "Idle"), 0, avatarAnimationTime);
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
