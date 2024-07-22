using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    Animator animator;
    enum State { Idle, ChooseDestination, Move }
    private State state = State.Idle;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private float counter;
    private float idleTime;
    [SerializeField]
    private float moveSpeed = 1;
    [SerializeField]
    private float moveRange = 3;
    [SerializeField]
    private float maxIdleTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void UpdateAnimation()
    {
        if (state != State.Move)
        {
            animator.Play("ZombieIdle");
            return;
        }

        animator.Play("ZombieWalk");
        var directionX = Mathf.Sign((endPosition - startPosition).x);
        GetComponent<SpriteRenderer>().flipX = directionX > 0.1f;

    }
    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();

        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.ChooseDestination:
                ChooseDestination();
                break;
            case State.Move:
                Move();
                break;
        }
    }

    void Idle()
    {
        counter += Time.deltaTime;
        if (counter < idleTime) return;
        
        state = State.ChooseDestination;
    }

    void ChooseDestination()
    {
        startPosition = transform.position;
        var direction = Random.insideUnitCircle;
        var hit = Physics2D.Raycast(startPosition, direction, moveRange);

        if (!hit)
        {
            endPosition = startPosition + direction * moveRange;
            state = State.Move;
        }
    }

    void Move()
    {
        transform.position += moveSpeed * Time.deltaTime * (Vector3)(endPosition - startPosition).normalized;
        if (Vector2.Distance(endPosition, transform.position) < moveSpeed)
        {
            counter = 0;
            idleTime = Random.Range(0, maxIdleTime);
            state = State.Idle;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, moveRange);
    }
}
