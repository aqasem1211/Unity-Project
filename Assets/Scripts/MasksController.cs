using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    void Start()
    {
        BonesInScene();
        sizeSlider.onValueChanged.AddListener(OnSizeSliderChanged);
        SetSize(0);
    }

    void Update()
    {
        BoneScales();
    }

    public void UpdateBoneDictionary()
    {
        BonesInScene();
    }

    public void OnSizeSliderChanged(float value)
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
                if (!_groupedBones.ContainsKey(boneData.boneName))
                {
                    _groupedBones[boneData.boneName] = new List<Transform>();
                }
                _groupedBones[boneData.boneName].Add(bone);
            }
        }
    }

    private void BoneScales()
    {
        foreach (KeyValuePair<string, List<Transform>> entry in _groupedBones)
        {
            Vector3 scales;
            Vector3 positions;

            if (isSmallSize)
            {
                scales = _boneData[0].smallMaskBonesScale;
                positions = _boneData[0].smallMaskBonesPosition;
            }
            else if (isMediumSize)
            {
                scales = _boneData[0].mediumMaskBonesScale;
                positions = _boneData[0].mediumMaskBonesPosition;
            }
            else
            {
                scales = _boneData[0].largeMaskBonesScale;
                positions = _boneData[0].largeMaskBonesPosition;
            }

            Vector3 globalScale = entry.Value[0].parent.lossyScale;
            Vector3 targetScale = Vector3.Scale(scales, new Vector3(1.0f / globalScale.x, 1.0f / globalScale.y, 1.0f / globalScale.z));

            foreach (Transform bone in entry.Value)
            {
                if (bone == null) 
                    continue; 

                if (_boneData[0].boneInfoList[0].scaleEnabled)
                {
                    bone.localScale = Vector3.Lerp(bone.localScale, targetScale, Time.deltaTime * 5f);
                }

                if (_boneData[0].boneInfoList[0].positionEnabled)
                {
                    bone.localPosition = Vector3.Lerp(bone.localPosition, positions, Time.deltaTime * 5f);
                }
            }
        }
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
        GameObject[] bonesInScene = SceneManager.GetActiveScene().GetRootGameObjects();

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

    public BoneData[] GetBoneData()
    {
        return _boneData;
    }
}
