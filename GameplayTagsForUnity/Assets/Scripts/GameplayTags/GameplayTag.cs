using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace werignac.GameplayTags
{
	/// <summary>
	/// A flag used to categorize different gameplay actions, objects, or effects.
	/// Based in the Unreal GameplayTag.
	/// The difference between GameplayTags and other types of descriptors (Enums, Unity Tags)
	/// is that GameplayTags are heirarchical. For example you could have a GameplayTag heirarchy
	/// of:
	/// Weapons ---+-> Ranged -+-> Crossbow
	///            |           |
	///			   |           +-> Sniper Rifle
	///			   |
	///            +-> Melee --+-> Axe
	///                        |
	///                        +-> Mace
	/// 
	/// GameModes -+-> PvP -+-> Capture the Flag
	///            |        |
	///            |        +-> King of the Hill
	///            |
	///            +-> Campaign
	/// 
	/// UserGameplayTags.Weapons.Ranged.Crossbow
	/// UserGameplayTags.Weapons.Ranged.SniperRifle
	/// UserGameplayTags.GameModes.Campaign
	/// and
	/// UserGameplayTags.Weapons.Melee
	/// 
	/// are all valid GameplayTags.
	/// 
	/// You can define gameplay tags using this dot notation (without UserGameplayTags)
	/// by looking at the GameplayTags tab in the Player Settings.
	/// 
	/// To create a GameplayTag member that can be displayed in-editor, use:
	/// [SerializeReference, GameplayTag(TYPE_CONSTRAINT)]
	/// ACCESSIBILITY GameplayTag MEMBER_NAME DEFAULT_VALUE;
	/// 
	/// where:
	/// TYPE_CONSTRAINT is nothing or typeof(UserGameplayTags.MyGameplayTag.MyOtherGameplayTag ...).
	///		This restricts the type of GameplayTags to only the children of the typeof.
	/// ACCESSIBILITY is private, public, protected, etc.
	/// MEMBER_NAME is the name of the member.
	/// DEFAULT_VALUE is nothing or = new UserGameplayTags.MyGameplayTag.MyOtherGameplayTag ... ()
	/// </summary>
	[System.Serializable]
    public class GameplayTag
    {
		/// <summary>
		/// The GameplayTag class that is the parent of this one in the GameplayTag chain (not via inheritance).
		/// </summary>
		public GameplayTag Parent
		{
			get
			{
				return GetParent();
			}
		}

		/// <summary>
		/// The GameplayTag classes that are the children of this tag in the GameplayTag chain (not via inheritance).
		/// </summary>
		public GameplayTag[] Children
		{
			get
			{
				return GetChildren();
			}
		}

		/// <summary>
		/// The name of the last GameplayTag in the chain. Not the full GameplayTag.
		/// </summary>
		public string Name
		{
			get
			{
				return GetName();
			}
		}

		/// <summary>
		/// The function that returns the parent of this GameplayTag.
		/// Overridden per GameplayTag.
		/// </summary>
		/// <returns></returns>
		protected virtual GameplayTag GetParent()
		{
			return null;
		}

		/// <summary>
		/// The function that returns the children of this GameplayTag.
		/// Overridden per GameplayTag.
		/// </summary>
		/// <returns></returns>
		protected virtual GameplayTag[] GetChildren()
		{
			return new GameplayTag[0];
		}

		/// <summary>
		/// The function that returns the name of this GameplayTag.
		/// Overriden per GameplayTag.
		/// </summary>
		/// <returns></returns>
		protected virtual string GetName()
		{
			return "";
		}

		/// <summary>
		/// Check whether two GameplayTags are the same.
		/// GameplayTags are not mutable, so checking their types is sufficient.
		/// </summary>
		/// <param name="obj">The other GameplayTag to check.</param>
		/// <returns>Whether the types of the GameplayTags match.</returns>
		public override bool Equals(object obj)
		{
			return GetType() == obj.GetType();
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode();
		}
	}
}
