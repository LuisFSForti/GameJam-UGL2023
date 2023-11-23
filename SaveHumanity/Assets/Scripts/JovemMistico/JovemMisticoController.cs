using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JovemMisticoController : MonoBehaviour
{
    private float IntervaloTiroMinimo = 4f, IntervaloTiroMaximo = 6f, UltimoTiro;
    private float IntervaloAtaqueMinimo = 1f, IntervaloAtaqueMaximo = 2f, UltimoAtaque;
    public GameObject TiroPrefab, TouroSigno, TouroPrefab, AquarioSigno, AquarioPrefab, CancerSigno, CancerPrefab, Player;
    public GameObject SpawnerSigno, SpawnerTouroE, SpawnerTouroD, SpawnerAquarioE, SpawnerAquarioD, SpawnerCancerE, SpawnerCancerD;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        UltimoTiro = Time.time + IntervaloTiroMinimo;
        UltimoAtaque = Time.time + IntervaloAtaqueMinimo;
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - Player.transform.position).x > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (UltimoTiro <= Time.time)
        {
            UltimoTiro = Time.time + Random.Range(IntervaloTiroMinimo, IntervaloTiroMaximo);
            GameObject instancia = Instantiate(TiroPrefab);
            instancia.GetComponent<TiroJovemMistico>().Ativar(Player, transform);
        }

        if (UltimoAtaque <= Time.time)
        {
            UltimoAtaque = Time.time + Random.Range(IntervaloAtaqueMinimo + 2f, IntervaloAtaqueMaximo + 2f);

            switch(Random.Range(1, 7))
            {
                case 1:
                    StartCoroutine(InvocarTouro(SpawnerSigno, SpawnerTouroE));
                    break;
                case 2:
                    StartCoroutine(InvocarTouro(SpawnerSigno, SpawnerTouroD));
                    break;
                case 3:
                    StartCoroutine(InvocarAquario(SpawnerSigno, SpawnerAquarioE));
                    break;
                case 4:
                    StartCoroutine(InvocarAquario(SpawnerSigno, SpawnerAquarioD));
                    break;
                case 5:
                    StartCoroutine(InvocarCancer(SpawnerSigno, SpawnerCancerE));
                    break;
                case 6:
                    StartCoroutine(InvocarCancer(SpawnerSigno, SpawnerCancerD));
                    break;
                default: break;
            }
        }
    }

    IEnumerator InvocarTouro(GameObject spawnerSigno, GameObject spawnerTouro)
    {
        GameObject instance = Instantiate(TouroSigno, SpawnerSigno.transform);
        yield return new WaitForSeconds(1);
        Destroy(instance);

        Instantiate(TouroPrefab, spawnerTouro.transform);
    }
    IEnumerator InvocarAquario(GameObject spawnerSigno, GameObject spawnerAquario)
    {
        GameObject instance = Instantiate(AquarioSigno, SpawnerSigno.transform);
        yield return new WaitForSeconds(1);
        Destroy(instance);

        Instantiate(AquarioPrefab, spawnerAquario.transform);
    }

    IEnumerator InvocarCancer(GameObject spawnerSigno, GameObject spawnerCancer)
    {
        GameObject instance = Instantiate(CancerSigno, SpawnerSigno.transform);
        yield return new WaitForSeconds(1);
        Destroy(instance);

        Instantiate(CancerPrefab, spawnerCancer.transform);
    }
}
