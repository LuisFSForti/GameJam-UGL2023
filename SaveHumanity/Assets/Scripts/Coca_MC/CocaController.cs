using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocaController : MonoBehaviour
{
    private float Velocidade = 3f, Desacelerando = 1f, Calorias = 0.2f;

    public void Ativar(Transform origem, float duracao)
    {
        transform.position = origem.position + new Vector3(0, transform.localScale.y/2f, 0);
        Velocidade = Velocidade * 10f/duracao;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.up * Velocidade * Time.deltaTime * 1 / Desacelerando;
        if (Desacelerando <= 1f)
            Desacelerando = 1f;
        else
        {
            Desacelerando -= 5f * Time.deltaTime;
        }

        if (transform.position.y < -(6 + transform.localScale.y / 2f))
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().Engordar(Calorias);
        }
        if (collision.tag == "Floor")
        {
            Desacelerando = 10f;
        }
    }
}
