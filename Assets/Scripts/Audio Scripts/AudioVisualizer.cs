using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioVisualizer : MonoBehaviour
{
    public enum AudioChannel { Sterio, Left, Right };
    public AudioChannel audioChannel = new AudioChannel();

    private AudioSource curAudioSource;

    public float[] samplesLeft = new float[512];
    public float[] samplesRight = new float[512];

    // Audio 8 Bands
    private int numOFFrequencyBands8 = 8;
    private float[] frequencyBand8 = new float[8];
    private float[] frequencyBandBuffer8 = new float[8];
    private float[] fBandBufferDecrease8 = new float[8];
    private float[] frequencyBandHighest8 = new float[8];

    [HideInInspector] public float[] audioBand8;
    [HideInInspector] public float[] audioBandBuffer8;

    // Audio 64 Bands
    private int numOfFrequencyBands64 = 64;
    private float[] frequencyBand64 = new float[64];
    private float[] frequencyBandBuffer64 = new float[64];
    private float[] fBandBufferDecrease64 = new float[64];
    private float[] frequencyBandHighest64 = new float[64];

    [HideInInspector] public float[] audioBand64;
    [HideInInspector] public float[] audioBandBuffer64;

    [HideInInspector] public float amplitude;
    [HideInInspector] public float amplitudeBuffer;
    private float amplitudeHighest;

    // Start is called before the first frame update
    void Start()
    {
        curAudioSource = GetComponent<AudioSource>();
        audioBand8 = new float[numOFFrequencyBands8];
        audioBandBuffer8 = new float[numOFFrequencyBands8];

        audioBand64 = new float[numOfFrequencyBands64];
        audioBandBuffer64 = new float[numOfFrequencyBands64];
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumDataSource();

        GenerateFrequencyBands8();
        GenerateFrequencyBands64();

        FrequencyBandBuffer8();
        FrequencyBandBuffer64();

        GenerateAudioBands8();
        GenerateAudioBands64();

        GetAmplitude();
    }

    private void GetSpectrumDataSource()
    {
        curAudioSource.GetSpectrumData(samplesLeft, 0, FFTWindow.Blackman);
        curAudioSource.GetSpectrumData(samplesRight, 1, FFTWindow.Blackman);
    }

    private void GetAmplitude()
    {
        float currentAmplitude = 0;
        float currentAmplitudeBuffer = 0;

        for (int i = 0; i < numOFFrequencyBands8; i++)
        {
            currentAmplitude += audioBand8[i];
            currentAmplitudeBuffer += audioBandBuffer8[i];
        }

        if (currentAmplitude > amplitudeHighest)
            amplitudeHighest = currentAmplitude;

        amplitude = currentAmplitude / amplitudeHighest;
        amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
    }

    private void GenerateFrequencyBands8()
    {
        int count = 0;

        for (int i = 0; i < numOFFrequencyBands8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
                sampleCount += 2;

            for (int x = 0; x < sampleCount; x++)
            {
                switch (audioChannel)
                {
                    case AudioChannel.Sterio:
                        average += samplesLeft[count] + samplesRight[count] * (count + 1);
                        break;
                    case AudioChannel.Left:
                        average += samplesLeft[count] * (count + 1);
                        break;
                    case AudioChannel.Right:
                        average += samplesRight[count] * (count + 1);
                        break;
                    default:
                        break;
                }
                count++;
            }

            average /= count;

            frequencyBand8[i] = average * 10;
        }
    }

    private void GenerateFrequencyBands64()
    {
        int count = 0;
        int power = 0;
        int sampleCount = 1;

        for (int i = 0; i < numOfFrequencyBands64; i++)
        {
            float average = 0;

            if (i == 16 || i == 32 || i == 40 || i == 48 || i == 56)

            {
                power++;
                sampleCount = (int)Mathf.Pow(2, power);
                if (power == 3)
                {
                    sampleCount -= 2;
                }
            }
            for (int x = 0; x < sampleCount; x++)
            {
                switch (audioChannel)
                {
                    case AudioChannel.Sterio:
                        average += samplesLeft[count] + samplesRight[count] * (count + 1);
                        break;
                    case AudioChannel.Left:
                        average += samplesLeft[count] * (count + 1);
                        break;
                    case AudioChannel.Right:
                        average += samplesRight[count] * (count + 1);
                        break;
                    default:
                        break;
                }
                count++;
            }

            average /= count;

            frequencyBand64[i] = average * 80;
        }
    }

    private void GenerateAudioBands8()
    {
        for (int i = 0; i < numOFFrequencyBands8; i++)
        {
            if (frequencyBand8[i] > frequencyBandHighest8[i])
                frequencyBandHighest8[i] = frequencyBand8[i];

            audioBand8[i] = frequencyBand8[i] / frequencyBandHighest8[i];
            audioBandBuffer8[i] = frequencyBandBuffer8[i] / frequencyBandHighest8[i];
        }
    }

    private void GenerateAudioBands64()
    {
        for (int i = 0; i < numOfFrequencyBands64; i++)
        {
            if (frequencyBand64[i] > frequencyBandHighest64[i])
                frequencyBandHighest64[i] = frequencyBand64[i];

            audioBand64[i] = frequencyBand64[i] / frequencyBandHighest64[i];
            audioBandBuffer64[i] = frequencyBandBuffer64[i] / frequencyBandHighest64[i];
        }
    }

    private void FrequencyBandBuffer8()
    {
        for (int i = 0; i < numOFFrequencyBands8; i++)
        {
            if (frequencyBand8[i] > frequencyBandBuffer8[i])
            {
                frequencyBandBuffer8[i] = frequencyBand8[i];
                fBandBufferDecrease8[i] = 0.005f;
            }

            if (frequencyBand8[i] < frequencyBandBuffer8[i])
            {
                frequencyBandBuffer8[i] -= fBandBufferDecrease8[i];
                fBandBufferDecrease8[i] *= 1.2f;
            }
        }      
    }
    private void FrequencyBandBuffer64()
    {
        for (int i = 0; i < numOfFrequencyBands64; i++)
        {
            if (frequencyBand64[i] > frequencyBandBuffer64[i])
            {
                frequencyBandBuffer64[i] = frequencyBand64[i];
                fBandBufferDecrease64[i] = 0.005f;
            }

            if (frequencyBand64[i] < frequencyBandBuffer64[i])
            {
                frequencyBandBuffer64[i] -= fBandBufferDecrease64[i];
                fBandBufferDecrease64[i] *= 1.2f;
            }
        }
    }
}
