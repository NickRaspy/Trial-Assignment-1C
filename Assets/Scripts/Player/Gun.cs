using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private GameObject bulletBank;

    public int BulletDamage { get; set; }
    public float BulletSpeed { get; set; }
    public void Shoot()
    {
        Vector3 rot = bullet.transform.rotation.eulerAngles;
        rot += transform.localRotation.eulerAngles;
        Bullet newBullet = Instantiate(bullet, transform.GetChild(0).position, Quaternion.Euler(rot));
        newBullet.transform.parent = bulletBank.transform;
        newBullet.Damage = BulletDamage;
        newBullet.Speed = BulletSpeed;
    }
}
