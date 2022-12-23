using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwipeSelectViewContent : MonoBehaviour
{
    private RectTransform[] _items;
    private Rect _frameRect;
    private RectTransform _rectTransform;

    private float _itemLength;
    private float _spaceLength;
    private int _currentIndex;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        

        _frameRect = transform.parent.GetComponent<RectTransform>().rect;

        int childCount = transform.childCount;
        _items = new RectTransform[childCount];
        for (int i = 0; i < childCount; i++)
            _items[i] = transform.GetChild(i).GetComponent<RectTransform>();

        _itemLength = _items[0].rect.width;
        _spaceLength = GetComponent<HorizontalLayoutGroup>().spacing +
                       (_rectTransform.rect.width - _itemLength * childCount) / childCount ;
    }

    public void MoveLeft()
    {
        if (_currentIndex > 0)
            _currentIndex--;

        Move(_currentIndex);
    }

    public void MoveRight()
    {
        if (_currentIndex < _items.Length - 1)
            _currentIndex++;

        Move(_currentIndex);
    }

    public void Move(int index)
    {
        Vector3 moveVec = new Vector3(-(_itemLength + _spaceLength) * index,
                                      transform.localPosition.y,
                                      transform.localPosition.z);
        _rectTransform.localPosition = moveVec;
    }
}
