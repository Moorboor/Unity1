using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private Grid<PathNode> grid;
    public int x;
    public int z;

    public int gCost;
    public int hCost;
    public int fCost;

    public IDictionary<string, bool> agentDic = new Dictionary<string, bool>();
    public bool isWalkable;

    public PathNode cameFromNode;

    public PathNode(Grid<PathNode> grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
        isWalkable = true;
    }
    public void SetIsWalkable(bool isWalkable)
    {
        this.isWalkable = isWalkable;
        grid.TriggerGridObjectChanged(x, z);
    }

    public void SetAgent(string agent)
    {
        if (!agentDic.ContainsKey(agent))
        {
            agentDic.Add(agent, true);
        }  
        else
        {
            agentDic[agent] = true;
        }
        grid.TriggerGridObjectChanged(x, z);
    }

    public void ClearAgent(string agent)
    {
        if (agentDic.ContainsKey(agent))
        {
            agentDic[agent] = false;
            grid.TriggerGridObjectChanged(x, z);
        }
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public override string ToString()
    {
        bool value;

        if (agentDic.TryGetValue("Hostile", out value))
        {
            return x + "," + z + "Hostile";
            
        }
        else
        {
            return x + "," + z;
        }
    }
}