using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ERService.Contracts.Constants;
using Prism.Regions;
using Syncfusion.SfSkinManager;
using Syncfusion.Windows.Shared;

namespace ERService.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ChromelessWindow
    {
        #region Fields
        private string currentVisualStyle;
        private string currentSizeMode;
        private readonly IRegionManager regionManager;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the current visual style.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string CurrentVisualStyle {
            get {
                return currentVisualStyle;
            }
            set {
                currentVisualStyle = value;
                OnVisualStyleChanged();
            }
        }

        /// <summary>
        /// Gets or sets the current Size mode.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string CurrentSizeMode {
            get {
                return currentSizeMode;
            }
            set {
                currentSizeMode = value;
                OnSizeModeChanged();
            }
        }
        #endregion

        public MainWindow(IRegionManager regionManager) {
            InitializeComponent();
            this.Loaded += OnLoaded;

            this.regionManager = regionManager;
            var detail = LogicalTreeHelper.GetParent(this.detailRegion);
            var detailMenu = LogicalTreeHelper.GetParent(this.detailMenuRegion);

            if (regionManager != null)
            {
                SetRegionManager(regionManager, this.detailRegion, RegionNames.DetailRegion);
                SetRegionManager(regionManager, this.detailMenuRegion, RegionNames.DetailMenuRegion);
            }
        }

        void SetRegionManager(IRegionManager regionManager, DependencyObject regionTarget, string regionName)
        {
            RegionManager.SetRegionName(regionTarget, regionName);
            RegionManager.SetRegionManager(regionTarget, regionManager);
        }

        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnLoaded(object sender, RoutedEventArgs e) {
            CurrentVisualStyle = "FluentLight";
            CurrentSizeMode = "Default";
        }
        /// <summary>
        /// On Visual Style Changed.
        /// </summary>
        /// <remarks></remarks>
        private void OnVisualStyleChanged() {
            VisualStyles visualStyle = VisualStyles.Default;
            Enum.TryParse(CurrentVisualStyle, out visualStyle);
            if (visualStyle != VisualStyles.Default) {
                SfSkinManager.ApplyStylesOnApplication = true;
                SfSkinManager.SetVisualStyle(this, visualStyle);
                SfSkinManager.ApplyStylesOnApplication = false;
            }
        }

        /// <summary>
        /// On Size Mode Changed event.
        /// </summary>
        /// <remarks></remarks>
        private void OnSizeModeChanged() {
            SizeMode sizeMode = SizeMode.Default;
            Enum.TryParse(CurrentSizeMode, out sizeMode);
            if (sizeMode != SizeMode.Default) {
                SfSkinManager.ApplyStylesOnApplication = true;
                SfSkinManager.SetSizeMode(this, sizeMode);
                SfSkinManager.ApplyStylesOnApplication = false;
            }
        }
    }
}
