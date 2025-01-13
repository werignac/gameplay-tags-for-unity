using UnityEngine;
using werignac.GameplayTags;

public class ExampleComponent : MonoBehaviour
{
	/// <summary>
	/// A general gameplay tag. This could be any gameplay tag in the
	/// GameplayTags project settings.
	/// </summary>
	[SerializeReference, GameplayTag()]
	private GameplayTag m_generalTag;

	/// <summary>
	/// A gameplay tag that is specifically a child of Action tag type.
	/// </summary>
	[SerializeReference, GameplayTag(typeof(UserGameplayTags.Action))]
	private GameplayTag m_actionTag;

	/// <summary>
	/// Another gameplay tag that is specifically a child of the Item tag type.
	/// This one has a default value.
	/// </summary>
	[SerializeReference, GameplayTag(typeof(UserGameplayTags.Item))]
	private GameplayTag m_itemTag = new UserGameplayTags.Item.Weapon.Melee();

	/// <summary>
	/// Another gameplay tag that is specifically a child of the more niche Item.Weapon.Ranged tag.
	/// </summary>
	[SerializeReference, GameplayTag(typeof(UserGameplayTags.Item.Weapon.Ranged))]
	private GameplayTag m_rangedWeaponTag;
}
