using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using System;

namespace werignac.GameplayTags
{
    public class GameplayTagSettings : ScriptableObject
    {
		public const string simulationSettingsResourcesPath = "Settings/GameplayTagSettings";
		public const string simulationSettingsPath = "Assets/Resources/Settings/GameplayTagSettings.asset";
		public const string compiledGameplayTagsPath = "Assets/Scripts/GameplayTags/UserGameplayTags.cs";

		[SerializeField]
		private string[] m_gameplayTags;

		/// <summary>
		/// Gets a singleton GameplayTagSettings reference. If there is already a
		/// GameplayTagSettings asset, use that. Otherwise, create a new GameplayTagSettings object.
		/// Creates an asset for the GameplayTagSettings if it doesn't exist, and we're in editor.
		/// </summary>
		/// <returns>The singleton GameplayTagSettings.</returns>
		public static GameplayTagSettings GetOrCreateSettings()
		{
			GameplayTagSettings settings;
#if UNITY_EDITOR
			settings = AssetDatabase.LoadAssetAtPath<GameplayTagSettings>(simulationSettingsPath);
#else
			settings = Resources.Load<GameplayTagSettings>(simulationSettingsResourcesPath);
#endif
			if (settings == null)
			{
				settings = ScriptableObject.CreateInstance<GameplayTagSettings>();
				settings.m_gameplayTags = new string[0];
#if UNITY_EDITOR
				AssetDatabase.CreateAsset(settings, simulationSettingsPath);
				AssetDatabase.SaveAssets();
#endif
			}
			return settings;
		}

#if UNITY_EDITOR
		public void SortGameplayTags()
		{
			Array.Sort(m_gameplayTags);
		}

		/// <summary>
		/// Creates a C# script that stores the heirarchy of GameplayTags in such a way that
		/// they can be used in other C# scripts.
		/// </summary>
		public void CompileGameplayTags()
		{
			GameplayTagHeirarchy heirarchy = CreateGameplayTagHeirarchy();
			if (heirarchy == null)
				return;
			
			string code = GameplayHeirarchyToManagerCode(heirarchy);

			if (File.Exists(compiledGameplayTagsPath))
			{
				File.Delete(compiledGameplayTagsPath);
			}

			File.WriteAllText(compiledGameplayTagsPath, code);
			
			AssetDatabase.Refresh();
		}

		/// <summary>
		/// A class used to keep track of the gameplay tag heirarchy from the dot form:
		/// tag1.tag2.tag3
		/// tag1.tag4
		/// 
		/// Becomes the tree:
		/// tag1 -+-> tag2 -> tag3
		///       |
		///       +-> tag4
		/// </summary>
		private class GameplayTagHeirarchy
		{
			public string name;
			public GameplayTagHeirarchy parent;
			public List<GameplayTagHeirarchy> children;

			public GameplayTagHeirarchy(string name, GameplayTagHeirarchy parent)
			{
				this.name = name;
				this.parent = parent;
				children = new List<GameplayTagHeirarchy>();
			}
		}

		/// <summary>
		/// Creates a GameplayTagHeirarchy from tag dot notation.
		/// Logs errors as they occur. Returns null if the heirarchy could not be built.
		/// </summary>
		/// <returns>The GameplayTagHeirarchy. Null if it could not be built.</returns>
		private GameplayTagHeirarchy CreateGameplayTagHeirarchy()
		{
			GameplayTagHeirarchy root = new GameplayTagHeirarchy("", null);

			// Keeps track of whether we found an invalid Gameplay Tag Name.
			// If there's an invalid name, we don't want to return a heirarchy.
			bool foundInvalidGameplayTag = false;

			foreach (string gameplayTagChain in m_gameplayTags)
			{
				string lastTagName = "";
				GameplayTagHeirarchy current = root;

				foreach (string tagName in gameplayTagChain.Split('.'))
				{
					// Stop trying to make a heirarchy if a tag name is invalid.
					if (!IsValidGameplayTagName(tagName))
					{
						Debug.LogError($"{tagName} in {gameplayTagChain} is not a valid tag name. Tag names must start with a capital letter, followed by underscores and letters. Some names like \"GameplayTag\" are not valid.");
						foundInvalidGameplayTag = true;
					}
					if (tagName == lastTagName)
					{
						Debug.LogError($"{tagName} in {gameplayTagChain} is used twice in a row. A parent cannot share the same nade as its child.");
						foundInvalidGameplayTag = true;
					}

					// See if this tag already exists.
					int exisingIndex = current.children.FindIndex((GameplayTagHeirarchy h) => h.name == tagName);

					// If not, create a new tag in the heiarchy.
					if (exisingIndex < 0)
					{
						current.children.Add(new GameplayTagHeirarchy(tagName, current));
						exisingIndex = current.children.Count - 1;
					}

					// Recurse.
					current = current.children[exisingIndex];
					lastTagName = tagName;
				}
			}

			// If there was a problem with validating GameplayTag names, don't continue.
			if (foundInvalidGameplayTag)
				return null;

			return root;
		}

