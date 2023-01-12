using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
