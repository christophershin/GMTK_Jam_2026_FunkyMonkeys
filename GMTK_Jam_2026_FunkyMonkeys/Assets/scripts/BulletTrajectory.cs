using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 10f;
    [SerializeField] private int bulletDamage = 5;

    private Vector3 moveVelocity;


    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    public void Initialize(Vector3 startPos, Vector3 endPos)
    {
        transform.position = startPos;

        Vector3 direction = (endPos - startPos).normalized;
        moveVelocity = direction * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += moveVelocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 3)
        {
            Destroy(this.gameObject);
        }
    }


    IEnumerator DestroyBullet()
    {

        yield return new WaitForSeconds(2f);

        Destroy(this.gameObject);
        
    }

}