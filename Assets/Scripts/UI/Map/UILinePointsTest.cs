using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UILinePointsTest : MonoBehaviour
{

    [SerializeField] GameSM _SMRef;

    // Array of Line Objects
    [SerializeField] UILineRenderer[] _lines;

    // List of Point Objects
    List<GameObject> _images = new List<GameObject>();
    public List<GameObject> Images => _images;

    // List of point object locations for ALL Line Objects
    List<Vector2> _localPoints = new List<Vector2>();

    List<int> _previousPoints = new List<int>();

    int _totalPointCount;
    float _imageWidth = 50f;

    RectTransform _rectTransRef;

    [SerializeField] AudioClip _buttonClickSFX;
    [SerializeField] AudioClip _buttonHoverSFX;

    private void Awake()
    {
        _rectTransRef = GetComponent<RectTransform>();

        // For every line object...
        for (int i = 0; i < _lines.Length; i++)
        {
            // Set References
            _lines[i] = transform.GetChild(i + 1).GetComponent<UILineRenderer>();
            // Add to total point count from each line's point count
            _totalPointCount += _lines[i]._points.Count;
            // All width values should be the same, so grab from whichever
            _imageWidth = _lines[0]._imageWidth;

            // Add every Vector2 from each line object to the Vector2 List
            for (int j = 0; j < _lines[i]._points.Count; j++)
            {
                _localPoints.Add(_lines[i]._points[j]);
            }
        }

        // Create and set default values for every point needed -- Also, add each gameobject to the _images List
        for (int j = 0; j < _totalPointCount; j++)
        {
            _images.Add(new GameObject("Point" + j + 1, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button), typeof(EventTrigger)));

            Transform trans = _images[j].transform;
            trans.SetParent(transform);
            trans.localScale = Vector3.one;
            trans.localRotation = Quaternion.Euler(0, 0, 0);

            RectTransform rect = _images[j].transform.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(_imageWidth, _imageWidth);
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0, 0);
            rect.pivot = new Vector2(0, 0);

            // Adds button, changes pressed color, and adds a listener
            Button button = _images[j].transform.GetComponent<Button>();
            ColorBlock cb = button.colors;
            cb.pressedColor = Color.cyan;
            button.colors = cb;

            // Lambda expression to inline a method with parameters
            button.onClick.AddListener(() => ButtonClick(button));
            button.interactable = false;
        }
    }

    void ButtonClick(Button button)
    {
        //Debug.Log(button.name);

        // Later on, set which event pool to choose from here
        AudioManager.Instance.PlaySound2D(_buttonClickSFX, .5f, UnityEngine.Random.Range(.8f, 1.2f));

        _SMRef.OnStateExit();
        _SMRef.ChangeState<EventChoiceState>();
        EnableDisableButtons();
    }

    void OnPointerEnterDelegate(PointerEventData data)
    {
        AudioManager.Instance.PlaySound2D(_buttonHoverSFX, .25f);
    }

    // Gives the LineRenderer script time to setup
    private void Start()
    {
        Invoke("Delay", .025f);
    }

    private void Delay()
    {
        // For each game object created, set the transform values to the vertex points of the line
        // This is done through the MoveImages method -- Have to subtract half of the rect transform's size becuase of the way the achors are set up
        int i = 1;
        for (int j = 0; j < _images.Count - 1; j++)
        {
            Vector2 point = _localPoints[j];
            Vector2 point2 = _localPoints[j + 1];

            _images[j].transform.localPosition = _lines[0].MoveImages(point) - new Vector3(_rectTransRef.sizeDelta.x/2, _rectTransRef.sizeDelta.y/2, 0);

            // This catches the final points in each line's list of points
            if (j + 1 == _lines[0]._points.Count*i - 1)
            {
                _images[j + 1].transform.localPosition = _lines[0].MoveImages(point2) - new Vector3(_rectTransRef.sizeDelta.x/2,_rectTransRef.sizeDelta.y/2,0);
                i++;
            }
        }

        // Remove duplicate images
        RemoveDuplicatesFrom(_images,_localPoints);
    }

    // Checking lists for duplicates
    private void RemoveDuplicatesFrom(List<GameObject> collection, List<Vector2> points)
    {
        // Removes point object duplicates
        for (int i = 0; i < collection.Count; i++)
        {
            for (int j = i + 1; j < collection.Count; j++)
            {
                // If duplicate is found, destroy and remove from list
                if (!Match(collection[i], collection[j]))
                    continue;

                Destroy(collection[j].gameObject);
                collection.RemoveAt(j);
                j--;
            }
        }

        // Removes vector2 position duplicates
        for (int i = 0; i < points.Count; i++)
        {
            for (int j = i + 1; j < points.Count; j++)
            {
                // If duplicate is found, remove from list
                if (!(points[i].x == points[j].x && points[i].y == points[j].y))
                    continue;

                points.RemoveAt(j);
                j--;
            }
        }

        // Renames so numbers are in order
        for (int i = 0; i < collection.Count; i++)
        {
            for (int j = i; j < collection.Count; j++)
            {
                collection[j].name = "Point" + (j).ToString();
            }
        }

        EnableDisableButtons();
    }

    // Test to see if transforms match
    private bool Match(GameObject object1, GameObject object2)
    {
        return object1.transform.position.x == object2.transform.position.x && object1.transform.position.y == object2.transform.position.y;
    }

    void EnableDisableButtons()
    {
        foreach (int index in _previousPoints)
        {
            _images[index].GetComponent<Button>().interactable = false;
            _images.RemoveAt(index);
            _localPoints.RemoveAt(index);
        }

        _previousPoints.Clear();

        if (_localPoints.Count > 0)
        {
            for (int i = 1; i < _localPoints.Count; i++)
            {
                if (_localPoints[0].x == _localPoints[i].x)
                {
                    _images[i].GetComponent<Button>().interactable = true;
                    _previousPoints.Add(i);

                    EventTrigger et = _images[i].transform.GetComponent<EventTrigger>();
                    EventTrigger.Entry entry = new EventTrigger.Entry();
                    entry.eventID = EventTriggerType.PointerEnter;
                    entry.callback.AddListener((data) => { OnPointerEnterDelegate((PointerEventData)data); });
                    et.triggers.Add(entry);
                }
            }

            EventTrigger et2 = _images[0].transform.GetComponent<EventTrigger>();
            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.PointerEnter;
            entry2.callback.AddListener((data) => { OnPointerEnterDelegate((PointerEventData)data); });
            et2.triggers.Add(entry2);

            _images[0].GetComponent<Button>().interactable = true;
            _previousPoints.Add(0);
        }
    }

}
