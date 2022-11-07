using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILinePointsTest : MonoBehaviour
{
    [SerializeField] UILineRenderer[] _lines;
    List<GameObject> _images = new List<GameObject>();
    List<Vector2> _pointzzz = new List<Vector2>();

    int _count;
    float _imageWidth = 50f;

    RectTransform recttt;

    private void Awake()
    {
        recttt = GetComponent<RectTransform>();

        for (int i = 0; i < _lines.Length; i++)
        {
            _lines[i] = transform.GetChild(i).GetComponent<UILineRenderer>();
            _count += _lines[i]._points.Count;
            _imageWidth = _lines[0]._imageWidth;

            for (int j = 0; j < _lines[i]._points.Count; j++)
            {
                _pointzzz.Add(_lines[i]._points[j]);
            }
        }

        for (int j = 0; j < _count; j++)
        {
            _images.Add(new GameObject("Point", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image)));

            Transform trans = _images[j].transform;
            trans.SetParent(transform);
            trans.localScale = Vector3.one;
            trans.localRotation = Quaternion.Euler(0, 0, 0);

            RectTransform rect = _images[j].transform.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(_imageWidth, _imageWidth);
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0, 0);
            rect.pivot = new Vector2(0, 0);

        }
    }

    private void Start()
    {
        Invoke("Delay", .01f);
    }

    private void Delay()
    {
        for (int j = 0; j < _images.Count - 1; j++)
        {
            Vector2 point = _pointzzz[j];
            Vector2 point2 = _pointzzz[j + 1];

            _images[j].transform.localPosition = _lines[0].MoveImages(point) - new Vector3(recttt.sizeDelta.x/2, recttt.sizeDelta.y/2, 0);
            if (j + 1 == _lines[0]._points.Count - 1 || j + 1 == _lines[0]._points.Count*2 - 1)
            {
                _images[j + 1].transform.localPosition = _lines[0].MoveImages(point2) - new Vector3(recttt.sizeDelta.x/2,recttt.sizeDelta.y/2,0);
            }
        }

        RemoveDuplicatesFrom(_images);
    }

    private void RemoveDuplicatesFrom(List<GameObject> collection)
    {
        for (int i = 0; i < collection.Count; i++)
        {
            for (int j = i + 1; j < collection.Count; j++)
            {
                if (!Match(collection[i], collection[j]))
                    continue;

                Destroy(collection[j].gameObject);
                collection.RemoveAt(j);
                j--;
            }
        }
    }

    private bool Match(GameObject object1, GameObject object2)
    {
        return
            object1.transform.position.x == object2.transform.position.x &&
            object1.transform.position.y == object2.transform.position.y;
    }

    private void FixedUpdate()
    {
        /*        for (int i = 0; i < _images.Count -1; i++)
                {
                    Vector2 point = _parent._points[i];
                    Vector2 point2 = _parent._points[i + 1];

                    _images[i].transform.localPosition = _parent.MoveImages(point);
                    if (i + 1 == _parent._points.Count - 1)
                    {
                        _images[i + 1].transform.localPosition = _parent.MoveImages(point2);
                    }
                }*/
    }

}
