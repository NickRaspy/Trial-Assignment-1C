using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FinishLine : MonoBehaviour
{
    [Inject] GameManager _gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            _gameManager.Health--;
        }
    }
}
