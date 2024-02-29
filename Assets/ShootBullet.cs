using UnityEngine;
using UnityEngine.InputSystem;

public class ShotBullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 10f;


    private void OnShoot(InputValue value)
    {

        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRigidbody = bulletInstance.GetComponent<Rigidbody>();


        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = firePoint.forward * bulletSpeed;
        }
    }

   
}