using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class McDonaldsBehaviour : MonoBehaviour
{
    private float IntervaloMinimo = 3f, IntervaloMaximo = 5f, UltimoAtaque = 0f;
    private float DuracaoBatata = 2f, IntervaloBatata = 0.25f, UltimoTiro = 0f;

    private int Ataque = 0;
    public GameObject BatataPrefab, NuggetPrefab, HamburguerPrefab, Player;

    // Start is called before the first frame update
    void Start()
    {
        UltimoAtaque = Time.time + IntervaloMaximo;
    }

    // Update is called once per frame
    void Update()
    {
        if(UltimoAtaque <= Time.time && Ataque == 0)
        {
            UltimoAtaque = Time.time;
            Ataque = Random.Range(1, 5);
        }

        switch (Ataque)
        {
            case 1:
            case 2:
                AtaqueBatatas();

                if (UltimoAtaque + DuracaoBatata < Time.time)
                {
                    UltimoAtaque += Random.Range(IntervaloMinimo, IntervaloMaximo);
                    Ataque = 0;
                }

                break;

            case 3:
                AtaqueNuggets();
                break;

            case 4:
                AtaqueHamburguer();
                break;

            default: break;
        }
    }

    void AtaqueBatatas()
    {
        if(UltimoTiro + IntervaloBatata < Time.time)
        {
            UltimoTiro = Time.time;
            GameObject instancia = Instantiate(BatataPrefab);
            instancia.GetComponent<BatataController>().Ativar(Player, transform);
        }
    }

    void AtaqueNuggets()
    {

    }

    void AtaqueHamburguer()
    {

    }
}
