using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Transform camTransform;
    private Quaternion _originalRotation;

    private void Start()
    {
        camTransform = camTransform != null
            ? camTransform
            : Camera.main.transform;

        _originalRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.rotation = camTransform.rotation * _originalRotation;
    }
}
