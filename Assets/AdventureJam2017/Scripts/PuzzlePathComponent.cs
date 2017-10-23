using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePathComponent : MonoBehaviour {

    public Action<PuzzlePathComponent> OnButtonClick;

    public List<PuzzlePathComponent> connectedTo;

    private void Awake()
    {
        var btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (OnButtonClick != null)
            OnButtonClick(this);
    }
}
