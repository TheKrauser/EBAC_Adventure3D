using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeCamera : Singleton<ShakeCamera>
{
    public CinemachineVirtualCamera virtualCamera;
    public float shakeTime;

    public void Shake(float amplitude, float frequency, float time)
    {
        var cam = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cam.m_AmplitudeGain = amplitude;
        cam.m_FrequencyGain = frequency;

        shakeTime = time;
    }

    private void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
        }
        else
        {
            var cam = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cam.m_AmplitudeGain = 0;
            cam.m_FrequencyGain = 0;
        }
    }
}
