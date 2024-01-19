using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammo : MonoBehaviour
{
    public float life = 3;
    public float damageAmount = 5;

    void Awaken(){
        Destroy(gameObject, life);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.CompareTag("Chaser"))
        {
            Chaser chaser = collision.gameObject.GetComponent<Chaser>();
            chaser.Damage(damageAmount);
        }

    }
}
