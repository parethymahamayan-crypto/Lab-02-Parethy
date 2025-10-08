using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField]
    public PathManager pathManager;
    public Animator animator;
    bool isWalking;

    List<waypoint> thePath;
    waypoint target;

    public float MoveSpeed;
    public float RotateSpeed;

    //Start is called before the firsn t frame upddate
    void Start()
    {
        thePath = pathManager.GetPath();
        if (thePath != null && thePath.Count > 0)
        {
            //set starting target to the first waypoint
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
            //we will overshoot the target
            //so we should do something smarter here
            return;
        }
        //take a step forward
        Vector3 moveDir = Vector3.forward;
        transform.Translate(moveDir * stepSize);
    }

    //update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            //toggle if any key is pressed
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
        //switch to the next target
        target = pathManager.GetNextTarget();
    }
}
