using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoList_Desktop.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// The <see cref="T:AutoList_Desktop.MainWindow" /> ViewModel
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        #region Private Members

        private Thickness _resizeBorderThickness;
        private Thickness _outerMarginThickness;
        private CornerRadius _windowCornerRadius;
        private WindowState _windowState;
        private int _titleHeight;

        #endregion

        #region Default Values

        private const int ResizeBorderThicknessDefault = 3;
        private const int OuterMarginThicknessDefault = 10;
        private const int WindowCornerRadiusDefault = 10;
        private const int TitleHeightDefault = 42;

        #endregion


        #region Constructor

        /// <summary>
        /// Sets all properties to the default values
        /// </summary>
        public MainWindowViewModel()
        {
            _resizeBorderThickness = new Thickness(ResizeBorderThicknessDefault);
            _outerMarginThickness = new Thickness(OuterMarginThicknessDefault);
            _windowCornerRadius = new CornerRadius(WindowCornerRadiusDefault);
            _windowState = WindowState.Normal;
            _titleHeight = TitleHeightDefault;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// The Thickness of the border around this window, taking into account
        /// the outer margin.
        /// </summary>
        public Thickness ResizeBorderThickness
        {
            get => WindowsHelpers.WindowHelper
                .AddThickness(_resizeBorderThickness, OuterMarginThickness);
            set
            {
                _resizeBorderThickness = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// The Current Window State. Note that this property will also notify
        /// The outer margin radius of a change
        /// </summary>
        public WindowState CurrentWindowState
        {
            get => _windowState;
            set
            {
                _windowState = value;

                switch ( value )
                {
                    case WindowState.Normal:
                        WindowCornerRadius = new CornerRadius(WindowCornerRadiusDefault);
                        OuterMarginThickness = new Thickness(OuterMarginThicknessDefault);
                        break;
                    case WindowState.Minimized:
                        break;
                    case WindowState.Maximized:
                        WindowCornerRadius = new CornerRadius(0);
                        OuterMarginThickness = new Thickness(0);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// The Corner Radius around the edges of the Window
        /// </summary>
        public CornerRadius WindowCornerRadius
        {
            get => _windowCornerRadius;
            set
            {
                _windowCornerRadius = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// The Outer Margin thickness for the drop shadow
        /// </summary>
        public Thickness OuterMarginThickness
        {
            get => _outerMarginThickness;
            set
            {
                _outerMarginThickness = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// The height of the title bar on the top of the window
        /// </summary>
        public int TitleHeight
        {
            get => _titleHeight + ResizeBorderThicknessDefault;
            set
            {
                _titleHeight = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}
