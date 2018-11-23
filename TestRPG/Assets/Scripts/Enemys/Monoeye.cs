using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monoeye : Enemy
{

    public Transform eyeBall;
    public Transform target;

    public GameObject deathEffect;
    
    void Start()
    {
        StartCoroutine(LookForTarget());
    }

    void Update ()
    {
        if (target != null)
        {
            ChaseTarget();
        }
    }

    void ChaseTarget()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);

        if (heath <= 0)
        {
            // Destroy(gameObject);
            Respawn(target.position);
        }

        eyeBall.LookAt(target);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Scene loadedLevel = SceneManager.GetActiveScene();
            //SceneManager.LoadScene(loadedLevel.buildIndex);
        }
    }



    IEnumerator LookForTarget()
    {
        while (true)
        {


            yield return null;

            var players = ServerManager.Instance.PlayerCharacters;

            if (target != null || players.Count == 0)
            {
                continue;
            }

            GameObject closest = null;
            Vector3 offset = Vector3.zero;
            float closestSqrMagnitude = 0f;

            for (int i = 0; i < players.Count; i++)
            {
                offset = transform.position - players[i].transform.position;

                if (i == 0)
                {
                    closest = players[i];
                    closestSqrMagnitude = offset.sqrMagnitude;
                }
                else
                {
                    if (offset.sqrMagnitude < closestSqrMagnitude)
                    {
                        closest = players[i];
                        closestSqrMagnitude = offset.sqrMagnitude;
                    }
                }
            }

            if (closest != null)
            {
                target = closest.transform;
            }
        }
    }
}
