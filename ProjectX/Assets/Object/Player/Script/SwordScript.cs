using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
	private float interval = 0.0f;

	public BossScript boss_;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Boss")
		{
			boss_.SetHp(boss_.GetHp() - 7);
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
				boss_.SetHp(boss_.GetHp() - 2);
			}
		}
	}
}
