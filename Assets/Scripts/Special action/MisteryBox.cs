using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisteryBox : MonoBehaviour {

    private const float RESPAWN_TIME = 2;
    private bool taken = false;

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "car" && !taken) {
            int enumLenght               = SpecialActions.GetValues(typeof(SpecialActions)).Length;
            SpecialActions randomSpecial = (SpecialActions)Random.Range(1, enumLenght);
            taken                        = true;

            GetComponent<AudioSource>().Play();
            other.gameObject.GetComponent<SpecialCarry>().AddSpecial(randomSpecial);
            StartCoroutine(Respawn(RESPAWN_TIME));
        }
    }

    private IEnumerator Respawn(float time) {
        transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        transform.GetChild(0).gameObject.SetActive(true);
        taken = false;
    }
}
