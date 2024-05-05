using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotateObjects : MonoBehaviour
{
    public List<GameObject> objectsToRotate;
    private List<Quaternion> _initialRotations;

    private float _transitionDuration = 0.5f;
    private float _rotationSpeed = 2;


    public delegate void ObjectHandler(GameObject obj);

    #pragma warning disable 0067
    public static event ObjectHandler ObjectInstantiatedOrDestroyed;
    #pragma warning restore 0067


    private void Start()
    {
        _initialRotations = new List<Quaternion>();

        foreach (GameObject obj in objectsToRotate)
        {
            _initialRotations.Add(obj.transform.rotation);
        }

        ObjectInstantiatedOrDestroyed += HandleInstantiatedOrDestroyedObject;
    }

    private void OnDestroy()
    {
        ObjectInstantiatedOrDestroyed -= HandleInstantiatedOrDestroyedObject;
    }

    public void HandleInstantiatedOrDestroyedObject(GameObject obj)
    {
        if (obj != null)
        {
            if (!IsObjectInList(obj))
            {
                objectsToRotate.Add(obj);
                _initialRotations.Add(obj.transform.rotation);
            }
            else
            {
                int index = objectsToRotate.IndexOf(obj);
                if (index != -1)
                {
                    objectsToRotate.RemoveAt(index);
                    if (index < _initialRotations.Count)
                    {
                        _initialRotations.RemoveAt(index);
                    }
                }
            }
        }
    }

    private bool IsObjectInList(GameObject obj)
    {
        return objectsToRotate.Contains(obj);
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
        float elapsedTime = 0;

        while (elapsedTime < _transitionDuration)
        {
            float t = elapsedTime / _transitionDuration;

            for (int i = 0; i < objectsToRotate.Count; i++)
            {
                if (i < _initialRotations.Count)
                {
                    Quaternion targetRotation = Quaternion.Lerp(objectsToRotate[i].transform.rotation, _initialRotations[i], t);
                    objectsToRotate[i].transform.rotation = Quaternion.RotateTowards(objectsToRotate[i].transform.rotation, targetRotation, _rotationSpeed);
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < objectsToRotate.Count; i++)
        {
            if (i < _initialRotations.Count)
            {
                objectsToRotate[i].transform.rotation = _initialRotations[i];
            }
        }
    }
}