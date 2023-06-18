using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public bool isMelee;
    public bool isRock;

    void OnCollisionEnter(Collision other)
    {
        if (!isRock && !isMelee && other.gameObject.tag == "Floor")
        {
            Destroy(gameObject, 3f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((!isMelee && other.gameObject.tag == "Wall") || (!isRock && !isMelee && other.gameObject.tag == "Floor"))
        {
            Destroy(gameObject);
        }
    }
}
