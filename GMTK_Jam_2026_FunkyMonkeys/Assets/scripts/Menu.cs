using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI highscoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        highscoreText.text = "HighScore: " + PlayerPrefs.GetInt("highscore").ToString();
    }


    public void Play()
    {
        SceneManager.LoadScene(1);
    }


    public void resetHighScore()
    {
        PlayerPrefs.DeleteAll();
    }
}
