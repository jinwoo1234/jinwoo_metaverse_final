using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalBall : MonoBehaviourPunCallbacks
{

    public FinalScoreManager _score;
    private void Start()
    {
        Debug.Log(_score);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "p1Goal")
        {
            transform.position = new Vector3(0, 7, 0);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            _score.AddScore(true);
        }

        if (other.tag == "p2Goal")
        {
            Debug.Log("p2goalTrigger");
            transform.position = new Vector3(0, 7, 0);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            _score.AddScore(false);
        }
    }

    [PunRPC]
private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            Vector3 direction = collision.transform.position - collision.gameObject.transform.Find("PlayerSee").position;

            direction.Normalize();

            float moveSpeed = 5f;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        if (collision.transform.tag == "Wall")
        {
            Debug.Log("wall");
            Vector3 reflectedDirection = Vector3.Reflect(transform.forward, collision.transform.forward);
            GetComponent<Rigidbody>().velocity = 3f * reflectedDirection * GetComponent<Rigidbody>().velocity.magnitude;
        }
    }

}
