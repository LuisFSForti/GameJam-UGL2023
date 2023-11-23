using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class McDonaldsBehaviour : MonoBehaviour
{
    //Geral
    private float IntervaloMinimo = 4f, IntervaloMaximo = 5f, UltimoAtaque;
    private int Ataque = 0;
    public GameObject BatataPrefab, NuggetPrefab, HamburguerPrefab, Player;

    //Batata
    private float DuracaoBatata = 2f, IntervaloBatata = 0.25f, UltimoTiro = 0f;

    //Nugget
    public GameObject Spawner1, Spawner2, Spawner3, SpawnerAtual = null, UltimoSpawner = null;
    private float DuracaoNugget = 2.5f, IntervaloNugget = 0.6f, UltimoNugget = 0f;

    //Hamburguer
    private float DuracaoHamburguer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        UltimoAtaque = Time.time + Random.Range(IntervaloMinimo, IntervaloMaximo);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(UltimoAtaque <= Time.time && Ataque == 0)
        {
            UltimoAtaque = Time.time;
            Ataque = Random.Range(1, 4);
            IntervaloMaximo -= 0.15f;
            IntervaloMinimo -= 0.15f;

            if (IntervaloMinimo < 2)
                IntervaloMinimo = 2f;
            if (IntervaloMaximo < 3)
                IntervaloMaximo = 3f;
        }

        switch (Ataque)
        {
            case 1:
                AtaqueBatatas();

                if (UltimoAtaque + DuracaoBatata < Time.time)
                {
                    UltimoAtaque += Random.Range(IntervaloMinimo, IntervaloMaximo);
                    Ataque = 0;
                }

                break;

            case 2:
                if(SpawnerAtual == null)
                {
                    do
                    {
                        switch (Random.Range(1, 4))
                        {
                            case 1:
                                SpawnerAtual = Spawner1;
                                break;
                            case 2:
                                SpawnerAtual = Spawner2;
                                break;
                            case 3:
                                SpawnerAtual = Spawner3;
                                break;
                            default:
                                SpawnerAtual = Spawner1;
                                break;
                        }
                    } while (SpawnerAtual == UltimoSpawner);
                }

                AtaqueNuggets();

                if (UltimoAtaque + DuracaoNugget < Time.time)
                {
                    UltimoAtaque += Random.Range(IntervaloMinimo, IntervaloMaximo);
                    Ataque = 0;
                    UltimoSpawner = SpawnerAtual;
                    SpawnerAtual = null;
                }

                break;

            case 3:
                if(SpawnerAtual == null)
                    AtaqueHamburguer();

                if (UltimoAtaque + DuracaoHamburguer < Time.time)
                {
                    UltimoAtaque += Random.Range(IntervaloMinimo, IntervaloMaximo);
                    Ataque = 0;
                    UltimoSpawner = SpawnerAtual;
                    SpawnerAtual = null;
                }

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
        if (UltimoNugget + IntervaloNugget < Time.time)
        {
            UltimoNugget = Time.time;
            GameObject instancia = Instantiate(NuggetPrefab);
            instancia.GetComponent<NuggetController>().Ativar(SpawnerAtual.transform);
        }
    }

    void AtaqueHamburguer()
    {
        do
        {
            switch (Random.Range(1, 4))
            {
                case 1:
                    SpawnerAtual = Spawner1;
                    break;
                case 2:
                    SpawnerAtual = Spawner2;
                    break;
                case 3:
                    SpawnerAtual = Spawner3;
                    break;
                default:
                    SpawnerAtual = Spawner1;
                    break;
            }
        } while (SpawnerAtual == UltimoSpawner);

        GameObject instancia = Instantiate(HamburguerPrefab);
        instancia.GetComponent<HamburguerController>().Ativar(SpawnerAtual.transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().ReceberDano(10);
        }
    }
}
