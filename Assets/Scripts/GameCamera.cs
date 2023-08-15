using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private Vector3 _targetRotation;
    [SerializeField] private float _speed;

    private Vector3 _baseRotation;
    // Start is called before the first frame update
    void Start()
    {
        _baseRotation = _camera.transform.rotation.eulerAngles;
    }


    public void SetCameraToPosition(Vector3 position)
    {
        _camera.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(_camera.transform.position, _targetPosition) > 0.1f)
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _targetPosition, _speed * Time.deltaTime);
        }


    }

    void CalculateQuaternionDifference(Quaternion a, Quaternion b)
    {
        var difference = a * Quaternion.Inverse(b);

    }
}
