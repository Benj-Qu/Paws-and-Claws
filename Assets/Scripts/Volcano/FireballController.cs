using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public Vector2 LeftRange = new Vector2(-8f, 1f);
    public Vector2 RightRange = new Vector2(8f, 1f);

    public float Speed = 5f;

    void Start()
    {
        float angle = Random.Range(lowerLimit(transform.position), upperLimit(transform.position));
        Vector2 direction = Quaternion.Euler(0f, 0f, angle) * Vector3.left;
        transform.eulerAngles = new Vector3(0f, 0f, angle);
        GetComponent<Rigidbody2D>().velocity = direction * Speed;
        StartCoroutine(DestroyCoroutine(5f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTerrain(other.gameObject))
        {
            StartCoroutine(DestroyCoroutine(0.1f));
        }
    }

    private IEnumerator DestroyCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }

    private float lowerLimit(Vector2 position)
    {
        return Vector2.Angle(Vector2.left, LeftRange - position);
    }

    private float upperLimit(Vector2 position)
    {
        return Vector2.Angle(Vector2.left, RightRange - position);
    }

    private bool isTerrain(GameObject other)
    {
        return other.CompareTag("Block") || other.CompareTag("Mountain") ||
               other.CompareTag("Wall") || other.CompareTag("Ice") || other.CompareTag("Player");
    }
}
