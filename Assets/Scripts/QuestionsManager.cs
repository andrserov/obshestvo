using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestionsManager : MonoBehaviour
{
    [SerializeField] private List<QuestionPack> _questions;
    [SerializeField]private List<QuestionButton> _buttons;
    [SerializeField]private TMP_Text _textMeshPro;
    [SerializeField] private Button _fifty;
    [SerializeField] private Button _secondHealth;
    [SerializeField] private Button _zal;
    [SerializeField] private Button _changeQuestion;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _zalPanel;
    [SerializeField] private TMP_Text _sceneQuestionNumberText;
    [SerializeField] private TMP_Text _questionNumberText;
    [SerializeField] private List<Image> _imagesPercent;
    [SerializeField] private AudioSource _zvyk;
    private List<int> _indexList;
    private int _questNember=0;
    private float _ellepsedTime=0;
    private int health=1;
    private int numquestion;

    void Start()
    {
        CreateAnswer(_questNember,0);
    }

    private void OnEnable()
    {
        _fifty.onClick.AddListener(Fifty);
        _zal.onClick.AddListener(Zal);
        _secondHealth.onClick.AddListener(SecondHealth);
        _changeQuestion.onClick.AddListener(ChangeQuestion);
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].OnClick += OnClickHandler;
        }
    }
    private void OnDisable()
    {
        _fifty.onClick.RemoveListener(Fifty);
        _zal.onClick.RemoveListener(Zal);
        _secondHealth.onClick.RemoveListener(SecondHealth);
        _changeQuestion.onClick.RemoveListener(ChangeQuestion);
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
            _mainPanel.gameObject.SetActive(true);
        }
    }
    public void CreateAnswer(int QuestPackNumber,float delay)
    {
        _indexList = new List<int>() { 0, 1, 2, 3 };
        numquestion = Random.Range(0, 2);
        Questions question = _questions[QuestPackNumber].Questions[numquestion];
        StopAllCoroutines();
        StartCoroutine(ShowQuestion(question._question,delay, _textMeshPro,question));
        health = 1;
        _zalPanel.gameObject.SetActive(false);

    } 
    private void OnClickHandler(int index)
    {       
        if (index == 0)
        {
            if (_questNember == 9)
            {
                SceneManager.LoadScene(5);
            }
            else
            {
                _ellepsedTime = 0;
                _questNember++;
                _sceneQuestionNumberText.text = $"следующий вопрос номер {_questNember + 1}";
                _questionNumberText.text = $"¬опрос номер {_questNember + 1}";
                CreateAnswer(_questNember,2);
                _mainPanel.gameObject.SetActive(false);
                _winPanel.gameObject.SetActive(true);
                _zvyk.Play();
            }                      
        }
        else
        {
            health--;
            if (health <= 0)
            {
                SceneManager.LoadScene(4);
            }
        }

    }
    private void Zal()
    {
        _zal.interactable = false;
        _zalPanel.gameObject.SetActive(true);
        for (int i = 0; i < _buttons.Count; i++)
        {
            if (_buttons[i].Index == 0)
            {
                StartCoroutine(ShowPodskazka(_imagesPercent[i], Random.Range(0.5f, 1)));
            }
            else 
            {
                StartCoroutine(ShowPodskazka(_imagesPercent[i], Random.Range(0, 0.6f)));
            }
        }
    }
    private void Fifty()
    {
        int kl = 0;
        for (int i = 0; i < _buttons.Count; i++)
        { 
            if (_buttons[i].Index != 0 && kl<2)
            {
                _buttons[i].Button.interactable = false;
                kl++;
            }
        }
        _fifty.interactable = false;
    }
    private void SecondHealth()
    {
        health++;
        _secondHealth.interactable = false;
    }
    private void ChangeQuestion()
    {
        if (numquestion == 1)
        {
            numquestion = 0;
        }
        else if (numquestion == 0)
        {
            numquestion = 1;
        }
        _changeQuestion.interactable = false;
        _indexList = new List<int>() { 0, 1, 2, 3 };
        Questions question = _questions[_questNember].Questions[numquestion];
        //_textMeshPro.text = question._question;
        StopAllCoroutines();
        StartCoroutine(ShowQuestion(question._question,0,_textMeshPro,question));
        health = 1;
        _zalPanel.gameObject.SetActive(false);
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
    private IEnumerator ShowQuestion(string questionzzz,float delay,TMP_Text text,Questions question)
    {
        text.text = "";
        foreach(QuestionButton button in _buttons)
        {
            button.Clear();
        }
        FillAnswers(question);
        yield return new WaitForSeconds(delay);
        foreach(char letter in questionzzz)
        {
            text.text+=letter;
            yield return new WaitForSeconds(0.02f);
        }
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].FillText();
        }

    }
    private IEnumerator ShowPodskazka(Image image,float znach)
    {
        float time = 2;
        float ellepsedTime = 0;
        while (ellepsedTime <= time)
        {
            ellepsedTime+=Time.deltaTime;
            image.fillAmount = Mathf.Lerp(0,znach,ellepsedTime/time);
            yield return null;
        }
    }

}
