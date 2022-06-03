using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    private Grid<PathNode> grid;
    private int x;
    private int z;

    private int gCost;
    private int hCost;
    private int fCost;

    public PathNode cameFromNode;

    public PathNode(Grid<PathNode> grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public override string ToString()
    {
        return x + "," + z;
    }
}
