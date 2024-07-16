using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;
public class EnemyBehavior : MonoBehaviour
{
    public GameManager GameManager { get; set; }

    [SerializeField] private Slider healthBar;
    private int currentHealth = 3;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set 
        { 
            currentHealth = value; 
            healthBar.value-- ; 
            if (currentHealth <= 0) 
            {
                GameManager.CurrentScore++; 
                Destroy(gameObject); 
            }  
        }
    }
    public float CurrentSpeed { get; set; }
    private void Start()
    {
        healthBar.maxValue = currentHealth;
        healthBar.value = currentHealth;
        transform.DOMoveY(-5f, Vector3.Distance(transform.position, new Vector3(transform.position.x, -5f, transform.position.z)) / CurrentSpeed); // move to point lower finish line (time calculated by distance/speed)
    }
    private void OnDisable()
    {
        transform.DOKill(); //stop DOTween when destroyed
    }
}
