using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject volcano;
    public GameObject winterland;
    public GameObject panda;
    public GameObject scarecrow;
    public GameObject northland;

    public GameObject currentMap;

    public Dictionary<string, GameObject> RightLand = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> LeftLand = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> UpLand = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> DownLand = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        volcano.SetActive(false);
        winterland.SetActive(false);
        panda.SetActive(false);
        scarecrow.SetActive(true);
        northland.SetActive(false);
        currentMap = scarecrow;
        RightLand[northland.name] = scarecrow;
        RightLand[volcano.name] = scarecrow;
        RightLand[scarecrow.name] = winterland;
        LeftLand[winterland.name] = scarecrow;
        LeftLand[panda.name] = scarecrow;
        LeftLand[scarecrow.name] = northland;
        UpLand[volcano.name] = northland;
        UpLand[panda.name] = winterland;
        DownLand[northland.name] = volcano;
        DownLand[winterland.name] = panda;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            // choose up
            if (UpLand.ContainsKey(currentMap.name))
            {
                currentMap.SetActive(false);
                currentMap = UpLand[currentMap.name];
                currentMap.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            // choose down
            if (DownLand.ContainsKey(currentMap.name))
            {
                currentMap.SetActive(false);
                currentMap = DownLand[currentMap.name];
                currentMap.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            // choose up
            if (LeftLand.ContainsKey(currentMap.name))
            {
                currentMap.SetActive(false);
                currentMap = LeftLand[currentMap.name];
                currentMap.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            // choose up
            if (RightLand.ContainsKey(currentMap.name))
            {
                currentMap.SetActive(false);
                currentMap = RightLand[currentMap.name];
                currentMap.SetActive(true);
            }
        }
    }
}
