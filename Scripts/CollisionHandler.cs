using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionVFX;


    private void OnTriggerEnter(Collider other)
    {
        
        StartCrashSequence();

    }

    private void StartCrashSequence()
    {
        explosionVFX.Play();
        
        GetComponent<PlayerControls>().enabled = false;
        GetComponent<Rigidbody>().useGravity = true;
        
        

        Invoke("ReloadLevel", 2f);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        
    }

}
