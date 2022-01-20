using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LolTowerAttacker : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffectPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float hitRange;

    private Minion targetMinion = null;
    private int damage = 0;


    private void FixedUpdate()
    {
        if(targetMinion != null)
        {
            GoToTarget();

            if (IsFireBallHitTarget())
                GetHitTarget();
        }

    }

    private void GoToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, targetMinion.transform.position, speed);
    }

    private void GetHitTarget()
    {
        targetMinion.GetHit(damage);

        //Create explosition effect and destroy it after 2 seconds
        Destroy(Instantiate(explosionEffectPrefab, transform.position, transform.rotation), 2f);

        Destroy(gameObject);
    }

    //if Fireball reach the target
    private bool IsFireBallHitTarget()
    {
        var sqrhitRange = (transform.position - targetMinion.transform.position).sqrMagnitude;

        if (sqrhitRange <= (hitRange * hitRange))
            return true;

        return false;
    }

    public void AttackSetup(Minion targetMinion, int damage)
    {
        this.targetMinion = targetMinion;
        this.damage = damage;
    }

}
