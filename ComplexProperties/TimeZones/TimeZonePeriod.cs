// ---------------------------------------------------------------------------
// <copyright file="TimeZonePeriod.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

//-----------------------------------------------------------------------
// <summary>Defines the TimeZonePeriod class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Exchange.WebServices.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents a time zone period as defined in the EWS schema.
    /// </summary>
    internal class TimeZonePeriod : ComplexProperty
    {
        internal const string StandardPeriodId = "Std";
        internal const string StandardPeriodName = "Standard";
        internal const string DaylightPeriodId = "Dlt";
        internal const string DaylightPeriodName = "Daylight";

        private TimeSpan bias;
        private string name;
        private string id;

        /// <summary>
        /// Reads the attributes from XML.
        /// </summary>
        /// <param name="reader">The reader.</param>
        internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
        {
            this.id = reader.ReadAttributeValue(XmlAttributeNames.Id);
            this.name = reader.ReadAttributeValue(XmlAttributeNames.Name);
            this.bias = EwsUtilities.XSDurationToTimeSpan(reader.ReadAttributeValue(XmlAttributeNames.Bias));
        }

        /// <summary>
        /// Writes the attributes to XML.
        /// </summary>
        /// <param name="writer">The writer.</param>
        internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
        {
            writer.WriteAttributeValue(XmlAttributeNames.Bias, EwsUtilities.TimeSpanToXSDuration(this.bias));
            writer.WriteAttributeValue(XmlAttributeNames.Name, this.name);
            writer.WriteAttributeValue(XmlAttributeNames.Id, this.id);
        }

        /// <summary>
        /// Serializes the property to a Json value.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns>
        /// A Json value (either a JsonObject, an array of Json values, or a Json primitive)
        /// </returns>
        internal override object InternalToJson(ExchangeService service)
        {
            JsonObject jsonTimeZonePeriod = new JsonObject();

            jsonTimeZonePeriod.Add(XmlAttributeNames.Bias, EwsUtilities.TimeSpanToXSDuration(this.bias));
            jsonTimeZonePeriod.Add(XmlAttributeNames.Name, this.name);
            jsonTimeZonePeriod.Add(XmlAttributeNames.Id, this.id);

            return jsonTimeZonePeriod;
        }

        /// <summary>
        /// Loads from XML.
        /// </summary>
        /// <param name="reader">The reader.</param>
        internal void LoadFromXml(EwsServiceXmlReader reader)
        {
            this.LoadFromXml(reader, XmlElementNames.Period);
        }

        /// <summary>
        /// Loads from json.
        /// </summary>
        /// <param name="jsonProperty">The json property.</param>
        /// <param name="service">The service.</param>
        internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
        {
            base.LoadFromJson(jsonProperty, service);

            foreach (string key in jsonProperty.Keys)
            {
                switch (key)
                {
                    case XmlAttributeNames.Id:
                        this.id = jsonProperty.ReadAsString(key);
                        break;
                    case XmlAttributeNames.Name:
                        this.name = jsonProperty.ReadAsString(key);
                        break;
                    case XmlAttributeNames.Bias:
                        this.bias = EwsUtilities.XSDurationToTimeSpan(jsonProperty.ReadAsString(key));
                        break;
                }
            }
        }

        /// <summary>
        /// Writes to XML.
        /// </summary>
        /// <param name="writer">The writer.</param>
        internal void WriteToXml(EwsServiceXmlWriter writer)
        {
            this.WriteToXml(writer, XmlElementNames.Period);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeZonePeriod"/> class.
        /// </summary>
        internal TimeZonePeriod()
            : base()
        {
        }

        /// <summary>
        /// Gets a value indicating whether this period represents the Standard period.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is standard period; otherwise, <c>false</c>.
        /// </value>
        internal bool IsStandardPeriod
        {
            get
            {
                return string.Compare(
                    this.name,
                    TimeZonePeriod.StandardPeriodName,
                    StringComparison.OrdinalIgnoreCase) == 0;
            }
        }

        /// <summary>
        /// Gets or sets the bias to UTC associated with this period.
        /// </summary>
        internal TimeSpan Bias
        {
            get { return this.bias; }
            set { this.bias = value; }
        }

        /// <summary>
        /// Gets or sets the name of this period.
        /// </summary>
        internal string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Gets or sets the id of this period.
        /// </summary>
        internal string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
    }
}
