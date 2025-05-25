using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    //singleton class
    public static CubeSpawner Instance;

    Queue<Cube> cubesQueue = new Queue<Cube>();
    [SerializeField] private int cubeQueueCapacity = 20;
    [SerializeField] private bool autoQueueGrow = true;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Color[] cubeColors;

    [HideInInspector] public int maxCubeNumber; //4096
    private int maxPower = 12;
    private Vector3 defaultSpawnPosition;

    private void Awake()
    {
        Instance = this;
        defaultSpawnPosition = transform.position;
        maxCubeNumber = (int)Mathf.Pow(2, maxPower);
        initializeCubesQueue();
    }
    private void initializeCubesQueue()
    {
        for (int i = 0; i < cubeQueueCapacity; i++)
        {
            AddCubeToQueue();
        }
    }
    public Cube Spawn(int number, Vector3 position)
    {
        if (cubesQueue.Count == 0)
        {
            if (autoQueueGrow)
            {
                cubeQueueCapacity++;
                AddCubeToQueue();
            }
            else
            {
                Debug.Log("no more cubes in queue");
            }
        }
        Cube cube = cubesQueue.Dequeue();
        cube.transform.position = position;
        cube.setNumber(number); cube.setColor(GetColor(number));
        cube.gameObject.SetActive(true);

        return cube;
    }
    public Cube SpawnRandom()
    {
        return Spawn(GenerateRandomNumber(), defaultSpawnPosition);
    }
    public void DestroyCube(Cube cube)
    {
        cube.CubeRb.velocity = Vector3.zero;
        cube.CubeRb.angularVelocity = Vector3.zero;
        cube.CubeRb.rotation = Quaternion.identity;
        cube.IsMainCube = false;
        cube.gameObject.SetActive(false);
        cubesQueue.Enqueue(cube);
    }

    private void AddCubeToQueue()
    {
        Cube cube = Instantiate(cubePrefab, defaultSpawnPosition, Quaternion.identity, transform)
               .GetComponent<Cube>();
        cube.gameObject.SetActive(false);
        cube.IsMainCube = false;
        cubesQueue.Enqueue(cube);
    }

    public int GenerateRandomNumber()
    {
        return (int)Mathf.Pow(2, Random.Range(1, 6));
    }
    public Color GetColor(int number)
    {
        return cubeColors[(int)(Mathf.Log(number) / Mathf.Log(2)) - 1];
    }

}

