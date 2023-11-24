using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.U2D;

public class Boss_SocialMedia : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float attacksCoolDown;

    [Header("Icons")]
    [SerializeField] private GameObject[] loveIcons;
    [SerializeField] private GameObject[] dislikeIcons;
    [SerializeField] private float iconsCoolDown;

    [Header("Money")]
    [SerializeField] private GameObject moneyPrefab;
    [SerializeField] private float moneyForce;
    [SerializeField] private float moneyCoolDown;
    [SerializeField] private float moneyAttDuration;

    private bool isAttacking;
    private Animator anim;

    #region "Starts"

    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Attack());
    }

    #endregion

    #region "Attacks"

    private IEnumerator Attack()
    {
        int r = Random.Range(0, 3);
        isAttacking = true;

        switch (r)
        {
            case 0:
            {
                anim.SetInteger("Atk", 0);
                StartCoroutine(LoveIcon(loveIcons));
                break;
            }
            case 1:
            {
                anim.SetInteger("Atk", 1);
                StartCoroutine(LoveIcon(dislikeIcons));
                break;
            }
            case 2:
            {
                anim.SetInteger("Atk", 2);
                StartCoroutine(Money());
                break;
            }
        }


        yield return new WaitUntil(() => !isAttacking);

        StartCoroutine(Attack());
    }

    private IEnumerator LoveIcon(GameObject[] list)
    {
        int r = Random.Range(0, 2);
        int i;

        if (r == 0)
        {
            for (i = 0; i < list.Length; i++)
            {
                list[i].SetActive(true);
                yield return new WaitForSeconds(iconsCoolDown);
            }
        }
        else
        {
            for (i = list.Length - 1; i >= 0; i--)
            {
                list[i].SetActive(true);
                yield return new WaitForSeconds(iconsCoolDown);
            }
        }

        yield return new WaitForSeconds(attacksCoolDown);

        isAttacking = false;
    }

    private IEnumerator Money()
    {
        float startTime = Time.time;
        
        while(Time.time - startTime < moneyAttDuration)
        {
            GameObject m = Instantiate(moneyPrefab);
            m.transform.position = transform.position;
            Rigidbody2D mRig = m.GetComponent<Rigidbody2D>();

            float force = Random.Range(-moneyForce, moneyForce);
            Vector2 dir = new Vector2(1, 1);
            mRig.AddForce(dir * force * Time.deltaTime, ForceMode2D.Impulse);

            mRig.gravityScale = 0;
            mRig.velocity = new Vector2(dir.x * force, Mathf.Abs(dir.y * force));
            mRig.gravityScale = 3;

            yield return new WaitForSeconds(moneyCoolDown); 
        }

        yield return new WaitForSeconds(attacksCoolDown);


        isAttacking = false;
        StartCoroutine(Attack());
    }

  

    #endregion
}
