using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MasksController : MonoBehaviour
{
    [System.Serializable]
    public class BoneData
    {
        [Space]
        [Header("Bone Name")]
        public string boneName;


        [Space]
        [Header("Scale")]
        public Vector3 smallMaskBonesScale;
        public Vector3 mediumMaskBonesScale;
        public Vector3 largeMaskBonesScale;
        public TMP_InputField scaleInputX;
        public TMP_InputField scaleInputY;
        public TMP_InputField scaleInputZ;

        [Space]
        [Header("Transform")]
        public Vector3 smallMaskBonesPosition;
        public Vector3 mediumMaskBonesPosition;
        public Vector3 largeMaskBonesPosition;
        public TMP_InputField transformInputX;
        public TMP_InputField transformInputY;
        public TMP_InputField transformInputZ;

        [Space]
        [Header("isEnabled?")]
        public bool scaleEnabled;
        public bool positionEnabled;
    }

    [SerializeField] private BoneData[] _boneData;
    private Dictionary<string, List<Transform>> _groupedBones = new Dictionary<string, List<Transform>>();

    public bool isSmallSize = true;
    public bool isMediumSize;

    public Slider sizeSlider;
    public Transform avatar;
    public GameObject instantiatedObjectPrefab;
    private GameObject instantiatedObject;

    public BoneData[] GetBoneData()
    {
        return _boneData;
    }

    private void Start()
    {
        sizeSlider.onValueChanged.AddListener(OnSizeSliderChanged);
        SetSize(0);

        foreach (BoneData boneData in _boneData)
        {
            AddInputListeners(boneData);
            UpdateInputFields(boneData);

            boneData.scaleInputX.text = boneData.smallMaskBonesScale.x.ToString();
            boneData.scaleInputY.text = boneData.smallMaskBonesScale.y.ToString();
            boneData.scaleInputZ.text = boneData.smallMaskBonesScale.z.ToString();

            boneData.transformInputX.text = boneData.smallMaskBonesPosition.x.ToString();
            boneData.transformInputY.text = boneData.smallMaskBonesPosition.y.ToString();
            boneData.transformInputZ.text = boneData.smallMaskBonesPosition.z.ToString();
        }
    }

    private void Update()
    {
        BoneScales();
    }

    private void AddInputListeners(BoneData boneData)
    {
        if (boneData.scaleInputX != null)
            boneData.scaleInputX.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.largeMaskBonesScale, 0));
        if (boneData.scaleInputY != null)
            boneData.scaleInputY.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.largeMaskBonesScale, 1));
        if (boneData.scaleInputZ != null)
            boneData.scaleInputZ.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.largeMaskBonesScale, 2));

        if (boneData.transformInputX != null)
            boneData.transformInputX.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.largeMaskBonesPosition, 0));
        if (boneData.transformInputY != null)
            boneData.transformInputY.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.largeMaskBonesPosition, 1));
        if (boneData.transformInputZ != null)
            boneData.transformInputZ.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.largeMaskBonesPosition, 2));
    }


    private void UpdateVector3FromInput(string value, Vector3 vector, int index)
    {
        float parsedValue;
        if (float.TryParse(value, out parsedValue))
        {
            vector[index] = parsedValue;
        }
    }

    private void UpdateInputFields(BoneData boneData)
    {
        if (boneData.scaleInputX != null)
            boneData.scaleInputX.text = boneData.smallMaskBonesScale.x.ToString();
        if (boneData.scaleInputY != null)
            boneData.scaleInputY.text = boneData.smallMaskBonesScale.y.ToString();
        if (boneData.scaleInputZ != null)
            boneData.scaleInputZ.text = boneData.smallMaskBonesScale.z.ToString();

        if (boneData.scaleInputX != null)
            boneData.scaleInputX.text = boneData.mediumMaskBonesScale.x.ToString();
        if (boneData.scaleInputY != null)
            boneData.scaleInputY.text = boneData.mediumMaskBonesScale.y.ToString();
        if (boneData.scaleInputZ != null)
            boneData.scaleInputZ.text = boneData.mediumMaskBonesScale.z.ToString();

        if (boneData.scaleInputX != null)
            boneData.scaleInputX.text = boneData.largeMaskBonesScale.x.ToString();
        if (boneData.scaleInputY != null)
            boneData.scaleInputY.text = boneData.largeMaskBonesScale.y.ToString();
        if (boneData.scaleInputZ != null)
            boneData.scaleInputZ.text = boneData.largeMaskBonesScale.z.ToString();

        if (boneData.transformInputX != null)
            boneData.transformInputX.text = boneData.smallMaskBonesPosition.x.ToString();
        if (boneData.transformInputY != null)
            boneData.transformInputY.text = boneData.smallMaskBonesPosition.y.ToString();
        if (boneData.transformInputZ != null)
            boneData.transformInputZ.text = boneData.smallMaskBonesPosition.z.ToString();

        if (boneData.transformInputX != null)
            boneData.transformInputX.text = boneData.mediumMaskBonesPosition.x.ToString();
        if (boneData.transformInputY != null)
            boneData.transformInputY.text = boneData.mediumMaskBonesPosition.y.ToString();
        if (boneData.transformInputZ != null)
            boneData.transformInputZ.text = boneData.mediumMaskBonesPosition.z.ToString();

        if (boneData.transformInputX != null)
            boneData.transformInputX.text = boneData.largeMaskBonesPosition.x.ToString();
        if (boneData.transformInputY != null)
            boneData.transformInputY.text = boneData.largeMaskBonesPosition.y.ToString();
        if (boneData.transformInputZ != null)
            boneData.transformInputZ.text = boneData.largeMaskBonesPosition.z.ToString();
    }

    private void OnSizeSliderChanged(float value)
    {
        SetSize(value);

        foreach (BoneData boneData in _boneData)
        {
            UpdateInputFields(boneData);
            UpdateBonePositions(boneData);

            if (isSmallSize)
            {
                boneData.scaleInputX.text = boneData.smallMaskBonesScale.x.ToString();
                boneData.scaleInputY.text = boneData.smallMaskBonesScale.y.ToString();
                boneData.scaleInputZ.text = boneData.smallMaskBonesScale.z.ToString();

                boneData.transformInputX.text = boneData.smallMaskBonesPosition.x.ToString();
                boneData.transformInputY.text = boneData.smallMaskBonesPosition.y.ToString();
                boneData.transformInputZ.text = boneData.smallMaskBonesPosition.z.ToString();
            }
            else if (isMediumSize)
            {
                boneData.scaleInputX.text = boneData.mediumMaskBonesScale.x.ToString();
                boneData.scaleInputY.text = boneData.mediumMaskBonesScale.y.ToString();
                boneData.scaleInputZ.text = boneData.mediumMaskBonesScale.z.ToString();

                boneData.transformInputX.text = boneData.mediumMaskBonesPosition.x.ToString();
                boneData.transformInputY.text = boneData.mediumMaskBonesPosition.y.ToString();
                boneData.transformInputZ.text = boneData.mediumMaskBonesPosition.z.ToString();
            }
            else
            {
                boneData.scaleInputX.text = boneData.largeMaskBonesScale.x.ToString();
                boneData.scaleInputY.text = boneData.largeMaskBonesScale.y.ToString();
                boneData.scaleInputZ.text = boneData.largeMaskBonesScale.z.ToString();

                boneData.transformInputX.text = boneData.largeMaskBonesPosition.x.ToString();
                boneData.transformInputY.text = boneData.largeMaskBonesPosition.y.ToString();
                boneData.transformInputZ.text = boneData.largeMaskBonesPosition.z.ToString();
            }
        }
    }


    public void UpdateBonePositions(BoneData boneData)
    {
        foreach (Transform bone in FindBonesWithName(boneData.boneName))
        {
            if (bone == null)
                continue;

            Vector3 newPosition = isSmallSize ? boneData.smallMaskBonesPosition :
                                  (isMediumSize ? boneData.mediumMaskBonesPosition : boneData.largeMaskBonesPosition);

            bone.localPosition = newPosition;
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

                if (boneData.positionEnabled)
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
            return boneData.scaleEnabled;
        }

        return false;
    }

    private int FindBoneIndex(BoneData boneData, string boneName)
    {
        //for (int i = 0; i < boneData.boneInfoList.Count; i++)
        //{
        //    if (boneData.boneInfoList[i].scaleEnabled && boneData.boneName == boneName)
        //    {
        //        return i;
        //    }
        //}
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

            foreach (BoneData boneData in _boneData)
            {
                UpdateBonePositions(boneData);
            }
        }
    }


    public void ApplyInputValues()
    {
        foreach (BoneData boneData in _boneData)
        {

            //boneData.smallMaskBonesScale.x = float.Parse(boneData.scaleInputX.text);
            //boneData.smallMaskBonesScale.y = float.Parse(boneData.scaleInputY.text);
            //boneData.smallMaskBonesScale.z = float.Parse(boneData.scaleInputZ.text);

            //boneData.mediumMaskBonesScale.x = float.Parse(boneData.scaleInputX.text);
            //boneData.mediumMaskBonesScale.y = float.Parse(boneData.scaleInputY.text);
            //boneData.mediumMaskBonesScale.z = float.Parse(boneData.scaleInputZ.text);

            boneData.largeMaskBonesScale.x = float.Parse(boneData.scaleInputX.text);
            boneData.largeMaskBonesScale.y = float.Parse(boneData.scaleInputY.text);
            boneData.largeMaskBonesScale.z = float.Parse(boneData.scaleInputZ.text);

            //boneData.smallMaskBonesPosition.x = float.Parse(boneData.transformInputX.text);
            //boneData.smallMaskBonesPosition.y = float.Parse(boneData.transformInputY.text);
            //boneData.smallMaskBonesPosition.z = float.Parse(boneData.transformInputZ.text);

            //boneData.mediumMaskBonesPosition.x = float.Parse(boneData.transformInputX.text);
            //boneData.mediumMaskBonesPosition.y = float.Parse(boneData.transformInputY.text);
            //boneData.mediumMaskBonesPosition.z = float.Parse(boneData.transformInputZ.text);

            boneData.largeMaskBonesPosition.x = float.Parse(boneData.transformInputX.text);
            boneData.largeMaskBonesPosition.y = float.Parse(boneData.transformInputY.text);
            boneData.largeMaskBonesPosition.z = float.Parse(boneData.transformInputZ.text);
        }

        foreach (BoneData boneData in _boneData)
        {
            UpdateBonePositions(boneData);
        }
    }
}
