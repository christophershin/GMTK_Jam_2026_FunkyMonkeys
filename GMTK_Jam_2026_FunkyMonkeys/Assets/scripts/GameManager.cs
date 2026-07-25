using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Mathematics;


public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private float MaxCountdown = 60f;

    private float countdown;

    [SerializeField] private Slider SliderItem;
    [SerializeField] private GameObject pointsText;
    [SerializeField] private GameObject Player;
    [SerializeField] private TextMeshProUGUI bombTimerText;
    [SerializeField] private GameObject playerHealthBar;
    [SerializeField] private GameObject playerPointSlider;
    [SerializeField] private GameObject goToMenuButton;
    
    public int playerPoints = 0;
    public int MaxPlayerPoints = 100;
    private bool isPlayerWin = false;

    [SerializeField] private int StandingStillPointsReduct = 10;
    [SerializeField] private int MovingPointsGain = 5;



    private float standingStillTimer = 0;
    private float MaxStandingStillTimer = 1;


    void Start()
    {

        playerPointSlider.GetComponent<Slider>().maxValue = MaxPlayerPoints;
        goToMenuButton.SetActive(false);
        countdown = MaxCountdown;
    }

    // Update is called once per frame
    void Update()
    {

        if (countdown >=0 && !isPlayerWin)
        {
            countdown -= Time.deltaTime;
            bombTimerText.text = ((int)countdown).ToString();
        }





        if (countdown <= 0)
        {
            countdown = 0;
            Player.GetComponent<PlayerController>().PlayerHP = 0;
        }


        if (playerPoints <= 0)
        {
            playerPoints = 0;
        }



        playerHealthBarUI();
        PlayerPointsFunction();
    }



    void playerHealthBarUI()
    {

        float playerHealth = Player.GetComponent<PlayerController>().PlayerHP;

        float playerHealthPerc = playerHealth / (Player.GetComponent<PlayerController>().MaxPlayerHP);


        playerHealthBar.GetComponent<Slider>().value = playerHealthPerc;

        if (playerHealth <= 0)
        {
            goToMenuButton.SetActive(true);

            if (playerPoints > 0)
            {
                CreatePointsText("-", playerPoints);
                playerPoints -= playerPoints;
                
            }

        }
    }


    void PlayerPointsFunction()
    {

        if (Player.GetComponent<PlayerController>().PlayerHP > 0)
        {


            if (Player.GetComponent<PlayerController>().bombDiffuseValue < 1 && !isPlayerWin)
            {
                float playerVelocity = Player.GetComponent<PlayerController>().horizontal;

                if (playerVelocity == 0)
                {
                    standingStillTimer -= Time.deltaTime;


                    if (standingStillTimer <= 0)
                    {
                        playerPoints -= StandingStillPointsReduct;
                        CreatePointsText("-", StandingStillPointsReduct);

                        standingStillTimer = MaxStandingStillTimer;
                    }

                }
                else
                {
                    standingStillTimer -= Time.deltaTime;


                    if (standingStillTimer <= 0)
                    {
                        CreatePointsText("+", MovingPointsGain);
                        playerPoints += MovingPointsGain;

                        standingStillTimer = MaxStandingStillTimer;
                    }
                }

            }
            else if (Player.GetComponent<PlayerController>().bombDiffuseValue >= 0 && !isPlayerWin)
            {

                isPlayerWin = true;

                float multipliedScore = (float)playerPoints * ((countdown / MaxCountdown));

                playerPoints += (int)multipliedScore;
                CreatePointsText("+", multipliedScore);

            }
        }


        



        playerPointSlider.GetComponent<Slider>().value = playerPoints;
    }



    public void CreatePointsText(string symbol, float numTex)
    {
        GameObject p = Instantiate(pointsText, SliderItem.transform);
        p.GetComponent<TextMeshProUGUI>().text = symbol.ToString() + ((int)numTex).ToString();
        p.transform.position = SliderItem.transform.position + new Vector3(400, 0, 0);

    }




    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }


}
