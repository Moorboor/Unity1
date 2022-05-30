using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Grid grid;

    public void Start()
    {
        grid = new Grid(4, 5, 10f);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(GetMouseWorldPosition(), 22);
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 worldPosition = Input.mousePosition;
        Debug.Log(worldPosition.x + " " + worldPosition.y + " " + worldPosition.z);
        worldPosition.y = 0f;
        return worldPosition;
    }
}
