using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    public GameObject player;
    public Vector3 _offset;//目標與攝影機距離

    public float smoothTime = 0.01f;
    private Vector3 cameraVelocity = Vector3.zero;
    private Camera mainCamera;

    void Start()
    {
        _offset = player.transform.position - transform.position;//目標與攝影機距離

        mainCamera = Camera.main;
    }


    void Update()
    {
        //transform.position = player.transform.position -  _offset;  //一般跟隨
        transform.position = Vector3.SmoothDamp(this.transform.position, player.transform.position - _offset, ref cameraVelocity, smoothTime); //平滑跟隨
    }

}
