using Godot;
using System;


public partial class ItemData : Resource
{
[Export]
public string ItemName;

[Export(PropertyHint.MultilineText)]
public string ItemDescription;

[Export]
public Texture2D ItemTextureInGame;

[Export]

public Texture2D ItemTextureInInventory;

}
