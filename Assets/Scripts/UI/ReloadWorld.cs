using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadWorld : MonoBehaviour {

	public void Reload() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
