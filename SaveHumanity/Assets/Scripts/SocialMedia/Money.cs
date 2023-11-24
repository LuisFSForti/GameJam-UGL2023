using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.player.ReceberDano(damage);
        }
        else if (collision.gameObject.tag == "Limit")
        {
            gameObject.SetActive(false);
        }
    }
}
