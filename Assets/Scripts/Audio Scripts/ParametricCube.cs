using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametricCube : MonoBehaviour
{
    public AudioVisualizer audioVisualizer;
    private Instantiate512Cubes cubeInstantiator;
    public float startScale;
    public float scaleMultiplier;
    public int bandIndex;
    public bool useBuffer;

    [Range(-10, 10)]
    public float colorMultiplier = 0;
    Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponentInChildren<MeshRenderer>().materials[0];
        cubeInstantiator = GetComponentInParent<Instantiate512Cubes>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (cubeInstantiator.audioBands)
        {
            case Instantiate512Cubes.AudioBands.Eight:
                Activate8();
                break;
            case Instantiate512Cubes.AudioBands.SixtyFour:
                Activate64();
                break;
            case Instantiate512Cubes.AudioBands.FiveHundredAndTwelve:
                break;
            default:
                break;
        }
    }

    private void Activate8()
    {
        if (useBuffer)
        {
            transform.localScale = new Vector3(startScale, (audioVisualizer.audioBandBuffer8[bandIndex] * scaleMultiplier) + startScale, startScale);
            if (transform.localScale.y <= startScale)
                transform.localScale = new Vector3(transform.localScale.x, startScale, transform.localScale.z);

            Color color = new Color(audioVisualizer.audioBandBuffer8[bandIndex], audioVisualizer.audioBandBuffer8[bandIndex], audioVisualizer.audioBandBuffer8[bandIndex]);
            material.SetColor("_EmissionColor", material.color * color * colorMultiplier);

        }
        else
        {
            transform.localScale = new Vector3(startScale, (audioVisualizer.audioBand8[bandIndex] * scaleMultiplier) + startScale, startScale);
            if (transform.localScale.y <= startScale)
                transform.localScale = new Vector3(transform.localScale.x, startScale, transform.localScale.z);

            Color color = new Color(audioVisualizer.audioBand8[bandIndex], audioVisualizer.audioBand8[bandIndex], audioVisualizer.audioBand8[bandIndex]);
            material.SetColor("_EmissionColor", material.color * color * colorMultiplier);
        }
    }

    private void Activate64()
    {
        if (useBuffer)
        {
            transform.localScale = new Vector3(startScale, (audioVisualizer.audioBandBuffer64[bandIndex] * scaleMultiplier) + startScale, startScale);
            if (transform.localScale.y <= startScale)
                transform.localScale = new Vector3(transform.localScale.x, startScale, transform.localScale.z);

            Color color = new Color(audioVisualizer.audioBandBuffer64[bandIndex], audioVisualizer.audioBandBuffer64[bandIndex], audioVisualizer.audioBandBuffer64[bandIndex]);
            material.SetColor("_EmissionColor", material.color * color * colorMultiplier);

        }
        else
        {
            transform.localScale = new Vector3(startScale, (audioVisualizer.audioBand64[bandIndex] * scaleMultiplier) + startScale, startScale);
            if (transform.localScale.y <= startScale)
                transform.localScale = new Vector3(transform.localScale.x, startScale, transform.localScale.z);

            Color color = new Color(audioVisualizer.audioBand64[bandIndex], audioVisualizer.audioBand64[bandIndex], audioVisualizer.audioBand64[bandIndex]);
            material.SetColor("_EmissionColor", material.color * color * colorMultiplier);
        }
    }

}
