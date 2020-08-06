using UnityEngine;

namespace Toon.Control
{
    public class PiranhaPlant : MonoBehaviour
    {
        GameObject player;
        Animator myAnimator;
        [SerializeField] float attackDistance = 4f;
        [SerializeField] bool canShootPoison = false;
        [SerializeField] float poisonAttackWaitTime = 3f;
        [SerializeField] GameObject projectile = null;
        [SerializeField] Transform shootingPoint = null;
        
        float timeBetweenPoisonAttacks = Mathf.Infinity;

        void Start()
        {
            myAnimator = GetComponent<Animator>();
            player = GameObject.FindWithTag("Player");
        }

        void Update()
        {
            if (canShootPoison)
            {
                timeBetweenPoisonAttacks += Time.deltaTime;
            }
            if (player != null)
            {
                Attack();
            }
        }

        private void Attack()
        {
            FlipSprite();
            if (InAttackRange())
            {
                // If plant shoots poison ball use set trigger 
                if (canShootPoison)
                {
                    if (timeBetweenPoisonAttacks < poisonAttackWaitTime) return;
                    PoisonAttack();
                    return;
                }

                myAnimator.SetBool("attack", true);
            }
            else
            {
                if (canShootPoison) return;

                myAnimator.SetBool("attack", false);
            }
        }

        private void PoisonAttack()
        {
            timeBetweenPoisonAttacks = 0;
            myAnimator.SetTrigger("poisonAttack");
        }

        void Shoot() //Animation event
        {
            Instantiate(projectile, shootingPoint.position, shootingPoint.rotation);
        }

        private void FlipSprite()
        {
            float playerPosX = player.transform.position.x;
            float plantPosX = transform.position.x;
            float distance = playerPosX - plantPosX;

            if (distance < 0)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        private bool InAttackRange()
        {
            float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
            return distanceToPlayer <= attackDistance;
        }
    }
}
