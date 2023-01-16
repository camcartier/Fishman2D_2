using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;

public class UpdateTexte : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _label;

    [SerializeField]
    private IntVariables _beerCount;




    // Update is called once per frame
    void Update()
    {

        _label.text = _beerCount.n_value.ToString();

    }
}
