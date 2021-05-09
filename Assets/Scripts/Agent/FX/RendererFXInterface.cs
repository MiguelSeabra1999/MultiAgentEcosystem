using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererFXInterface : MonoBehaviour
{
    public Renderer skin;
    public Renderer[] allSkins;

    public float fadeOutTime;
    private void Awake() {
        allSkins = GetComponentsInChildren<Renderer>();

    }

    [ContextMenu("changeToRed")]
    private void ChangeColor()
    {
        skin.material.color = Color.red;
    }

    public void SetColor(Color c)
    {
        skin.material.color = c;
    }
    [ContextMenu("fade out")]
    public void StartFadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        float startTime = Time.time;
        float percent = 0;
        while(percent < 1)
        {
            percent = (Time.time - startTime)/fadeOutTime;
            SetRendererAlpha(Mathf.Lerp(1,0,percent));
            yield return null;
        }
    }

    private void SetRendererAlpha(float alpha)
    {
       
       
            skin.material.color = new Color(skin.material.color.r ,skin.material.color.g, skin.material.color.b , alpha);
     
    }
}
