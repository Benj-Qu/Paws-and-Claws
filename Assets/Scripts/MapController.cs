<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    private float joystickInputx = 0f;
    private float joystickInputy = 0f;
    private int numButton;

    private bool respond = true;
    private float notRespondTime = 0.08f;
    private float notRespondTimer = 0f;
    public GameObject volcano;
    public GameObject winterland;
    public GameObject panda;
    public GameObject scarecrow;
    public GameObject iceland;

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
        iceland.SetActive(false);
        currentMap = scarecrow;
        RightLand[iceland.name] = scarecrow;
        RightLand[volcano.name] = scarecrow;
        RightLand[scarecrow.name] = winterland;
        LeftLand[winterland.name] = scarecrow;
        LeftLand[panda.name] = scarecrow;
        LeftLand[scarecrow.name] = iceland;
        UpLand[volcano.name] = iceland;
        UpLand[panda.name] = winterland;
        DownLand[iceland.name] = volcano;
        DownLand[winterland.name] = panda;
    }

    // Update is called once per frame
    void Update()
    {
        joystickInputy = Input.GetAxis("Vertical1") + Input.GetAxis("Vertical2");
        joystickInputx = Input.GetAxis("Horizontal1") + Input.GetAxis("Horizontal2");
        if (respond)
        {
            if (Mathf.Abs(joystickInputx) > Mathf.Abs(joystickInputy))
            {
                joystickInputy = 0;
            }
            else
            {
                joystickInputx = 0;
            }
            respond = false;
        }
        else
        {
            if (joystickInputx != 0 || joystickInputy != 0)
            {
                notRespondTimer += Time.deltaTime;
                if (notRespondTimer >= notRespondTime)
                {
                    notRespondTimer = 0f;
                    respond = true;
                }
            }
            joystickInputx = 0;
            joystickInputy = 0;
        }
        
        
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("A1") || Input.GetButtonDown("A2"))
        {
            SceneManager.LoadScene(currentMap.name);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || joystickInputy < 0)
        {
            // choose up
            if (UpLand.ContainsKey(currentMap.name))
            {
                currentMap.SetActive(false);
                currentMap = UpLand[currentMap.name];
                currentMap.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || joystickInputy > 0)
        {
            // choose down
            if (DownLand.ContainsKey(currentMap.name))
            {
                currentMap.SetActive(false);
                currentMap = DownLand[currentMap.name];
                currentMap.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || joystickInputx < 0)
        {
            // choose up
            if (LeftLand.ContainsKey(currentMap.name))
            {
                currentMap.SetActive(false);
                currentMap = LeftLand[currentMap.name];
                currentMap.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || joystickInputx > 0)
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
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    private float joystickInputx = 0f;
    private float joystickInputy = 0f;
    private int numButton;

    private bool respond = true;
    private float notRespondTime = 0.08f;
    private float notRespondTimer = 0f;
    public GameObject volcano;
    public GameObject winterland;
    public GameObject panda;
    public GameObject scarecrow;
    public GameObject iceland;

    public GameObject currentMap;

    public GameObject FarmManager;
    public GameObject IceLandManager;
    public GameObject WinterLandManager;
    public GameObject VolcanoManager;
    public GameObject LanternManager;

    public Dictionary<string, GameObject> RightLand = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> LeftLand = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> UpLand = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> DownLand = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> SceneManagers = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        volcano.SetActive(false);
        winterland.SetActive(false);
        panda.SetActive(false);
        scarecrow.SetActive(true);
        iceland.SetActive(false);
        currentMap = scarecrow;
        RightLand[iceland.name] = scarecrow;
        RightLand[volcano.name] = scarecrow;
        RightLand[scarecrow.name] = winterland;
        LeftLand[winterland.name] = scarecrow;
        LeftLand[panda.name] = scarecrow;
        LeftLand[scarecrow.name] = iceland;
        UpLand[volcano.name] = iceland;
        UpLand[panda.name] = winterland;
        DownLand[iceland.name] = volcano;
        DownLand[winterland.name] = panda;
        SceneManagers[iceland.name] = IceLandManager;
        SceneManagers[scarecrow.name] = FarmManager;
        SceneManagers[panda.name] = LanternManager;
        SceneManagers[volcano.name] = VolcanoManager;
        SceneManagers[winterland.name] = WinterLandManager;
    }

    // Update is called once per frame
    void Update()
    {
        joystickInputy = Input.GetAxis("Vertical1") + Input.GetAxis("Vertical2");
        joystickInputx = Input.GetAxis("Horizontal1") + Input.GetAxis("Horizontal2");
        if (respond)
        {
            if (Mathf.Abs(joystickInputx) > Mathf.Abs(joystickInputy))
            {
                joystickInputy = 0;
            }
            else
            {
                joystickInputx = 0;
            }
            respond = false;
        }
        else
        {
            if (joystickInputx != 0 || joystickInputy != 0)
            {
                notRespondTimer += Time.deltaTime;
                if (notRespondTimer >= notRespondTime)
                {
                    notRespondTimer = 0f;
                    respond = true;
                }
            }
            joystickInputx = 0;
            joystickInputy = 0;
        }
        
        
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("A1") || Input.GetButtonDown("A2"))
        {
            SceneManagers[currentMap.name].GetComponent<Michsky.LSS.LoadingScreenManager>().LoadScene(currentMap.name);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || joystickInputy < 0)
        {
            // choose up
            if (UpLand.ContainsKey(currentMap.name))
            {
                currentMap.SetActive(false);
                currentMap = UpLand[currentMap.name];
                currentMap.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || joystickInputy > 0)
        {
            // choose down
            if (DownLand.ContainsKey(currentMap.name))
            {
                currentMap.SetActive(false);
                currentMap = DownLand[currentMap.name];
                currentMap.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || joystickInputx < 0)
        {
            // choose up
            if (LeftLand.ContainsKey(currentMap.name))
            {
                currentMap.SetActive(false);
                currentMap = LeftLand[currentMap.name];
                currentMap.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || joystickInputx > 0)
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
>>>>>>> ec498613991897b27a3b585570c652936394dafd
