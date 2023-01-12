using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    // spote player
    [SerializeField]
    float distance;
    [SerializeField]
    float angle;
    [SerializeField] AudioClip chaseMusic;

    AudioSource audioSource;

    public Vector3 direction;

    [SerializeField]
    GameObject player;

    // tid til at spote player
    [SerializeField]
    float timeTillPlayerSpoted;



    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.player;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Music();

        if (player)
        {
            Vector3 delta = player.transform.position - transform.position;
            float dot = Vector3.Dot(direction.normalized, delta.normalized);
            if (delta.magnitude <= distance && dot * dot * Mathf.Sign(dot) * 90 >= 90 - angle)
            {
                StartCoroutine(SpottingPlayer());
                return;
            }
        }
        //stop chasing player
        StopCoroutine(SpottingPlayer());
        gameObject.GetComponent<PatrolEnemy>().enabled = true;
        gameObject.GetComponent<ChasingPlayer>().enabled = false;
    }

    private void OnDrawGizmos()
    {
        Vector3 endPoint = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * distance + transform.position;
        Debug.DrawLine(transform.position, endPoint);
        endPoint = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), -Mathf.Sin(angle * Mathf.Deg2Rad), 0) * distance + transform.position;
        Debug.DrawLine(transform.position, endPoint);

    }
    IEnumerator SpottingPlayer()
    {
        //start chasing player
        yield return new WaitForSeconds(timeTillPlayerSpoted);
        Debug.Log("spotted");
        Spotted();
    }

    public void Spotted()
    {
        gameObject.GetComponent<PatrolEnemy>().enabled = false;
        gameObject.GetComponent<ChasingPlayer>().enabled = true;
    }
    private void Music()
    {
        if (gameObject.GetComponent<ChasingPlayer>().enabled == true)
        {     
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(chaseMusic);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
