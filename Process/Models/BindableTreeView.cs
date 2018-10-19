using System.Windows;
using System.Windows.Controls;

namespace Processes.Models
{
    internal sealed class BindableTreeView : TreeView
    {
        public static readonly DependencyProperty BindableSelectedItemProperty = DependencyProperty.Register(
            nameof(BindableSelectedItem),
            typeof(object),
            typeof(BindableTreeView),
            new PropertyMetadata(null)
        );

        public BindableTreeView()
        {
            SelectedItemChanged += (sender, e) => BindableSelectedItem = SelectedItem;
        }

        public object BindableSelectedItem
        {
            get => GetValue(BindableSelectedItemProperty);
            set => SetValue(BindableSelectedItemProperty, value);
        }
    }
}