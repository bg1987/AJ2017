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
        cb.normalColor = new Color(10, 10, 10, 200);

        btn.colors = cb;
    }

    void OnClick()
    {
        if (OnButtonClick != null)
            OnButtonClick(this);
    }
}
