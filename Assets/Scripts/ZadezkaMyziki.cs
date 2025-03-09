using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZadezkaMyziki : MonoBehaviour
{
    [SerializeField] private AudioSource _phone;
    [SerializeField] private float _delay;
    void Start()
    {
        Invoke("Play",_delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Play()
    {
        _phone.Play(); 
    }
}
