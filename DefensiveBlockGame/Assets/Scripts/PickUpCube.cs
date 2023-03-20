using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PickUpCube : MonoBehaviour
{

    [SerializeField] private Transform dropDestination;
    private BlockGenerator blockGeneratorScript;
    [SerializeField] GameObject blockGenerator;

    Inventory inventory;
    PlayerControls controls;


    private void Start()
    {
        blockGeneratorScript = blockGenerator.GetComponent<BlockGenerator>();
    }


    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag(GlobalScript.playerTag).GetComponent<Inventory>();
        controls = new PlayerControls();

        //Allow player to pick up cubes
        controls.Gameplay.PickUp.performed += ctx => PickUp();
        //Allow player to drop cubes
        controls.Gameplay.Drop.performed += ctx => Drop();
    }


    private void PickUp()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance <= GlobalScript.pickUpDistance)
            {
                GameObject itemGrabbed = hit.collider.gameObject;
                if(itemGrabbed.CompareTag(GlobalScript.cubeTag)) PlaceItemInInventory(itemGrabbed);
            }
        }
    }


    private void PlaceItemInInventory(GameObject itemGrabbed)
    {
        if (!inventory.isFull)
        {
            inventory.slot.GetComponent<Image>().sprite = itemGrabbed.GetComponent<Item>().icon;
            inventory.cube = itemGrabbed;
            inventory.isFull = true;
            GlobalScript.generatorHasNewCube = false;
            itemGrabbed.SetActive(false);
        }
    }

    private Vector3 GetCubePosition()
    {
        var newXPos = Mathf.FloorToInt(dropDestination.position.x);
        var newYPos = 0.5f;
        var newZPos = Mathf.FloorToInt(dropDestination.position.z);

        Vector3 position = new Vector3(newXPos, newYPos, newZPos); 

        return position;

    }


    private void Drop()
    {
        if (inventory.isFull)
        {
            if (!CubeExistsAt(GetCubePosition().x, GetCubePosition().y, GetCubePosition().z))
            {
                inventory.cube.transform.position = GetCubePosition();
                inventory.cube.SetActive(true);
                ClearInventory();
            }
        }
    }


    private void ClearInventory()
    {
        inventory.isFull = false;
        inventory.slot.GetComponent<Image>().sprite = null;
        inventory.cube = null;
    }

    private bool CubeExistsAt(float x, float y, float z)
    {
        var cubePos = GetCubePosition();

        float radius = inventory.cube.transform.localScale.x;

        if (Physics.CheckSphere(cubePos, radius))
        {
            Debug.Log("Block does not exists at current location");
            return false;
        }
        else
        {
            Debug.Log("Block exists at current location");
            return true;
        }
    }


    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }


    private void OnDisable()
    {
        controls.Disable();
    }
}
