using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverAnimalsController : MonoBehaviour
{
    public List<GameObject> animals;
    public GameObject lastanimal;
    public float init_velocity = 4f;
    public float time_interval = 8f;
    private float time = 0f;
    public int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            animals.Add(transform.GetChild(i).gameObject);
        }
        lastanimal = InstantiateAnimals(index);
        index = (index + 1) % transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(Mathf.Abs(time - time_interval) <= 0.5f)
        {
            // spawn an animal every 1s;
            time = 0f;
            Destroy(lastanimal);
            lastanimal = InstantiateAnimals(index);
            index = (index + 1) % transform.childCount;
        }
    }

    GameObject InstantiateAnimals(int index)
    {
        GameObject newanimal;
        if (index % 2 == 1)
        {
            newanimal = Instantiate(animals[index], GenerateRandPos(index), Quaternion.identity);
            newanimal.GetComponent<Rigidbody2D>().velocity = new Vector2(init_velocity, 0);
        }
        else
        {
            newanimal = Instantiate(animals[index], GenerateRandPos(index), Quaternion.Euler(0f, 180f, 0f));
            newanimal.GetComponent<Rigidbody2D>().velocity = new Vector2(-init_velocity, 0);
        }
        return newanimal;
    }


    Vector2 GenerateRandPos(int index)
    {
        float x, y;
        if (index % 2 == 1)
        {
            x = -9f;
        }
        else
        {
            x = 9f;
        }
        if(index % 2 == 1)
        {
            y = Random.Range(1f, 4f);
        }
        else
        {
            y = Random.Range(-4f, -1f);
        }
        return new Vector2(x, y);
    }
}
