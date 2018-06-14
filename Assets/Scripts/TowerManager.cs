using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Singleton<TowerManager>
{
    private Tower newTower;
    private SpriteRenderer sprite;
    private SpriteRenderer spriteFromButton;
    private List<Tower> towerList;

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
        towerList = new List<Tower>();
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
                sprite.enabled = false;
                newTower = null;
                colliderPoint.collider.tag = "Buildsitefull";
            }
        }
    }

    public void fromPressedButton(Tower selectedTower)
    {
        newTower = selectedTower;
        sprite.enabled = true;
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
