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
    /// ListenerTemplate
    /// </summary>
    [DataContract]
    public partial class ListenerTemplate :  IEquatable<ListenerTemplate>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerTemplate" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected ListenerTemplate() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerTemplate" /> class.
        /// </summary>
        /// <param name="listenerId">listenerId (required).</param>
        /// <param name="templateId">templateId (required).</param>
        /// <param name="organizationId">organizationId (required).</param>
        public ListenerTemplate(string listenerId = default(string), string templateId = default(string), string organizationId = default(string))
        {
            // to ensure "listenerId" is required (not null)
            if (listenerId == null)
            {
                throw new InvalidDataException("listenerId is a required property for ListenerTemplate and cannot be null");
            }
            else
            {
                this.ListenerId = listenerId;
            }

            // to ensure "templateId" is required (not null)
            if (templateId == null)
            {
                throw new InvalidDataException("templateId is a required property for ListenerTemplate and cannot be null");
            }
            else
            {
                this.TemplateId = templateId;
            }

            // to ensure "organizationId" is required (not null)
            if (organizationId == null)
            {
                throw new InvalidDataException("organizationId is a required property for ListenerTemplate and cannot be null");
            }
            else
            {
                this.OrganizationId = organizationId;
            }

        }
        
        /// <summary>
        /// Gets or Sets ListenerId
        /// </summary>
        [DataMember(Name="listenerId", EmitDefaultValue=false)]
        public string ListenerId { get; set; }

        /// <summary>
        /// Gets or Sets TemplateId
        /// </summary>
        [DataMember(Name="templateId", EmitDefaultValue=false)]
        public string TemplateId { get; set; }

        /// <summary>
        /// Gets or Sets OrganizationId
        /// </summary>
        [DataMember(Name="organizationId", EmitDefaultValue=false)]
        public string OrganizationId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ListenerTemplate {\n");
            sb.Append("  ListenerId: ").Append(ListenerId).Append("\n");
            sb.Append("  TemplateId: ").Append(TemplateId).Append("\n");
            sb.Append("  OrganizationId: ").Append(OrganizationId).Append("\n");
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
            return this.Equals(input as ListenerTemplate);
        }

        /// <summary>
        /// Returns true if ListenerTemplate instances are equal
        /// </summary>
        /// <param name="input">Instance of ListenerTemplate to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ListenerTemplate input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.ListenerId == input.ListenerId ||
                    (this.ListenerId != null &&
                    this.ListenerId.Equals(input.ListenerId))
                ) && 
                (
                    this.TemplateId == input.TemplateId ||
                    (this.TemplateId != null &&
                    this.TemplateId.Equals(input.TemplateId))
                ) && 
                (
                    this.OrganizationId == input.OrganizationId ||
                    (this.OrganizationId != null &&
                    this.OrganizationId.Equals(input.OrganizationId))
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
                if (this.ListenerId != null)
                    hashCode = hashCode * 59 + this.ListenerId.GetHashCode();
                if (this.TemplateId != null)
                    hashCode = hashCode * 59 + this.TemplateId.GetHashCode();
                if (this.OrganizationId != null)
                    hashCode = hashCode * 59 + this.OrganizationId.GetHashCode();
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

            // TemplateId (string) maxLength
            if(this.TemplateId != null && this.TemplateId.Length > 36)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for TemplateId, length must be less than 36.", new [] { "TemplateId" });
            }

            // TemplateId (string) minLength
            if(this.TemplateId != null && this.TemplateId.Length < 36)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for TemplateId, length must be greater than 36.", new [] { "TemplateId" });
            }

            yield break;
        }
    }

}
