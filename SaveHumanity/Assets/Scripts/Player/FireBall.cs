using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    [HideInInspector] public int direction = 1;

    private Vector2 dir;

    private void Start()
    {
        if(direction > 0)
            dir = Vector2.right;
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            dir = Vector2.left;
        }
    }

    private void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<BossVidaController>().SofrerDano(damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
