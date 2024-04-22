using UnityEngine;
using System.Collections;
using TMPro.Examples;
using Cinemachine;

public class RotateObjects : MonoBehaviour
{

    public GameObject[] objectsToRotate;
    private Quaternion[] _initialRotations;

    [SerializeField]
    private float _transitionDuration;

    [SerializeField]
    private float _rotationSpeed;

    void Start()
    {

        _initialRotations = new Quaternion[objectsToRotate.Length];
        for (int i = 0; i < objectsToRotate.Length; i++)
        {
            _initialRotations[i] = objectsToRotate[i].transform.rotation;
        }
    }

    private void Update()
    {
       
    }

    private void OnMouseDrag()
    {
        float xAxisRotation = Input.GetAxis("Mouse X") * _rotationSpeed;

        foreach (GameObject obj in objectsToRotate)
        {
            obj.transform.Rotate(Vector3.down, xAxisRotation);
        }
    }

    private void OnMouseUp()
    {
        StartCoroutine(TransitionToInitialRotations());
    }

    private IEnumerator TransitionToInitialRotations()
    {
        float elapsedTime = 0f;

        Quaternion[] currentRotations = new Quaternion[objectsToRotate.Length];
        for (int i = 0; i < objectsToRotate.Length; i++)
        {
            currentRotations[i] = objectsToRotate[i].transform.rotation;
        }

        while (elapsedTime < _transitionDuration)
        {
            float t = elapsedTime / _transitionDuration;

            for (int i = 0; i < objectsToRotate.Length; i++)
            {
                objectsToRotate[i].transform.rotation = Quaternion.Lerp(currentRotations[i], _initialRotations[i], t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < objectsToRotate.Length; i++)
        {
            objectsToRotate[i].transform.rotation = _initialRotations[i];
        }
    }
}
