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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if(Chefao)
        {
            if(GameObject.FindGameObjectsWithTag("Boss").Length == 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if(next.buildIndex % 2 == 0 || next.buildIndex == 1)
        {
            Player.SetActive(false);
            Canvas.SetActive(false);
            Chefao = false;
        }
        else
        {
            Player.SetActive(true);
            Canvas.SetActive(true);
            Chefao = true;
        }
        Player.transform.position = Vector3.zero;
    }
}
