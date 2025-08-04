namespace MenuGraph.Editor
{
	using UnityEditor;
	using UnityEngine.UIElements;
	using VisualElementHelper;

	/// <summary>
	/// This is the <see cref="EditorWindow"/> for the Menu Graph Editor.
	/// This contains:
	/// <list type="bullet">
	/// <item>The node editor canvas with the menus, the link between them etc;</item>
	/// <item>An inspector section to manage menu;</item>
	/// <item>A toolbar, for utility stuff.</item>
	/// </list>
	/// </summary>
	internal sealed class MenuGraphWindow : EditorWindow
	{
		#region Fields
		private MenuGraphToolbar _menuGraphToolbar = null;
		private MenuGraphInspector _menuGraphInspector = null;
		private MenuGraphCanvas _menuGraphCanvas = null;
		#endregion Fields

		#region Methods
		#region Statics
		// TODO-16: Change the path.
		[MenuItem("Window/Menu Graph Editor", priority = -10000)]
		internal static void ShowWindow()
		{
			EditorWindow.CreateWindow<MenuGraphWindow>();
		}
		#endregion Statics

		#region Lifecycle
		private void CreateGUI()
		{
			rootVisualElement.LoadUXML();

			_menuGraphToolbar = rootVisualElement.Q<MenuGraphToolbar>();
			_menuGraphInspector = rootVisualElement.Q<MenuGraphInspector>();
			_menuGraphCanvas = rootVisualElement.Q<MenuGraphCanvas>();

			MenuGraph currentMenuGraph = SelectMenuGraph();

			_menuGraphCanvas.PopulateMenuGraph(currentMenuGraph);
		}

		private void OnDestroy()
		{
			_menuGraphToolbar?.Dispose();
			_menuGraphToolbar = null;

			_menuGraphInspector?.Dispose();
			_menuGraphInspector = null;

			_menuGraphCanvas?.Dispose();
			_menuGraphCanvas = null;
		}
		#endregion Lifecycle

		#region Privates
		private MenuGraph SelectMenuGraph()
		{
			// TODO-4 : Currently only take the first MenuGraph found in project.
			string[] assetsGuids = AssetDatabase.FindAssets("t:MenuGraph");

			if (assetsGuids.Length == 0)
			{
				return null;
			}

			string manuGraphPath = AssetDatabase.GUIDToAssetPath(assetsGuids[0]);
			return AssetDatabase.LoadAssetAtPath<MenuGraph>(manuGraphPath);
		}
		#endregion Privates
		#endregion Methods
	}
}