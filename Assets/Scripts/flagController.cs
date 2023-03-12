using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagController : MonoBehaviour
{
    public List<int> x_min = new List<int>(){-7, -4, 3};
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
                x = Random.Range(x_min[i], x_max[i]);
                y = Random.Range(y_min, y_max + 1);
            }
            flag.transform.position = new Vector3(x, y, -1);
        }
    }

    bool invalidPosition(int x, int y)
    {
        if(x >= -3 && x <= 4 && y <= -1)
        {
            return true;
        }
        return false;
    }
}
