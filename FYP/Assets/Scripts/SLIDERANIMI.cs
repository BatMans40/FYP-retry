using UnityEngine;
using UnityEngine.UI;

public class SliderFillAnimation : MonoBehaviour
{
    public Slider slider;
    public RawImage fillImage;

    private RectTransform fillImageRectTransform;
    private float imageWidth;
    private float imageSpeed = 1f; // Adjust this value to control the speed of the animation

    private void Start()
    {
        fillImageRectTransform = fillImage.rectTransform;
        imageWidth = fillImageRectTransform.rect.width;
    }

    private void Update()
    {
        UpdateFillImagePosition();
    }

    private void UpdateFillImagePosition()
    {
        float fillAmount = slider.value;
        float sliderWidth = slider.maxValue;
        fillImageRectTransform.anchoredPosition = new Vector2(fillAmount / sliderWidth * slider.gameObject.GetComponent<RectTransform>().rect.width - imageWidth / 2, fillImageRectTransform.anchoredPosition.y);

        // Move the image from left to right
        fillImageRectTransform.Translate(Vector3.right * imageSpeed * Time.deltaTime);

        // Reset the image position when it reaches the end of the slider
        if (fillImageRectTransform.anchoredPosition.x > slider.gameObject.GetComponent<RectTransform>().rect.width + imageWidth / 2)
        {
            fillImageRectTransform.anchoredPosition = new Vector2(-imageWidth / 2, fillImageRectTransform.anchoredPosition.y);
        }
    }
}