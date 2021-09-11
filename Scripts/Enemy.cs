using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject projectileVFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePoints = 15;
    [SerializeField] int timesHit = 10;

    ScoreBoard scoreBoard;
    
    



    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (timesHit < 1)
        {
            KillEnemy();
        }
    }



    void ProcessHit()
    {
        GameObject vfx = Instantiate(projectileVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        timesHit--;
        Debug.Log("I'm hit");
    }

        private void KillEnemy()
    {
        scoreBoard.IncraseScore(scorePoints);
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
        
    }
}
