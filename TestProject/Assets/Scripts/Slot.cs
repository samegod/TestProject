using UnityEngine;

public class Slot : MonoBehaviour
{
    public int id;
    public int cost;
    public string Name;
    public GameObject Pac;
    public GameObject BuyButton;
    public GameObject UseButton;
    public GameObject IsUsed;
    public GameObject Coin;
    public GameObject Cost;
    public GameObject Galka;

    // Start is called before the first frame update
    void Start()
    {
        if (id == 3)
        {
            PlayerPrefs.SetString(Name, "Closed");
        }

        if (PlayerPrefs.GetString(Name) == "Open" || id == 0)
        {
            UseButton.SetActive(true);
            BuyButton.SetActive(false);
            IsUsed.SetActive(false);
            Coin.SetActive(false);
            Cost.SetActive(false);
            Galka.SetActive(true);
        }else
        {
            UseButton.SetActive(false);
            BuyButton.SetActive(true);
            IsUsed.SetActive(false);
            Coin.SetActive(true);
            Cost.SetActive(true);
            Galka.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Char") == id)
        {
            UseButton.SetActive(false);
            IsUsed.SetActive(true);
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("Char") != id && (PlayerPrefs.GetString(Name) == "Open" || id == 0))
            UnUse();
    }

    private void UnUse()
    {
        UseButton.SetActive(true);
        IsUsed.SetActive(false);

    }

    public void Use()
    {
        Debug.Log("USE!");
        PlayerPrefs.SetInt("Char", id);
        UseButton.SetActive(false);
        IsUsed.SetActive(true);
    }

    public void Buy()
    {
        if (PlayerPrefs.GetInt("Coins") >= cost)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - cost);
            PlayerPrefs.SetString(Name, "Open");

            UseButton.SetActive(true);
            BuyButton.SetActive(false);
            IsUsed.SetActive(false);
            Coin.SetActive(false);
            Cost.SetActive(false);
            Galka.SetActive(true);
        }
    }
}
