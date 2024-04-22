using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasksController : MonoBehaviour
{
    [System.Serializable]
    public class BoneData
    {
        public string boneName;
        public Vector3 smallMaskBonesScale;
        public Vector3 mediumMaskBonesScale;
        public Vector3 largeMaskBonesScale;

        public Vector3 smallMaskBonesPosition;
        public Vector3 mediumMaskBonesPosition;
        public Vector3 largeMaskBonesPosition;

        [System.Serializable]
        public class BoneInfo
        {
            public bool scaleEnabled;
            public bool positionEnabled;
        }

        public List<BoneInfo> boneInfoList = new List<BoneInfo>();
    }

    [SerializeField] private BoneData[] _boneData;
    private Dictionary<string, List<Transform>> _groupedBones = new Dictionary<string, List<Transform>>();

    public bool isSmallSize = true;
    public bool isMediumSize;

    public Slider sizeSlider;
    public Transform avatar;
    public GameObject instantiatedObjectPrefab;
    private GameObject instantiatedObject;

    private void Start()
    {
        sizeSlider.onValueChanged.AddListener(OnSizeSliderChanged);

        SetSize(0);
    }

    private void Update()
    {
        BoneScales();
    }

    private void OnSizeSliderChanged(float value)
    {
        SetSize(value);
    }

    private void BonesInScene()
    {
        _groupedBones.Clear();

        foreach (BoneData boneData in _boneData)
        {
            Transform[] bones = FindBonesWithName(boneData.boneName);
            foreach (Transform bone in bones)
            {
                string boneName = bone.name;
                if (!_groupedBones.ContainsKey(boneName))
                {
                    _groupedBones[boneName] = new List<Transform>();
                }
                _groupedBones[boneName].Add(bone);
            }
        }
    }

    private void BoneScales()
    {
        foreach (BoneData boneData in _boneData)
        {
            Transform[] bones = FindBonesWithName(boneData.boneName);
            Vector3 scales;
            Vector3 positions;

            if (isSmallSize)
            {
                scales = boneData.smallMaskBonesScale;
                positions = boneData.smallMaskBonesPosition;
            }
            else if (isMediumSize)
            {
                scales = boneData.mediumMaskBonesScale;
                positions = boneData.mediumMaskBonesPosition;
            }
            else
            {
                scales = boneData.largeMaskBonesScale;
                positions = boneData.largeMaskBonesPosition;
            }

            foreach (Transform bone in bones)
            {
                if (bone == null)
                    continue;

                if (ShouldScaleBone(boneData, bone.name))
                {
                    Vector3 globalScale = bone.parent.lossyScale;
                    Vector3 targetScale = Vector3.Scale(scales, new Vector3(1.0f / globalScale.x, 1.0f / globalScale.y, 1.0f / globalScale.z));

                    bone.localScale = Vector3.Lerp(bone.localScale, targetScale, Time.deltaTime * 5f);
                }

                int index = FindBoneIndex(boneData, bone.name);
                if (index != -1 && boneData.boneInfoList[index].positionEnabled)
                {
                    bone.localPosition = Vector3.Lerp(bone.localPosition, positions, Time.deltaTime * 5f);
                }
            }
        }
    }

    private bool ShouldScaleBone(BoneData boneData, string boneName)
    {
        if ((isSmallSize && boneData.smallMaskBonesScale != Vector3.zero) ||
            (isMediumSize && boneData.mediumMaskBonesScale != Vector3.zero) ||
            (!isSmallSize && !isMediumSize && boneData.largeMaskBonesScale != Vector3.zero))
        {
            int index = FindBoneIndex(boneData, boneName);
            return index != -1 && boneData.boneInfoList[index].scaleEnabled;
        }

        return false;
    }


    private int FindBoneIndex(BoneData boneData, string boneName)
    {
        for (int i = 0; i < boneData.boneInfoList.Count; i++)
        {
            if (boneData.boneInfoList[i].scaleEnabled && boneData.boneName == boneName)
            {
                return i;
            }
        }
        return -1;
    }

    public void SetSize(float value)
    {
        if (value < 0.33f)
        {
            isSmallSize = true;
            isMediumSize = false;
        }
        else if (value < 0.66f)
        {
            isSmallSize = false;
            isMediumSize = true;
        }
        else
        {
            isSmallSize = false;
            isMediumSize = false;
        }
    }

    private Transform[] FindBonesWithName(string boneName)
    {
        List<Transform> bones = new List<Transform>();
        GameObject[] bonesInScene = FindObjectsOfType<GameObject>();

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

    public void InstantiateObject()
    {
        if (instantiatedObjectPrefab != null)
        {
            instantiatedObject = Instantiate(instantiatedObjectPrefab, avatar);
            instantiatedObject.transform.localScale = avatar.localScale;
        }
    }
}
