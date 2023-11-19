using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuggetController : MonoBehaviour
{
    private float Velocidade = 3f, Calorias = 0.05f;

    public void Ativar(Transform origem)
    {
        this.transform.position = origem.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * Velocidade;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Engordar(Calorias);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
