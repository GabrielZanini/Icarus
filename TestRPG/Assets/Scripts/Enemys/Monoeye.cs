using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monoeye : Enemy
{

    public Transform eyeBall;
    public Transform target;

	void Update ()
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

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Scene loadedLevel = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadedLevel.buildIndex);
        }
    }
}
