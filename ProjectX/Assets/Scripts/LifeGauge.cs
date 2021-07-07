using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{
    public int Life = 0;

    private Image _image;
    public Paladin_Script player_;
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Life = player_.GetHp();

        _image.fillAmount = Life / 100.0f;
    }
}
