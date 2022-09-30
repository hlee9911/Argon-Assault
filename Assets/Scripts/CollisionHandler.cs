using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float loadDelay = 1f;
    [SerializeField] private ParticleSystem explosionVFX;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{this.name} ** Triggered by {other.gameObject.name}");
        StarcrashingSequence();
    }

    void StarcrashingSequence()
    {
        explosionVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerControls>().SetLaserActive(false);
        GetComponent<PlayerControls>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Invoke("ReloadScnee", loadDelay);
    }

    void ReloadScnee()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentBuildIndex);
    }
}
