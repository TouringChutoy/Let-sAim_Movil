using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebotar : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PerderControl>().TomarGolpe(collision.GetContact(0).normal);
        }
    }
}
