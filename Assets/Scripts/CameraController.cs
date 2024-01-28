using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour
{
    Rigidbody2D rb;
    public Volume vol;
    Vignette vignette;
    Bloom bloom;
    ColorAdjustments colorAdjustments;
    DepthOfField blur;
    LensDistortion lens;
    ChromaticAberration chromAb;
    float vignetteRand;
    float bloomRand;
    float colorRand;
    float blurRand;
    float lensRand;
    float chromAbRand;

    public ParticleSystem.MinMaxCurve parabolicCurve;
    public ParticleSystem.MinMaxCurve sinusoidCurve;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        vol.profile.TryGet(out vignette);
        vol.profile.TryGet(out bloom);
        vol.profile.TryGet(out colorAdjustments);
        vol.profile.TryGet(out blur);
        vol.profile.TryGet(out lens);
        vol.profile.TryGet(out chromAb);

        vignetteRand = Random.value;
        bloomRand = Random.value;
        colorRand = Random.value;
        blurRand = Random.value;
        lensRand = Random.value;
        chromAbRand = Random.value;
    }

    // Update is called once per frame
    void Update()
    {
        float drinkRatio = GameManager.Instance.GetDrinkCount()/10.0f;
        //Debug.Log(drinkRatio);
        vignette.intensity.Override(sinusoidCurve.Evaluate(Time.time + vignetteRand,vignetteRand) * 0.5f * drinkRatio);
        //bloom.intensity.Override(sinusoidCurve.Evaluate(Time.time + bloomRand, bloomRand) * 50.0f * drinkRatio);
        colorAdjustments.hueShift.Override(sinusoidCurve.Evaluate(Time.time + colorRand, colorRand) * 180 * drinkRatio);
        blur.focalLength.Override(sinusoidCurve.Evaluate(Time.time + blurRand, blurRand) * 300 * drinkRatio);
        lens.intensity.Override((sinusoidCurve.Evaluate(Time.time + lensRand, lensRand) * 2.0f -1) * drinkRatio);
        chromAb.intensity.Override(sinusoidCurve.Evaluate(Time.time + chromAbRand, chromAbRand) * 1.0f * drinkRatio);
    }

    public IEnumerator BloomFlash(float intensity, int numFrames)
    {
        //Range 0 to 100
        float baseIntensity = 0;// (float)bloom.intensity;
        float randSeed = Random.value;

        for(float i = 0; i < numFrames/2; i++)
        {
            bloom.intensity.Override(parabolicCurve.Evaluate(i/numFrames*2, randSeed)*intensity+baseIntensity);
            yield return null;
        }
        for (float i = numFrames / 2; i >= 0; i--)
        {
            bloom.intensity.Override(parabolicCurve.Evaluate(i / numFrames * 2, randSeed) * intensity + baseIntensity);
            yield return null;
        }
        bloom.intensity.Override(baseIntensity);
    }

    public IEnumerator VignetteFlash(float intensity, int numFrames)
    {
        //Range 0 to 1
        float baseIntensity = 0;//(float)vignette.intensity;
        float randSeed = Random.value;

        for (float i = 0; i < numFrames / 2; i++)
        {
            vignette.intensity.Override(parabolicCurve.Evaluate(i / numFrames * 2, randSeed) * intensity + baseIntensity);
            yield return null;
        }
        for (float i = numFrames / 2; i >= 0; i--)
        {
            vignette.intensity.Override(parabolicCurve.Evaluate(i / numFrames * 2, randSeed) * intensity + baseIntensity);
            yield return null;
        }
        vignette.intensity.Override(baseIntensity);
    }

    public IEnumerator ColorFlash(float intensity, int numFrames)
    {
        //Range -180 to 180
        float baseIntensity = 0;//(float)colorAdjustments.hueShift;
        float randSeed = Random.value;

        for (float i = 0; i < numFrames / 2; i++)
        {
            colorAdjustments.hueShift.Override(parabolicCurve.Evaluate(i / numFrames * 2, randSeed) * intensity + baseIntensity);
            yield return null;
        }
        for (float i = numFrames / 2; i >= 0; i--)
        {
            colorAdjustments.hueShift.Override(parabolicCurve.Evaluate(i / numFrames * 2, randSeed) * intensity + baseIntensity);
            yield return null;
        }
        colorAdjustments.hueShift.Override(baseIntensity);
    }

    public IEnumerator BlurFlash(float intensity, int numFrames)
    {
        //Range 1 to 300
        float baseIntensity = 0;//(float)blur.focalLength;
        float randSeed = Random.value;

        for (float i = 0; i < numFrames / 2; i++)
        {
            blur.focalLength.Override(parabolicCurve.Evaluate(i / numFrames * 2, randSeed) * intensity + baseIntensity);
            yield return null;
        }
        for (float i = numFrames / 2; i >= 0; i--)
        {
            blur.focalLength.Override(parabolicCurve.Evaluate(i / numFrames * 2, randSeed) * intensity + baseIntensity);
            yield return null;
        }
        blur.focalLength.Override(baseIntensity);
    }

    public IEnumerator LensFlash(float intensity, int numFrames)
    {
        //Range -1 to 1
        float baseIntensity = 0;//(float)lens.intensity;
        float randSeed = Random.value;

        for (float i = 0; i < numFrames / 2; i++)
        {
            lens.intensity.Override(parabolicCurve.Evaluate(i / numFrames * 2, randSeed) * intensity + baseIntensity);
            yield return null;
        }
        for (float i = numFrames / 2; i >= 0; i--)
        {
            lens.intensity.Override(parabolicCurve.Evaluate(i / numFrames * 2, randSeed) * intensity + baseIntensity);
            yield return null;
        }
        lens.intensity.Override(baseIntensity);
    }
    public IEnumerator ChromAbFlash(float intensity, int numFrames)
    {
        //Range 0 to 1
        float baseIntensity = 0;//(float)chromAb.intensity;
        float randSeed = Random.value;

        for (float i = 0; i < numFrames / 2; i++)
        {
            chromAb.intensity.Override(parabolicCurve.Evaluate(i / numFrames * 2, randSeed) * intensity + baseIntensity);
            yield return null;
        }
        for (float i = numFrames / 2; i >= 0; i--)
        {
            chromAb.intensity.Override(parabolicCurve.Evaluate(i / numFrames * 2, randSeed) * intensity + baseIntensity);
            yield return null;
        }
        chromAb.intensity.Override(baseIntensity);
    }
}
