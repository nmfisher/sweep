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
    /// ListenerAction
    /// </summary>
    [DataContract]
    public partial class ListenerAction :  IEquatable<ListenerAction>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerAction" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected ListenerAction() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerAction" /> class.
        /// </summary>
        /// <param name="id">id (required).</param>
        /// <param name="eventId">eventId (required).</param>
        /// <param name="listenerId">listenerId (required).</param>
        /// <param name="organizationId">organizationId (required).</param>
        /// <param name="completed">completed (required) (default to false).</param>
        /// <param name="error">error.</param>
        public ListenerAction(string id = default(string), string eventId = default(string), string listenerId = default(string), string organizationId = default(string), bool completed = false, string error = default(string))
        {
            // to ensure "id" is required (not null)
            if (id == null)
            {
                throw new InvalidDataException("id is a required property for ListenerAction and cannot be null");
            }
            else
            {
                this.Id = id;
            }

            // to ensure "eventId" is required (not null)
            if (eventId == null)
            {
                throw new InvalidDataException("eventId is a required property for ListenerAction and cannot be null");
            }
            else
            {
                this.EventId = eventId;
            }

            // to ensure "listenerId" is required (not null)
            if (listenerId == null)
            {
                throw new InvalidDataException("listenerId is a required property for ListenerAction and cannot be null");
            }
            else
            {
                this.ListenerId = listenerId;
            }

            // to ensure "organizationId" is required (not null)
            if (organizationId == null)
            {
                throw new InvalidDataException("organizationId is a required property for ListenerAction and cannot be null");
            }
            else
            {
                this.OrganizationId = organizationId;
            }

            // to ensure "completed" is required (not null)
            if (completed == null)
            {
                throw new InvalidDataException("completed is a required property for ListenerAction and cannot be null");
            }
            else
            {
                this.Completed = completed;
            }

            this.Error = error;
        }
        
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets EventId
        /// </summary>
        [DataMember(Name="eventId", EmitDefaultValue=false)]
        public string EventId { get; set; }

        /// <summary>
        /// Gets or Sets ListenerId
        /// </summary>
        [DataMember(Name="listenerId", EmitDefaultValue=false)]
        public string ListenerId { get; set; }

        /// <summary>
        /// Gets or Sets OrganizationId
        /// </summary>
        [DataMember(Name="organizationId", EmitDefaultValue=false)]
        public string OrganizationId { get; set; }

        /// <summary>
        /// Gets or Sets Completed
        /// </summary>
        [DataMember(Name="completed", EmitDefaultValue=false)]
        public bool Completed { get; set; }

        /// <summary>
        /// Gets or Sets Error
        /// </summary>
        [DataMember(Name="error", EmitDefaultValue=false)]
        public string Error { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ListenerAction {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  EventId: ").Append(EventId).Append("\n");
            sb.Append("  ListenerId: ").Append(ListenerId).Append("\n");
            sb.Append("  OrganizationId: ").Append(OrganizationId).Append("\n");
            sb.Append("  Completed: ").Append(Completed).Append("\n");
            sb.Append("  Error: ").Append(Error).Append("\n");
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
            return this.Equals(input as ListenerAction);
        }

        /// <summary>
        /// Returns true if ListenerAction instances are equal
        /// </summary>
        /// <param name="input">Instance of ListenerAction to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ListenerAction input)
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
                    this.EventId == input.EventId ||
                    (this.EventId != null &&
                    this.EventId.Equals(input.EventId))
                ) && 
                (
                    this.ListenerId == input.ListenerId ||
                    (this.ListenerId != null &&
                    this.ListenerId.Equals(input.ListenerId))
                ) && 
                (
                    this.OrganizationId == input.OrganizationId ||
                    (this.OrganizationId != null &&
                    this.OrganizationId.Equals(input.OrganizationId))
                ) && 
                (
                    this.Completed == input.Completed ||
                    (this.Completed != null &&
                    this.Completed.Equals(input.Completed))
                ) && 
                (
                    this.Error == input.Error ||
                    (this.Error != null &&
                    this.Error.Equals(input.Error))
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
                if (this.EventId != null)
                    hashCode = hashCode * 59 + this.EventId.GetHashCode();
                if (this.ListenerId != null)
                    hashCode = hashCode * 59 + this.ListenerId.GetHashCode();
                if (this.OrganizationId != null)
                    hashCode = hashCode * 59 + this.OrganizationId.GetHashCode();
                if (this.Completed != null)
                    hashCode = hashCode * 59 + this.Completed.GetHashCode();
                if (this.Error != null)
                    hashCode = hashCode * 59 + this.Error.GetHashCode();
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

            // EventId (string) maxLength
            if(this.EventId != null && this.EventId.Length > 36)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for EventId, length must be less than 36.", new [] { "EventId" });
            }

            // EventId (string) minLength
            if(this.EventId != null && this.EventId.Length < 36)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for EventId, length must be greater than 36.", new [] { "EventId" });
            }

            // ListenerId (string) maxLength
            if(this.ListenerId != null && this.ListenerId.Length > 36)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for ListenerId, length must be less than 36.", new [] { "ListenerId" });
            }

            // ListenerId (string) minLength
            if(this.ListenerId != null && this.ListenerId.Length < 36)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for ListenerId, length must be greater than 36.", new [] { "ListenerId" });
            }

            // OrganizationId (string) maxLength
            if(this.OrganizationId != null && this.OrganizationId.Length > 36)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for OrganizationId, length must be less than 36.", new [] { "OrganizationId" });
            }

            // OrganizationId (string) minLength
            if(this.OrganizationId != null && this.OrganizationId.Length < 36)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for OrganizationId, length must be greater than 36.", new [] { "OrganizationId" });
            }

            yield break;
        }
    }

}
