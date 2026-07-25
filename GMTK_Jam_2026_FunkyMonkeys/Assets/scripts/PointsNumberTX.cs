using System.Collections;
using NUnit.Framework.Internal;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PointsNumberTX : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        float randVelX = Random.Range(-100, 100);

        GetComponent<Rigidbody2D>().linearVelocityX = randVelX;
        StartCoroutine(textDecay());
    }

    // Update is called once per frame
    void Update()
    {


        GetComponent<TextMeshProUGUI>().alpha -= Time.deltaTime;
    }


    IEnumerator textDecay()
    {
        yield return new WaitForSeconds(2f);

        Destroy(this.gameObject);
    }
}
