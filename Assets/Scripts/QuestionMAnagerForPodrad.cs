using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestionMAnagerForPodrad : MonoBehaviour
{
    [SerializeField] private List<Questions> _questions;
    [SerializeField] private List<QuestionButton> _buttons;
    [SerializeField] private TMP_Text _textMeshPro;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private TMP_Text _sceneQuestionNumberText;
    [SerializeField] private TMP_Text _sceneQuestionNumberTex;
    [SerializeField] private TMP_Text _questionNumberText;
    [SerializeField] private AudioSource _zvyk;
    [SerializeField] private AudioSource _zvyk1;
    private List<int> _indexList;
    private int _questNember = 0;
    private float _ellepsedTime = 0;
    //private int numquestion;

    void Start()
    {
        CreateAnswer(_questNember,0);
    }

    private void OnEnable()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].OnClick += OnClickHandler;
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].OnClick -= OnClickHandler;
        }
    }


    void Update()
    {
        _ellepsedTime += Time.deltaTime;
        if (_ellepsedTime > 2)
        {
            _winPanel.gameObject.SetActive(false);
            _losePanel.gameObject.SetActive(false);
            _mainPanel.gameObject.SetActive(true);
        }
    }
    public void CreateAnswer(int QuestionNumber,float delay)
    {
        _indexList = new List<int>() { 0, 1, 2, 3 };
        Questions question = _questions[QuestionNumber];
        _textMeshPro.text = question._question;
        StopAllCoroutines();
        StartCoroutine(ShowQuestion(question._question, delay, _textMeshPro, question));
    }
    private void OnClickHandler(int index)
    {
        if (index == 0)
        {
            if (_questNember == 19)
            {
                SceneManager.LoadScene(6);
                
            }
            else
            {
                _ellepsedTime = 0;
                _questNember++;
                _sceneQuestionNumberText.text = $"Следующий вопрос номер {_questNember + 1}";

                CreateAnswer(_questNember,2);
                _mainPanel.gameObject.SetActive(false);
                _winPanel.gameObject.SetActive(true);
                _zvyk.Play();   
            }
        }
        else
        {
            if (_questNember == 19)
            {
                SceneManager.LoadScene(6);

            }
            else
            {
                _ellepsedTime = 0;
                _questNember++;
                CreateAnswer(_questNember, 2);
                _sceneQuestionNumberTex.text = $"Следующий вопрос номер {_questNember + 1}";

                _mainPanel.gameObject.SetActive(false);
                _losePanel.gameObject.SetActive(true);
                _zvyk1.Play();

            }
        }
        _questionNumberText.text = $"Вопрос номер {_questNember + 1}";

    }
    private void FillAnswers(Questions question)
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            int number = Random.Range(0, _indexList.Count);
            _buttons[i].Inicialize(question._answers[_indexList[number]], _indexList[number]);
            _indexList.Remove(_indexList[number]);
            _buttons[i].Button.interactable = true;
        }
    }
    private IEnumerator ShowQuestion(string questionzzz, float delay, TMP_Text text, Questions question)
    {
        text.text = "";
        foreach (QuestionButton button in _buttons)
        {
            button.Clear();
        }
        FillAnswers(question);
        yield return new WaitForSeconds(delay);
        foreach (char letter in questionzzz)
        {
            text.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].FillText();
        }


    }
}
