using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
	Collider m_Collider;

	// Start is called before the first frame update
	void Start()
    {
		m_Collider = GetComponent<Collider>();
		m_Collider.enabled = false;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			col.GetComponent<Paladin_Script>().SetHp(col.GetComponent<Paladin_Script>().GetHp() - 20);
		}
	}

    private void OnCollisionEnter(Collision col)
    {
		if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<Paladin_Script>().SetHp(col.gameObject.GetComponent<Paladin_Script>().GetHp() - 20);
		}
	}

	public void AttackStart()
    {
		m_Collider.enabled = true;
	}

	public void AttackEnd()
    {
		m_Collider.enabled = false;
	}

}
