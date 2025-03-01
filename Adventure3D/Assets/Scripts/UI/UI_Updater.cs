using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Updater : MonoBehaviour
{
    public Image uiImage;

    public float duration = 0.1f;
    public Ease ease = Ease.OutBack;

    private Tween currentTween;

    private void OnValidate()
    {
        if (uiImage != null) uiImage = GetComponent<Image>();
    }

    public void UpdateValue(float f)
    {
        uiImage.fillAmount = f;
    }

    public void UpdateValue(float max, float current)
    {
        if (currentTween != null) currentTween.Kill();
        currentTween = uiImage.DOFillAmount(1 - (current/max), duration).SetEase(ease);
    }
}
