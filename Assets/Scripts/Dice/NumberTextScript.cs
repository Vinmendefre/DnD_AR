
using UnityEngine;
using UnityEngine.UI;

public class NumberTextScript : MonoBehaviour
{
    Text text;

    public static int diceNumber;
 

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = diceNumber.ToString();
    }
}
