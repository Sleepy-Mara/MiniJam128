using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] cameraPositions;
    private int _actualPosition;
    private Camera _camera;
    public int cameraPlaceCard;
    public int cameraHand;
    private void Start()
    {
        _camera = FindObjectOfType<Camera>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            ChangeCamera(1);
        if (Input.GetKeyDown(KeyCode.S))
            ChangeCamera(-1);
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            ChangeCamera(-1);
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            ChangeCamera(1);
    }
    private void ChangeCamera(int x)
    {
        _actualPosition += x;
        if (_actualPosition < 0)
            _actualPosition = 0;
        if (_actualPosition >= cameraPositions.Length)
            _actualPosition = cameraPositions.Length - 1;
        _camera.transform.position = cameraPositions[_actualPosition].position;
        _camera.transform.rotation = cameraPositions[_actualPosition].rotation;
    }
    public void PlaceCardCamera()
    {
        _actualPosition = cameraPlaceCard;
        ChangeCamera(0);
    }
    public void HandCamera()
    {
        _actualPosition = cameraHand;
        ChangeCamera(0);
    }
}
