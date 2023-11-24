using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteWomanInstagram : MonoBehaviour
{
    [Header("LoveIcon")]
    [SerializeField] private GameObject[] loveIcons;
    [SerializeField] private float loveIconsCoolDown;

    #region "Starts"

    private void Start()
    {
        StartCoroutine(LoveIcon());
    }

    #endregion

    #region "Attacks"

    private IEnumerator LoveIcon()
    {
        int r = Random.Range(0, 2);
        int i;

        if(r == 0)
        { 
            for(i = 0; i < loveIcons.Length; i++)
            {
                loveIcons[i].SetActive(true);
                yield return new WaitForSeconds(loveIconsCoolDown);
            }
        }
        else
        {
            for (i = loveIcons.Length-1; i >= 0; i--)
            {
                loveIcons[i].SetActive(true);
                yield return new WaitForSeconds(loveIconsCoolDown);
            }
        }
    }


    #endregion
}
