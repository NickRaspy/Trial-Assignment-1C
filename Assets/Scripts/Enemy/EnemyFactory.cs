using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory : MonoBehaviour
{
    [Inject] GameManager _gameManager;

    [SerializeField] private EnemyBehavior enemy;
    public void GetNewEnemy(Vector3 spawnPosition, float speed, int health)
    {
        EnemyBehavior newEnemy = Instantiate(enemy, transform);
        newEnemy.CurrentHealth = health; newEnemy.CurrentSpeed = speed; newEnemy.transform.position = spawnPosition;
        newEnemy.GameManager = _gameManager; //inject workaround
    }
}
