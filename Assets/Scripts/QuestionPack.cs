using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question pack", menuName = "Create Pack", order = 34)]
public class QuestionPack : ScriptableObject
{
    [SerializeField] private List<Questions> questions;
    public List<Questions> Questions=>questions;
}
