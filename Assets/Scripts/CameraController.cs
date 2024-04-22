using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public GameObject lookAt;

    public float rotationSpeed = 5f;
    public float transitionDuration = 2f;

    public Vector3 targetPosition;
    private Quaternion targetRotation;
    private Vector3 targetLookAtPosition;
    private float transitionTimer;

    private bool isTransitioning = false;

    void Start()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();


    }

    void Update()
    {
        if (isTransitioning)
        {
            float interpolationFactor = Mathf.Clamp01(transitionTimer / transitionDuration);

            virtualCamera.transform.position = Vector3.Lerp(virtualCamera.transform.position, targetPosition, interpolationFactor);
            virtualCamera.transform.rotation = Quaternion.Slerp(virtualCamera.transform.rotation, targetRotation, interpolationFactor);
            lookAt.transform.position = Vector3.Lerp(lookAt.transform.position, targetLookAtPosition, interpolationFactor);

            transitionTimer += Time.deltaTime;

            if (transitionTimer >= transitionDuration)
            {
                isTransitioning = false;
                transitionTimer = 0f;
            }
        }
    }


    public void MoveCameraToMainLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.8f, 1.8f, 1.8f);
        targetRotation = Quaternion.Euler(22.75f, -157, 0);
        targetLookAtPosition = new Vector3(0, 0.75f, 0);

        isTransitioning = true;
    }

    public void MoveCameraToHairLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.42f, 1.78f, 0.662f);
        targetRotation = Quaternion.Euler(17.4f, -157.2f, 0f);
        targetLookAtPosition = new Vector3(0, 1.25f, -0.25f);

        isTransitioning = true;
    }
    public void MoveCameraToButtLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.8f, 1.8f, -1.452f);
        targetRotation = Quaternion.Euler(17.4f, -157.2f, 0f);
        targetLookAtPosition = new Vector3(0, 1.25f, -0.25f);

        isTransitioning = true;
    }

    public void MoveCameraToOuterwearLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.6f, 1.75f, 1.1f);
        targetRotation = Quaternion.Euler(18.6f, -159, 0);
        targetLookAtPosition = new Vector3(0.1f, 1.1f, -0.1f);

        isTransitioning = true;
    }

    public void MoveCameraToTopsLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.6f, 1.75f, 1.1f);
        targetRotation = Quaternion.Euler(18.6f, -159, 0);
        targetLookAtPosition = new Vector3(0.1f, 1.1f, -0.1f);

        isTransitioning = true;
    }

    public void MoveCameraToDressesLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.8f, 1.8f, 1.65f);
        targetRotation = Quaternion.Euler(24.1f, -154, 0);
        targetLookAtPosition = new Vector3(-0.05f, 0.74f, 0);

        isTransitioning = true;
    }

    public void MoveCameraToUnderwearLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.51f, 1.489f, 0.8f);
        targetRotation = Quaternion.Euler(20.768f, -150.186f, 0);
        targetLookAtPosition = new Vector3(0, 0.91f, 0);

        isTransitioning = true;
    }

    public void MoveCameraToPantsLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.722f, 1.261f, 1.163f);
        targetRotation = Quaternion.Euler(25.216f, -150.049f, 0);
        targetLookAtPosition = new Vector3(0, 0.39f, 0);

        isTransitioning = true;
    }

    public void MoveCameraToBootsLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.366f, 0.57f, 0.984f);
        targetRotation = Quaternion.Euler(16.195f, -167.099f, 0f);
        targetLookAtPosition = new Vector3(0.12f, 0.06f, 0);

        isTransitioning = true;
    }
    public void MoveCameraToShouldersLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.137f, 1.791f, 1.261f);
        targetRotation = Quaternion.Euler(19.505f, -174.21f, 0f);
        targetLookAtPosition = new Vector3(0, 1.12f, 0);

        isTransitioning = true;
    }
    public void MoveCameraToArmsLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.844f, 1.69f, 0.974f);
        targetRotation = Quaternion.Euler(20.212f, -141.577f, 0f);
        targetLookAtPosition = new Vector3(0, 1f, 0);

        isTransitioning = true;
    }
    public void MoveCameraToFrontShouldersLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.162f, 1.668f, 0.672f);
        targetRotation = Quaternion.Euler(16.313f, -167.998f, 0f);
        targetLookAtPosition = new Vector3(0, 1.25f, 0);

        isTransitioning = true;
    }
    public void MoveCameraToBustLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.189f, 1.673f, 1.14f);
        targetRotation = Quaternion.Euler(20.54f, -174.474f, 0f);
        targetLookAtPosition = new Vector3(0.07f, 1.02f, 0);

        isTransitioning = true;
    }
    public void MoveCameraToAboveBustLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.517f, 1.556f, 0.613f);
        targetRotation = Quaternion.Euler(16.531f, -143.668f, 0f);
        targetLookAtPosition = new Vector3(0, 1.107f, 0);

        isTransitioning = true;
    }
    public void MoveCameraToFullBustLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.506f, 1.558f, 0.565f);
        targetRotation = Quaternion.Euler(20.168f, -142.313f, 0f);
        targetLookAtPosition = new Vector3(0, 1, 0);

        isTransitioning = true;
    }

    public void MoveCameraToUnderBustLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.532f, 1.387f, 0.517f);
        targetRotation = Quaternion.Euler(14.584f, -138.767f, 0f);
        targetLookAtPosition = new Vector3(0, 0.987f, 0);

        isTransitioning = true;
    }
    public void MoveCameraToWaistLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.346f, 1.708f, 1.324f);
        targetRotation = Quaternion.Euler(19.588f, -166.25f, 0f);
        targetLookAtPosition = new Vector3(0, 1, 0);

        isTransitioning = true;
    }
    public void MoveCameraToHipsLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.125f, 1.402f, 1.125f);
        targetRotation = Quaternion.Euler(16.612f, -176.938f, 0f);
        targetLookAtPosition = new Vector3(0.06f, 0.849f, 0);

        isTransitioning = true;
    }
    public void MoveCameraToHighHipLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.622f, 1.319f, 0.667f);
        targetRotation = Quaternion.Euler(15.949f, -140.591f, 0f);
        targetLookAtPosition = new Vector3(0, 0.849f, 0);

        isTransitioning = true;
    }
    public void MoveCameraToLowHipLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.622f, 1.319f, 0.667f);
        targetRotation = Quaternion.Euler(15.949f, -140.591f, 0f);
        targetLookAtPosition = new Vector3(0, 0.78f, 0);

        isTransitioning = true;
    }

    public void MoveCameraToThighLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.26f, 1.09f, 1.264f);
        targetRotation = Quaternion.Euler(15.95f, -172.844f, 0f);
        targetLookAtPosition = new Vector3(0.09f, 0.51f, 0);

        isTransitioning = true;
    }
    public void MoveCameraToHighThighLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.751f, 1.09f, 0.773f);
        targetRotation = Quaternion.Euler(18.824f, -138.97f, 0f);
        targetLookAtPosition = new Vector3(0, 0.51f, 0);

        isTransitioning = true;
    }
    public void MoveCameraToMedThighLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.751f, 1.09f, 0.773f);
        targetRotation = Quaternion.Euler(18.824f, -138.97f, 0f);
        targetLookAtPosition = new Vector3(0, 0.38f, 0);

        isTransitioning = true;
    }
    public void MoveCameraToCalfsLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.18f, 0.959f, 1.231f);
        targetRotation = Quaternion.Euler(19.352f, -179.133f, 0f);
        targetLookAtPosition = new Vector3(0.16f, 0.305f, 0);

        isTransitioning = true;
    }
    public void MoveCameraToArmscyeLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.14f, 1.58f, 0.46f);
        targetRotation = Quaternion.Euler(21.749f, -175.84f, 0f);
        targetLookAtPosition = new Vector3(0.1f, 1.17f, 0);

        isTransitioning = true;
    }

    public void MoveCameraToUpperArmLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.718f, 1.575f, 1.007f);
        targetRotation = Quaternion.Euler(13.532f, -154.558f, 0f);
        targetLookAtPosition = new Vector3(0.12f, 1.05f, -0.16f);

        isTransitioning = true;
    }
    public void MoveCameraToForearmLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.738f, 1.624f, 0.83f);
        targetRotation = Quaternion.Euler(25.099f, -148.762f, 0f);
        targetLookAtPosition = new Vector3(0.18f, 0.93f, 0);

        isTransitioning = true;
    }

    public void MoveCameraToNeckLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.19f, 1.801f, 0.924f);
        targetRotation = Quaternion.Euler(20.757f, -169.387f, 0f);
        targetLookAtPosition = new Vector3(0, 1.22f, 0);

        isTransitioning = true;
    }

    public void MoveCameraToHeadLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.42f, 1.78f, 0.662f);
        targetRotation = Quaternion.Euler(17.4f, -157.2f, 0f);
        targetLookAtPosition = new Vector3(0, 1.25f, -0.25f);

        isTransitioning = true;
    }

    public void MoveCameraToEpicondyleLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.609f, 1.526f, 0.8f);
        targetRotation = Quaternion.Euler(17.305f, -145.617f, 0f);
        targetLookAtPosition = new Vector3(0, 1, 0f);

        isTransitioning = true;
    }

    public void MoveCameraToWristLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.67f, 1.416f, 0.83f);
        targetRotation = Quaternion.Euler(22.047f, -150.998f, 0f);
        targetLookAtPosition = new Vector3(0.16f, 0.8f, 0f);

        isTransitioning = true;
    }
    public void MoveCameraToHandLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.67f, 1.416f, 0.83f);
        targetRotation = Quaternion.Euler(22.047f, -150.998f, 0f);
        targetLookAtPosition = new Vector3(0.16f, 0.8f, 0f);

        isTransitioning = true;
    }
    public void MoveCameraToKneesLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.124f, 0.996f, 1.184f);
        targetRotation = Quaternion.Euler(20.873f, -181.619f, 0f);
        targetLookAtPosition = new Vector3(0.16f, 0.32f, 0f);

        isTransitioning = true;
    }
    public void MoveCameraToAnkleLocation()
    {
        ResetTransitionTimer();

        targetPosition = new Vector3(0.702f, 0.575f, 0.889f);
        targetRotation = Quaternion.Euler(11.952f, -144.357f, 0f);
        targetLookAtPosition = new Vector3(0, 0.13f, 0f);

        isTransitioning = true;
    }
    private void ResetTransitionTimer()
    {
        transitionTimer = 0f;
    }
}