		/// <summary>
		/// Determines whether a the name for a gameplay tag is valid in isolation (avoids C# conflicts).
		/// </summary>
		/// <param name="gameplayTagName">The name of the gameplay tag.</param>
		/// <returns>Whether the name is valid.</returns>
		private static bool IsValidGameplayTagName(string gameplayTagName)
		{
			// Check for letters and underscores only.
			// Check for a name with length > 0.
			if (gameplayTagName.Length == 0)
				return false;

			Regex regex = new Regex("^[A-Z]([a-zA-Z_])*$");
			Match match = regex.Match(gameplayTagName);
			if (!match.Success)
				return false;

			// Check that the Gameplay Tag does not conflict with any special objects.
			switch(gameplayTagName)
			{
				case "GameplayTagAttribute":
				case "GameplayTagSettings":
				case "GameplayTagPropertyDrawer":
				case "GameplayTagProvider":
				case "GameplayTags":
				case "GameplayTagExtensions":
				case "GameplayTag":
				case "UserGameplayTags":
					return false;
				default:
					return true;
			}
		}

		/// <summary>
		/// Converts a GameplayHeirarchy to C# code.
		/// All User-defined GameplayTags are classes within a UserGameplayTag class.
		/// All User-defined GameplayTags inherit from the GameplayTag class.
		/// </summary>
		/// <param name="heirarchy">The heirarchy to turn into C# code.</param>
		/// <returns>The code form of the GameplayTagHeirarchy.</returns>
		private string GameplayHeirarchyToManagerCode(GameplayTagHeirarchy heirarchy)
		{
			string header = @"
namespace werignac.GameplayTags {
	public sealed class UserGameplayTags {
";
			string footer = @"
	}
}";
			string classes = "";
			string rootClassesList = "";

			foreach (GameplayTagHeirarchy tag in heirarchy.children)
			{
				classes += GameplayHeirarchyToGameplayTagClassCode(tag, 2);

				if (rootClassesList.Length > 0)
					rootClassesList += ", ";

				rootClassesList += $"new {tag.name}()";
			}

			string rootClassesProperty = @"
		public static GameplayTag[] RootTags {
			get { return new GameplayTag[] {" + rootClassesList + @"}; }
		}";

			return header + classes + rootClassesProperty + footer;
		}

		/// <summary>
		/// Recursively creates code to turn GameplayTagHeirarchies into C# classes.
		/// </summary>
		/// <param name="heirarchy">The heirarchy to convert.</param>
		/// <param name="indentLevel">The indent level for the code that will be written.</param>
		/// <returns>The code.</returns>
		private string GameplayHeirarchyToGameplayTagClassCode(GameplayTagHeirarchy heirarchy, int indentLevel)
		{
			// null or constructed parent.
			string parent = heirarchy.parent.name;
			if (heirarchy.parent.name == "")
			{
				parent = "null";
			}
			else
			{
				parent = $"new {parent}()";
			}

			// Comma-separated list of children constructors.
			string children = "";
			foreach (GameplayTagHeirarchy child in heirarchy.children)
			{
				if (children.Length > 0)
				{
					children += ", ";
				}

				children += $"new {child.name}()";
			}

			// Write the internals for the class, except for the subclass.
			List<string> lines = new List<string>();

			// Class Header
			lines.Add("public sealed class " + heirarchy.name + " : GameplayTag {");
			
			// StaticName
			lines.Add("\tpublic static string StaticName {");
			lines.Add("\t\tget { return \"" + heirarchy.name + "\"; }");
			lines.Add("\t}");
			// StaticParent
			lines.Add("\tpublic static GameplayTag StaticParent {");
			lines.Add("\t\tget { return " + parent + "; }");
			lines.Add("\t}");
			// StaticChildren
			lines.Add("\tpublic static GameplayTag[] StaticChildren {");
			lines.Add("\t\tget { return new GameplayTag[] { " + children + " }; }");
			lines.Add("\t}");
			// GetName()
			lines.Add("\tprotected override string GetName() {");
			lines.Add("\t\t return StaticName;");
			lines.Add("\t}");
			// GetParent()
			lines.Add("\tprotected override GameplayTag GetParent() {");
			lines.Add("\t\t return StaticParent;");
			lines.Add("\t}");
			// GetChildren()
			lines.Add("\tprotected override GameplayTag[] GetChildren() {");
			lines.Add("\t\t return StaticChildren;");
			lines.Add("\t}");

			// Class Footer
			lines.Add("}");

			// Get the internals to line up with the indents.
			{
				// Construct a string of all the indents that will need to be added to each line.
				string indents = "";
				for (int i = 0; i < indentLevel; i++)
				{
					indents += "\t";
				}

				// Add the indents.
				for (int i = 0; i < lines.Count; i++)
				{
					lines[i] = indents + lines[i];
				}
			}

			// Add the children as subclasses to this class.
			// These will be indented automatically.
			foreach (GameplayTagHeirarchy child in heirarchy.children)
			{
				// Insert after the header but before any Properties.
				lines.Insert(1, GameplayHeirarchyToGameplayTagClassCode(child, indentLevel + 1));
			}

			// Compose the final string.
			string compositeClass = "";
			foreach (string line in lines)
			{
				compositeClass += line + "\r\n";
			}

			return compositeClass;
		}

#endif

	}
}
