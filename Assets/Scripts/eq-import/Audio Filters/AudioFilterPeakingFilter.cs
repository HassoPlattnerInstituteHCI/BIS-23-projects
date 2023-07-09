using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioFilterPeakingFilter : AudioFilterParent
{
    public List<float> frequencies = new List<float>();
    public float[] dbGain = new float[]{0};
    public float resonance = 1f;
    public float startingFrequency;
    public float midFrequency;
    public float endingFrequency;
    public int numOfBands = 30;
    private float sampleRate;
    
    override protected void InitFilters()
    {
        
        sampleRate = AudioSettings.outputSampleRate;
        int midNum = numOfBands % 2 != 0 ? (numOfBands / 2) + 1 : (numOfBands / 2);
        frequencies = GetHertzBands(startingFrequency, midFrequency, midNum);
        List<float> highBands = GetHertzBands(midFrequency, endingFrequency, numOfBands / 2);
        highBands.RemoveAt(0);
        frequencies.AddRange(highBands);
        myFilters = new BiQuadFilter[numOfBands+1];
        for (int i = 0; i < numOfBands+1; i++) {
            myFilters[i] = BiQuadFilter.PeakingEQ(sampleRate, frequencies[i], resonance, dbGain[i]);
        }
    }
    List<float> GetHertzBands(float startingPointHertz, float endPointHertz, int numberOfBands)
    {
        float[] bands = new float[numberOfBands + 1];
        float c = (endPointHertz - startingPointHertz) / (2<<numberOfBands);
        bands[0] = startingPointHertz;
        for (int i = 1; i < (numberOfBands + 1); i++)
        {
            bands[i] = startingPointHertz + c * (2<<i);
        }
        return new List<float> (bands);
    }
}
