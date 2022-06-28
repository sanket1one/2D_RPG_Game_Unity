using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
/*    [SerializeField] private GameObject[] fireballs;*/
    [SerializeField] private GameObject fireball;

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
        // get input from player
        // spawn a projectile
        // where to spawn the projectile

    }
    private void Attack() {
        //Play an attack animation

        //Detect enemies in range of attack
        // damage  them 
        Instantiate(fireball, firePoint.position, firePoint.rotation);

        anim.SetTrigger("attack");
        cooldownTimer = 0;

    }

}
