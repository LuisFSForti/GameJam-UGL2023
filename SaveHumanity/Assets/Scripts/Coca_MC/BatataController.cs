using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

public class BatataController : MonoBehaviour
{
    private GameObject Player;
    private float Velocidade = 8f;
    private bool Preparando = true;
    private float Calorias = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Ativar(GameObject player, Transform origem)
    {
        Player = player;
        this.transform.position = origem.position;
        this.transform.eulerAngles = new Vector3(0, 0, Random.Range(85f,96f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Velocidade * Time.deltaTime;

        if(Preparando)
        {
            if(transform.position.y > 3)
            {
                Preparando = false;

                //https://www.youtube.com/watch?v=RNPetUa8_PQ
                Vector3 vectorToTarget = Player.transform.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().Engordar(Calorias);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
