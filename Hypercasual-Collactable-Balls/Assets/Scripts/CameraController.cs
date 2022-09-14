using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform ballTransform;
    [SerializeField] private float _lerpTime;
    private Vector3 _offset; //Camera ve Ball transformları arasındaki mesafeyi _offset ile tutacağız

    private void Start()
    {
        _offset = transform.position - ballTransform.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 _newPos = Vector3.Lerp(transform.position, ballTransform.position + _offset, _lerpTime * Time.deltaTime);
        transform.position = _newPos;
    }
}
