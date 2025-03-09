using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TheoryManager : MonoBehaviour
{
    [SerializeField] private List<TheoryCard> cards = new List<TheoryCard>();
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private int _exitScene;
    private int _currentIndex=0;
    void Start()
    {
        _nameText.text = cards[_currentIndex]._name;
        _descriptionText.text = cards[_currentIndex]._description;
    }


    void Update()
    {
        
    }
    public void Exit()
    {
        SceneManager.LoadScene(_exitScene);
    }
    public void NextCard()
    {
        _currentIndex++;
        if (_currentIndex >= cards.Count)
        {
            _currentIndex = 0;  
        }
        _nameText.text = cards[_currentIndex]._name;
        _descriptionText.text = cards[_currentIndex]._description;
    }
    public void BackCard()
    {
        _currentIndex--;
        if (_currentIndex < 0)
        {
            _currentIndex = cards.Count-1;
        }
        _nameText.text = cards[_currentIndex]._name;
        _descriptionText.text = cards[_currentIndex]._description;
    }
}
