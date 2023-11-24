using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveIcon : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float damage;

    private Vector2 originalPos;

    private void Awake()
    {
        originalPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.player.ReceberDano(damage);
        }
        else if (collision.gameObject.tag == "Limit")
        {
            transform.position = originalPos;
            gameObject.SetActive(false);
        }
    }
}
