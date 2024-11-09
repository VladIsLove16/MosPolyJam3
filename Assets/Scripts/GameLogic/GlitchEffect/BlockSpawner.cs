using UnityEngine;
using System.Collections.Generic;
[RequireComponent(typeof(ObjectCollector))]
public class BlockSpawner : MonoBehaviour
{
    [Header("Block Settings")]
    public int numberOfBlocks = 5;  // Number of blocks to spawn
    public float blockSize = 50.0f;  // Size of the block in pixels (width/height)
    public float blockDuration = 1.0f;// How long the block stays on the screen (seconds)
    public float SpawnInterval = 5f;
    public float SpawnIntervalDelta = 2f;
    
    public Texture2D blockTexture;  // Texture for the block

    [Header("Block Prefab")]
    public GameObject blockPrefab;  // Prefab of the block (must have Renderer to apply texture)

    private GameObject[] blocks;  // Array to store block references
    private float[] blockTimers;  // Array to track block timers for destruction
    
    private ObjectCollector objectCollector;  // Reference to ObjectCollector script
    private List<GameObject> objectsInScene;  // List to store objects on screen
    private Time lastTimeSpawned;
    void Start()
    {
        blocks = new GameObject[numberOfBlocks];  // Initialize the array for blocks
        blockTimers = new float[numberOfBlocks];  // Initialize array to track block lifetimes
        objectsInScene = new List<GameObject>();  // Initialize the list for visible objects

        // Fetch the ObjectCollector component
        objectCollector = GetComponent<ObjectCollector>();

        // Spawn the blocks immediately
        for (int i = 0; i < numberOfBlocks; i++)
        {
            SpawnBlock(i);
        }
    }

    void Update()
    {
        // Collect visible objects on screen from the ObjectCollector
        if(CanSpawn())
        {
            objectsInScene = objectCollector.CollectObjectsOnScreen();
            SpawnBlocks(numberOfBlocks);
        }
        DestroyAllBlocksByTime();
        // Update each block's timer and destroy the block if its duration has passed
       
    }

    private void SpawnBlocks(int numberOfBlocks)
    {
        for (int i = 0; i < numberOfBlocks; i++)
        {
            SpawnBlock(i);
        }
    }
    private void DestroyAllBlocksByTime()
    {
        for (int i = 0; i < numberOfBlocks; i++)
        {
            if (blocks[i] != null)
            {
                blockTimers[i] += Time.deltaTime;

                if (blockTimers[i] >= blockDuration)
                {
                    DestroyBlock(i);
                }
            }
        }
    }

    private bool CanSpawn()
    {
        return blocks.Length < numberOfBlocks;
    }

    void SpawnBlock(int index)
    {
        // Check if there are objects on screen
        if (objectsInScene.Count == 0)
        {
            return;  // No objects on screen to attach the blocks to
        }

        // Select a random object from those on screen
        GameObject selectedObject = objectsInScene[Random.Range(0, objectsInScene.Count)];

        // Get the position of the selected object
        Vector3 objectPosition = selectedObject.transform.position;

        // Instantiate the block prefab at the object's position
        blocks[index] = Instantiate(blockPrefab, objectPosition, Quaternion.identity);

        // Set the scale (size) of the block
        blocks[index].transform.localScale = new Vector3(blockSize, blockSize, 1);

        // Apply the texture to the block
        Renderer blockRenderer = blocks[index].GetComponent<Renderer>();
        if (blockRenderer != null && blockTexture != null)
        {
            blockRenderer.material.mainTexture = blockTexture;  // Set the texture
        }

        // Initialize the block's timer
        blockTimers[index] = 0.0f;
        objectsInScene.Remove(selectedObject);
    }

    void DestroyBlock(int index)
    {
        // Destroy the current block
        if (blocks[index] != null)
        {
            Destroy(blocks[index]);
            blocks[index] = null;  // Clear reference
        }
    }
}
