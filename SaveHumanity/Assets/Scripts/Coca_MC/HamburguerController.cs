using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamburguerController : MonoBehaviour
{
    private float Espera = 1f, Inicio = 0f, Velocidade = 20f;

    public void Ativar(Transform origem)
    {
        this.transform.position = origem.position + new Vector3(0,0.2f,0);
        Inicio = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Inicio + Espera <= Time.time)
        {
            transform.position += transform.right * Velocidade * Time.deltaTime;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
