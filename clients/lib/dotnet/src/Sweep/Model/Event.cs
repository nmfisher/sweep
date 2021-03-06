/* 
 * Sweep API
 *
 * API definitions for the Sweep server/dashboard.
 *
 * OpenAPI spec version: 1.0.0-oas3
 * Contact: contact@avinium.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = Sweep.Client.OpenAPIDateConverter;

namespace Sweep.Model
{
    /// <summary>
    /// Event
    /// </summary>
    [DataContract]
    public partial class Event :  IEquatable<Event>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Event" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected Event() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Event" /> class.
        /// </summary>
        /// <param name="id">id (required).</param>
        /// <param name="eventName">eventName (required).</param>
        /// <param name="_params">_params.</param>
        /// <param name="receivedOn">receivedOn (required).</param>
        /// <param name="processedOn">processedOn.</param>
        /// <param name="error">error.</param>
        /// <param name="organizationId">organizationId (required).</param>
        /// <param name="actions">actions.</param>
        public Event(string id = default(string), string eventName = default(string), Dictionary<string, Object> _params = default(Dictionary<string, Object>), DateTime receivedOn = default(DateTime), DateTime processedOn = default(DateTime), string error = default(string), string organizationId = default(string), List<ListenerAction> actions = default(List<ListenerAction>))
        {
            // to ensure "id" is required (not null)
            if (id == null)
            {
                throw new InvalidDataException("id is a required property for Event and cannot be null");
            }
            else
            {
                this.Id = id;
            }

            // to ensure "eventName" is required (not null)
            if (eventName == null)
            {
                throw new InvalidDataException("eventName is a required property for Event and cannot be null");
            }
            else
            {
                this.EventName = eventName;
            }

            // to ensure "receivedOn" is required (not null)
            if (receivedOn == null)
            {
                throw new InvalidDataException("receivedOn is a required property for Event and cannot be null");
            }
            else
            {
                this.ReceivedOn = receivedOn;
            }

            // to ensure "organizationId" is required (not null)
            if (organizationId == null)
            {
                throw new InvalidDataException("organizationId is a required property for Event and cannot be null");
            }
            else
            {
                this.OrganizationId = organizationId;
            }

            this.Params = _params;
            this.ProcessedOn = processedOn;
            this.Error = error;
            this.Actions = actions;
        }
        
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets EventName
        /// </summary>
        [DataMember(Name="eventName", EmitDefaultValue=false)]
        public string EventName { get; set; }

        /// <summary>
        /// Gets or Sets Params
        /// </summary>
        [DataMember(Name="params", EmitDefaultValue=false)]
        public Dictionary<string, Object> Params { get; set; }

        /// <summary>
        /// Gets or Sets ReceivedOn
        /// </summary>
        [DataMember(Name="receivedOn", EmitDefaultValue=false)]
        public DateTime ReceivedOn { get; set; }

        /// <summary>
        /// Gets or Sets ProcessedOn
        /// </summary>
        [DataMember(Name="processedOn", EmitDefaultValue=false)]
        public DateTime ProcessedOn { get; set; }

        /// <summary>
        /// Gets or Sets Error
        /// </summary>
        [DataMember(Name="error", EmitDefaultValue=false)]
        public string Error { get; set; }

        /// <summary>
        /// Gets or Sets OrganizationId
        /// </summary>
        [DataMember(Name="organizationId", EmitDefaultValue=false)]
        public string OrganizationId { get; set; }

        /// <summary>
        /// Gets or Sets Actions
        /// </summary>
        [DataMember(Name="actions", EmitDefaultValue=false)]
        public List<ListenerAction> Actions { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Event {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  EventName: ").Append(EventName).Append("\n");
            sb.Append("  Params: ").Append(Params).Append("\n");
            sb.Append("  ReceivedOn: ").Append(ReceivedOn).Append("\n");
            sb.Append("  ProcessedOn: ").Append(ProcessedOn).Append("\n");
            sb.Append("  Error: ").Append(Error).Append("\n");
            sb.Append("  OrganizationId: ").Append(OrganizationId).Append("\n");
            sb.Append("  Actions: ").Append(Actions).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as Event);
        }

        /// <summary>
        /// Returns true if Event instances are equal
        /// </summary>
        /// <param name="input">Instance of Event to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Event input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.EventName == input.EventName ||
                    (this.EventName != null &&
                    this.EventName.Equals(input.EventName))
                ) && 
                (
                    this.Params == input.Params ||
                    this.Params != null &&
                    this.Params.SequenceEqual(input.Params)
                ) && 
                (
                    this.ReceivedOn == input.ReceivedOn ||
                    (this.ReceivedOn != null &&
                    this.ReceivedOn.Equals(input.ReceivedOn))
                ) && 
                (
                    this.ProcessedOn == input.ProcessedOn ||
                    (this.ProcessedOn != null &&
                    this.ProcessedOn.Equals(input.ProcessedOn))
                ) && 
                (
                    this.Error == input.Error ||
                    (this.Error != null &&
                    this.Error.Equals(input.Error))
                ) && 
                (
                    this.OrganizationId == input.OrganizationId ||
                    (this.OrganizationId != null &&
                    this.OrganizationId.Equals(input.OrganizationId))
                ) && 
                (
                    this.Actions == input.Actions ||
                    this.Actions != null &&
                    this.Actions.SequenceEqual(input.Actions)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.EventName != null)
                    hashCode = hashCode * 59 + this.EventName.GetHashCode();
                if (this.Params != null)
                    hashCode = hashCode * 59 + this.Params.GetHashCode();
                if (this.ReceivedOn != null)
                    hashCode = hashCode * 59 + this.ReceivedOn.GetHashCode();
                if (this.ProcessedOn != null)
                    hashCode = hashCode * 59 + this.ProcessedOn.GetHashCode();
                if (this.Error != null)
                    hashCode = hashCode * 59 + this.Error.GetHashCode();
                if (this.OrganizationId != null)
                    hashCode = hashCode * 59 + this.OrganizationId.GetHashCode();
                if (this.Actions != null)
                    hashCode = hashCode * 59 + this.Actions.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            // Id (string) maxLength
            if(this.Id != null && this.Id.Length > 36)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Id, length must be less than 36.", new [] { "Id" });
            }

            // Id (string) minLength
            if(this.Id != null && this.Id.Length < 36)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Id, length must be greater than 36.", new [] { "Id" });
            }

            yield break;
        }
    }

}
