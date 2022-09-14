using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour
{
    [SerializeField] private TMP_Text _ballCountText = null;
    
    //Topladığımız topları bu List'te tutacağız.
    [SerializeField] private List<GameObject> _balls = new List<GameObject>();
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _horizontalLimit;
    private float _horizontal;
    private int _gateNumber;
    private int _targetCount;
    private void Update()
    {
        HorizontalBallMove();
        ForwardBallMove();
        UpdateBallCountText();
    }

    private void HorizontalBallMove()
    {
        float _newX;
        
        if (Input.GetMouseButton(0))//Mouse sol click basılıyor ise;
        {
            _horizontal = Input.GetAxisRaw("Mouse X");//Unty içerinde ki Axis'i _horizontal değerimize atadık.
        }
        else
        {//Mouse sol click basılmıyor ise;
            _horizontal = 0;
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

    private void UpdateBallCountText()
    {
        _ballCountText.text = _balls.Count.ToString();//ballCount.text'ini balls List'imizin uzunluğuna atadık.
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StackBall"))//Çarpılan nesnenin tag'ı "StackBall" ise;
        {
            other.gameObject.transform.SetParent(transform);//Çarpılan nesneyi child nesne olarak alıyoruz.
            other.gameObject.GetComponent<SphereCollider>().enabled = false;//Çarpılan nesnenin colliderını devre dışı bıraktık.
            other.gameObject.transform.localPosition = new Vector3(0f, 0f, _balls[_balls.Count - 1].transform.localPosition.z -1);//Çarpılan nesneyi -1 bir pzisyonunda parent nesnemize ekliyoruz.
            _balls.Add(other.gameObject);//Çarpılan nesneyi _ball List'ine ekliyoruz. 
        }

        if (other.gameObject.CompareTag("Gate"))//Çarpılan nesnenin tag'ı "Gate" ise;
        {
            _gateNumber = other.gameObject.GetComponent<GateController>().GetGateNumber();//Gate'in değeri ne ise _gateNumber'a atıyoruz
            _targetCount = _balls.Count + _gateNumber;

            if (_gateNumber > 0)//gateNumber 0'dan büyük ise;(Pozitif)  
            {
                IncreaseBallCount(); 
            }
           else if (_gateNumber < 0)//gateNumber 0'dan küçük ise;(Negatif)  
            {
                DecreaseBallCount();
            }
        }
    }

    private void IncreaseBallCount()
    {
        for (int i = 0; i < _gateNumber; i++)
        {
            GameObject _newBall = Instantiate(ballPrefab);
            _newBall.transform.SetParent(transform);
            _newBall.GetComponent<SphereCollider>().enabled = false; 
            _newBall.gameObject.transform.localPosition = new Vector3(0f, 0f, _balls[_balls.Count - 1].transform.localPosition.z -1f);
            _balls.Add(_newBall);//newBall'ı List'e dahil ettik
        }
    }

    private void DecreaseBallCount()
    {
        for (int i = _balls.Count -1; i  >= _targetCount; i--)
        {
            _balls[i].SetActive(false);
            _balls.RemoveAt(i);
           
        }
    }
} 
