using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.U2D;
using UnityEngine.UI;
using DG.Tweening;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private Camera _camera2;
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private PixelPerfectCamera _pixelPerfectCamera;
    [SerializeField] private PixelPerfectCamera _pixelPerfectCamera2;

    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private Vector3 _targetRotation;
    [SerializeField] private float _speed;


    private float _swipeTimer = 0f;
    private float _swipeDelay = 0.2f;

    public static GameCamera Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private Vector3 _baseRotation;
    // Start is called before the first frame update
    void Start()
    {
        _baseRotation = _camera.transform.rotation.eulerAngles;

        // int screenDecrement = 2;

        // GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_UNorm;
        // GraphicsFormat depthStencilFormat = GraphicsFormat.D24_UNorm_S8_UInt;
        // _camera.targetTexture = new RenderTexture(Screen.width / screenDecrement, Screen.height / screenDecrement, colorFormat, depthStencilFormat);
        // _camera.targetTexture.filterMode = FilterMode.Point;
        // _rawImage.texture = _camera.targetTexture;


        // _pixelPerfectCamera.refResolutionX = Screen.width / screenDecrement;
        // _pixelPerfectCamera2.refResolutionX = Screen.width / screenDecrement;
        // _pixelPerfectCamera.refResolutionY = Screen.height / screenDecrement;
        // _pixelPerfectCamera2.refResolutionY = Screen.height / screenDecrement;
        // int calculatePPU = 20;
        // _pixelPerfectCamera.assetsPPU = calculatePPU;
        // _pixelPerfectCamera2.assetsPPU = calculatePPU;
        // //_pixelPerfectCamera.upscaleRT = true;
        // _pixelPerfectCamera.pixelSnapping = true;
        // _pixelPerfectCamera2.pixelSnapping = true;
        // _pixelPerfectCamera.cropFrameX = true;
        // _pixelPerfectCamera2.cropFrameX = true;
        // _pixelPerfectCamera.cropFrameY = true;
        // _pixelPerfectCamera2.cropFrameY = true;
        // _pixelPerfectCamera.stretchFill = true;
        // _pixelPerfectCamera.enabled = true;
        // _pixelPerfectCamera2.enabled = true;

    }


    public void SetCameraToPosition(Vector3 position)
    {
        _targetPosition = position;
    }

    // Update is called once per frame
    void Update()
    {
        //   _targetRotation.x = 45;
        _camera.transform.position = _targetPosition;
        // if (Vector3.Distance(_camera.transform.position, _targetPosition) > 0.1f)
        // {
        //     // 
        //     _camera.transform.position = Vector3.Lerp(_camera.transform.position, _targetPosition, _speed * Time.deltaTime);
        // }

        // if (Vector3.Distance(_camera.transform.rotation.eulerAngles, _targetRotation) > 0.1f)
        // {
        //     _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, Quaternion.Euler(_targetRotation), _speed * Time.deltaTime);

        //     // RotateCamera(1f);
        // }

        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     _targetRotation.y -= 90f;
        // }

        // if(Input.GetKeyDown(KeyCode.D))
        // {
        //     _targetRotation.y += 90f;
        // }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _camera.transform.DORotate(new Vector3(
                _camera.transform.rotation.eulerAngles.x,
                _camera.transform.rotation.eulerAngles.y + 90,
                _camera.transform.rotation.eulerAngles.z), 1f).SetEase(Ease.OutSine);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _camera.transform.DORotate(new Vector3(
                _camera.transform.rotation.eulerAngles.x,
                _camera.transform.rotation.eulerAngles.y - 90,
                _camera.transform.rotation.eulerAngles.z), 1f).SetEase(Ease.OutSine);
        }





        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // Adjust camera field of view
            _pixelPerfectCamera.assetsPPU -= (int)(deltaMagnitudeDiff / 10f);
            _pixelPerfectCamera2.assetsPPU -= (int)(deltaMagnitudeDiff / 10f);

            _pixelPerfectCamera2.assetsPPU = Mathf.Clamp(_pixelPerfectCamera2.assetsPPU, 16, 50);
            _pixelPerfectCamera.assetsPPU = Mathf.Clamp(_pixelPerfectCamera.assetsPPU, 16, 50);
        }

        //if swipe left then rotate camera left
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            if (touchDeltaPosition.x < 5)
            {
                // Swipe left
                _swipeTimer += Time.deltaTime;
                if (_swipeTimer >= _swipeDelay)
                {
                    _camera.transform.DORotate(new Vector3(
                        _camera.transform.rotation.eulerAngles.x,
                        _camera.transform.rotation.eulerAngles.y - 90,
                        _camera.transform.rotation.eulerAngles.z), 1f).SetEase(Ease.OutSine);
                    _swipeTimer = 0f;
                }
            }
            else if (touchDeltaPosition.x > -5)
            {
                // Swipe right
                _swipeTimer += Time.deltaTime;
                if (_swipeTimer >= _swipeDelay)
                {
                    _camera.transform.DORotate(new Vector3(
                        _camera.transform.rotation.eulerAngles.x,
                        _camera.transform.rotation.eulerAngles.y + 90,
                        _camera.transform.rotation.eulerAngles.z), 1f).SetEase(Ease.OutSine);
                    _swipeTimer = 0f;
                }
            }
        }
        else
        {
            _swipeTimer = 0f;
        }



    }
}
