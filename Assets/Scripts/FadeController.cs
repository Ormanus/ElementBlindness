using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public float duration = 1;
    private UnityEngine.UI.Image img;
    private float startTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        img = GetComponent<UnityEngine.UI.Image>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (img != null)
        {
            float a = Mathf.Clamp01((Time.time - startTime) / duration);
            Color oc = img.color;
            img.color = new Color(oc.r, oc.g, oc.b, a);
        }
    }
}
