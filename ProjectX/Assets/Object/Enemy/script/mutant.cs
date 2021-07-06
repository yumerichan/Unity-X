using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mutant : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator Animator_;

    public Paladin_Script player;
    void Start()
    {
        Animator_ = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;


        Vector3 Pos = player.GetPos();

        float DistanceE2P = SubDistance(Pos, pos);

        Debug.Log(DistanceE2P);
      

        if(DistanceE2P<5)
        {
            Animator_.SetBool("IsAttack", true);
        }
        else if (DistanceE2P <= 10)
        {
            Animator_.SetBool("IsWalk", true);
        }
        else if (DistanceE2P<=20)
        {
            Animator_.SetBool("IsRun", true);
        }






        transform.position = pos;
        transform.rotation = rot;
    }

    float SubDistance(Vector3 a,Vector3 b)
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


