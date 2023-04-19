using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadUI : MonoBehaviour
{
    public Text myText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartDisableText(float delay)
    {
        StartCoroutine(DisableTextCorountine(delay));
    }

    private IEnumerator DisableTextCorountine(float delay)
    {
        yield return new WaitForSeconds(delay);
        myText.gameObject.SetActive(false);
    }
    public void EnableText()
    {
        myText.gameObject.SetActive(true);
    }
}
