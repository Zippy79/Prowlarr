using System.Collections.Generic;
using FluentValidation;
using NzbDrone.Core.Annotations;
using NzbDrone.Core.ThingiProvider;
using NzbDrone.Core.Validation;

namespace NzbDrone.Core.Applications.Lidarr
{
    public class LidarrSettingsValidator : AbstractValidator<LidarrSettings>
    {
        public LidarrSettingsValidator()
        {
            RuleFor(c => c.BaseUrl).IsValidUrl();
            RuleFor(c => c.ProwlarrUrl).IsValidUrl();
            RuleFor(c => c.ApiKey).NotEmpty();
        }
    }

    public class LidarrSettings : IApplicationSettings
    {
        private static readonly LidarrSettingsValidator Validator = new LidarrSettingsValidator();

        public LidarrSettings()
        {
            ProwlarrUrl = "http://localhost:9696";
            BaseUrl = "http://localhost:8686";
            SyncCategories = new[] { 3000, 3010, 3030, 3040, 3050, 3060 };
        }

        public IEnumerable<int> SyncCategories { get; set; }

        [FieldDefinition(0, Label = "Prowlarr Server", HelpText = "Prowlarr server URL as Lidarr sees it, including http(s):// and port if needed")]
        public string ProwlarrUrl { get; set; }

        [FieldDefinition(1, Label = "Lidarr Server", HelpText = "Lidarr server URL, including http(s):// and port if needed")]
        public string BaseUrl { get; set; }

        [FieldDefinition(2, Label = "ApiKey", Privacy = PrivacyLevel.ApiKey, HelpText = "The ApiKey generated by Lidarr in Settings/General")]
        public string ApiKey { get; set; }

        public NzbDroneValidationResult Validate()
        {
            return new NzbDroneValidationResult(Validator.Validate(this));
        }
    }
}
