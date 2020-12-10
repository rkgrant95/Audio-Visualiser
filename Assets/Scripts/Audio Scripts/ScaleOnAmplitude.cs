using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAmplitude : MonoBehaviour
{
    [SerializeField]
    private AudioVisualizer audioVisualizer;

    public float startScale;
    public float maxScale;

    public bool useBuffer;
    private Material material;
    public float red, green, blue;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (useBuffer)
        {
            transform.localScale = new Vector3((audioVisualizer.amplitudeBuffer * maxScale) + startScale, (audioVisualizer.amplitudeBuffer * maxScale) + startScale, (audioVisualizer.amplitudeBuffer * maxScale) + startScale);
            Color color = new Color(red * audioVisualizer.amplitudeBuffer, green * audioVisualizer.amplitudeBuffer, blue * audioVisualizer.amplitudeBuffer);
            material.SetColor("_EmissionColor", material.color * color);
        }   
        else
        {
            transform.localScale = new Vector3((audioVisualizer.amplitude * maxScale) + startScale, (audioVisualizer.amplitude * maxScale) + startScale, (audioVisualizer.amplitude * maxScale) + startScale);
            Color color = new Color(red * audioVisualizer.amplitude, green * audioVisualizer.amplitude, blue * audioVisualizer.amplitude);
            material.SetColor("_EmissionColor", material.color * color);
        }
    }
}
