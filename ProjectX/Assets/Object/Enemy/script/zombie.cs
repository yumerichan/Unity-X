using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour
{

    private Animator animator_;
    public Paladin_Script player;
    // Start is called before the first frame update
    void Start()
    {
        animator_ = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 rot = transform.localEulerAngles;
        Vector3 scale = transform.localScale;


        Vector3 Pos = player.GetPos();

        float DistanceE2P = SubDistance(Pos, pos);




        if (DistanceE2P < 5)
        {
            animator_.SetBool("attack", true);
        }
        else if (DistanceE2P <= 10)
        {
            animator_.SetBool("walk", true);
        }
        else if (DistanceE2P <= 20)
        {
            animator_.SetBool("run", true);
        }






        transform.position = pos;
        transform.localEulerAngles = rot;
        transform.localScale = scale;
    }
    float SubDistance(Vector3 a, Vector3 b)
    {
        float ans = 0;
        float x, y, z;
        x = a.x - b.x;
        y = a.y - b.y;
        z = a.z - b.z;

        ans = Mathf.Sqrt(x * x + y * y + z * z);
        return ans;
    }
}
