using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Mathematics;
using System.Collections.Generic;
using System.Threading.Tasks;


public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private float MaxCountdown = 30f;

    public float countdown;
    private GameObject endofGameText;

    [SerializeField] private Slider SliderItem;
    [SerializeField] private GameObject pointsText;
    [SerializeField] private GameObject Player;
    [SerializeField] private TextMeshProUGUI bombTimerText;
    [SerializeField] private GameObject playerHealthBar;
    [SerializeField] private GameObject playerPointSlider;
    [SerializeField] private GameObject goToMenuButton;
    
    public int playerPoints = 0;
    public int MaxPlayerPoints = 100;
    public bool isPlayerWin = false;

    [SerializeField] private int StandingStillPointsReduct = 10;
    [SerializeField] private int MovingPointsGain = 5;



    private float standingStillTimer = 0;
    private float MaxStandingStillTimer = 1;

    [SerializeField] private List<AudioClip> sounds;
    private AudioSource managerAudioSource;


    void Start()
    {
        managerAudioSource = GetComponent<AudioSource>();
        endofGameText = GameObject.Find("EndofGameText");
        playerPointSlider.GetComponent<Slider>().maxValue = MaxPlayerPoints;
        goToMenuButton.SetActive(false);
        countdown = MaxCountdown;

        managerAudioSource.clip = sounds[0];
        managerAudioSource.loop = sounds[0];
        managerAudioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {

        if (countdown >=0 && !isPlayerWin && Player.GetComponent<PlayerController>().PlayerHP>0)
        {
            countdown -= Time.deltaTime;
            bombTimerText.text = ((int)countdown).ToString();
        }



        if (countdown < 0)
        {
            countdown = 1;
            Player.GetComponent<PlayerController>().PlayerHP = 0;
            managerAudioSource.clip = sounds[1];
            managerAudioSource.loop = false;
            managerAudioSource.Play();

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
            endofGameText.GetComponent<TextMeshProUGUI>().text = "Holy Aura Loss";



            if (playerPoints >0)
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

                // if player stands still
                if (playerVelocity == 0 && playerPoints>0)
                {
                    standingStillTimer -= Time.deltaTime;


                    if (standingStillTimer <= 0)
                    {
                        playerPoints -= StandingStillPointsReduct;
                        CreatePointsText("-", StandingStillPointsReduct);

                        standingStillTimer = MaxStandingStillTimer;
                    }

                }// if player is moving
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

            } // When Player Wins
            else if (Player.GetComponent<PlayerController>().bombDiffuseValue >= 0 && !isPlayerWin)
            {

                isPlayerWin = true;

                float missingTime = MaxCountdown - countdown;

                float multipliedScore = (float)playerPoints * ((missingTime / MaxCountdown));

                playerPoints += (int)multipliedScore;
                CreatePointsText("+", multipliedScore);

                goToMenuButton.SetActive(true);
                endofGameText.GetComponent<TextMeshProUGUI>().text = "Absolute Cinema!";

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
