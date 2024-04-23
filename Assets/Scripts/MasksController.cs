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

        //[Space]
        //[Header("Bone Text Field")]
        //public TMP_Text boneScaleText;
        //public TMP_Text boneTransformText;

        [Space]
        [Header("Small Scale")]
        public Vector3 smallMaskBonesScale;
        public TMP_InputField smallScaleInputX;
        public TMP_InputField smallScaleInputY;
        public TMP_InputField smallScaleInputZ;

        [Space]
        [Header("Medium Scale")]
        public Vector3 mediumMaskBonesScale;
        public TMP_InputField mediumScaleInputX;
        public TMP_InputField mediumScaleInputY;
        public TMP_InputField mediumScaleInputZ;

        [Space]
        [Header("Large Scale")]
        public Vector3 largeMaskBonesScale;
        public TMP_InputField largeScaleInputX;
        public TMP_InputField largeScaleInputY;
        public TMP_InputField largeScaleInputZ;

        [Space]
        [Header("Small Transform")]
        public Vector3 smallMaskBonesPosition;
        public TMP_InputField smallTransformInputX;
        public TMP_InputField smallTransformInputY;
        public TMP_InputField smallTransformInputZ;

        [Space]
        [Header("Medium Transform")]
        public Vector3 mediumMaskBonesPosition;
        public TMP_InputField mediumTransformInputX;
        public TMP_InputField mediumTransformInputY;
        public TMP_InputField mediumTransformInputZ;

        [Space]
        [Header("Large Transform")]
        public Vector3 largeMaskBonesPosition;
        public TMP_InputField largeTransformInputX;
        public TMP_InputField largeTransformInputY;
        public TMP_InputField largeTransformInputZ;

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

    private void Start()
    {
        sizeSlider.onValueChanged.AddListener(OnSizeSliderChanged);
        SetSize(0);

        foreach (BoneData boneData in _boneData)
        {
            AddInputListeners(boneData);
            UpdateInputFields(boneData);
        }
    }

    private void Update()
    {
        BoneScales();
    }

    private void AddInputListeners(BoneData boneData)
    {
        if (boneData.smallScaleInputX != null)
            boneData.smallScaleInputX.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.smallMaskBonesScale, 0));
        if (boneData.smallScaleInputY != null)
            boneData.smallScaleInputY.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.smallMaskBonesScale, 1));
        if (boneData.smallScaleInputZ != null)
            boneData.smallScaleInputZ.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.smallMaskBonesScale, 2));

        if (boneData.mediumScaleInputX != null)
            boneData.mediumScaleInputX.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.mediumMaskBonesScale, 0));
        if (boneData.mediumScaleInputY != null)
            boneData.mediumScaleInputY.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.mediumMaskBonesScale, 1));
        if (boneData.mediumScaleInputZ != null)
            boneData.mediumScaleInputZ.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.mediumMaskBonesScale, 2));

        if (boneData.largeScaleInputX != null)
            boneData.largeScaleInputX.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.largeMaskBonesScale, 0));
        if (boneData.largeScaleInputY != null)
            boneData.largeScaleInputY.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.largeMaskBonesScale, 1));
        if (boneData.largeScaleInputZ != null)
            boneData.largeScaleInputZ.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.largeMaskBonesScale, 2));

        if (boneData.smallTransformInputX != null)
            boneData.smallTransformInputX.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.smallMaskBonesPosition, 0));
        if (boneData.smallTransformInputY != null)
            boneData.smallTransformInputY.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.smallMaskBonesPosition, 1));
        if (boneData.smallTransformInputZ != null)
            boneData.smallTransformInputZ.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.smallMaskBonesPosition, 2));

        if (boneData.mediumTransformInputX != null)
            boneData.mediumTransformInputX.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.mediumMaskBonesPosition, 0));
        if (boneData.mediumTransformInputY != null)
            boneData.mediumTransformInputY.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.mediumMaskBonesPosition, 1));
        if (boneData.mediumTransformInputZ != null)
            boneData.mediumTransformInputZ.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.mediumMaskBonesPosition, 2));

        if (boneData.largeTransformInputX != null)
            boneData.largeTransformInputX.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.largeMaskBonesPosition, 0));
        if (boneData.largeTransformInputY != null)
            boneData.largeTransformInputY.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.largeMaskBonesPosition, 1));
        if (boneData.largeTransformInputZ != null)
            boneData.largeTransformInputZ.onValueChanged.AddListener((value) => UpdateVector3FromInput(value, boneData.largeMaskBonesPosition, 2));
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
        if(boneData.smallScaleInputX != null)
            boneData.smallScaleInputX.text = boneData.smallMaskBonesScale.x.ToString();
        if (boneData.smallScaleInputY != null)
            boneData.smallScaleInputY.text = boneData.smallMaskBonesScale.y.ToString();
        if (boneData.smallScaleInputZ != null)
            boneData.smallScaleInputZ.text = boneData.smallMaskBonesScale.z.ToString();


        if (boneData.mediumScaleInputX != null)
            boneData.mediumScaleInputX.text = boneData.mediumMaskBonesScale.x.ToString();
        if (boneData.mediumScaleInputY != null)
            boneData.mediumScaleInputY.text = boneData.mediumMaskBonesScale.y.ToString();
        if (boneData.mediumScaleInputZ != null)
            boneData.mediumScaleInputZ.text = boneData.mediumMaskBonesScale.z.ToString();


        if (boneData.largeScaleInputX != null)
            boneData.largeScaleInputX.text = boneData.largeMaskBonesScale.x.ToString();
        if (boneData.largeScaleInputY != null)
            boneData.largeScaleInputY.text = boneData.largeMaskBonesScale.y.ToString();
        if (boneData.largeScaleInputZ != null)
            boneData.largeScaleInputZ.text = boneData.largeMaskBonesScale.z.ToString();

        if (boneData.smallTransformInputX != null)
            boneData.smallTransformInputX.text = boneData.smallMaskBonesPosition.x.ToString();
        if (boneData.smallTransformInputY != null)
            boneData.smallTransformInputY.text = boneData.smallMaskBonesPosition.y.ToString();
        if (boneData.smallTransformInputZ != null)
            boneData.smallTransformInputZ.text = boneData.smallMaskBonesPosition.z.ToString();

        if (boneData.mediumTransformInputX != null)
            boneData.mediumTransformInputX.text = boneData.mediumMaskBonesPosition.x.ToString();
        if (boneData.mediumTransformInputY != null)
            boneData.mediumTransformInputY.text = boneData.mediumMaskBonesPosition.y.ToString();
        if (boneData.mediumTransformInputZ != null)
            boneData.mediumTransformInputZ.text = boneData.mediumMaskBonesPosition.z.ToString();

        if (boneData.largeTransformInputX != null)
            boneData.largeTransformInputX.text = boneData.largeMaskBonesPosition.x.ToString();
        if (boneData.largeTransformInputY != null)
            boneData.largeTransformInputY.text = boneData.largeMaskBonesPosition.y.ToString();
        if (boneData.largeTransformInputZ != null)
            boneData.largeTransformInputZ.text = boneData.largeMaskBonesPosition.z.ToString();
    }

    private void OnSizeSliderChanged(float value)
    {
        SetSize(value);

        foreach (BoneData boneData in _boneData)
        {
            UpdateInputFields(boneData);
            UpdateBonePositions(boneData);
        }
    }

    private void UpdateBonePositions(BoneData boneData)
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
            
            boneData.smallMaskBonesScale.x = float.Parse(boneData.smallScaleInputX.text);
            boneData.smallMaskBonesScale.y = float.Parse(boneData.smallScaleInputY.text);
            boneData.smallMaskBonesScale.z = float.Parse(boneData.smallScaleInputZ.text);

            boneData.mediumMaskBonesScale.x = float.Parse(boneData.mediumScaleInputX.text);
            boneData.mediumMaskBonesScale.y = float.Parse(boneData.mediumScaleInputY.text);
            boneData.mediumMaskBonesScale.z = float.Parse(boneData.mediumScaleInputZ.text);

            boneData.largeMaskBonesScale.x = float.Parse(boneData.largeScaleInputX.text);
            boneData.largeMaskBonesScale.y = float.Parse(boneData.largeScaleInputY.text);
            boneData.largeMaskBonesScale.z = float.Parse(boneData.largeScaleInputZ.text);

            boneData.smallMaskBonesPosition.x = float.Parse(boneData.smallTransformInputX.text);
            boneData.smallMaskBonesPosition.y = float.Parse(boneData.smallTransformInputY.text);
            boneData.smallMaskBonesPosition.z = float.Parse(boneData.smallTransformInputZ.text);

            boneData.mediumMaskBonesPosition.x = float.Parse(boneData.mediumTransformInputX.text);
            boneData.mediumMaskBonesPosition.y = float.Parse(boneData.mediumTransformInputY.text);
            boneData.mediumMaskBonesPosition.z = float.Parse(boneData.mediumTransformInputZ.text);

            boneData.largeMaskBonesPosition.x = float.Parse(boneData.largeTransformInputX.text);
            boneData.largeMaskBonesPosition.y = float.Parse(boneData.largeTransformInputY.text);
            boneData.largeMaskBonesPosition.z = float.Parse(boneData.largeTransformInputZ.text);
        }

        foreach (BoneData boneData in _boneData)
        {
            UpdateBonePositions(boneData);
        }
       
    }
}
