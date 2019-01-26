using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public int score;
    public float timeLeft;
    public float maxTimeLeft;

    public List<Transform> spawnPositions;
    public List<PickUpObject> allPickUpObjects;
    private List<PickUpObject> pickedUpItems;

    private void Awake()
    {
        if(Instance == null)
        {
            GameManager.Instance = this;
            score = 0;
            timeLeft = -1;
            pickedUpItems = new List<PickUpObject>();
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public void StartGame()
    {
        timeLeft = maxTimeLeft;
        score = 0;
        SpawnObjects();
    }
    public void EndGame()
    {

    }


    private void SpawnObjects()
    {
        throw new NotImplementedException();
    }
    public void AddItem(PickUpObject pickUpObject)
    {
        score += pickUpObject.scoreValue;
        pickedUpItems.Add(pickUpObject);
        pickUpObject.gameObject.SetActive(false);
    }
}
