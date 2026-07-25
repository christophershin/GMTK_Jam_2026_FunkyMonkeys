using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float countdown = 60f;
    [SerializeField] private GameObject Player;
    [SerializeField] private TextMeshProUGUI bombTimerText;
    [SerializeField] private GameObject playerHealthBar;
    [SerializeField] private GameObject goToMenuButton;
    
    public int PlayerScore = 0;


    void Start()
    {
        goToMenuButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (countdown >=0)
        {
            countdown -= Time.deltaTime;
            bombTimerText.text = ((int)countdown).ToString();
        }





        if (countdown <= 0)
        {
            countdown = 0;
            Player.GetComponent<PlayerController>().PlayerHP = 0;
        }


        if (PlayerScore <= 0)
        {
            PlayerScore = 0;
        }



        playerHealthBarUI();
    }



    void playerHealthBarUI()
    {

        float playerHealth = Player.GetComponent<PlayerController>().PlayerHP;

        float playerHealthPerc = playerHealth / (Player.GetComponent<PlayerController>().MaxPlayerHP);


        playerHealthBar.GetComponent<Slider>().value = playerHealthPerc;

        if (playerHealth <= 0)
        {
            goToMenuButton.SetActive(true);
        }
    }


    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }


}
