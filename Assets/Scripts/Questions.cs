using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Qustion", menuName = "Create Qustion", order = 42)]

public class Questions : ScriptableObject
{
    [SerializeField] public string _question;
    [SerializeField] public List<string> _answers;
    
}
