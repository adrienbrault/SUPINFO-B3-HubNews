using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HubNews.Helper
{
    public class DataTemplateSelector : ContentControl
    {
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            ContentTemplate = this.FindResource<DataTemplate>(newContent.GetType().FullName);
        }
    }

    public static class Extensions
    {
        public static T FindResource<T>(this DependencyObject initial, string key) where T : DependencyObject
        {
            DependencyObject current = initial;

            while (current != null)
            {
                if (current is FrameworkElement)
                {
                    if ((current as FrameworkElement).Resources.Contains(key))
                    {
                        return (T)(current as FrameworkElement).Resources[key];
                    }
                }

                current = VisualTreeHelper.GetParent(current);
            }

            if (Application.Current.Resources.Contains(key))
            {
                return (T)Application.Current.Resources[key];
            }

            return default(T);
        }
    }
}