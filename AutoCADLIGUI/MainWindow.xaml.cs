using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoCADLIGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Regex LwPolylineRegex;


        public MainWindow()
        {
            LwPolylineRegex = new Regex(@"length\s*\d*\.\d{1-4}");
            InitializeComponent();
        }

        private void ExtractionButton_OnClick(object sender, RoutedEventArgs e)
        {
            ParseLiText();
        }

        // When the Blocks Check box is checked check the polylines and hatches
        // boxes and disable them
        private void BlocksCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            // Setting then disabling the Polylines checkbox
            PolylinesCheckBox.IsChecked = true;
            PolylinesCheckBox.IsEnabled = false;

            // Setting then disabling the Hatch checkbox
            HatchesCheckBox.IsChecked = true;
            HatchesCheckBox.IsEnabled = false;
        }

        // When the Blocks Check box is unchecked undo the actions that checking it did
        // with regard to the polylines and hatches checkboxes
        private void BlocksCheckBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            // Uncheck then Enable the Polylines checkbox
            PolylinesCheckBox.IsChecked = false;
            PolylinesCheckBox.IsEnabled = true;

            // Uncheck then Enable the Hatch checkbox
            HatchesCheckBox.IsChecked = false;
            HatchesCheckBox.IsEnabled = true;
        }

        private void ParseLiText()
        {
            var lines = MainTextBox.Text.Split(
                new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

        }

    }

}
