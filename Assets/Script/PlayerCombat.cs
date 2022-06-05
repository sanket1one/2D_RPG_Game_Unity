using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField]private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    // Update is called once per frame

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetMouseButton(0)  && cooldownTimer > attackCooldown && playerMovement.canAttack()){
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }
    private void Attack() {
        //Play an attack animation

        //Detect enemies in range of attack
        // damage  them 
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        /*        fireballs[FindFireballs()].transform.position = firePoint.position;
                fireballs[FindFireballs()].GetComponent<Projectile>().setDirection(transform.localScale.x);
        */
        fireballs[0].transform.position = firePoint.position;
        fireballs[0].GetComponent<Projectile>().setDirection(Mathf.Sign(transform.localScale.x));
        /*poolimg*/
    }

    private int FindFireballs() {
        for (int i = 0; i < fireballs.Length; i++) {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
