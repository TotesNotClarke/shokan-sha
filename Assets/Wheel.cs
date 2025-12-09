using UnityEngine;

public class Wheel : StateMachineBehaviour
{

    public float speed;
    public float timer;

    public bool moveRight = true;

    private Transform playerPos;
    private Transform bossPos;
    Rigidbody2D rb;
    Boss boss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        timer = 30;

        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bossPos = GameObject.FindGameObjectWithTag("Boss").GetComponent<Transform>();
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();

        if(bossPos.position.x >= playerPos.position.x)
        {
            moveRight = false;
        }
        else
        {
            moveRight = true;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(timer <= 0)
        {
            animator.SetTrigger("Walk");
        }
        else
        {
            timer -= Time.deltaTime;
            float direction = moveRight ? 1f : -1f;
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
        }

        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
