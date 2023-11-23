using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquarioController : MonoBehaviour
{
    public float Velocidade, Dano;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.up * Time.deltaTime * Velocidade;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().ReceberDano(Dano);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
