using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerBehavior : MonoBehaviour
{
    [Inject] GameManager _gameManager;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float distanceRadius = 10f;
    [SerializeField] private float shootingSpeed = 1f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float bulletSpeed = 5f;

    [SerializeField] private PlayerRadius playerRadius;
    [SerializeField] private Gun gun;

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        gun.GetComponent<Animator>().speed = shootingSpeed;
        gun.BulletDamage = bulletDamage;
        gun.BulletSpeed = bulletSpeed;
        playerRadius.Radius = distanceRadius;
    }
    void Update()
    {
        Move();
        GunShoot();
    }
    void Move()
    {
        if (!_gameManager.IsGameGoing) return;
        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0f);
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            animator.SetBool("isWalking", true);
        else animator.SetBool("isWalking", false);
        Bounds bounds = CameraExtensions.OrthographicBounds(Camera.main);
        Vector3 clampedPos = new(Mathf.Clamp(transform.position.x, bounds.min.x, bounds.max.x), Mathf.Clamp(transform.position.y, bounds.min.y, _gameManager.finishLine.position.y), 0f);
        transform.position = clampedPos;
    }
    void GunShoot()
    {
        if (playerRadius.ClosestEnemy == null)
        {
            gun.GetComponent<Animator>().SetBool("isShooting", false);
            return;
        }
        Vector3 ePos = playerRadius.ClosestEnemy.position;
        Vector3 delta = ePos - gun.transform.position;
        float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - 90f;
        gun.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        gun.GetComponent<Animator>().SetBool("isShooting", true);
    }
}
