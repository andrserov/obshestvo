using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Reflection;
using System.Collections;

public class QuestionButton : MonoBehaviour
{
    [SerializeField]private TMP_Text _TextMeshPro;
    [SerializeField] private Button _button;
    private int _index;
    private string _quest;
    public Button Button => _button;
    public int Index => _index;
    public event Action<int> OnClick;
   
    public void Inicialize(string quest,int index)
    {
        _quest=quest;
        _index = index;
        Button.interactable = true;
    }
    public void FillText()
    {
        StartCoroutine(ShowQuestion(_quest, 0, _TextMeshPro));
    }
    public void OnEnable()
    {
        Button.onClick.AddListener(OnClickHander);
        Button.interactable = true;

    }
    public void OnDisable()
    {
        Button.onClick.RemoveListener(OnClickHander);
        Button.interactable = true;
    }
    private void OnClickHander()
    {
        OnClick.Invoke(Index);
        Button.interactable = false;
    }
    private IEnumerator ShowQuestion(string question, float delay, TMP_Text text)
    {
        text.text = "";
        yield return new WaitForSeconds(delay);
        foreach (char letter in question)
        {
            text.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }
    public void Clear()
    {
        _index=0;
        _TextMeshPro.text = "";
    }

}
