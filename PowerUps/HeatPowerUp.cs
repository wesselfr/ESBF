using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatPowerUp : MonoBehaviour {

    public static PowerUpEvent OnUse;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            collision.GetComponent<PlayerScript>().Heat += 3;
            if (OnUse != null)
            {
                OnUse(transform.position);
            }
            Destroy(this.gameObject);
        }
    }

}
