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
    /// Organization
    /// </summary>
    [DataContract]
    public partial class Organization :  IEquatable<Organization>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Organization" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected Organization() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Organization" /> class.
        /// </summary>
        /// <param name="id">id (required).</param>
        /// <param name="primaryApiKey">primaryApiKey (required).</param>
        /// <param name="secondaryApiKey">secondaryApiKey (required).</param>
        public Organization(string id = default(string), string primaryApiKey = default(string), string secondaryApiKey = default(string))
        {
            // to ensure "id" is required (not null)
            if (id == null)
            {
                throw new InvalidDataException("id is a required property for Organization and cannot be null");
            }
            else
            {
                this.Id = id;
            }

            // to ensure "primaryApiKey" is required (not null)
            if (primaryApiKey == null)
            {
                throw new InvalidDataException("primaryApiKey is a required property for Organization and cannot be null");
            }
            else
            {
                this.PrimaryApiKey = primaryApiKey;
            }

            // to ensure "secondaryApiKey" is required (not null)
            if (secondaryApiKey == null)
            {
                throw new InvalidDataException("secondaryApiKey is a required property for Organization and cannot be null");
            }
            else
            {
                this.SecondaryApiKey = secondaryApiKey;
            }

        }
        
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryApiKey
        /// </summary>
        [DataMember(Name="primaryApiKey", EmitDefaultValue=false)]
        public string PrimaryApiKey { get; set; }

        /// <summary>
        /// Gets or Sets SecondaryApiKey
        /// </summary>
        [DataMember(Name="secondaryApiKey", EmitDefaultValue=false)]
        public string SecondaryApiKey { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Organization {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  PrimaryApiKey: ").Append(PrimaryApiKey).Append("\n");
            sb.Append("  SecondaryApiKey: ").Append(SecondaryApiKey).Append("\n");
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
            return this.Equals(input as Organization);
        }

        /// <summary>
        /// Returns true if Organization instances are equal
        /// </summary>
        /// <param name="input">Instance of Organization to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Organization input)
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
                    this.PrimaryApiKey == input.PrimaryApiKey ||
                    (this.PrimaryApiKey != null &&
                    this.PrimaryApiKey.Equals(input.PrimaryApiKey))
                ) && 
                (
                    this.SecondaryApiKey == input.SecondaryApiKey ||
                    (this.SecondaryApiKey != null &&
                    this.SecondaryApiKey.Equals(input.SecondaryApiKey))
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
                if (this.PrimaryApiKey != null)
                    hashCode = hashCode * 59 + this.PrimaryApiKey.GetHashCode();
                if (this.SecondaryApiKey != null)
                    hashCode = hashCode * 59 + this.SecondaryApiKey.GetHashCode();
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
