using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate512Cubes : MonoBehaviour
{
    public enum AudioBands { Eight, SixtyFour, FiveHundredAndTwelve };
    public AudioBands audioBands = new AudioBands();

    [SerializeField]
    private AudioVisualizer audioVisualizer;
    public GameObject sampleCubePrefab;

    private GameObject[] sampleCube;
    [SerializeField] private bool useBuffer;
    // Start is called before the first frame update
    void Start()
    {
        switch (audioBands)
        {
            case AudioBands.Eight:
                sampleCube = new GameObject[8];
                Generate8CubesInCircle();
                break;
            case AudioBands.SixtyFour:
                sampleCube = new GameObject[64];
                Generate64CubesInCircle();
                break;
            case AudioBands.FiveHundredAndTwelve:
                sampleCube = new GameObject[512];
                Generate512CubesInCircle();
                break;
            default:
                break;
        }
    }

    private void Generate8CubesInCircle()
    {
        for (int i = 0; i < sampleCube.Length; i++)
        {
            GameObject sampleCubeInstance = Instantiate(sampleCubePrefab);
            sampleCubeInstance.transform.position = this.transform.position;
            sampleCubeInstance.transform.parent = this.transform;
            sampleCubeInstance.name = "SampleCube " + i;
            ParametricCube pCube = sampleCubeInstance.GetComponent<ParametricCube>();

            pCube.audioVisualizer = GetComponentInParent<AudioVisualizer>();
            pCube.bandIndex = i;
            pCube.scaleMultiplier = 100;
            pCube.useBuffer = useBuffer;
            pCube.colorMultiplier = 2;
            pCube.startScale = 10;

            transform.eulerAngles = new Vector3(0, -45 * i, 0);

            sampleCubeInstance.transform.position = Vector3.forward * 100;
            sampleCube[i] = sampleCubeInstance;
        }
    }

    private void Generate64CubesInCircle()
    {
        for (int i = 0; i < sampleCube.Length; i++)
        {
            GameObject sampleCubeInstance = Instantiate(sampleCubePrefab);
            sampleCubeInstance.transform.position = this.transform.position;
            sampleCubeInstance.transform.parent = this.transform;
            sampleCubeInstance.name = "SampleCube " + i;
            ParametricCube pCube = sampleCubeInstance.GetComponent<ParametricCube>();

            pCube.audioVisualizer = GetComponentInParent<AudioVisualizer>();
            pCube.bandIndex = i;
            pCube.scaleMultiplier = 100;
            pCube.useBuffer = useBuffer;
            pCube.colorMultiplier = 2;
            pCube.startScale = 2.5f;

            transform.eulerAngles = new Vector3(0, -5.625f * i, 0);

            sampleCubeInstance.transform.position = Vector3.forward * 100;
            sampleCube[i] = sampleCubeInstance;
        }
    }

    private void Generate512CubesInCircle()
    {
        for (int i = 0; i < sampleCube.Length; i++)
        {
            GameObject sampleCubeInstance = Instantiate(sampleCubePrefab);
            sampleCubeInstance.transform.position = this.transform.position;
            sampleCubeInstance.transform.parent = this.transform;
            sampleCubeInstance.name = "SampleCube " + i;
            ParametricCube pCube = sampleCubeInstance.GetComponent<ParametricCube>();

            pCube.audioVisualizer = GetComponentInParent<AudioVisualizer>();
            pCube.bandIndex = i;
            pCube.scaleMultiplier = 100;
            pCube.useBuffer = useBuffer;
            pCube.colorMultiplier = 2;
            pCube.startScale = 1;

            transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);

            sampleCubeInstance.transform.position = Vector3.forward * 100;
            sampleCube[i] = sampleCubeInstance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < sampleCube.Length; i++)
        {
            if (sampleCube != null)
            {
                sampleCube[i].GetComponent<ParametricCube>().useBuffer = useBuffer;
            }
        }
        /*
        for (int i = 0; i < sampleCube.Length; i++)
        {
            if (sampleCube != null)
            {
                // sampleCube[i].transform.localScale = new Vector3(scaleX, (audioVisualizer.samplesLeft[i] * maxScaleY) + 2, scaleZ);
                transform.localScale = new Vector3(transform.localScale.x, (audioVisualizer.audioBandBuffer8[bandIndex] * scaleMultiplier) + startScale, transform.localScale.z);

            }
        }
        */
    }
}
