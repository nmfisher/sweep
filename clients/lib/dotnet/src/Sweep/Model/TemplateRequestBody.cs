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
    /// TemplateRequestBody
    /// </summary>
    [DataContract]
    public partial class TemplateRequestBody :  IEquatable<TemplateRequestBody>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateRequestBody" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected TemplateRequestBody() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateRequestBody" /> class.
        /// </summary>
        /// <param name="content">content (required).</param>
        /// <param name="subject">subject (required).</param>
        /// <param name="fromAddress">fromAddress (required).</param>
        /// <param name="fromName">fromName (required).</param>
        /// <param name="sendTo">sendTo (required).</param>
        public TemplateRequestBody(string content = default(string), string subject = default(string), string fromAddress = default(string), string fromName = default(string), List<string> sendTo = default(List<string>))
        {
            // to ensure "content" is required (not null)
            if (content == null)
            {
                throw new InvalidDataException("content is a required property for TemplateRequestBody and cannot be null");
            }
            else
            {
                this.Content = content;
            }

            // to ensure "subject" is required (not null)
            if (subject == null)
            {
                throw new InvalidDataException("subject is a required property for TemplateRequestBody and cannot be null");
            }
            else
            {
                this.Subject = subject;
            }

            // to ensure "fromAddress" is required (not null)
            if (fromAddress == null)
            {
                throw new InvalidDataException("fromAddress is a required property for TemplateRequestBody and cannot be null");
            }
            else
            {
                this.FromAddress = fromAddress;
            }

            // to ensure "fromName" is required (not null)
            if (fromName == null)
            {
                throw new InvalidDataException("fromName is a required property for TemplateRequestBody and cannot be null");
            }
            else
            {
                this.FromName = fromName;
            }

            // to ensure "sendTo" is required (not null)
            if (sendTo == null)
            {
                throw new InvalidDataException("sendTo is a required property for TemplateRequestBody and cannot be null");
            }
            else
            {
                this.SendTo = sendTo;
            }

        }
        
        /// <summary>
        /// Gets or Sets Content
        /// </summary>
        [DataMember(Name="content", EmitDefaultValue=false)]
        public string Content { get; set; }

        /// <summary>
        /// Gets or Sets Subject
        /// </summary>
        [DataMember(Name="subject", EmitDefaultValue=false)]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or Sets FromAddress
        /// </summary>
        [DataMember(Name="fromAddress", EmitDefaultValue=false)]
        public string FromAddress { get; set; }

        /// <summary>
        /// Gets or Sets FromName
        /// </summary>
        [DataMember(Name="fromName", EmitDefaultValue=false)]
        public string FromName { get; set; }

        /// <summary>
        /// Gets or Sets SendTo
        /// </summary>
        [DataMember(Name="sendTo", EmitDefaultValue=false)]
        public List<string> SendTo { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TemplateRequestBody {\n");
            sb.Append("  Content: ").Append(Content).Append("\n");
            sb.Append("  Subject: ").Append(Subject).Append("\n");
            sb.Append("  FromAddress: ").Append(FromAddress).Append("\n");
            sb.Append("  FromName: ").Append(FromName).Append("\n");
            sb.Append("  SendTo: ").Append(SendTo).Append("\n");
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
            return this.Equals(input as TemplateRequestBody);
        }

        /// <summary>
        /// Returns true if TemplateRequestBody instances are equal
        /// </summary>
        /// <param name="input">Instance of TemplateRequestBody to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TemplateRequestBody input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Content == input.Content ||
                    (this.Content != null &&
                    this.Content.Equals(input.Content))
                ) && 
                (
                    this.Subject == input.Subject ||
                    (this.Subject != null &&
                    this.Subject.Equals(input.Subject))
                ) && 
                (
                    this.FromAddress == input.FromAddress ||
                    (this.FromAddress != null &&
                    this.FromAddress.Equals(input.FromAddress))
                ) && 
                (
                    this.FromName == input.FromName ||
                    (this.FromName != null &&
                    this.FromName.Equals(input.FromName))
                ) && 
                (
                    this.SendTo == input.SendTo ||
                    this.SendTo != null &&
                    this.SendTo.SequenceEqual(input.SendTo)
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
                if (this.Content != null)
                    hashCode = hashCode * 59 + this.Content.GetHashCode();
                if (this.Subject != null)
                    hashCode = hashCode * 59 + this.Subject.GetHashCode();
                if (this.FromAddress != null)
                    hashCode = hashCode * 59 + this.FromAddress.GetHashCode();
                if (this.FromName != null)
                    hashCode = hashCode * 59 + this.FromName.GetHashCode();
                if (this.SendTo != null)
                    hashCode = hashCode * 59 + this.SendTo.GetHashCode();
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
            yield break;
        }
    }

}