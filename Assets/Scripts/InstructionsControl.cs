using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionsControl : MonoBehaviour
{
    private TextMeshProUGUI _instructions;

    private void Awake()
    {
        _instructions = gameObject.GetComponentInParent<TextMeshProUGUI>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        _instructions.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    }
}
