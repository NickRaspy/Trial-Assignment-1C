using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage { get; set; }
    public float Speed {get; set;}
    [SerializeField] private float lifetime = 2f;
    private void Start()
    {
        StartCoroutine(Lifetime());
    }
    void Update()
    {
        transform.Translate(Speed * Time.deltaTime * Vector2.right, Space.Self);
    }
    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyBehavior>().CurrentHealth -= Damage;
            Destroy(gameObject);
        }
    }
}
