using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Demo.PropertySetting
{
    /// <summary>
    /// Declares names of the property settings.
    /// </summary>
    public static class PropertySettingNames
    {
        ///// <summary>
        ///// Abp.<......>
        ///// </summary>
        //public const string DefaultFromAddress = "Abp.<....>";

        /// <summary>
        /// Incidents settings.
        /// </summary>
        public static class Incidents
        {
            /// <summary>
            /// Abp.Incidents.IncidentNo
            /// </summary>
            public const string IncidentNo = "Demo.Incidents.IncidentNo";

            /// <summary>
            /// Abp.Incidents.IncidentDescr
            /// </summary>
            public const string IncidentDescription = "Demo.Incidents.IncidentDescr";

            /// <summary>
            /// Abp.Incidents.IncidentType
            /// </summary>
            public const string IncidentType = "Demo.Incidents.IncidentType";

            /// <summary>
            /// Abp.Incidents.OccurenceDate
            /// </summary>
            public const string OccurenceDate = "Demo.Incidents.OccurenceDate";

            /// <summary>
            /// Abp.Incidents.ReportTo
            /// </summary>
            public const string ReportTo = "Demo.Incidents.ReportTo";

            /// <summary>
            /// Abp.Incidents.Status
            /// </summary>
            public const string Status = "Demo.Incidents.Status";

            /// <summary>
            /// Reviews settings.
            /// </summary>
            public static class Reviews
            {
                /// <summary>
                /// Abp.Incidents.Status
                /// </summary>
                public const string IncidentStatus = "Demo.Incidents.Reviews.IncidentStatus";
            }
        }
    }
}
