using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoverTexto : MonoBehaviour
{
    private float Velocidade = 1.5f;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * Velocidade;

        if (transform.position.y > 10 || Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
