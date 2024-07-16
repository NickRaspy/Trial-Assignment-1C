using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRadius : MonoBehaviour
{
    private float radius = 1f;
    public float Radius 
    {
        get { return radius; }
        set 
        { 
            radius = value;
            transform.localScale = new Vector3(radius, radius, radius);
        }
    }
    public readonly List<Transform> enemiesInRadius = new();
    public Transform ClosestEnemy { get; set; }
    private float lastDistance = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) enemiesInRadius.Add(collision.transform);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) enemiesInRadius.Remove(collision.transform);
    }
    private void Update()
    {
        if (enemiesInRadius.Count > 1)
        {
            enemiesInRadius.ForEach(a =>
            {
                float distance = Vector3.Distance(transform.position, a.position);
                if (lastDistance == 0 || distance < lastDistance)
                {
                    lastDistance = distance;
                    ClosestEnemy = a;
                }
            });
        }
        else if (enemiesInRadius.Count == 1)
        {
            ClosestEnemy = enemiesInRadius[0];
        }
        else ClosestEnemy = null;
    }
}
