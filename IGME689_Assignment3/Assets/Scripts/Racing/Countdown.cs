using UnityEngine;
using TMPro;
using System.Collections;

public class Countdown : MonoBehaviour
{
    [SerializeField] TMP_Text countdownText; 
    [SerializeField] float countdownTime = 3f;
    [SerializeField] CarController playerCar;
    [SerializeField] AICarController[] AICars;

    void Start()
    {
        playerCar.enabled = false;
        foreach(AICarController AICar in AICars)
        {
            AICar.enabled = false;
        }

        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        float timeLeft = countdownTime;

        while (timeLeft > 0)
        {
            countdownText.text = Mathf.CeilToInt(timeLeft).ToString();
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownText.text = "";

        playerCar.enabled = true;
        foreach (AICarController aiCar in AICars)
        {
            aiCar.enabled = true;
        }
    }
}
