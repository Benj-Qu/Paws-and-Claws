using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public textController tc;
    public Vector3 target;
    public float distance = 1.0f;
    public float size_ = 1f;
    public float changeInterval = 1f;
    
    private Vector3 ori;
    private float ori_size;

    private bool resize_closer = false;
    private bool resize_further = false;
    private bool move = false;

    private Vector3 moveSpeed;
    private float resizeSpeed;

    public List<Vector3> posList;
    Subscription<DoneWithSelection> done_with_selection_event;

    private void OnDestroy()
    {
        EventBus.Unsubscribe(done_with_selection_event);
    }

    private void Awake()
    {
        done_with_selection_event = EventBus.Subscribe<DoneWithSelection>(OnRoundInc);
    }

    private void OnRoundInc(DoneWithSelection e)
    {
        Debug.Log("done in func");
        GameObject Flag = GameObject.Find("Flags");
        posList.Clear();
        foreach (Transform child in Flag.transform)
        {
            posList.Add(child.position);
        }
        if(tc) tc.updateText("");
        EventBus.Publish<CameraEvent>(new CameraEvent(true));
        StartCoroutine(MoveCamera(0));
    }

    private IEnumerator MoveCamera(int index)
    {
        target = posList[index] - transform.forward * distance;
        moveSpeed = (target - transform.position) / changeInterval;
        resizeSpeed = (size_ - GetComponent<Camera>().orthographicSize) / changeInterval;
        resize_closer = true;
        move = true;
        yield return new WaitForSeconds(changeInterval);
        resize_closer = false;
        move = false;
        yield return new WaitForSeconds(changeInterval);
        moveSpeed = (ori - transform.position) / changeInterval;
        resizeSpeed = (ori_size - size_) / changeInterval;
        resize_further = true;
        move = true;
        yield return new WaitForSeconds(changeInterval);
        resize_further = false;
        move = false;

        if (index != posList.Count - 1)
        {
            StartCoroutine(MoveCamera(index + 1));
        }
        else
        {
            transform.position = ori;
            GetComponent<Camera>().orthographicSize = ori_size;
            if(tc) tc.updateText("[speed=0.08]<b>Guadians are well planned. They can build perfect path towards flags!</b>");
            EventBus.Publish<CameraEvent>(new CameraEvent(false));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        posList = new List<Vector3>();
        ori = transform.position;
        ori_size = GetComponent<Camera>().orthographicSize;
    }
    
    void Update()
    {
        if (move)
        {
            transform.position += moveSpeed * Time.deltaTime; 
        }

        if (resize_closer || resize_further)
        {
            GetComponent<Camera>().orthographicSize += resizeSpeed * Time.deltaTime;
        }
    }
}

public class CameraEvent
{
    public bool startOrFinish; // false means finish, true means start
    public CameraEvent(bool _startOrFinish)
    {
        startOrFinish = _startOrFinish;
        return;
    }
}
