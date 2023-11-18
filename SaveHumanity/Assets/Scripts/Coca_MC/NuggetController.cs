using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuggetController : MonoBehaviour
{
    private float Velocidade = 3f;

    public void Ativar(Transform origem)
    {
        this.transform.position = origem.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * Velocidade;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
