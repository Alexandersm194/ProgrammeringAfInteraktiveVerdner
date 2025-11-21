using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSwing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float angle = 1f;    
    public float speed = 6f;       

    private bool isHovered = false;
    private RectTransform rectTransform;
    private Quaternion originalRotation;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalRotation = rectTransform.localRotation;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        StopAllCoroutines();
        StartCoroutine(Swing());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        StopAllCoroutines();
        StartCoroutine(ResetRotation());
    }

    private IEnumerator Swing()
    {
        while (isHovered)
        {
            float rot = Mathf.Sin(Time.time * speed) * angle;
            rectTransform.localRotation = Quaternion.Euler(0, 0, rot);
            yield return null;
        }
    }

    private IEnumerator ResetRotation()
    {
        while (Quaternion.Angle(rectTransform.localRotation, originalRotation) > 0.1f)
        {
            rectTransform.localRotation = Quaternion.Lerp(
                rectTransform.localRotation,
                originalRotation,
                Time.deltaTime * 10f
            );
            yield return null;
        }

        rectTransform.localRotation = originalRotation;
    }
}