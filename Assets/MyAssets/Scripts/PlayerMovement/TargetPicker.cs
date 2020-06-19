using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This is the visual target, it does not controlthe player
public class TargetPicker : MonoBehaviour
{
    public float surfaceOffset = 0.05f;


    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point + hit.normal * surfaceOffset;

            return;
        }
      //  transform.position = hit.point + hit.normal * surfaceOffset;
    }
}
