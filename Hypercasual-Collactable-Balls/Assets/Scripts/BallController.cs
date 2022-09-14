using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _horizontalLimit;
    private float _horizontal;

    private void Update()
    {
        HorizontalBallMove();
        ForwardBallMove();
    }

    private void HorizontalBallMove()
    {
        float _newX;
        
        if (Input.GetMouseButton(0))//Mouse sol click basılıyor ise;
        {
            _horizontal = Input.GetAxisRaw("Mouse X");//Unty içerinde ki Axis'i _horizontal değerimize atadık.
        }

        _newX = transform.position.x + _horizontal * _horizontalSpeed * Time.deltaTime;
        _newX = Mathf.Clamp(_newX, -_horizontalLimit, _horizontalLimit);//Ball hareketini x düzleminde sınırladık

        transform.position = new Vector3(
        _newX,
        transform.position.y,
        transform.position.z
        );
    }

    private void ForwardBallMove()
    {
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
    }
}
