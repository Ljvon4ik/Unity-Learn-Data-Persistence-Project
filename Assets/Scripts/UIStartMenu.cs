using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIStartMenu : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textPlayerName;
    [SerializeField]
    GameObject errorPanel;
    [SerializeField]
    Button closeErrorButton;
    [SerializeField]
    TextMeshProUGUI bestScoreText;
    private void Start()
    {
        closeErrorButton.onClick.AddListener(CloseErrorPanel);
        LoadData();
    }
    public void SaveAndPlay()
    {
        var numberOfCharacters = textPlayerName.text.ToString().Length;

        if (numberOfCharacters > 16 || numberOfCharacters <= 1)
        {
            errorPanel.SetActive(true);
        }
        else
        {
            SaveNamePlayer();
            SceneManager.LoadScene(1);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }    

    private void SaveNamePlayer()
    {
        SaveName.Instance.playerName = textPlayerName.text.ToString();
    }

    private void CloseErrorPanel()
    {
        errorPanel?.SetActive(false);
    }

    private void LoadData()
    {
        SaveData data = new SaveData();
        string bestPlayerName;
        int bestScore;
        data.BestResult(out bestPlayerName, out bestScore);
        bestScoreText.text = $"Best Score: \r\n {bestPlayerName} : {bestScore}";
    }
}
