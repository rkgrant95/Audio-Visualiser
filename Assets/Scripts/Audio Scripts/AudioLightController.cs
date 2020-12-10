using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLightController : MonoBehaviour
{
    [SerializeField]
    AudioVisualizer audioVisualizer;
    public int bandIndex;
    public float minIntensity;
    public float maxIntensity;

    Light light;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = (audioVisualizer.audioBandBuffer8[bandIndex] * (maxIntensity - minIntensity)) + minIntensity;
    }
}
