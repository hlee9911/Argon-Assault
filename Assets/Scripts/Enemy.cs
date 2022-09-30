using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathVFXandSFX;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private float healthPoint = 5f;
    [SerializeField] private int scorePerHit = 10;
    [SerializeField] private int scoreOnExplosion = 100;
    // private Rigidbody rgbody;
    private ScoreBoard scoreBoard;
    [SerializeField] private GameObject parentGameObject;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidbody();
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (healthPoint <= 0f)
        {
            KillEnemy(other);
        }
    }

    void KillEnemy(GameObject otherGameobject)
    {
        scoreBoard.IncreaseScore(scoreOnExplosion);
        GameObject deathvfx = Instantiate(deathVFXandSFX, transform.position, Quaternion.identity);
        deathvfx.transform.parent = parentGameObject.transform;
        // Debug.Log(this.name + " got hit by " + otherGameobject.gameObject.name + "!");
        Destroy(this.gameObject);
    }

    void ProcessHit()
    {
        scoreBoard.IncreaseScore(scorePerHit);
        GameObject hitvfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        hitvfx.transform.parent = parentGameObject.transform;
        Mathf.Clamp(healthPoint -= 1f, 0f, healthPoint);
        // Debug.Log(gameObject.name + "'s HP: " + healthPoint);
    }
}
