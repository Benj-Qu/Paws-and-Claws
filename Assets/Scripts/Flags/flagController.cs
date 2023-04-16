using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagController : MonoBehaviour
{
    public List<int> x_min = new List<int>(){-7, -3, 4};
    public List<int> x_max = new List<int>(){-4, 3, 7};
    public int y_min = -4;
    public int y_max = 4;
    // private List<GameObject> flags;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlagGeneration()
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject flag = transform.GetChild(i).gameObject;
            flag.SetActive(true);
            int x = Random.Range(x_min[i], x_max[i]);
            int y = Random.Range(y_min, y_max + 1);
            while (invalidPosition(x, y))
            {
                Debug.Log("invalid");
                x = Random.Range(x_min[i], x_max[i]);
                y = Random.Range(y_min, y_max + 1);
            }
            flag.transform.position = new Vector3(x, y, -1);
        }
    }

    bool invalidPosition(int x, int y)
    {
        Debug.Log(GameController.instance.level + " " + x + " " + y);
        if(GameController.instance.level == "Winter Land")
        {
            if (x > -3 && x < 4 && y < -1)
            {
                return true;
            }
        }
        if (GameController.instance.level == "Lantern Festival")
        {
            if (x > -7 && x < -3 && y < -1)
            {
                return true;
            }
            if (x > -2 && x < 8 && y < -2){
                return true;
            }
            if(x > 5 && x < 8 && y < 0)
            {
                return true;
            }
        }
        if (GameController.instance.level == "Farm")
        {
            if(y < 0 || y > 3){
                return true;
            }
        }
        if(GameController.instance.level == "Volcano")
        {
            if(x > -2 && x < 2 && y >= -2 && y < 1)
            {
                return true;
            }
            if(x > -4 && x < 4 && y < -2)
            {
                return true;
            }
        }
        if(GameController.instance.level == "Iceland")
        {
            if((x > 2 || x < -2) && y < -1)
            {
                return true;
            }
        }
        Debug.Log("valid");
        return false;
    }

    // Added by Xinyi
    public void DestroyFlags()
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject flag = transform.GetChild(i).gameObject;
            flag.GetComponent<flagTransformer>().showAddScore.textObject.SetActive(false);
            flag.SetActive(false);
        }
    }

    public void StartPartyTime()
    {
        for(var i = transform.childCount - 1; i >= 0; i--)
        {
            transform.GetChild(i).GetComponent<flagTransformer>().StartPartyTime();
        }
    }

    public void EndPartyTime()
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            transform.GetChild(i).GetComponent<flagTransformer>().EndPartyTime();
        }
    }

}
