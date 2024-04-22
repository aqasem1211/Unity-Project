using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    private GameObject instantiatedObject;
    public Animator characterAnimator;

    public UnityEvent onClothesInstantiated;

    private MasksController controller;

    private bool isAvatarWalking = false;
    private float avatarAnimationTime = 0f;

    private void Start()
    {
        controller = FindObjectOfType<MasksController>();

    }

    public void PrefabsInstantiate()
    {
        if (instantiatedObject != null)
        {
            ClearBones();
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

                if (onClothesInstantiated != null)
                    onClothesInstantiated.Invoke();

                UpdateBones();
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

    private void UpdateBones()
    {
        if (instantiatedObject != null && controller != null)
        {
            MasksController.BoneData[] boneData = controller.GetBoneData();
            foreach (MasksController.BoneData data in boneData)
            {
                Transform[] bones = FindBonesWithName(data.boneName);
                foreach (Transform bone in bones)
                {
                    if (data.boneInfoList.Count > 0)
                    {
                        MasksController.BoneData.BoneInfo boneInfo = data.boneInfoList[0];
                        if (boneInfo.scaleEnabled)
                        {
                            Vector3 targetScale = GetTargetScale(data, bone);
                            bone.localScale = targetScale;
                        }
                        if (boneInfo.positionEnabled)
                        {
                            Vector3 targetPosition = GetTargetPosition(data, bone);
                            bone.localPosition = targetPosition;
                        }
                    }
                }
            }
        }
    }

    private void ClearBones()
    {
        if (instantiatedObject != null)
        {
            foreach (Transform bone in instantiatedObject.transform)
            {
                bone.localScale = Vector3.one;
                bone.localPosition = Vector3.zero;
            }
        }
    }

    private Transform[] FindBonesWithName(string boneName)
    {
        List<Transform> bones = new List<Transform>();
        GameObject[] bonesInScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject go in bonesInScene)
        {
            Transform[] tempBones = go.GetComponentsInChildren<Transform>(true);
            foreach (Transform bone in tempBones)
            {
                if (bone.name == boneName)
                {
                    bones.Add(bone);
                }
            }
        }
        return bones.ToArray();
    }

    private Vector3 GetTargetScale(MasksController.BoneData data, Transform bone)
    {
        Vector3 scales;
        if (controller.isSmallSize)
        {
            scales = data.smallMaskBonesScale;
        }
        else if (controller.isMediumSize)
        {
            scales = data.mediumMaskBonesScale;
        }
        else
        {
            scales = data.largeMaskBonesScale;
        }

        Vector3 globalScale = bone.parent.lossyScale;
        return Vector3.Scale(scales, new Vector3(1.0f / globalScale.x, 1.0f / globalScale.y, 1.0f / globalScale.z));
    }

    private Vector3 GetTargetPosition(MasksController.BoneData data, Transform bone)
    {
        if (controller.isSmallSize)
        {
            return data.smallMaskBonesPosition;
        }
        else if (controller.isMediumSize)
        {
            return data.mediumMaskBonesPosition;
        }
        else
        {
            return data.largeMaskBonesPosition;
        }
    }
}
