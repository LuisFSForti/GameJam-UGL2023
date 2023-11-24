using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameObject Player, Canvas;
    private bool Chefao = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            AvancarCena();
        }

        if(Chefao)
        {
            if (GameObject.FindGameObjectsWithTag("Boss").Length == 0)
                AvancarCena();
        }

        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    public void AvancarCena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if(next.buildIndex % 2 == 1 || next.buildIndex == 2)
        {
            if(Player != null) {
                Player.SetActive(false);
                Canvas.SetActive(false);
            }
            Chefao = false;
        }
        else
        {
            if (Player != null)
            {
                Player.SetActive(true);
                Canvas.SetActive(true);
            }
            Chefao = true;
        }
        if (Player != null)
            Player.transform.position = Vector3.zero;
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(0);
    }
}
