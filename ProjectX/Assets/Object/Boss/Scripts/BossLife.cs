using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLife : MonoBehaviour
{
    private int Life = 0;
    private bool Initiaraiz = false;
    private Image _image;
    public Paladin_Script player_;
    public BossScript boss_;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();

        _image.color = new Color(255.0f, 9.0f, 9.0f, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        ////ボスステージに入ってる
        //if (!player_.GetCheckBossStage()) return;

        //if (!Initiaraiz)
        //{
        //    _image.color = new Color(255.0f, 9.0f, 9.0f, 1.0f);
        //    Initiaraiz = true;
        //}

        //Life = boss_.GetHp();

        //_image.fillAmount = Life / 100.0f;
    }
}
