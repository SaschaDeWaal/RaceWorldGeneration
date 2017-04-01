using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

    public Text text;

	public void Set(string name) {
        transform.localScale = Vector3.one;
        text.text = text.text.Replace("[winner]", name);
    }
}
