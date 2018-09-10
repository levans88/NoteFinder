using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteFinder : MonoBehaviour
{
    public double gain = 0.05;
    private double increment;
    private double phase;
    private double sampleRate = 48000;
    private int highestNote = 440;
    private int lowestNote = 220;
    private float sliderValue = 0;
    //private double frequency = 0;
    //private string playPauseButtonTitle = "Pause";

    // http://pages.mtu.edu/~suits/notefreqs.html

    //private double[] tones = { 440.0f, 1046.5f, 3729.3f, 1046.5f };
    //private int tone_index;
    //private float last_time;

    void Start()
    {
        //Debug.Log("" + tone_index + ": " + tones[tone_index]);
        sliderValue = 0;

    }

    void OnGUI()
    {
        //if (GUI.Button(new Rect(10, 70, 150, 30), playPauseButtonTitle))
        //{
            //if (playPauseButtonTitle == "Play") {
            //    playPauseButtonTitle = "Pause";
            //    sliderValue = 0;
            //}
            //else
            //{
            //    playPauseButtonTitle = "Play";
            //}
        //}

        sliderValue = GUI.VerticalSlider(new Rect(25, 25, 100, 500), sliderValue, highestNote, lowestNote);

        Debug.Log(sliderValue);
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        // update increment in case frequency has changed
        // 220
        // 1760
        increment = sliderValue * 2 * Math.PI / sampleRate;

        if (increment != 0) {
            for (var i = 0; i < data.Length; i += channels)
            {
                phase += increment;

                data[i] = (float)(gain * Math.Sin(phase));

                // If stereo, copy data to second channel...
                if (channels == 2)
                {
                    data[i + 1] = data[i];
                }

                if (phase > 2 * Math.PI)
                {
                    phase = 0;
                }
            }
        }
    }
}