using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaysController : MonoBehaviour
{
    public GameplayEvents gameplayEvents;
    public Light DirectionalLight;
    public LightingPreset Preset;
    public float daysCounter = 0;
    [SerializeField, Range(0, 24)] private float TimeOfDay;

   void Awake() {
       
   }
       private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            //(Replace with a reference to the game time)
            TimeOfDay += Time.deltaTime;
            if(TimeOfDay >= 24f) {
                daysCounter++;
                gameplayEvents.InvokeSaveData();
            }
            TimeOfDay %= 24; //Modulus to ensure always between 0-24
                daysCounter = Mathf.Floor(daysCounter) + TimeOfDay / 24f;
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }


    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        //RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
