using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatataController : MonoBehaviour
{
    private GameObject Player;
    private float Velocidade = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Ativar(GameObject player, Transform transform)
    {
        Player = player;
        this.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * Velocidade;
    }
}
