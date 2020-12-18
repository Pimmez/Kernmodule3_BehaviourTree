using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardGun : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private Rigidbody bulletPrefab;
    public void Shoot()
    {
        Rigidbody _bulletInstance;
        _bulletInstance = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation) as Rigidbody;
        _bulletInstance.AddForce(muzzle.forward * 5000);
    }
}