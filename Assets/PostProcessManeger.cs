using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessManeger : MonoBehaviour
{
    public Volume volume;
    Vignette Vin;
    public GameObject player;
    PlayerController playerSkript;
    public float[] bPM;
    float time;
    float time2;
    float timeScale = 0.1f;
    public AnimationCurve heartbeat;
    public AnimationCurve hbScale;

    private void Awake()
    {
        volume.profile.TryGet(out Vin);
        playerSkript = player.GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (playerSkript.isAnxius){
            for (int i = 0; i < playerSkript.anxietyInteval.Length; i++){
                if (playerSkript.anxietyLevel > playerSkript.anxietyInteval[i] && playerSkript.anxietyLevel < playerSkript.anxietyInteval[i + 1]){
                    Vin.intensity.value = heartbeat.Evaluate(time);
                    timeScale = hbScale.Evaluate(time2);
                    time += Time.fixedDeltaTime * bPM[i] * timeScale;
                    time2 += Time.fixedDeltaTime;
                }

            }
        }
        else if (Vin.intensity.value != 0 && !playerSkript.isAnxius) {
            Vin.intensity.value = heartbeat.Evaluate(time);
            time += Time.fixedDeltaTime;
            if (Vin.intensity.value < 0.02)
                Vin.intensity.value = 0;
        }
    }

    /*void HeartBeat() {
        Debug.Log(Vin.intensity.value);
        for (int i = 0; i < playerSkript.anxietyInteval.Length; i++)
        {
            if (playerSkript.anxietyLevel > playerSkript.anxietyInteval[i] && playerSkript.anxietyLevel < playerSkript.anxietyInteval[i + 1])
            {
                if (Vin.intensity.value != 0 && Vin.intensity.value >= 0 && !incresing)
                {
                    Vin.intensity.value -= Time.fixedDeltaTime / (bPM[i] * 2) * intensity;
                    //Vin.intensity.value = Mathf.Lerp(1*intensity,0, Time.fixedDeltaTime / (bPM[i] * 2));
                    if (Vin.intensity.value < 0.02f * intensity)
                    {
                        Debug.Log("Beat");
                        Vin.intensity.value = 0;
                        incresing = true;
                    }
                }
                else if (Vin.intensity.value != 1 * intensity && Vin.intensity.value <= 1 * intensity && incresing)
                {
                    
                    Vin.intensity.value += Time.fixedDeltaTime / (bPM[i] * 2) * intensity;
                    //Vin.intensity.value = Mathf.Lerp(0, 1*intensity, Time.deltaTime / (bPM[i] * 2));
                    if (Vin.intensity.value > 0.98f * intensity)
                    {
                        Debug.Log("Beat");
                        Vin.intensity.value = 1 * intensity;
                        incresing = false;
                    }
                }
            }

        }
    }*/
}
