using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVidaController : MonoBehaviour
{
    [SerializeField] private float Vida, Regeneracao;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vida <= 0)
        {
            Destroy(gameObject);
        }

        Vida += Regeneracao * Time.deltaTime;
    }

    public void SofrerDano(float dano)
    {
        Vida -= dano;
    }
}
