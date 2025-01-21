using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed; // Viteza normală
    public float boostMultiplier = 2f; // Factorul de multiplicare a vitezei pentru boost
    public float boostDuration = 5f; // Durata boost-ului în secunde
    private bool isBoostActive = false; // Indică dacă boost-ul este activ
    private float originalSpeed; // Salvează viteza normală pentru resetare

    Rigidbody playerRigidbody;
    public float m_Thrust = 20f; // Variabilă pentru săritură

    public TMP_Text textAfisat;
    private string litereColectate;

    public Button BtnMeniu;
    public Button BtnReload;

    // Timer variables
    public float timeRemaining = 30f; // Timp total de joc
    public TMP_Text timerText; // Text UI pentru afișarea timpului
    private bool isGameOver = false;

    void Start()
    {
        textAfisat.text = "Collected: ";
        litereColectate = "";

        // Setare inactivitate butoane la început de joc
        BtnMeniu.gameObject.SetActive(false);
        BtnReload.gameObject.SetActive(false);

        playerRigidbody = GetComponent<Rigidbody>();
        originalSpeed = speed; // Salvează viteza normală
    }

    private void FixedUpdate()
    {
        if (isGameOver) return;

        // Mișcare jucător
        float movementHorizontal = Input.GetAxis("Horizontal");
        float movementVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(movementHorizontal, 0.0f, movementVertical);
        playerRigidbody.AddForce(movement * speed * Time.deltaTime);

        // Salt
        if (Input.GetButton("Jump"))
        {
            playerRigidbody.AddForce(transform.up * m_Thrust);
        }

        // Timer logic
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.RoundToInt(timeRemaining);
        }
        else
        {
            isGameOver = true;
            TimerEnded();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGameOver) return;

        if (other.gameObject.tag == "LiteraColectabila")
        {
            other.gameObject.SetActive(false);
            Debug.Log("Am colectat litera.");

            // Asignăm textului UI literele colectate
            litereColectate = litereColectate + other.gameObject.GetComponent<TextMeshPro>().text;
            textAfisat.text = "Collected: " + litereColectate;
        }
        else if (other.gameObject.tag == "Boost")
        {
            other.gameObject.SetActive(false);
            Debug.Log("Boost colectat!");
            ActivateBoost();
        }
        else if (other.gameObject.tag == "LiteraPierdere") // Condiția pentru literă care determină pierderea
        {
            Debug.Log("Ai colectat o literă interzisă! Ai pierdut!");
            textAfisat.text = "You lost!";
            isGameOver = true;
            BtnReload.gameObject.SetActive(true);
            BtnReload.GetComponentInChildren<Text>().text = "Retry!";
            return; // Ieșim din funcție deoarece jocul s-a terminat
        }

        Debug.Log("nr letters= " + litereColectate.Length);

        if (litereColectate.Equals("ETTI"))
        {
            BtnMeniu.gameObject.SetActive(true);
            BtnReload.GetComponentInChildren<Text>().text = "Go to menu!";
            isGameOver = true;
        }
        else if (litereColectate.Length >= 4)
        {
            BtnReload.gameObject.SetActive(true);
            BtnReload.GetComponentInChildren<Text>().text = "Retry!";
            isGameOver = true;
        }
    }

    private void TimerEnded()
    {
        Debug.Log("Timp expirat!");
        timerText.text = "Time: 0"; // Setăm timpul la 0 în UI
        textAfisat.text = "Time's up!";

        // Activăm butonul de reload
        BtnReload.gameObject.SetActive(true);
        BtnReload.GetComponentInChildren<Text>().text = "Try Again!";
    }

    private void ActivateBoost()
    {
        if (!isBoostActive) // Dacă boost-ul nu este deja activ
        {
            isBoostActive = true;
            speed *= boostMultiplier; // Crește viteza
            StartCoroutine(BoostCountdown()); // Pornește timer-ul pentru boost
        }
    }

    private IEnumerator BoostCountdown()
    {
        yield return new WaitForSeconds(boostDuration); // Așteaptă durata boost-ului
        speed = originalSpeed; // Revine la viteza normală
        isBoostActive = false;
        Debug.Log("Boost terminat.");
    }
}