using UnityEngine;

public class BlockGenerator : MonoBehaviour
{

    [Header("Block Position")]
    [SerializeField] private Transform[] blocks;

    Inventory inventory;

    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag(GlobalScript.playerTag).GetComponent<Inventory>();
    }

    private void Start()
    {
        InstantiateBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GlobalScript.generatorHasNewCube)
        {
            InstantiateBlock();
        }
    }

    public void InstantiateBlock()
    {
        var cube = blocks[Random.Range(0, blocks.Length)];
        cube.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        Instantiate(cube);
        GlobalScript.generatorHasNewCube = true;


    }
} 
