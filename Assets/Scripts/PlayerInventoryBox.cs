using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryBox : InventoryBox
{
    public (int id, int quantity)[] items = new (int id, int quantity)[9];



    public override bool PeekItem(int index, out int itemid, out int itemquantity)
    {
        itemid = -1;
        itemquantity = -1;

        // indexが範囲外の場合returnする
        if (items.Length < index || 0 > index) return false;

        itemid = items[index].id;
        itemquantity = items[index].quantity;
        return true;
    }

    public override bool GetItem(int index, out int itemid)
    {
        itemid = -1;

        // indexが範囲外の場合returnする
        if (items.Length < index || 0 > index) return false;

        // quantityが0以下の場合returnする
        if (items[index].quantity <= 0) return false;

        // quantityを1減らしてreturnする
        items[index].quantity--;
        itemid = items[index].id;

        if (items[index].quantity == 0) items[index].id = -1;

        return true;
    }

    public override bool AddItem(int index, int itemid)
    {
        // indexが範囲外の場合returnする
        if (items.Length < index || 0 > index) return false;

        // id が一致しない場合returnする
        if (items[index].quantity > 0 && items[index].id != itemid) return false;

        items[index].quantity++;
        items[index].id = itemid;
        return true;
    }
}