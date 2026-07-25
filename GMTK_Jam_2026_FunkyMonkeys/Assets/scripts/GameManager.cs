using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float countdown = 60f;
    [SerializeField] private GameObject Player;
    [SerializeField] private TextMeshProUGUI bombTimerText;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        bombTimerText.text = ((int)countdown).ToString();

        if (countdown <= 0)
        {
            Player.GetComponent<PlayerController>().PlayerHP = 0;
        }
    }
}
