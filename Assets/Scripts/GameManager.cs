using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController playerPrefab;
    public Camera PlayerCamera;
    public GameObject poopPrefab;
    private PlayerController currentPlayer;
    private GameObject currentPoop;
    public int confortScore;
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
            DontDestroyOnLoad(this);
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
        {
            UpdateTime();
            UpdateSteps();
        }
    }

    public void StartGame()
    {
        ClearObjects();
        pickedUpItems = new List<PickUpObject>();
        currentTime = maxTimeLeft;
        gameRunning = true;
        confortScore = 0;
        entertainmentScore = 0;
        hygieneScore = 0;
        SpawnObjects();
        UIManager.Instance.SetTime(currentTime);
        UIManager.Instance.SetSteps(currentPlayer.currentSteps);
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
        int currentSteps = currentPlayer.currentSteps;

        if (currentSteps <= 0)
        {
            EndGame(false);
            Debug.Log("no more steps");
        }
    }
    public void EndGame(bool won)
    {
        if (won)
        {
            UIManager.Instance.SetEnd(true, pickedUpItems);
        }
        else
        {
            hygieneScore -= 100;
            UIManager.Instance.SetEnd(false, pickedUpItems);
            currentPoop = Instantiate(poopPrefab, currentPlayer.transform);
            //currentPoop.transform.position = currentPlayer.transform.position - (Vector3.down * 0.5f);
        }
        gameRunning = false;
        UIManager.Instance.SetTime(currentTime);
        currentTime = -1;
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
        currentPlayer.currentSteps = playerPrefab.currentSteps;

        Transform[] allSpawnPoints = spawnPositionsParent.GetComponentsInChildren<Transform>();
        allPickUpObjects.Shuffle();

        List<Transform> allSpawnPointsList = new List<Transform>();
        foreach (Transform t in allSpawnPoints)
        {
            if (t == spawnPositionsParent)
                continue;
            allSpawnPointsList.Add(t);
        }
        allSpawnPointsList.Shuffle();

        for (int i = 0; i < allPickUpObjects.Count; i++)
        {
            spawnedObjects.Add(Instantiate(allPickUpObjects[i], allSpawnPointsList[i]).gameObject);
        }
    }
    private void ClearObjects()
    {
        foreach (GameObject gameObject in spawnedObjects)
            Destroy(gameObject);
        if (currentPoop != null)
            Destroy(currentPoop);
    }
    public void AddItem(PickUpObject pickUpObject)
    {
        confortScore += pickUpObject.confortValue;
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
