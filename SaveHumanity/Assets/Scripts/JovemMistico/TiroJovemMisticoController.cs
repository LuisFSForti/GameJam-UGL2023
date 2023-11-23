using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

public class TiroJovemMistico  : MonoBehaviour
{
    private GameObject Player;
    private float Velocidade = 8f, Dano = 20f, PeriodoPerseguicao = 0.7f, TempoVivo = 0f;

    public void Ativar(GameObject player, Transform origem)
    {
        Player = player;
        this.transform.position = origem.position;

        //https://www.youtube.com/watch?v=RNPetUa8_PQ
        Vector3 vectorToTarget = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    // Update is called once per frame
    void Update()
    {
        TempoVivo += Time.deltaTime;

        if(TempoVivo < PeriodoPerseguicao)
        {
            Vector3 vectorToTarget = Player.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        transform.position += transform.right * Velocidade * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().ReceberDano(Dano);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
