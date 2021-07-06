using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
	private float interval = 0.0f;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Boss")
		{
			col.GetComponent<BossScript>().SetHp(col.GetComponent<BossScript>().GetHp() - 10);
		}
	}

	void OnTriggerStay(Collider col)
	{
		interval += 1.0f / 60.0f;

		if (interval >= 0.5)
		{
			interval = 0.0f;

			if (col.tag == "Boss")
			{
				col.GetComponent<BossScript>().SetHp(col.GetComponent<BossScript>().GetHp() - 5);
			}
		}
	}
}
