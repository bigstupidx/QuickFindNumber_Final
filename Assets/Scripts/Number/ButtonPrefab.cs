using UnityEngine;
using UnityEngine.UI;

public class ButtonPrefab : MonoBehaviour
{

    public Image number;
    public Text txtNumber;
    public int id;
    public Image circle;

    public void SetInfo(string _number, int index)
    {
        //number.sprite = Resources.Load<Sprite>("Sprites/Numbers/" + _number);
        txtNumber.text = _number;
        id = index;
    }

    public void SetCircle(string _circle)
    {
        circle.sprite = Resources.Load<Sprite>("Sprites/CompressSprites/" + _circle);
        circle.gameObject.SetActive(true);
    }
}
