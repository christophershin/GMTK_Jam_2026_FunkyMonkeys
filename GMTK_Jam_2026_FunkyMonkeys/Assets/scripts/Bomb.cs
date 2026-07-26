using UnityEngine;
using UnityEngine.UIElements;

public class Bomb : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject explosion;
    public GameObject gameManager;
    public GameObject player;

    private float growthSpeed = 400;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<GameManager>().countdown <= 1 && gameManager.GetComponent<GameManager>().isPlayerWin == false && player.GetComponent<PlayerController>().PlayerHP<=0)
        {
            explosion.SetActive(true);
            explosion.transform.localScale += new Vector3(growthSpeed*Time.deltaTime, growthSpeed * Time.deltaTime, 1);
            explosion.GetComponent<SpriteRenderer>().color -= new Color(1f, 1f, 1f, Time.deltaTime); 
        }
    }
}
