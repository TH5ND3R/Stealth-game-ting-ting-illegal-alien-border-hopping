using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChasingPlayer : MonoBehaviour
{
    public float chasingSpeed;
    public Transform target;
    public float minimumDistance;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.player.transform;
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, chasingSpeed * Time.deltaTime);
            GetComponent<EnemyVision>().direction = (target.position - transform.position).normalized;
        }
        else
        {
            // attacking player. aka player loses (restarts scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
