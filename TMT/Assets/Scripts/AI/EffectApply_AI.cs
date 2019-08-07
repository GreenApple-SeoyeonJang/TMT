using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectApply_AI : MonoBehaviour
{
    public Vector2 Player1CoinProfileLocation;
    public Vector2 Player2CoinProfileLocation;
    public Vector2[] BoardCoinLocation = new Vector2[29];
    public GameObject CoinImageObject;
    public GameObject Canvas;
    public static bool MoneyEffectApplyingGoingOn;
    public static bool HealthEffectApplyingGoingOn;
    public GameObject Player1HealthPlus;
    public GameObject Player1HealthMinus;
    public GameObject Player2HealthPlus;
    public GameObject Player2HealthMinus;
    public AudioClip CoinSound;

    void Start()
    {
        Player1CoinProfileLocation = GameObject.Find("Player1CoinImage").transform.position;
        Player2CoinProfileLocation = GameObject.Find("Player2CoinImage").transform.position;
        MoneyEffectApplyingGoingOn = false;
        HealthEffectApplyingGoingOn = false;
        Player1HealthPlus.SetActive(false);
        Player1HealthMinus.SetActive(false);
        Player2HealthPlus.SetActive(false);
        Player2HealthMinus.SetActive(false);

        for (int i = 0; i < 29; i++)
        {
            BoardCoinLocation[i] = GameObject.Find("BoardCoinLocation (" + i.ToString() + ")").transform.position;
        }

    }

    public IEnumerator MoneyChangeApply(int Who, int MoneyAmount, int BoardLocation)
    {
        this.GetComponent<AudioSource>().clip = CoinSound;
        int MoneyStep;
        int MoneyTarget;
        Vector2 CoinBoardLocation;
        if (MoneyAmount > 0) //돈 얻기
        {
            if (Who == 1) //플레이어1이 얻는 경우
            {
                MoneyStep = MoneySystem_AI.Player1PossessMoney;
                MoneyTarget = MoneySystem_AI.Player1PossessMoney + MoneyAmount;
                CoinBoardLocation = BoardCoinLocation[BoardLocation];
                GameObject FirstCoin = Instantiate(CoinImageObject, CoinBoardLocation, Quaternion.identity, Canvas.transform);
                this.GetComponent<AudioSource>().Play();
                StartCoroutine(PlusCoinMove(Player1CoinProfileLocation, CoinBoardLocation, FirstCoin));
                while (MoneyStep != MoneyTarget)
                {
                    MoneyStep = MoneyStep + (MoneyAmount / 100);
                    if ((MoneyStep % 2) == 0) //조금씩 올라가는 걸 보여주기 위함
                    {
                        MoneySystem_AI.Player1PossessMoney = MoneyStep;
                    }
                    if ((MoneyStep % 7000) == 0)
                    {
                        GameObject Coin = Instantiate(CoinImageObject, CoinBoardLocation, Quaternion.identity, Canvas.transform);
                        this.GetComponent<AudioSource>().Play();
                        StartCoroutine(PlusCoinMove(Player1CoinProfileLocation, CoinBoardLocation, Coin));
                    }
                    yield return 0.0001f;
                }
                MoneySystem_AI.Player1PossessMoney = MoneyStep;
            }
            else //AI가 얻는 경우
            {
                MoneyStep = MoneySystem_AI.Player2PossessMoney;
                MoneyTarget = MoneySystem_AI.Player2PossessMoney + MoneyAmount;
                CoinBoardLocation = BoardCoinLocation[BoardLocation];
                GameObject FirstCoin = Instantiate(CoinImageObject, CoinBoardLocation, Quaternion.identity, Canvas.transform);
                this.GetComponent<AudioSource>().Play();
                StartCoroutine(PlusCoinMove(Player2CoinProfileLocation, CoinBoardLocation, FirstCoin));
                while (MoneyStep != MoneyTarget)
                {
                    MoneyStep = MoneyStep + (MoneyAmount / 100);
                    if ((MoneyStep % 2) == 0) //조금씩 올라가는 걸 보여주기 위함
                    {
                        MoneySystem_AI.Player2PossessMoney = MoneyStep;
                    }
                    if ((MoneyStep % 7000) == 0)
                    {
                        GameObject Coin = Instantiate(CoinImageObject, CoinBoardLocation, Quaternion.identity, Canvas.transform);
                        this.GetComponent<AudioSource>().Play();
                        StartCoroutine(PlusCoinMove(Player2CoinProfileLocation, CoinBoardLocation, Coin));
                    }
                    yield return 0.0001f;
                }
                MoneySystem_AI.Player2PossessMoney = MoneyStep;
            }
        }
        else if (MoneyAmount < 0) //돈 잃기
        {
            if (Who == 1) //플레이어1이 잃는 경우
            {
                MoneyStep = MoneySystem_AI.Player1PossessMoney;
                MoneyTarget = MoneySystem_AI.Player1PossessMoney + MoneyAmount; //잃는 경우 MoneyAmount는 마이너스
                CoinBoardLocation = BoardCoinLocation[BoardLocation];
                GameObject FirstCoin = Instantiate(CoinImageObject, Player1CoinProfileLocation, Quaternion.identity, Canvas.transform);
                this.GetComponent<AudioSource>().Play();
                StartCoroutine(MinusCoinMove(Player1CoinProfileLocation, CoinBoardLocation, FirstCoin));
                while (MoneyStep != MoneyTarget)
                {
                    MoneyStep = MoneyStep + (MoneyAmount / 100);
                    if ((MoneyStep % 2) == 0) //조금씩 내려가는 걸 보여주기 위함
                    {
                        MoneySystem_AI.Player1PossessMoney = MoneyStep;
                    }
                    if ((MoneyStep % 7000) == 0)
                    {
                        GameObject Coin = Instantiate(CoinImageObject, Player1CoinProfileLocation, Quaternion.identity, Canvas.transform);
                        this.GetComponent<AudioSource>().Play();
                        StartCoroutine(MinusCoinMove(Player1CoinProfileLocation, CoinBoardLocation, Coin));
                    }
                    yield return 0.0001f;
                }
                MoneySystem_AI.Player1PossessMoney = MoneyStep;
            }
            else //AI가 얻는 경우
            {
                MoneyStep = MoneySystem_AI.Player2PossessMoney;
                MoneyTarget = MoneySystem_AI.Player2PossessMoney + MoneyAmount; //잃는 경우 MoneyAmount는 마이너스
                CoinBoardLocation = BoardCoinLocation[BoardLocation];
                GameObject FirstCoin = Instantiate(CoinImageObject, Player2CoinProfileLocation, Quaternion.identity, Canvas.transform);
                this.GetComponent<AudioSource>().Play();
                StartCoroutine(MinusCoinMove(Player2CoinProfileLocation, CoinBoardLocation, FirstCoin));
                while (MoneyStep != MoneyTarget)
                {
                    MoneyStep = MoneyStep + (MoneyAmount / 100);
                    if ((MoneyStep % 2) == 0) //조금씩 내려가는 걸 보여주기 위함
                    {
                        MoneySystem_AI.Player2PossessMoney = MoneyStep;
                    }
                    if ((MoneyStep % 7000) == 0)
                    {
                        GameObject Coin = Instantiate(CoinImageObject, Player2CoinProfileLocation, Quaternion.identity, Canvas.transform);
                        this.GetComponent<AudioSource>().Play();
                        StartCoroutine(MinusCoinMove(Player2CoinProfileLocation, CoinBoardLocation, Coin));
                    }
                    yield return 0.0001f;
                }
                MoneySystem_AI.Player2PossessMoney = MoneyStep;
            }
        }
        yield return new WaitForSeconds(1.3f);
        MoneyEffectApplyingGoingOn = false;
    }

    IEnumerator PlusCoinMove(Vector2 PlayerProfileCoinLocation, Vector2 CoinBoardLocation, GameObject Coin)
    {
        float xdif = (PlayerProfileCoinLocation.x - CoinBoardLocation.x) / 20;
        float ydif = (PlayerProfileCoinLocation.y - CoinBoardLocation.y) / 20;

        for (int i = 0; i < 20; i++)
        {
            Coin.transform.position = new Vector2(Coin.transform.position.x + xdif, Coin.transform.position.y + ydif);
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(Coin);
    }

    IEnumerator MinusCoinMove(Vector2 PlayerProfileCoinLocation, Vector2 CoinBoardLocation, GameObject Coin)
    {
        float xdif = (CoinBoardLocation.x - PlayerProfileCoinLocation.x) / 20;
        float ydif = (CoinBoardLocation.y - PlayerProfileCoinLocation.y) / 20;

        for (int i = 0; i < 20; i++)
        {
            Coin.transform.position = new Vector2(Coin.transform.position.x + xdif, Coin.transform.position.y + ydif);
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(Coin);
    }

    public IEnumerator HealthChangeApply(int Who, int HealthAmount)
    {
        int HealthStep;
        int HealthTarget;
        if (HealthAmount > 0)
        {
            if (Who == 1) // 플레이어1이 체력 얻는 경우
            {
                HealthStep = TirednessSystem_AI.Player1Health;
                HealthTarget = TirednessSystem_AI.Player1Health + HealthAmount;
                while (HealthStep != HealthTarget)
                {
                    HealthStep = HealthStep + 5;
                    TirednessSystem_AI.Player1Health = HealthStep;
                    if ((HealthStep % 40) == 0)
                        Player1HealthPlus.SetActive(true);
                    else if ((HealthStep % 20) == 0)
                        Player1HealthPlus.SetActive(false);
                    yield return new WaitForSeconds(0.1f);
                }
                Player1HealthPlus.SetActive(false);
                TirednessSystem_AI.Player1Health = HealthStep;
            }
            else// 플레이어2가 체력 얻는 경우
            {
                HealthStep = TirednessSystem_AI.Player2Health;
                HealthTarget = TirednessSystem_AI.Player2Health + HealthAmount;
                while (HealthStep != HealthTarget)
                {
                    HealthStep = HealthStep + 5;
                    TirednessSystem_AI.Player2Health = HealthStep;
                    if ((HealthStep % 40) == 0)
                        Player2HealthPlus.SetActive(true);
                    else if ((HealthStep % 20) == 0)
                        Player2HealthPlus.SetActive(false);
                    yield return new WaitForSeconds(0.1f);
                }
                Player2HealthPlus.SetActive(false);
                TirednessSystem_AI.Player2Health = HealthStep;
            }
        }
        else if (HealthAmount < 0)
        {
            if (Who == 1) // 플레이어1이 체력 잃는 경우
            {
                HealthStep = TirednessSystem_AI.Player1Health;
                HealthTarget = TirednessSystem_AI.Player1Health + HealthAmount; //HealthAmount는 마이너스 값
                while (HealthStep != HealthTarget)
                {
                    HealthStep = HealthStep - 5;
                    TirednessSystem_AI.Player1Health = HealthStep;
                    if ((HealthStep % 40) == 0)
                        Player1HealthMinus.SetActive(true);
                    else if ((HealthStep % 20) == 0)
                        Player1HealthMinus.SetActive(false);
                    yield return new WaitForSeconds(0.1f);
                }
                Player1HealthMinus.SetActive(false);
                TirednessSystem_AI.Player1Health = HealthStep;
            }
            else// 플레이어2가 체력 잃는 경우
            {
                HealthStep = TirednessSystem_AI.Player2Health;
                HealthTarget = TirednessSystem_AI.Player2Health + HealthAmount; //HealthAmount는 마이너스 값
                while (HealthStep != HealthTarget)
                {
                    HealthStep = HealthStep - 5;
                    TirednessSystem_AI.Player2Health = HealthStep;
                    if ((HealthStep % 40) == 0)
                        Player2HealthMinus.SetActive(true);
                    else if ((HealthStep % 20) == 0)
                        Player2HealthMinus.SetActive(false);
                    yield return new WaitForSeconds(0.1f);
                }
                Player2HealthMinus.SetActive(false);
                TirednessSystem_AI.Player2Health = HealthStep;
            }
        }
        HealthEffectApplyingGoingOn = false;
    }
}
