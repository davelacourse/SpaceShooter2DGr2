using UnityEngine;
using TMPro;

public class UIEnd : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textPointage;
    [SerializeField] private TextMeshProUGUI _textRecord;

    private void Start()
    {
        _textPointage.text = $"Pointage :{PlayerPrefs.GetInt("PlayerScore", 0)}";
        _textRecord.text = $"Record: {PlayerPrefs.GetInt("PlayerHighScore", 0)}";
    }

    public void OnDeleteHighScoreClick()
    {
        PlayerPrefs.DeleteKey("PlayerHighScore");
        PlayerPrefs.Save();
        _textRecord.text = $"Record: {PlayerPrefs.GetInt("PlayerHighScore", 0)}";
    }
}
