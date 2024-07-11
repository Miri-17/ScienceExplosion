using UnityEngine;
using System.Collections;

public class AudioSpectrumManager : MonoBehaviour
{
    // public int numberOfSamples = 1024;
    // public BandType bandType = BandType.TenBand;
    // public float fallSpeed = 0.08f;
    // public float sensibility = 8.0f;

    // public enum BandType
    // {
    //     FourBand,
    //     FourBandVisual,
    //     EightBand,
    //     TenBand,
    //     TwentySixBand,
    //     ThirtyOneBand
    // };

    // private AudioSource audioSource;
    // private float[] rawSpectrum;
    // private float[] levels;
    // private float[] peakLevels;
    // private float[] meanLevels;

    // public float[] Levels => levels;
    // public float[] PeakLevels => peakLevels;
    // public float[] MeanLevels => meanLevels;

    // void Awake()
    // {
    //     audioSource = GetComponent<AudioSource>();
    //     if (audioSource == null)
    //     {
    //         Debug.LogError("AudioSource component not found on the GameObject.");
    //         enabled = false; // Disable the script if AudioSource is not found
    //         return;
    //     }

    //     // Initialize arrays based on the selected band type
    //     int bandCount = GetBandCount();
    //     levels = new float[bandCount];
    //     peakLevels = new float[bandCount];
    //     meanLevels = new float[bandCount];
    //     rawSpectrum = new float[numberOfSamples];
    // }

    // void Update()
    // {
    //     // Get spectrum data from the AudioSource
    //     audioSource.GetSpectrumData(rawSpectrum, 0, FFTWindow.BlackmanHarris);

    //     // Calculate levels based on the selected band type
    //     CalculateLevels();
    // }

    // private void CalculateLevels()
    // {
    //     float[] middleFrequencies = GetMiddleFrequencies();
    //     float bandwidth = GetBandwidth();

    //     float falldown = fallSpeed * Time.deltaTime;
    //     float filter = Mathf.Exp(-sensibility * Time.deltaTime);

    //     for (int bi = 0; bi < levels.Length; bi++)
    //     {
    //         int imin = FrequencyToSpectrumIndex(middleFrequencies[bi] / bandwidth);
    //         int imax = FrequencyToSpectrumIndex(middleFrequencies[bi] * bandwidth);

    //         float bandMax = 0.0f;
    //         for (int fi = imin; fi <= imax; fi++)
    //         {
    //             bandMax = Mathf.Max(bandMax, rawSpectrum[fi]);
    //         }

    //         levels[bi] = bandMax;
    //         peakLevels[bi] = Mathf.Max(peakLevels[bi] - falldown, bandMax);
    //         meanLevels[bi] = bandMax - (bandMax - meanLevels[bi]) * filter;
    //     }
    // }

    // private int FrequencyToSpectrumIndex(float f)
    // {
    //     int i = Mathf.FloorToInt(f / AudioSettings.outputSampleRate * 2.0f * rawSpectrum.Length);
    //     return Mathf.Clamp(i, 0, rawSpectrum.Length - 1);
    // }

    // private int GetBandCount()
    // {
    //     BandType[] bandTypes = (BandType[])System.Enum.GetValues(typeof(BandType));
    //     return middleFrequenciesForBands[(int)bandType].Length;
    // }

    // private float[] GetMiddleFrequencies()
    // {
    //     return middleFrequenciesForBands[(int)bandType];
    // }

    // private float GetBandwidth()
    // {
    //     return bandwidthForBands[(int)bandType];
    // }

    // private static float[][] middleFrequenciesForBands = {
    //     new float[]{ 125.0f, 500, 1000, 2000 },
    //     new float[]{ 250.0f, 400, 600, 800 },
    //     new float[]{ 63.0f, 125, 500, 1000, 2000, 4000, 6000, 8000 },
    //     new float[]{ 31.5f, 63, 125, 250, 500, 1000, 2000, 4000, 8000, 16000 },
    //     new float[]{ 25.0f, 31.5f, 40, 50, 63, 80, 100, 125, 160, 200, 250, 315, 400, 500, 630, 800, 1000, 1250, 1600, 2000, 2500, 3150, 4000, 5000, 6300, 8000 },
    //     new float[]{ 20.0f, 25, 31.5f, 40, 50, 63, 80, 100, 125, 160, 200, 250, 315, 400, 500, 630, 800, 1000, 1250, 1600, 2000, 2500, 3150, 4000, 5000, 6300, 8000, 10000, 12500, 16000, 20000 },
    // };

    // private static float[] bandwidthForBands = {
    //     1.414f, // 2^(1/2)
    //     1.260f, // 2^(1/3)
    //     1.414f, // 2^(1/2)
    //     1.414f, // 2^(1/2)
    //     1.122f, // 2^(1/6)
    //     1.122f  // 2^(1/6)
    // };
}
