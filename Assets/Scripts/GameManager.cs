using UnityEngine;

public class GameManager : MonoBehaviour {
    [Header("Панели UI")]
    [SerializeField]
    private GameObject _pausePanel;
    [SerializeField]
    private GameObject _winPanel;
    [SerializeField]
    private GameObject _losePanel;
    [SerializeField]
    private GameObject _talkButton;
    [SerializeField]
    private GameObject _talkingPanel;
    [SerializeField]
    private GameObject _gameMenues;
    [SerializeField]
    private GameObject _gamePanel;
    private void ShowPanel(GameObject panel)
    {
        _pausePanel.SetActive(panel == _pausePanel);
        _winPanel.SetActive(panel == _winPanel);
        _losePanel.SetActive(panel == _losePanel);
        _gameMenues.SetActive(panel == _gameMenues);
        _talkButton.SetActive(panel == _talkButton);
        _talkingPanel.SetActive(panel == _talkingPanel);
        _gamePanel.SetActive(panel == _gamePanel);
    }
    public void ShowPausePanel()
    {
        Time.timeScale = 0f;
        ShowPanel(_pausePanel);
    }
    public void ShowWinPanel()
    {
        Time.timeScale = 0f;
        ShowPanel(_winPanel);
    }
    public void ShowLosePanel()
    {
        Time.timeScale = 0f;
        ShowPanel(_losePanel);
    }
    public void ShowGamePanel()
    {
        Time.timeScale = 1f;
        ShowPanel(_gamePanel);
    }
    public void ShowTalkButton()
    {

        ShowPanel(_talkButton);
    }
    public void ShowTalking()
    {
        Time.timeScale = 0f;
        ShowPanel(_talkingPanel);
    }
    public void ShowGameMenues()
    {
        if (_gameMenues.activeSelf == false)
        {
            Time.timeScale = 0f;
            ShowPanel(_gameMenues);
        }
        else 
        { 
            ShowGamePanel();
        }
    }
        
}
	

