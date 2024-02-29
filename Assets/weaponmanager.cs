using System;
using UnityEngine.Events;
using UnityEngine;


public class weaponmanager : MonoBehaviour
{
    public GameObject ActiveWeapon1;
    public GameObject DeactiveWeapon2;
    public GameObject DeactiveWeapon3;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActiveWeapon1.gameObject.SetActive(true);
            DeactiveWeapon2.gameObject.SetActive(false);
            DeactiveWeapon3.gameObject.SetActive(false);


        }
    }

   
}
