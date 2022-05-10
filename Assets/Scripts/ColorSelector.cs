using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelector : MonoBehaviour {
    
    [SerializeField] private Image _image;
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private MenuBarController _menuBar;

    public void SetColor() {
        _gridManager.ColorTheme = _image.color;
        _menuBar.UpdateColor(_image.color);
        _menuBar.ToggleEnabled();
    }
}
