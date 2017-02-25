using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventClickEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 _scale;
    public float ScaleRatio = .8f;
    
    void Start()
    {
        _scale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable)
            transform.localScale = new Vector3(_scale.x * ScaleRatio, _scale.y * ScaleRatio, 1);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = new Vector3(_scale.x, _scale.y, 1);
    }
}
