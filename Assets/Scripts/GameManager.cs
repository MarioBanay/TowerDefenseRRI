using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum gameStatus {
    next, play, gameover, win
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private int totalWaves;
    [SerializeField]
    private Text totalMoneyLbl;
    [SerializeField]
    private Text currentWaveLbl;
    [SerializeField]
    private Text totalEscapedLbl;
    [SerializeField]
    private Text playBtnLbl;
    [SerializeField]
    private Button playBtn;
    [SerializeField]
    private Enemy[] enemyType;

    private List<Enemy> enemyList = new List<Enemy>();
    [SerializeField]
    private int waveNumber;
    [SerializeField]
    private int totalEnemies;
    [SerializeField]
    private int totalMoney;
    private int totalEscaped = 0;
    private int roundEscaped = 0;
    private int whichEnemiesToSpawn = 0;
    private gameStatus currentState = gameStatus.play;
    private int totalKilled = 0;
    private AudioSource audioSource;
    private const float TIME_BETWEEN_SPAWN = 2f;

    public gameStatus CurrentState{
        get{
            return currentState;
        }
    }
    
    public int TotalEscaped{
        get{
            return totalEscaped;
        }
        set {
            totalEscaped = value;
        }
    }
    public int RoundEscaped{
        get{
            return roundEscaped;
        }
        set {
            roundEscaped = value;
        }
    }
    public int TotalKilled{
        get {
            return totalKilled;
        }
        set {
            totalKilled = value;
        }
    }
    public List<Enemy> EnemyList
    {
        get
        {
            return enemyList;
        }
    }
    public int TotalMoney{
        get{
            return totalMoney;
        }
        set{
            totalMoney = value;
            totalMoneyLbl.text = totalMoney.ToString();
        }
    }

    public AudioSource AudioSource{
        get{
            return audioSource;
        }
    }



    // Use this for initialization
    void Start()
    {
        playBtn.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        showMenu();
        
    }

    private void showMenu()
    {
        switch (currentState)
        {
            case gameStatus.gameover:
                playBtnLbl.text = "Game over! Play Again?";
                AudioSource.PlayOneShot(SoundManager.Instance.Gameover);
                break;
            case gameStatus.next:
                playBtnLbl.text = "Next level";
                break;
            case gameStatus.play:
                playBtnLbl.text = "Play";
                break;
            case gameStatus.win:
                playBtnLbl.text = "You crushed it! Play again?";
                break;
        }
        playBtn.gameObject.SetActive(true);
    }

    public void playBtnPressed(){
        switch (currentState)
        {
            case gameStatus.next:
                waveNumber += 1;               
                break;
            default:
                waveNumber = 1;
            TowerManager.Instance.destroyAllTowers();
            TowerManager.Instance.renameTagsBildSites();
            TotalEscaped = 0;
            TotalMoney = 15;
            totalMoneyLbl.text = TotalMoney.ToString();
            totalEscapedLbl.text = "Escaped " + TotalEscaped + "/" + totalEnemies;
            audioSource.PlayOneShot(SoundManager.Instance.Newgame);
            break;
        }
        DestroyAllEnemies();
        TotalKilled = 0;
        RoundEscaped = 0;
        
        currentWaveLbl.text = "Level " + waveNumber.ToString();
        StartCoroutine(Spawn());
        playBtn.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Instanciranje enemy-a svakih TIME_BETWEEN_SPAWN sekundi
    IEnumerator Spawn()
    {
        for (int i = 0; i < totalEnemies; i++)
        {
            Enemy newEnemy = Instantiate(enemyType[waveNumber-1]) as Enemy;
            yield return new WaitForSeconds(TIME_BETWEEN_SPAWN);
        }
    }

    // dodaje enemy u listu
    public void RegisterEnemy (Enemy enemy)
    {
        enemyList.Add(enemy);
    }

    // briše enemy iz liste
    public void UnregisterEnemy (Enemy enemy)
    {
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
        TotalEscaped += 1;
    }
    private void DestroyAllEnemies()
    {
        foreach (var enemy in enemyList)
        {
            Destroy(enemy.gameObject);
        }
        EnemyList.Clear();
    }

    public void addMoney (int amount){
        TotalMoney += amount;
    }

    public void subtractMoney (int amount){
        TotalMoney -= amount;
    }

    public void isWaveOver(){
        totalEscapedLbl.text = "Escaped " + TotalEscaped + "/" + totalEnemies;
        if ((RoundEscaped + TotalKilled) == totalEnemies)
        {
            
            setCurrentGameState();
            showMenu();
        }
    }

    private void setCurrentGameState()
    {
        if (TotalEscaped >= totalEnemies)
        {
            currentState = gameStatus.gameover;
        } else if (waveNumber == 1 && (TotalKilled + RoundEscaped == 0))
        {
            currentState = gameStatus.play;
        } else if (waveNumber == totalWaves)
        {
            currentState = gameStatus.win;
            Debug.Log("check");
        } else {
            currentState = gameStatus.next;
        }
    }
}
