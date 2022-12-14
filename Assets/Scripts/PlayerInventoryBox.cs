using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInventoryBox : InventoryBox
{
    public InventoryTile[] items = new InventoryTile[9];
    public Action onChange;

    public override bool PeekItem(int index, out int itemid, out int itemquantity)
    {
        itemid = 0;
        itemquantity = 0;

        // indexが範囲外の場合returnする
        if (items.Length < index || 0 > index) return false;

        itemid = items[index].id;
        itemquantity = items[index].quantity;
        return true;
    }

    public override bool GetItem(int index, out int itemid)
    {
        itemid = 0;

        // indexが範囲外の場合returnする
        if (items.Length < index || 0 > index) return false;

        // quantityが0以下の場合returnする
        if (items[index].quantity <= 0) return false;

        // quantityを1減らしてreturnする
        items[index].quantity--;
        itemid = items[index].id;

        if (items[index].quantity == 0) items[index].id = 0;

        onChange();
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

        onChange();
        return true;
    }

    public override void Writer(DataWriter writer)
    {
        writer.Put(items.Length);
        for (int i = 0; i < items.Length; i++)
        {
            items[i].Writer(writer);
        }

        onChange();
    }

    public override void Reader(DataReader reader)
    {
        int count = reader.GetInt();
        for (int i = 0; i < count; i++)
        {
            items[i].Reader(reader);
        }

        onChange();
    }
}
