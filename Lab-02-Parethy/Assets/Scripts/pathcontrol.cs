using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class pathcontrol : MonoBehaviour
{
    [SerializeField]
    public PathManager pathManager;
    public Animator animator;
    bool isWalking;

    List<waypoint> thePath;
    waypoint target;

    public float MoveSpeed;
    public float RotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        thePath = pathManager.GetPath();
        if (thePath != null && thePath.Count > 0)
        {
            // set starting target to the first waypoint
            target = thePath[0];
        }

        isWalking = false;
        animator.SetBool("isWalking", isWalking);
    }

    void rotateTowardsTarget()
    {
        float stepSize = RotateSpeed * Time.deltaTime;

        Vector3 targetDir = target.pos - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void moveForward()
    {
        float stepSize = Time.deltaTime * MoveSpeed;
        float distanceToTarget = Vector3.Distance(transform.position, target.pos);
        if (distanceToTarget < stepSize)
        {
            // we will overshoot the target
            return;
        }
        // take a step forward
        Vector3 moveDir = Vector3.forward;
        transform.Translate(moveDir * stepSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            // toggle if any key is pressed
            isWalking = !isWalking;
            animator.SetBool("isWalking", isWalking);
        }
        if (isWalking)
        {
            rotateTowardsTarget();
            moveForward();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("works");

        if (other.name.Equals("Cube"))
        {
            // stop moving and go idle
            isWalking = false;
            animator.SetBool("isWalking", false);
            Debug.Log("Hit obstacle → switching to Idle");
        }

        else
        {
            target = pathManager.GetNextTarget();
            Debug.Log("hit waypoint");
        } 

    }
}





