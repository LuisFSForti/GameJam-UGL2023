using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JovemMisticoController : MonoBehaviour
{
    private float IntervaloMinimo = 4f, IntervaloMaximo = 5f, UltimoAtaque;
    public GameObject TiroPrefab, Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        UltimoAtaque = Time.time + IntervaloMinimo;
    }

    // Update is called once per frame
    void Update()
    {
        if((transform.position - Player.transform.position).x > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (UltimoAtaque <= Time.time)
        {
            UltimoAtaque = Time.time + Random.Range(IntervaloMinimo, IntervaloMaximo);
            GameObject instancia = Instantiate(TiroPrefab);
            instancia.GetComponent<TiroJovemMistico>().Ativar(Player, transform);
        }
    }
}
