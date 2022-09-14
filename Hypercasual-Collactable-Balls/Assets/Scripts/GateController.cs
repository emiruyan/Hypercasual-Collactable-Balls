 using System;
 using System.Collections;
using System.Collections.Generic;
 using TMPro;
 using UnityEngine;
 using Random = UnityEngine.Random;

 public class GateController : MonoBehaviour
{
 [SerializeField] private enum  GateType
 {
  PositiveGate,
  NegativeGate
 }

 [SerializeField] private TMP_Text _gateNumberText = null;
 [SerializeField] private GateType _gateType;
 [SerializeField] private int _gateNumber;

 public int GetGateNumber()
 {
  return _gateNumber;
 }

 private void Start()  
 {
  RandomGateNumber();
 }

 private void RandomGateNumber()
 {
  switch (_gateType)
  {//PositiveGate'a random olarak 1 ila 10 arası bir sayı veriyoruz
   case GateType.PositiveGate : _gateNumber = Random.Range(1,10);
                                _gateNumberText.text = _gateNumber.ToString();//Ve bunu gateNumberText'ine atıyoruz.
    break;
   //NegativeGate'a random olarak -10 ila -1 arası bir sayı veriyoruz
   case GateType.NegativeGate : _gateNumber = Random.Range(-10, -1);
                                _gateNumberText.text = _gateNumber.ToString();//Ve bunu gateNumberText'ine atıyoruz.
    break;
  }
 }
}
