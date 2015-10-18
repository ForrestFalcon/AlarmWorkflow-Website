using AlarmWorkflow.Backend.ServiceContracts.Communication;
using AlarmWorkflow.BackendService.SettingsContracts;
using AlarmWorkflow.Shared;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Web.Configuration;

namespace AlarmWorkflow.Website.Reports.Models
{
    public class SettingsData
    {
        public enum MapType { Straße, Hybrid, Terrain, Satellit }

        private Configuration Config;

        /// <summary>
        /// Gets whether or not to edit the settings in the web interface
        /// </summary>
        public bool EditSettings { get; private set; }
        /// <summary>
        /// Gets whether or not to display the traffic.
        /// </summary>
        [DisplayName("Zeige Traffic")]
        [Required()]
        public bool Traffic { get; set; }
        /// <summary>
        /// Gets whether or not to apply tilt to the map.
        /// </summary>
        [DisplayName("Map Tilt")]
        [Required()]
        public bool Tilt { get; set; }
        /// <summary>
        /// Gets whether or not to display the route.
        /// </summary>
        [DisplayName("Zeige Route")]
        [Required()]
        public bool Route { get; set; }
        /// <summary>
        /// Gets the zoom control value.
        /// </summary>
        [DisplayName("Zeige ZoomControl")]
        [Required()]
        public bool ZoomControl { get; set; }
        /// <summary>
        /// Gets the map type to use.
        /// </summary>
        [DisplayName("Google MapType")]
        [Required()]
        public MapType MapTypeGoogle { get; set; }
        /// <summary>
        /// Gets the home address.
        /// </summary>
        public string Home { get; set; }
        /// <summary>
        /// Gets the maximum age of operations.
        /// </summary>
        [DisplayName("Max. Alter für Alarme (in Minuten)")]
        [Required()]
        public int MaxAge { get; set; }
        /// <summary>
        /// Gets the update interval in milliseconds.
        /// </summary>
        [DisplayName("Update Interval für die Alarme (in ms)")]
        [Required()]
        public int UpdateIntervalMs { get; set; }
        /// <summary>
        /// Gets whether or not to filter only acknowledged operations (default is true).
        /// </summary>
        [DisplayName("Nur nicht-angenommene Alarme")]
        [Required()]
        public bool NonAcknowledgedOnly { get; set; }
        /// <summary>
        /// Gets the zoom level to use for GMaps.
        /// </summary>
        [DisplayName("Map Zoom Level")]
        [Required()]
        [Range(0, 100, ErrorMessage = "Zoom Level für GoogleMaps muss zwischen 0 und 100 sein")]
        public int GoogleZoomLevel { get; set; }
        /// <summary>
        /// Gets the zoom level to use for OSM.
        /// </summary>
        [DisplayName("OSM Zoom Level")]
        [Required()]
        [Range(0, 18, ErrorMessage = "Zoom Level für OSM muss zwischen 0 und 18 sein")]
        public int OSMZoomLevel { get; set; }
        
        #region Constructors

        public SettingsData()
        {
            using (var service = ServiceFactory.GetCallbackServiceWrapper<ISettingsService>(new SettingsServiceCallback()))
            {
                Home = service.Instance.GetSetting(SettingKeys.FDStreet).GetValue<string>() + " " +
                       service.Instance.GetSetting(SettingKeys.FDStreetNumber).GetValue<string>() + " " +
                       service.Instance.GetSetting(SettingKeys.FDZipCode).GetValue<string>() + " " +
                       service.Instance.GetSetting(SettingKeys.FDCity).GetValue<string>();
            }

            Config = WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection appSettings = Config.AppSettings;
            
            EditSettings = appSettings.Settings["EditSettings"].Value.ToLower().Equals("true");
            Traffic = appSettings.Settings["Traffic"].Value.ToLower().Equals("true");
            Tilt = appSettings.Settings["Tilt"].Value.ToLower().Equals("true");
            Route = appSettings.Settings["Route"].Value.ToLower().Equals("true");
            ZoomControl = appSettings.Settings["ZoomControl"].Value.ToLower().Equals("true");
            GoogleZoomLevel = int.Parse(appSettings.Settings["GoogleZoomLevel"].Value);
            
            OSMZoomLevel = int.Parse(appSettings.Settings["OSMZoomLevel"].Value);

            NonAcknowledgedOnly = appSettings.Settings["NonAcknowledgedOnly"].Value.ToLower().Equals("true");
            UpdateIntervalMs = int.Parse(appSettings.Settings["UpdateInterval"].Value);
            MaxAge = int.Parse(appSettings.Settings["MaxAge"].Value);
        }

        #endregion

        #region Methods

        public void Save()
        {
            if (!EditSettings)
                throw new System.Exception("Edit settings is not allowed");

            AppSettingsSection appSettings = Config.AppSettings;

            appSettings.Settings["Traffic"].Value = Traffic.ToString();
            appSettings.Settings["Tilt"].Value = Tilt.ToString();
            appSettings.Settings["Route"].Value = Route.ToString();
            appSettings.Settings["ZoomControl"].Value = ZoomControl.ToString();

            appSettings.Settings["GoogleZoomLevel"].Value = GoogleZoomLevel.ToString();
            appSettings.Settings["OSMZoomLevel"].Value = OSMZoomLevel.ToString();

            appSettings.Settings["NonAcknowledgedOnly"].Value = NonAcknowledgedOnly.ToString();
            appSettings.Settings["UpdateInterval"].Value = UpdateIntervalMs.ToString();
            appSettings.Settings["MaxAge"].Value = MaxAge.ToString();
            
            Config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public string GetGoogleMapType()
        {
            string type = WebConfigurationManager.AppSettings["MapType"].ToLower();
            string map = string.Empty;
            switch (type)
            {
                case "straße":
                    map = "ROADMAP";
                    break;
                case "hybrid":
                    map = "HYBRID";
                    break;
                case "terrain":
                    map = "TERRAIN";
                    break;
                case "satellit":
                    map = "SATELLITE";
                    break;
            }

            return "google.maps.MapTypeId." + map;
        }

        #endregion
    }
}
