using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FadeManager : Singleton<FadeManager>
{
    /// <summary>�Ó]�p���e�N�X�`��</summary>
    private Texture2D blackTexture;
    /// <summary>�t�F�[�h���̓����x</summary>
    private float fadeAlpha = 0;
    /// <summary>�t�F�[�h�����ǂ���</summary>
    private bool isFading = false;

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        //�����ō��e�N�X�`�����
        this.blackTexture = new Texture2D(32, 32, TextureFormat.RGB24, false);
        this.blackTexture.ReadPixels(new Rect(0, 0, 32, 32), 0, 0, false);
        this.blackTexture.SetPixel(0, 0, Color.white);
        this.blackTexture.Apply();
    }

    public void OnGUI()
    {
        if (!this.isFading)
            return;

        //�����x���X�V���č��e�N�X�`����`��
        GUI.color = new Color(0, 0, 0, this.fadeAlpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), this.blackTexture);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scene"> �J�ڐ�</param>
    /// <param name="interval"> </param>
    public void LoadLevel(string scene, float interval)
    {
        StartCoroutine(TransScene(scene, interval));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="interval"></param>
    public void FadeStart(float interval)
    {
        StartCoroutine(Trans(interval));
    }


    /// �V�[���J�ڗp�R���[�`��
    private IEnumerator TransScene(string scene, float interval)
    {
        //���񂾂�Â�
        this.isFading = true;
        float time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        //�V�[���ؑ�
        SceneManager.LoadScene(scene);

        //���񂾂񖾂邭
        time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        this.isFading = false;
    }

    /// �V�[���J�ڗp�R���[�`��
    /// <param name='scene'>�V�[����
    /// <param name='interval'>�Ó]�ɂ����鎞��(�b)
    private IEnumerator Trans(float interval)
    {
        //���񂾂�Â�
        this.isFading = true;
        float time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        //���񂾂񖾂邭
        time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        this.isFading = false;
    }

}
