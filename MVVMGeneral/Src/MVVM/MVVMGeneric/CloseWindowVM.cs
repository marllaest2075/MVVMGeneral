using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMGeneric
{
    public class CloseWindowVM :BaseViewModel, ICloseWindow
    {
        // Agregar  WindowCloser.EnableWindowClose = true 
        //en el mainWindow.xaml

        private DelegateCommand _closeCommand;

        public DelegateCommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new DelegateCommand(closeWindow)); }
        }

        private void closeWindow()
        {
            Close?.Invoke();
        }

        public Action Close { get; set; }
        public bool CanClose() 
        {
            return false;
        }
    }

    public interface ICloseWindow
    {
        Action Close { get; set; }
        bool CanClose();
    }
    public class WindowsCloser 
    {
        public static bool GetEnableWindowClose(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableWindowCloseProperty);
        }

        public static void SetEnableWindowClose(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableWindowCloseProperty, value);
        }

        // Using a DependencyProperty as the backing store for EnableWindowClose.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnableWindowCloseProperty =
            DependencyProperty.RegisterAttached("EnableWindowClose", typeof(bool), typeof(WindowsCloser), new PropertyMetadata(false, OnEnableWindowCloseChange));

        private static void OnEnableWindowCloseChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                window.Loaded += (s, e) =>
                {
                    if (window.DataContext is ICloseWindow vm)
                    {
                        vm.Close += () => { window.Close(); };
                        window.Closing += (s, e) => { e.Cancel = !vm.CanClose(); };
                    }
                };
            }
        }
    }

}
