using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class BossGFX : MonoBehaviour
{
    public AIPath AIPath;
    
    void Update()
    {
        if (AIPath.desiredVelocity.x>= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (AIPath.desiredVelocity.x<=0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
