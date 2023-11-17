using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class McDonaldsBehaviour : MonoBehaviour
{
    private float IntervaloMinimo = 3f, IntervaloMaximo = 5f, UltimoAtaque = 0f;
    private float DuracaoBatata = 5f, IntervaloBatata = 0.5f, UltimoTiro = 0f;

    public GameObject BatataPrefab, NuggetPrefab, HamburguerPrefab, Player;

    // Start is called before the first frame update
    void Start()
    {
        UltimoAtaque = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(UltimoAtaque <= Time.time)
        {
            UltimoAtaque = Time.time;
            Debug.Log(UltimoAtaque);
            int teste = Random.Range(1, 5);
            Debug.Log(teste);
            switch (teste)
            {
                case 1:
                case 2:
                    Debug.Log("BBBBBBBBB");
                    Debug.Break();
                    AtaqueBatatas();
                    break;

                case 3:
                    AtaqueNuggets();
                    break;

                case 4:
                    AtaqueHamburguer();
                    break;

                default: break;
            }
            UltimoAtaque += Random.Range(IntervaloMinimo, IntervaloMaximo);
        }
    }

    void AtaqueBatatas()
    {
        while(Time.time < UltimoAtaque + DuracaoBatata)
        {
            if(UltimoTiro + IntervaloBatata < Time.time)
            {
                UltimoTiro = Time.time;
                GameObject instancia = Instantiate(BatataPrefab);
                instancia.GetComponent<BatataController>().Ativar(Player, transform);
            }
        }
    }

    void AtaqueNuggets()
    {

    }

    void AtaqueHamburguer()
    {

    }
}
