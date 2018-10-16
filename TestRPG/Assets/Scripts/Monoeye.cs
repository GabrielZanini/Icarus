using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monoeye : MonoBehaviour {

    public int hp = 2;
    public float speed = 2f;
    public float rotationSpeed = 2f;
    public Transform target;


	void Start ()
    {
		
	}
	

	void Update ()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);

        if (hp <= 0)
        {
            // Destroy(gameObject);
            Respawn();
        }
    }

    void Respawn()
    {
        hp = 2;

        Vector3 newPosition;

        newPosition = transform.position + new Vector3(Random.Range(-20, 20), Random.Range(-1, 10), Random.Range(-20, 20));

        newPosition += (newPosition - target.position).normalized * 10;

        transform.position = newPosition;
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
