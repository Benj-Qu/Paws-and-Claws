using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public float BornTime = 2f;
    public float LifeTime = 10f;
    public float LeftLimit = -30f;
    public float RightLimit = 30f;
    public float LowerLimit = 10f;
    public float UpperLimit = 20f;

    private bool destroyable = false;

    private void Start()
    {
        float angle = Random.Range(LeftLimit - 90f, RightLimit - 90f);
        float Speed = Random.Range(LowerLimit, UpperLimit);
        Vector2 direction = Quaternion.Euler(0f, 0f, angle) * Vector3.left;
        GetComponent<Rigidbody2D>().velocity = direction * Speed;
        StartCoroutine(DestroyCoroutine(LifeTime, true));
    }

    private void Update()
    {
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        if (velocity.magnitude == 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 90f);
        }
        else
        {
            float angle = Vector2.Angle(GetComponent<Rigidbody2D>().velocity, Vector2.left);
            if (velocity.y <= 0)
            {
                destroyable = true;
                transform.eulerAngles = new Vector3(0f, 0f, angle);
            }
            else
            {
                transform.eulerAngles = new Vector3(0f, 0f, -angle);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTerrain(other.gameObject))
        {
            StartCoroutine(DestroyCoroutine(0.1f, false));
        }
    }

    private IEnumerator DestroyCoroutine(float time, bool force)
    {
        if (force || destroyable)
        {
            yield return new WaitForSeconds(time);
            if (gameObject)
            {
                Destroy(gameObject);
            }
        }
    }

    private bool isTerrain(GameObject other)
    {
        return other.CompareTag("Block") || other.CompareTag("Mountain") ||
               other.CompareTag("Wall") || other.CompareTag("Ice") || other.CompareTag("Player");
    }
}
