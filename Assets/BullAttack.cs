using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullAttack : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate;
    private float nextFireTime;
    private Transform player;
    Animator anim;


    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;
    public int damage;

    public GameObject players;

    private bool isAttack;

    public Collider col;

    private int vur;

    // Start is called before the first frame update
    void Start()
    {
        vur = 0;
        isAttack = true;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 targetPosition = new Vector3(player.position.x, transform.position.z);

        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);


        if (GameObject.FindWithTag("Player"))
        {
            if (isAttack)
            {
                float distanceFromPlayer = Vector3.Distance(player.position, transform.position);

                Vector3 targetDirection = player.position - transform.position;

                float singleStep = 1 * Time.deltaTime;

                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                
                
                transform.rotation = Quaternion.LookRotation(newDirection);
                Debug.DrawRay(transform.position, newDirection, Color.red);



                if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
                {
                    transform.position = Vector3.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
                    anim.SetBool("Running", true);
                }
                else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
                {
                    isAttack = false;
                    anim.SetTrigger("Attack");
                    nextFireTime = Time.time + fireRate;
                    StartCoroutine(ExampleCoroutine());

                }
                else
                {
                    //anim.SetBool("Running", false);
                }
                Flip();
            }
            
        }

    }

    IEnumerator ExampleCoroutine()
    {
        
        yield return new WaitForSeconds(1);
        isAttack = true;
    }


    public void Attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        Debug.Log(enemiesToDamage);
        
        foreach (Collider2D col in enemiesToDamage)
        {
            //Destroy(players);
           
            
            if (vur == 1)
            {
                col.GetComponent<Player>().TakeDamage(damage);
            }
            
        }

        
    }

    public void FinishedAttack()
    {
        anim.SetBool("Running", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.z > player.position.z)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;

        }

        transform.eulerAngles = rotation;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Capsule")
        {
            vur = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Capsule")
        {
            vur = 0;
        }
    }

}
