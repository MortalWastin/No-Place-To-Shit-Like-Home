using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int confidenceScore;
    public int entertainmentScore;
    public int hygieneScore;

    public List<Transform> spawnPositions;
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
            EndGame();
    }
    public void EndGame()
    {
        gameRunning = false;
        currentTime = -1;
        UIManager.Instance.SetTime(currentTime);
    }

    private void SpawnObjects()
    {
        int total = allPickUpObjects.Count;

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
