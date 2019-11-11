using System.Collections.Generic;
using System;
using UnityEngine;

public struct Contacts
{
    public List<Contact> contactList;
    public Contacts(List<Contact> contacts)
    {
        this.contactList = contacts;
    }

}

[System.Serializable]
public struct Contact : IComparable<Contact>
{
    public string name;
    public string phoneNumber;
    public string email;
    public Sprite sp;

    public int CompareTo(Contact other)
    {
        return this.name.CompareTo(other.name);
    }
}
