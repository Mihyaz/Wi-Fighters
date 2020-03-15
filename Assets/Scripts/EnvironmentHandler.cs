using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHandler : MonoBehaviour
{
    public GameObject HitParticle;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(HitParticle, collision.gameObject.transform.position, collision.gameObject.transform.rotation.normalized);
            Destroy(collision.gameObject); //Sorry to use "Destroy". Too lazy to implement pooler
        }
    }
}
