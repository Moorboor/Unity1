using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Grid<TGridObject> grid;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;

    public void Start()
    {
        grid = new Grid<TGridObject>(10, 10, 5f, Vector3.zero, (Grid<TGridObject> g, int x, int z) => new TGridObject(g, x, z));
    }

    public void Update()
    {
        float speed = 10f;
        float moveX = 0f;
        float moveZ = 0f;
        if (Input.GetMouseButtonDown(0))
        {
            TGridObject tGridObject = grid.GetGridObject(GetMouseWorldPosition());
            if (tGridObject != null)
            {
                tGridObject.AddValue(20);
            }
        }


        if (Input.GetKey(KeyCode.W))
        {
            moveZ += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveZ += -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX += 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX += -1f;
        }
        transform.position += new Vector3(moveX, 0, moveZ) * speed * Time.deltaTime;
    }

    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
        {
            transform.position = raycastHit.point;
            Debug.Log(raycastHit.point);
            return raycastHit.point;
        } else
        {
            return Vector3.zero;
        }
    }
}

public class TGridObject
{
    private Grid<TGridObject> grid;
    private int x;
    private int z;
    public int value;

    public TGridObject(Grid<TGridObject> grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
    }
    public void AddValue(int addValue)
    {
        value += addValue;
        grid.TriggerGridObjectChanged(x, z);
    }

    public float GetValuenormalized()
    {
        return (float)value;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}
