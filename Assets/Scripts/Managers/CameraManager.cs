using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] cameraPositions;
    private int _currentPosition;
    private Camera _camera;
    public int cameraPlaceCard;
    public int cameraHand;
    private Animator animator;
    private void Start()
    {
        _camera = FindObjectOfType<Camera>();
        animator = _camera.transform.GetComponentInParent<Animator>();
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
        // por que no usar clamp()?
        _currentPosition += x;
        if (_currentPosition < 0)
            _currentPosition = 0;
        if (_currentPosition >= cameraPositions.Length)
            _currentPosition = cameraPositions.Length - 1;
        animator.SetInteger("Position", _currentPosition);
        //_camera.transform.position = cameraPositions[_currentPosition].position;
        //_camera.transform.rotation = cameraPositions[_currentPosition].rotation;
    }
    public void PlaceCardCamera()
    {
        _currentPosition = cameraPlaceCard;
        ChangeCamera(0);
    }
    public void HandCamera()
    {
        _currentPosition = cameraHand;
        ChangeCamera(0);
    }
    public int CameraPosition()
    {
        return _currentPosition;
    }
}
