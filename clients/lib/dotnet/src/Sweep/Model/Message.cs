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
    /// Message
    /// </summary>
    [DataContract]
    public partial class Message :  IEquatable<Message>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Message" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected Message() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Message" /> class.
        /// </summary>
        /// <param name="id">id (required).</param>
        /// <param name="content">content (required).</param>
        /// <param name="subject">subject (required).</param>
        /// <param name="fromAddress">fromAddress (required).</param>
        /// <param name="fromName">fromName (required).</param>
        /// <param name="sendTo">sendTo (required).</param>
        /// <param name="organizationId">organizationId (required).</param>
        /// <param name="listenerActionId">listenerActionId (required).</param>
        /// <param name="sentOn">sentOn.</param>
        public Message(string id = default(string), string content = default(string), string subject = default(string), string fromAddress = default(string), string fromName = default(string), List<string> sendTo = default(List<string>), string organizationId = default(string), string listenerActionId = default(string), DateTime sentOn = default(DateTime))
        {
            // to ensure "id" is required (not null)
            if (id == null)
            {
                throw new InvalidDataException("id is a required property for Message and cannot be null");
            }
            else
            {
                this.Id = id;
            }

            // to ensure "content" is required (not null)
            if (content == null)
            {
                throw new InvalidDataException("content is a required property for Message and cannot be null");
            }
            else
            {
                this.Content = content;
            }

            // to ensure "subject" is required (not null)
            if (subject == null)
            {
                throw new InvalidDataException("subject is a required property for Message and cannot be null");
            }
            else
            {
                this.Subject = subject;
            }

            // to ensure "fromAddress" is required (not null)
            if (fromAddress == null)
            {
                throw new InvalidDataException("fromAddress is a required property for Message and cannot be null");
            }
            else
            {
                this.FromAddress = fromAddress;
            }

            // to ensure "fromName" is required (not null)
            if (fromName == null)
            {
                throw new InvalidDataException("fromName is a required property for Message and cannot be null");
            }
            else
            {
                this.FromName = fromName;
            }

            // to ensure "sendTo" is required (not null)
            if (sendTo == null)
            {
                throw new InvalidDataException("sendTo is a required property for Message and cannot be null");
            }
            else
            {
                this.SendTo = sendTo;
            }

            // to ensure "organizationId" is required (not null)
            if (organizationId == null)
            {
                throw new InvalidDataException("organizationId is a required property for Message and cannot be null");
            }
            else
            {
                this.OrganizationId = organizationId;
            }

            // to ensure "listenerActionId" is required (not null)
            if (listenerActionId == null)
            {
                throw new InvalidDataException("listenerActionId is a required property for Message and cannot be null");
            }
            else
            {
                this.ListenerActionId = listenerActionId;
            }

            this.SentOn = sentOn;
        }
        
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }

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
        /// Gets or Sets OrganizationId
        /// </summary>
        [DataMember(Name="organizationId", EmitDefaultValue=false)]
        public string OrganizationId { get; set; }

        /// <summary>
        /// Gets or Sets ListenerActionId
        /// </summary>
        [DataMember(Name="listenerActionId", EmitDefaultValue=false)]
        public string ListenerActionId { get; set; }

        /// <summary>
        /// Gets or Sets SentOn
        /// </summary>
        [DataMember(Name="sentOn", EmitDefaultValue=false)]
        public DateTime SentOn { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Message {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Content: ").Append(Content).Append("\n");
            sb.Append("  Subject: ").Append(Subject).Append("\n");
            sb.Append("  FromAddress: ").Append(FromAddress).Append("\n");
            sb.Append("  FromName: ").Append(FromName).Append("\n");
            sb.Append("  SendTo: ").Append(SendTo).Append("\n");
            sb.Append("  OrganizationId: ").Append(OrganizationId).Append("\n");
            sb.Append("  ListenerActionId: ").Append(ListenerActionId).Append("\n");
            sb.Append("  SentOn: ").Append(SentOn).Append("\n");
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
            return this.Equals(input as Message);
        }

        /// <summary>
        /// Returns true if Message instances are equal
        /// </summary>
        /// <param name="input">Instance of Message to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Message input)
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
                ) && 
                (
                    this.OrganizationId == input.OrganizationId ||
                    (this.OrganizationId != null &&
                    this.OrganizationId.Equals(input.OrganizationId))
                ) && 
                (
                    this.ListenerActionId == input.ListenerActionId ||
                    (this.ListenerActionId != null &&
                    this.ListenerActionId.Equals(input.ListenerActionId))
                ) && 
                (
                    this.SentOn == input.SentOn ||
                    (this.SentOn != null &&
                    this.SentOn.Equals(input.SentOn))
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
                if (this.OrganizationId != null)
                    hashCode = hashCode * 59 + this.OrganizationId.GetHashCode();
                if (this.ListenerActionId != null)
                    hashCode = hashCode * 59 + this.ListenerActionId.GetHashCode();
                if (this.SentOn != null)
                    hashCode = hashCode * 59 + this.SentOn.GetHashCode();
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
