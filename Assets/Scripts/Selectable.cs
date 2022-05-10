using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour {
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _baseColor = Color.white;

    private Color selectedColor = Color.gray;

    private bool selected = false;
    public bool Selected {
        get { return selected; }
        set {
            selected = value;
            UpdateSprite();
        }
    }

    private void Start() {
        _spriteRenderer.color = _baseColor;
        selectedColor = new Color(_baseColor.r-0.1f, _baseColor.g-0.1f, _baseColor.b-0.1f);
    }

    private void OnMouseEnter() { Selected = true; }
    private void OnMouseExit() { Selected = false; }

    private void UpdateSprite() {
        _spriteRenderer.color = selected ? selectedColor : _baseColor;
    }

    public void UpdateColor(Color color) {
        _baseColor = color;
        selectedColor = new Color(_baseColor.r-0.1f, _baseColor.g-0.1f, _baseColor.b-0.1f);
        UpdateSprite();
    }
}
