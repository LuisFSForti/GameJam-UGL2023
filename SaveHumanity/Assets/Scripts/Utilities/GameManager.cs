using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Flow Game Properties")]
    [HideInInspector] public bool isPaused;

    public Player player;


    #region "INITIAL SETTINGS / UPDATE"
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
        //SET PLAYER SPAWN POSITION
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            SetGamePause(!isPaused);
    }


    #endregion

    #region "PAUSE GAME"

    public void SetGamePause(bool pause)
    {
        isPaused = pause;

        if (pause)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    #endregion
}
