using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollision : MonoBehaviour
{
    public BossScript boss_;

    // Update is called once per frame
    void Update()
    {
        transform.position = boss_.GetPos();
    }
}
