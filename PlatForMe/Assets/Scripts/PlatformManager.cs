using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    public float platformCount { get; private set; }

    public static PlatformManager instance;

    public static Action<bool> PlatformCreated;

    public List<GameObject> playerPlatforms = new List<GameObject>();

    public Color platformColor { get; private set; }
    public List<Color> collectedColors = new List<Color>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        platformCount = 10;
    }

    private void Start()
    {
        platformColor = PlayerMovement.instance.GetComponentInChildren<SpriteRenderer>().color;
        collectedColors.Add(platformColor);
    }

    private void OnEnable()
    {
        DragDrop.dropped += CreatePlatform;
    }

    private void OnDisable()
    {
        DragDrop.dropped -= CreatePlatform;
    }

    /* prototype code that spawned platform directly beneath player
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(platformPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    */

    public void AddToCount(float sum)
    {
        float originalSum = platformCount;
        platformCount += sum;
        PlatformCreated?.Invoke(true);
       /* Debug.Log("Original: " + originalSum + " New: " + platformCount);*/
    }

    public void ChangeColor(Color targetColor)
    {
        platformColor = targetColor;

        SpriteRenderer playerRenderer = PlayerMovement.instance.GetComponentInChildren<SpriteRenderer>();
        playerRenderer.color = platformColor;

        foreach (GameObject platform in playerPlatforms)
        {
            SpriteRenderer renderer = platform.GetComponent<SpriteRenderer>();
            renderer.color = platformColor;
        }

        if (!collectedColors.Contains(targetColor))
        {
            collectedColors.Add(targetColor);
        }
    }

    private void CreatePlatform(DragDrop.PlatformData data)
    {
        /*print("Create Platform call at " + data.positon);*/
        if (platformCount > 0)
        {
            GameObject newPlat = Instantiate(data.platformPrefab, data.positon, Quaternion.identity);
            newPlat.transform.SetParent(transform);
            newPlat.GetComponent<SpriteRenderer>().color = platformColor;
            playerPlatforms.Add(newPlat);
            AddToCount(-1);
        }
        else
        {
            PlatformCreated?.Invoke(false);
        }
    }
}
