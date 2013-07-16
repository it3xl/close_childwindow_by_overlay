using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System;

namespace TreeTraversingProject
{
	/// <summary>
	/// Some subset of traversing methods for a elements tree.
	/// </summary>
	public static class DependencyObjectExtension
	{
		/// <summary>
		/// Get a first child element with speceifed name.
		/// </summary>
		/// <param name="parent">A parent element.</param>
		/// <param name="targetElementName">A element's name for search.</param>
		/// <returns>The null or first child element with specified name.</returns>
		public static DependencyObject GetChildByName(this DependencyObject parent, String targetElementName)
		{
			foreach (var dependencyChild in parent.GetChildren())
			{
				var elementChild = dependencyChild as FrameworkElement;

				if (elementChild != null
					&& elementChild.Name == targetElementName)
				{
					return elementChild;
				}

				var deeperChild = dependencyChild.GetChildByName(targetElementName);
				if (deeperChild == null)
				{
					continue;
				}

				return deeperChild;
			}

			return null;
		}

		/// <summary>
		/// Get the child elements as <see cref="IEnumerable{DependencyObject}"/>.
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		public static IEnumerable<DependencyObject> GetChildren(this DependencyObject parent)
		{
			int count = VisualTreeHelper.GetChildrenCount(parent);

			for (int index = 0; index < count; index++)
			{
				yield return VisualTreeHelper.GetChild(parent, index);
			}
		}

		/// <summary>
		/// Checks if the <see cref="IEnumerable{DependencyObject}"/> has children elements.
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		public static Boolean HasChildren(this DependencyObject parent)
		{
			return 0 < VisualTreeHelper.GetChildrenCount(parent);
		}

	}
}
