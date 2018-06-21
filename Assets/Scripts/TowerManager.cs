using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Singleton<TowerManager>
{

    private Tower newTower;
    private SpriteRenderer sprite;
    private SpriteRenderer spriteFromButton;
    private List<Tower> towerList = new List<Tower>();
    public List<Collider2D> buildList = new List<Collider2D>();
    private Collider2D buildTile;

    public List<Tower> TowerList
    {
        get
        {
            return towerList;
        }
    }


    // Use this for initialization
    void Start()
    {
        buildTile = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (sprite.enabled)
        {
            spriteFromButton = newTower.GetComponent<SpriteRenderer>();
            DragSprite(spriteFromButton.sprite);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cancelButtonPress();
        }

        if (newTower != null && Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            placeTower(clickPosition, newTower);
        }
    }

    public void registerBuildSite(Collider2D buildTag){
        buildList.Add(buildTag);
    }

    public void destroyAllTowers(){
        foreach (Tower tower in towerList)
        {
            Destroy(tower.gameObject);
        }
        TowerList.Clear();
    }

    public void renameTagsBildSites()
    {
        foreach (Collider2D buildTag in buildList)
        {
            buildTag.tag = "Buildsite";
        }
        buildList.Clear();
    }

    public void placeTower(Vector2 position, Tower selectedTower)
    {
        RaycastHit2D colliderPoint = Physics2D.Raycast(position, Vector2.zero);
        if (colliderPoint)
        {
            if (colliderPoint.collider.tag == "Buildsite" && colliderPoint)
            {
                Tower tower = Instantiate(selectedTower) as Tower;
                RegisterTower(tower);
                tower.transform.position = position;
                buyTower(selectedTower.TowerPrice);
                GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.TowerBuilt);
                sprite.enabled = false;
                newTower = null;
                colliderPoint.collider.tag = "Buildsitefull";
                registerBuildSite(colliderPoint.collider);

            }
        }
    }

    public void buyTower(int price){
        GameManager.Instance.subtractMoney(price);
    }

    public void fromPressedButton(Tower selectedTower)
    {
        Debug.Log("Price: " + selectedTower.TowerPrice);
        if (selectedTower.TowerPrice <= GameManager.Instance.TotalMoney)
        {
            
        newTower = selectedTower;
        sprite.enabled = true;
        }
    }

    private void DragSprite(Sprite spriteToDrag)
    {
        sprite.sprite = spriteToDrag;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    private void cancelButtonPress()
    {
        sprite.enabled = false;
        newTower = null;
    }

    private void RegisterTower(Tower tower)
    {
        towerList.Add(tower);
    }

    private void UnregisterTower(Tower tower)
    {
        towerList.Remove(tower);
    }


}
