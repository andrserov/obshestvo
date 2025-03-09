using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonTheory : MonoBehaviour
{
    [SerializeField] private int  _numScene;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Switch()
    {
        SceneManager.LoadScene(_numScene);
    }
}
