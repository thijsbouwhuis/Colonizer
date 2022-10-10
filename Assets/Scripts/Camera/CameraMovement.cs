using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //Movement
    [SerializeField] private float panSpeed = 20f;
    
    //Scrolling
    [SerializeField] private float scrollSpeed = 100f;
    [SerializeField] private float minScrollLimit = 3f;
    [SerializeField] private float maxScrollLimit = 20f;

    // simple movement & Scrolling
    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        Vector3 pos = transform.position;
        float movement = panSpeed * Time.deltaTime;
        float scrollMovement = scrollSpeed * Time.deltaTime;

        //Movement
        if (Input.GetKey("w")) { pos.x -= movement; }
        if (Input.GetKey("s")) { pos.x += movement; }
        if (Input.GetKey("a")) { pos.z -= movement; }
        if (Input.GetKey("d")) { pos.z += movement; }
        
        //Scrolling
        if (Input.mouseScrollDelta.y > 0) { pos.y = Math.Max(pos.y - scrollMovement, minScrollLimit); }
        if (Input.mouseScrollDelta.y < 0) { pos.y = Math.Min(pos.y + scrollMovement, maxScrollLimit); }

        transform.position = pos;
    }
}
