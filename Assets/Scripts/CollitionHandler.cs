using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollitionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 1f;
    [SerializeField] private GameObject deathFX;

    // Start is called before the first frame update
    void Start()
    {

    }

    //private void OnCollisionEnter(Collision collision)
    //{

    //}

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        deathFX.SetActive(true);
        Invoke(nameof(ReloadScene), levelLoadDelay);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
    }

    // Update is called once per frame
    void Update()
    {

    }

}
