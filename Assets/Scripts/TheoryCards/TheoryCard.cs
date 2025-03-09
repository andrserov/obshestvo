using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName ="Create Card", order =52)]
public class TheoryCard : ScriptableObject
{
    [SerializeField] public string _name;
    [SerializeField] public string _description;    

}
