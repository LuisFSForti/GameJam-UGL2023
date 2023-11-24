using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoverTexto : MonoBehaviour
{
    private float Velocidade = 1.5f, TempoDeTela;

    private void Start()
    {
        TempoDeTela = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * Velocidade;

        if (transform.position.y > 10 || Input.GetKeyDown(KeyCode.Space) && TempoDeTela + 2f <= Time.time)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
