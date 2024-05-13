using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortfolioItemCarousel : MonoBehaviour
{
    private List<GameObject> items;
    private int currentItem;
    private Vector3 currentItemLocation;
    private Vector3 nextItemLocation;
    private Vector3 previousItemLocation;
    private Vector3 behindCurrentItemLocation;

    public void OnEnable()
    {
        items = new List<GameObject>();
        currentItem = 0;
    }

    public void addItem(GameObject item)
    {
        switch (items.Count)
        {
            case 0:
                item.transform.position = currentItemLocation; break;
            case 1:
                item.transform.position = nextItemLocation; break;
            case 2:
                item.transform.position = previousItemLocation; break;
            default: item.transform.position = behindCurrentItemLocation; break;
        }

        items.Add(item);
    }

    public GameObject Item
    {
        get
        {
            return items[currentItem];
        }
    }

    public void startRotateLeft()
    {
        Next();
        StartCoroutine(Rotate());
    }

    public void startRotateRight()
    {
        Prev();
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        while (Item.transform.position != currentItemLocation)
        {
            Item.transform.position = Vector3.MoveTowards(Item.transform.position, currentItemLocation, 8 * Time.deltaTime);
            NextItem.transform.position = Vector3.MoveTowards(NextItem.transform.position, nextItemLocation, 8 * Time.deltaTime);
            PreviousItem.transform.position = Vector3.MoveTowards(PreviousItem.transform.position, previousItemLocation, 8 * Time.deltaTime);
            foreach (var i in items)
            {
                if (i != Item && i != NextItem && i != PreviousItem)
                {
                    i.transform.position = Vector3.MoveTowards(i.transform.position, behindCurrentItemLocation, 8 * Time.deltaTime);
                }
            }
            yield return null;
        }
    }

    public GameObject PreviousItem
    {
        get
        {
            if (currentItem == 0)
            {
                return items[items.Count - 1];
            }
            else
            {
                return items[currentItem - 1];
            }
        }
    }

    public void Next()
    {
        currentItem++;
        if (currentItem == items.Count)
        {
            currentItem = 0;
        }
    }

    public void Prev()
    {
        currentItem--;
        if (currentItem == -1)
        {
            currentItem = items.Count - 1;
        }
    }
    public Vector3 Location
    {
        set
        {
            currentItemLocation = value;
            nextItemLocation = new Vector3(value.x < 0 ? value.x - 15 : value.x + 15, value.y, value.x < 0 ? value.z - 10 : value.z + 10);
            previousItemLocation = new Vector3(value.x < 0 ? value.x - 15 : value.x + 15, value.y, value.x < 0 ? value.z + 10 : value.z - 10);
            behindCurrentItemLocation = new Vector3(value.x < 0 ? value.x - 30 : value.x + 30, value.y, value.z);
        }
    }

    public Vector3 CurrentLocation
    {
        get
        {
            return currentItemLocation;
        }
    }
    public Vector3 NextLocation
    {
        get { return nextItemLocation; }
    }
    public Vector3 PrevLocation
    {
        get { return previousItemLocation; }
    }
    public Vector3 BehindCurrentLocation
    {
        get { return behindCurrentItemLocation; }
    }
    public GameObject NextItem
    {
        get
        {
            if (currentItem == items.Count - 1)
            {
                return items[0];
            }
            else
            {
                return items[currentItem + 1];
            }
        }
    }
}
