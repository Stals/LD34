// AutoFade.cs
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AutoFade : MonoBehaviour
{
    private static AutoFade m_Instance = null;
    private Material m_Material = null;
    private string m_LevelName = "";
    private int m_LevelIndex = 0;
    private bool m_Fading = false;
	
	GameObject loadingObject;

    private static AutoFade Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (new GameObject("AutoFade")).AddComponent<AutoFade>();
				//m_Instance.transform.position = new Vector3(0, 0, 0);
				m_Instance.createLoadingText();
            }
            return m_Instance;
        }
    }
    public static bool Fading
    {
        get { return Instance.m_Fading; }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        m_Instance = this;
		m_Material = new Material(Shader.Find("GreyScale"));
    }
    
    private void DrawQuad(Color aColor, float aAlpha)
    {
		SpriteRenderer[] renderers = loadingObject.GetComponentsInChildren<SpriteRenderer> ();
		foreach (var r in renderers) {
			Color newColor = r.color;
			newColor.a = aAlpha;
			r.color = newColor;
		}

		Text label = loadingObject.GetComponentInChildren<Text> ();
		Color labelColor = label.color;
		labelColor.a = aAlpha;
		label.color = labelColor;


		/*float z = 0f;
        aColor.a = aAlpha;
        m_Material.color = aColor;
        m_Material.SetPass(0);
        GL.PushMatrix();
        GL.LoadOrtho();
        GL.Begin(GL.QUADS);
		GL.Vertex3(0, 0, z);
		GL.Vertex3(0, 1, z);
		GL.Vertex3(1, 1, z);
		GL.Vertex3(1, 0, z);
        GL.End();
        GL.PopMatrix();*/
    }

    private IEnumerator Fade(float aFadeOutTime, float aFadeInTime, Color aColor)
    {
        float t = 0.0f;
        while (t < 1.0f)
        {
            yield return new WaitForEndOfFrame();
            t = Mathf.Clamp01(t + unscaledClampedDT() / aFadeOutTime);
            DrawQuad(aColor, t);
        }
        if (m_LevelName != "")
            Application.LoadLevel(m_LevelName);
        else
            Application.LoadLevel(m_LevelIndex);
        yield return null; // skip first frame
        while (t > 0.0f)
        {
            yield return new WaitForEndOfFrame();
            t = Mathf.Clamp01(t - unscaledClampedDT() / aFadeInTime);
            DrawQuad(aColor, t);
        }
        m_Fading = false;
    }
    private void StartFade(float aFadeOutTime, float aFadeInTime, Color aColor)
    {
        m_Fading = true;
        StartCoroutine(Fade(aFadeOutTime, aFadeInTime, aColor));
    }

    float unscaledClampedDT()
    {
        return Mathf.Clamp(Time.unscaledDeltaTime ,0.00001f, Time.maximumDeltaTime);
    }

    public static void LoadLevel(string aLevelName, float aFadeOutTime, float aFadeInTime, Color aColor)
    {
        if (Fading) return;
        Instance.m_LevelName = aLevelName;
        Instance.StartFade(aFadeOutTime, aFadeInTime, aColor);
    }

    public static void LoadLevel(int aLevelIndex, float aFadeOutTime, float aFadeInTime, Color aColor)
    {
        if (Fading) return;
        Instance.m_LevelName = "";
        Instance.m_LevelIndex = aLevelIndex;
        Instance.StartFade(aFadeOutTime, aFadeInTime, aColor);
    }

	void createLoadingText()
	{
		if (loadingObject != null)
			return;

		loadingObject = Instantiate(Resources.Load("LoadingObject"), new Vector3(0, 0, 2), Quaternion.identity) as GameObject;
		loadingObject.transform.parent = this.transform;

		Camera cam = loadingObject.GetComponent<Camera> ();
	}

}