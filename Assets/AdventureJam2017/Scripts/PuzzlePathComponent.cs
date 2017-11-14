using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePathComponent : MonoBehaviour {

    public Action<PuzzlePathComponent> OnButtonClick;

    public List<PuzzlePathComponent> connectedTo;

    [Header("position in the puzzle")]
    public int xpos;
    public int ypos;

    private void Awake()
    {
        var btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);

        ColorBlock cb = new ColorBlock();
		cb.normalColor = new Color(0.9f, 0.9f, 0.9f, 0.5f);
		cb.pressedColor = cb.normalColor;
		cb.disabledColor = cb.normalColor;
		cb.highlightedColor = cb.normalColor;

        btn.colors = cb;
    }

    void OnClick()
    {
		Debug.Log(string.Format("Clicked on {0}, pos {1},{2}", gameObject.name, xpos, ypos));

        if (OnButtonClick != null)
            OnButtonClick(this);
    }
}
