
using UnityEngine;
using UnityEngine.UI;

public class DiceNumberTextScript : MonoBehaviour
{
    Text text;

    public static int diceNumber1;
    public static int diceNumber2;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int sum = diceNumber1 + diceNumber2;
        text.text = sum.ToString();
    }
}
