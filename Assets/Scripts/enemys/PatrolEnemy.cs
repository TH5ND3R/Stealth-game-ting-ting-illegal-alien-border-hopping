using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public float speed;
    public Transform[] patrolPoints;
    public float waitTime;
    int currentPointIndex;

    bool once;
    private void Update()
    {
        //calculating were to, go i think 
        if (patrolPoints.Length == 0)
            return;
        if ((transform.position - patrolPoints[currentPointIndex].position).magnitude > 0.1f)
        {
            // points vision in deriction it moves 
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
            gameObject.GetComponent<EnemyVision>().direction = (patrolPoints[currentPointIndex].position - transform.position).normalized;
        } else
        {
            if (once == false)
            {
                once = true;
                StartCoroutine(Wait());
            }
        }
    }

    IEnumerator Wait()
    {
        // waits to go to next patrol point 
        yield return new WaitForSeconds(waitTime);
        if (currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
        } else
        {
            currentPointIndex = 0;
        }
        once = false;
    }
}