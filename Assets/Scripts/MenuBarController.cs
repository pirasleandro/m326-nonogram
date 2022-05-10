using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBarController : MonoBehaviour {

    [SerializeField] private GameObject _menuBar;
    [SerializeField] private Image _image;
    private bool barEnabled = false;

    private void Start() {
        _menuBar.SetActive(barEnabled);
    }
    
    public void ToggleEnabled() {
        barEnabled = !barEnabled;
        _menuBar.SetActive(barEnabled);
    }

    public void UpdateColor(Color color) {
        _image.color = color;
    }
}
