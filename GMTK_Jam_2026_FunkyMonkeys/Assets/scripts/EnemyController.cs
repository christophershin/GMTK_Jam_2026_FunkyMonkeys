using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform rotator;
    private Transform player;
    [SerializeField] private Transform handPos;
    [SerializeField] private GameObject bullet;
    [SerializeField] private int EnemyHealth = 100;
    
    private float time = 0;

    [SerializeField] private List<AudioClip> Enemysounds;
    private AudioSource EnemyAudioSource;

    private void Start()
    {
        EnemyAudioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rotator = this.transform.GetChild(0);
    }

    void Update()
    {
        if (player.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        
        Vector2 direction = player.position - rotator.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotator.rotation = Quaternion.Euler(0, 0, angle);
        
        time += Time.deltaTime;

        if (time > 1)
        {
            time = 0;
            GameObject bul =  Instantiate(bullet, handPos.position, rotator.rotation);
            Bullet bulScript = bul.GetComponent<Bullet>();
            bulScript.Initialize(handPos.position, player.position);

            EnemyAudioSource.clip = Enemysounds[1];
            EnemyAudioSource.Play();
        }

        
    }


    public void EnemyTakeDamage(int num)
    {
        EnemyHealth -= num;

        if (EnemyHealth <= 0)
        {
            EnemyAudioSource.clip = Enemysounds[0];
            EnemyAudioSource.Play();
            Destroy(this.gameObject);
        }
    }
}
