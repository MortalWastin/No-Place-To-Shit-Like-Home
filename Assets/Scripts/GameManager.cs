﻿using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController playerPrefab;
    private PlayerController currentPlayer;
    public int confidenceScore;
    public int entertainmentScore;
    public int hygieneScore;

    public List<GameObject> spawnedObjects;
    public Transform spawnPositionsParent;
    public List<PickUpObject> allPickUpObjects;
    private List<PickUpObject> pickedUpItems;

    private float currentTime;
    public float maxTimeLeft;

    private bool gameRunning;

    private void Awake()
    {
        if (Instance == null)
        {
            GameManager.Instance = this;
            currentTime = -1;
            gameRunning = false;
            pickedUpItems = new List<PickUpObject>();
            DontDestroyOnLoad(this);
            StartGame();
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
    void Update()
    {
        if (gameRunning)
            UpdateTime();
    }

    public void StartGame()
    {
        currentTime = maxTimeLeft;
        gameRunning = true;
        confidenceScore = 0;
        entertainmentScore = 0;
        hygieneScore = 0;
        SpawnObjects();
        UIManager.Instance.SetTime(currentTime);
    }
    private void UpdateTime()
    {
        UIManager.Instance.SetTime(currentTime);
        currentTime -= 1 * Time.deltaTime;

        if (currentTime <= 0)
            EndGame(false);
    }

    private void UpdateSteps()
    {
       // int currentSteps = playerPrefab.currentSteps;

        //if (currentSteps <= 0)
        //    EndGame(false);
    }
    public void EndGame(bool won)
    {
        gameRunning = false;
        currentTime = -1;
        UIManager.Instance.SetTime(currentTime);
        currentPlayer.isMovable = false;
    }

    private void SpawnObjects()
    {
        spawnedObjects = new List<GameObject>();
        if (currentPlayer == null)
            currentPlayer = Instantiate(playerPrefab);
        else
            currentPlayer.Start();
        currentPlayer.transform.position = Vector3.zero;

        Transform[] allSpawnPoints = spawnPositionsParent.GetComponentsInChildren<Transform>();
        allPickUpObjects.Shuffle();

        List<Transform> allSpawnPointsList = new List<Transform>();
        foreach (Transform t in allSpawnPoints)
        {
            if (t.gameObject == this.gameObject)
                continue;
            allSpawnPointsList.Add(t);
        }
        allSpawnPointsList.Shuffle();

        for(int i = 0; i < allPickUpObjects.Count; i++)
        {
            spawnedObjects.Add(Instantiate(allPickUpObjects[i], allSpawnPointsList[i]).gameObject);
        }
    }
    private void ClearObjects()
    {
        foreach (GameObject gameObject in spawnedObjects)
            Destroy(gameObject);
    }
    public void AddItem(PickUpObject pickUpObject)
    {
        confidenceScore += pickUpObject.confidenceValue;
        entertainmentScore += pickUpObject.entertainmentValue;
        hygieneScore += pickUpObject.hygieneValue;

        pickedUpItems.Add(pickUpObject);
        pickUpObject.gameObject.SetActive(false);
    }
}

public static class ListShuffle
{
    //EXTRA 
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
