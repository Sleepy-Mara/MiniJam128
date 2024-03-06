using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowToSelect : MonoBehaviour
{
    public GameObject arrowHead;
    public GameObject arrowBody;
    public int arrowNodeNum;
    public float scaleFactor = 1f;
    public Vector3 origin;
    private List<Transform> _arrowNodes = new List<Transform>();
    private List<Vector2> _controlPoints = new List<Vector2>();
    private readonly List<Vector2> _controlPointFactors = new List<Vector2> { new Vector2(-0.3f, 0.8f), new Vector2(0.1f, 1.4f) };

    private void Awake()
    {
        for (int i = 0; i < arrowNodeNum; i++)
            _arrowNodes.Add(Instantiate(arrowBody, transform).GetComponent<Transform>());
        _arrowNodes.Add(Instantiate(arrowHead, transform).GetComponent<Transform>());
        _arrowNodes.ForEach(a => a.GetComponent<Transform>().position = new Vector2(-1000, -1000));
        for (int i = 0; i < 4; i++)
            _controlPoints.Add(Vector2.zero);
    }
    public void Arrow()
    {
        _controlPoints[0] = new Vector2(origin.x, origin.y);
        _controlPoints[3] = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        _controlPoints[1] = _controlPoints[0] + (_controlPoints[3] - _controlPoints[0]);
        _controlPoints[2] = _controlPoints[0] + (_controlPoints[3] - _controlPoints[0]);
        for (int i = 0; i < _arrowNodes.Count; i++)
        {
            var t = Mathf.Log(1f * i / (_arrowNodes.Count - 1) + 1, 2f);
            _arrowNodes[i].position =
                Mathf.Pow(1 - t, 1.2f) * _controlPoints[0] +
                Mathf.Pow(t, 1.2f) * _controlPoints[3];
            Debug.Log(Mathf.Pow(1 - t, 1.2f) + " * " + _controlPoints[0].ToString() + " = " + Mathf.Pow(1 - t, 1.2f) * _controlPoints[0] + " +++ " + Mathf.Pow(t, 1.2f) + " * " + _controlPoints[3] + " = " + Mathf.Pow(t, 1.2f) * _controlPoints[3] + " === " + _arrowNodes[i].localPosition.ToString());
            if (i > 0)
            {
                var euler = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, _arrowNodes[i].position - _arrowNodes[i - 1].position));
                _arrowNodes[i].rotation = Quaternion.Euler(euler);
            }
            var scale = scaleFactor * (1f - 0.03f * (_arrowNodes.Count - 1 - i));
            _arrowNodes[i].localScale = new Vector3(scale, scale, 1f);
        }
        _arrowNodes[0].transform.rotation = _arrowNodes[1].transform.rotation;
    }
    public void Hide()
    {
        _arrowNodes.ForEach(a => a.GetComponent<Transform>().position = new Vector2(-1000, -1000));
    }

    //public GameObject[] points;
    //public GameObject arrow;
    //public GameObject point;
    //public int numberOfPoints;
    //[HideInInspector] public Vector2 startPoint;
    //public float distance = 1f;
    //public Vector2 direction;
    //public float force;
    //private void Start()
    //{
    //    points = new GameObject[numberOfPoints];
    //    for (int i = 0; i < numberOfPoints; i++)
    //        if (i != numberOfPoints - 1)
    //        {
    //            points[i] = Instantiate(point, transform.position, Quaternion.identity);
    //            points[i].SetActive(false);
    //        } else
    //        {
    //            points[i] = Instantiate(arrow, transform.position, Quaternion.identity);
    //            points[i].SetActive(false);
    //        }
    //}
    //public void Arrow()
    //{
    //    for(int i = 0; i < numberOfPoints; i++)
    //    {
    //        Vector2 viewportPoint = Camera.main.WorldToScreenPoint(startPoint);
    //        viewportPoint.y -= 170;
    //        points[i].transform.position = Vector2.Lerp(viewportPoint, direction, i * 0.1f);
    //        direction = Input.mousePosition;
    //        distance = Vector3.Distance(viewportPoint, direction);
    //        force = distance / 10;
    //    }
    //}
    //public void Show()
    //{
    //    for (int i = 0; i < numberOfPoints; i++)
    //        points[i].SetActive(true);
    //}
    //public void Hide()
    //{
    //    for (int i = 0; i < numberOfPoints; i++)
    //        points[i].SetActive(false);
    //}
}
