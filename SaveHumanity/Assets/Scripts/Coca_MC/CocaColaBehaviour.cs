using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocaColaBehaviour : MonoBehaviour
{
    public GameObject CocaPrefab, Spawner1, Spawner2;
    private float UltimoAtaque, IntervaloMinimo = 5f, IntervaloMaximo = 7f, DuracaoAtaque = 3f;

    // Start is called before the first frame update
    void Start()
    {
        UltimoAtaque = Time.time + Random.Range(IntervaloMinimo, IntervaloMaximo);
    }

    // Update is called once per frame
    void Update()
    {
        if (UltimoAtaque <= Time.time)
        {
            UltimoAtaque = Time.time + Random.Range(IntervaloMinimo, IntervaloMaximo) + DuracaoAtaque;
            AtacarCoca();
        }
    }

    void AtacarCoca()
    {
        GameObject instancia = Instantiate(CocaPrefab);
        switch (Random.Range(1,3))
        {
            case 1:
                instancia.GetComponent<CocaController>().Ativar(Spawner1.transform, DuracaoAtaque);
                break;
            case 2:
                instancia.GetComponent<CocaController>().Ativar(Spawner2.transform, DuracaoAtaque);
                break;
        }
    }
}
