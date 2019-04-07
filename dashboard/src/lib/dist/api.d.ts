/**
 * Sweep API
 * API definitions for the Sweep server/dashboard.
 *
 * OpenAPI spec version: 1.0.0-oas3
 * Contact: contact@avinium.com
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */
/// <reference path="../custom.d.ts" />
import { Configuration } from './configuration';
import { AxiosPromise, AxiosInstance } from 'axios';
import { RequestArgs, BaseAPI } from './base';
/**
 *
 * @export
 * @interface BaseMessage
 */
export interface BaseMessage {
    /**
     *
     * @type {string}
     * @memberof BaseMessage
     */
    id: string;
    /**
     *
     * @type {string}
     * @memberof BaseMessage
     */
    content: string;
    /**
     *
     * @type {string}
     * @memberof BaseMessage
     */
    subject: string;
    /**
     *
     * @type {string}
     * @memberof BaseMessage
     */
    fromAddress: string;
    /**
     *
     * @type {string}
     * @memberof BaseMessage
     */
    fromName: string;
    /**
     *
     * @type {Array<string>}
     * @memberof BaseMessage
     */
    sendTo: Array<string>;
    /**
     *
     * @type {string}
     * @memberof BaseMessage
     */
    organizationId: string;
}
/**
 *
 * @export
 * @interface Event
 */
export interface Event {
    /**
     *
     * @type {string}
     * @memberof Event
     */
    id: string;
    /**
     *
     * @type {string}
     * @memberof Event
     */
    eventName: string;
    /**
     *
     * @type {{ [key: string]: any; }}
     * @memberof Event
     */
    params?: {
        [key: string]: any;
    };
    /**
     *
     * @type {Date}
     * @memberof Event
     */
    receivedOn: Date;
    /**
     *
     * @type {Date}
     * @memberof Event
     */
    processedOn?: Date;
    /**
     *
     * @type {string}
     * @memberof Event
     */
    error?: string;
    /**
     *
     * @type {string}
     * @memberof Event
     */
    organizationId: string;
}
/**
 *
 * @export
 * @interface EventRequestBody
 */
export interface EventRequestBody {
    /**
     *
     * @type {string}
     * @memberof EventRequestBody
     */
    eventName: string;
    /**
     *
     * @type {{ [key: string]: any; }}
     * @memberof EventRequestBody
     */
    params?: {
        [key: string]: any;
    };
}
/**
 *
 * @export
 * @interface Listener
 */
export interface Listener {
    /**
     *
     * @type {string}
     * @memberof Listener
     */
    id?: string;
    /**
     *
     * @type {string}
     * @memberof Listener
     */
    eventName: string;
    /**
     *
     * @type {string}
     * @memberof Listener
     */
    organizationId: string;
}
/**
 *
 * @export
 * @interface ListenerTemplate
 */
export interface ListenerTemplate {
    /**
     *
     * @type {string}
     * @memberof ListenerTemplate
     */
    listenerId: string;
    /**
     *
     * @type {string}
     * @memberof ListenerTemplate
     */
    templateId: string;
    /**
     *
     * @type {string}
     * @memberof ListenerTemplate
     */
    organizationId: string;
}
/**
 *
 * @export
 * @interface Message
 */
export interface Message {
    /**
     *
     * @type {string}
     * @memberof Message
     */
    id: string;
    /**
     *
     * @type {string}
     * @memberof Message
     */
    content: string;
    /**
     *
     * @type {string}
     * @memberof Message
     */
    subject: string;
    /**
     *
     * @type {string}
     * @memberof Message
     */
    fromAddress: string;
    /**
     *
     * @type {string}
     * @memberof Message
     */
    fromName: string;
    /**
     *
     * @type {Array<string>}
     * @memberof Message
     */
    sendTo: Array<string>;
    /**
     *
     * @type {string}
     * @memberof Message
     */
    organizationId: string;
}
/**
 *
 * @export
 * @interface Organization
 */
export interface Organization {
    /**
     *
     * @type {string}
     * @memberof Organization
     */
    id: string;
}
/**
 *
 * @export
 * @interface Template
 */
export interface Template {
    /**
     *
     * @type {string}
     * @memberof Template
     */
    id: string;
    /**
     *
     * @type {string}
     * @memberof Template
     */
    content: string;
    /**
     *
     * @type {string}
     * @memberof Template
     */
    subject: string;
    /**
     *
     * @type {string}
     * @memberof Template
     */
    fromAddress: string;
    /**
     *
     * @type {string}
     * @memberof Template
     */
    fromName: string;
    /**
     *
     * @type {Array<string>}
     * @memberof Template
     */
    sendTo: Array<string>;
    /**
     *
     * @type {string}
     * @memberof Template
     */
    organizationId: string;
    /**
     *
     * @type {boolean}
     * @memberof Template
     */
    deleted?: boolean;
    /**
     *
     * @type {string}
     * @memberof Template
     */
    userId: string;
}
/**
 *
 * @export
 * @interface User
 */
export interface User {
    /**
     *
     * @type {string}
     * @memberof User
     */
    id: string;
    /**
     *
     * @type {string}
     * @memberof User
     */
    username?: string;
    /**
     *
     * @type {string}
     * @memberof User
     */
    password?: string;
    /**
     *
     * @type {string}
     * @memberof User
     */
    apiKey?: string;
    /**
     *
     * @type {string}
     * @memberof User
     */
    organizationId: string;
}
/**
 * EventApi - axios parameter creator
 * @export
 */
export declare const EventApiAxiosParamCreator: (configuration?: Configuration) => {
    /**
     *
     * @summary Raise an event
     * @param {EventRequestBody} eventRequestBody Raises an Event with associated parameters
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    addEvent(eventRequestBody: EventRequestBody, options?: any): RequestArgs;
    /**
     *
     * @summary Find raised event by ID
     * @param {string} eventId ID of event that needs to be fetched
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getEventById(eventId: string, options?: any): RequestArgs;
    /**
     * Returns a list of all events
     * @summary List all received events
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    listEvents(options?: any): RequestArgs;
};
/**
 * EventApi - functional programming interface
 * @export
 */
export declare const EventApiFp: (configuration?: Configuration) => {
    /**
     *
     * @summary Raise an event
     * @param {EventRequestBody} eventRequestBody Raises an Event with associated parameters
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    addEvent(eventRequestBody: EventRequestBody, options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Response>;
    /**
     *
     * @summary Find raised event by ID
     * @param {string} eventId ID of event that needs to be fetched
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getEventById(eventId: string, options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Event>;
    /**
     * Returns a list of all events
     * @summary List all received events
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    listEvents(options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Event[]>;
};
/**
 * EventApi - factory interface
 * @export
 */
export declare const EventApiFactory: (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) => {
    /**
     *
     * @summary Raise an event
     * @param {EventRequestBody} eventRequestBody Raises an Event with associated parameters
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    addEvent(eventRequestBody: EventRequestBody, options?: any): AxiosPromise<Response>;
    /**
     *
     * @summary Find raised event by ID
     * @param {string} eventId ID of event that needs to be fetched
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getEventById(eventId: string, options?: any): AxiosPromise<Event>;
    /**
     * Returns a list of all events
     * @summary List all received events
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    listEvents(options?: any): AxiosPromise<Event[]>;
};
/**
 * EventApi - object-oriented interface
 * @export
 * @class EventApi
 * @extends {BaseAPI}
 */
export declare class EventApi extends BaseAPI {
    /**
     *
     * @summary Raise an event
     * @param {EventRequestBody} eventRequestBody Raises an Event with associated parameters
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof EventApi
     */
    addEvent(eventRequestBody: EventRequestBody, options?: any): AxiosPromise<Response>;
    /**
     *
     * @summary Find raised event by ID
     * @param {string} eventId ID of event that needs to be fetched
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof EventApi
     */
    getEventById(eventId: string, options?: any): AxiosPromise<Event>;
    /**
     * Returns a list of all events
     * @summary List all received events
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof EventApi
     */
    listEvents(options?: any): AxiosPromise<Event[]>;
}
/**
 * ListenerApi - axios parameter creator
 * @export
 */
export declare const ListenerApiAxiosParamCreator: (configuration?: Configuration) => {
    /**
     *
     * @summary Create a new Listener
     * @param {Listener} listener A Listener object to be added
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    addListener(listener: Listener, options?: any): RequestArgs;
    /**
     *
     * @summary Associates a Template to a Listener
     * @param {string} listenerId Listener id to disassociate
     * @param {string} templateId Template id to associate with the Listener
     * @param {string} [apiKey]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    addListenerTemplate(listenerId: string, templateId: string, apiKey?: string, options?: any): RequestArgs;
    /**
     *
     * @summary Deletes a Listener
     * @param {string} listenerId ID of listener to return
     * @param {string} [apiKey]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    deleteListener(listenerId: string, apiKey?: string, options?: any): RequestArgs;
    /**
     *
     * @summary Disassociates a Template from a Listener
     * @param {string} listenerId Listener id to disassociate
     * @param {string} templateId Template id to delete
     * @param {string} [apiKey]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    deleteListenerTemplate(listenerId: string, templateId: string, apiKey?: string, options?: any): RequestArgs;
    /**
     * Returns a list of templates associated with this listener
     * @summary List Templates for Listener
     * @param {string} listenerId ID of listener
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    listListenerTemplates(listenerId: string, options?: any): RequestArgs;
    /**
     * Returns a list of Listeners
     * @summary List all Listeners
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    listListeners(options?: any): RequestArgs;
};
/**
 * ListenerApi - functional programming interface
 * @export
 */
export declare const ListenerApiFp: (configuration?: Configuration) => {
    /**
     *
     * @summary Create a new Listener
     * @param {Listener} listener A Listener object to be added
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    addListener(listener: Listener, options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Response>;
    /**
     *
     * @summary Associates a Template to a Listener
     * @param {string} listenerId Listener id to disassociate
     * @param {string} templateId Template id to associate with the Listener
     * @param {string} [apiKey]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    addListenerTemplate(listenerId: string, templateId: string, apiKey?: string, options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Response>;
    /**
     *
     * @summary Deletes a Listener
     * @param {string} listenerId ID of listener to return
     * @param {string} [apiKey]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    deleteListener(listenerId: string, apiKey?: string, options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Response>;
    /**
     *
     * @summary Disassociates a Template from a Listener
     * @param {string} listenerId Listener id to disassociate
     * @param {string} templateId Template id to delete
     * @param {string} [apiKey]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    deleteListenerTemplate(listenerId: string, templateId: string, apiKey?: string, options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Response>;
    /**
     * Returns a list of templates associated with this listener
     * @summary List Templates for Listener
     * @param {string} listenerId ID of listener
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    listListenerTemplates(listenerId: string, options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<ListenerTemplate[]>;
    /**
     * Returns a list of Listeners
     * @summary List all Listeners
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    listListeners(options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Listener[]>;
};
/**
 * ListenerApi - factory interface
 * @export
 */
export declare const ListenerApiFactory: (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) => {
    /**
     *
     * @summary Create a new Listener
     * @param {Listener} listener A Listener object to be added
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    addListener(listener: Listener, options?: any): AxiosPromise<Response>;
    /**
     *
     * @summary Associates a Template to a Listener
     * @param {string} listenerId Listener id to disassociate
     * @param {string} templateId Template id to associate with the Listener
     * @param {string} [apiKey]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    addListenerTemplate(listenerId: string, templateId: string, apiKey?: string, options?: any): AxiosPromise<Response>;
    /**
     *
     * @summary Deletes a Listener
     * @param {string} listenerId ID of listener to return
     * @param {string} [apiKey]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    deleteListener(listenerId: string, apiKey?: string, options?: any): AxiosPromise<Response>;
    /**
     *
     * @summary Disassociates a Template from a Listener
     * @param {string} listenerId Listener id to disassociate
     * @param {string} templateId Template id to delete
     * @param {string} [apiKey]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    deleteListenerTemplate(listenerId: string, templateId: string, apiKey?: string, options?: any): AxiosPromise<Response>;
    /**
     * Returns a list of templates associated with this listener
     * @summary List Templates for Listener
     * @param {string} listenerId ID of listener
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    listListenerTemplates(listenerId: string, options?: any): AxiosPromise<ListenerTemplate[]>;
    /**
     * Returns a list of Listeners
     * @summary List all Listeners
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    listListeners(options?: any): AxiosPromise<Listener[]>;
};
/**
 * ListenerApi - object-oriented interface
 * @export
 * @class ListenerApi
 * @extends {BaseAPI}
 */
export declare class ListenerApi extends BaseAPI {
    /**
     *
     * @summary Create a new Listener
     * @param {Listener} listener A Listener object to be added
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof ListenerApi
     */
    addListener(listener: Listener, options?: any): AxiosPromise<Response>;
    /**
     *
     * @summary Associates a Template to a Listener
     * @param {string} listenerId Listener id to disassociate
     * @param {string} templateId Template id to associate with the Listener
     * @param {string} [apiKey]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof ListenerApi
     */
    addListenerTemplate(listenerId: string, templateId: string, apiKey?: string, options?: any): AxiosPromise<Response>;
    /**
     *
     * @summary Deletes a Listener
     * @param {string} listenerId ID of listener to return
     * @param {string} [apiKey]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof ListenerApi
     */
    deleteListener(listenerId: string, apiKey?: string, options?: any): AxiosPromise<Response>;
    /**
     *
     * @summary Disassociates a Template from a Listener
     * @param {string} listenerId Listener id to disassociate
     * @param {string} templateId Template id to delete
     * @param {string} [apiKey]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof ListenerApi
     */
    deleteListenerTemplate(listenerId: string, templateId: string, apiKey?: string, options?: any): AxiosPromise<Response>;
    /**
     * Returns a list of templates associated with this listener
     * @summary List Templates for Listener
     * @param {string} listenerId ID of listener
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof ListenerApi
     */
    listListenerTemplates(listenerId: string, options?: any): AxiosPromise<ListenerTemplate[]>;
    /**
     * Returns a list of Listeners
     * @summary List all Listeners
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof ListenerApi
     */
    listListeners(options?: any): AxiosPromise<Listener[]>;
}
/**
 * MessageApi - axios parameter creator
 * @export
 */
export declare const MessageApiAxiosParamCreator: (configuration?: Configuration) => {
    /**
     * Returns a single message
     * @summary Find message by ID
     * @param {string} messageId ID of message to return
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getMessageById(messageId: string, options?: any): RequestArgs;
    /**
     * Returns a list of messages
     * @summary List all messages
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    listMessages(options?: any): RequestArgs;
};
/**
 * MessageApi - functional programming interface
 * @export
 */
export declare const MessageApiFp: (configuration?: Configuration) => {
    /**
     * Returns a single message
     * @summary Find message by ID
     * @param {string} messageId ID of message to return
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getMessageById(messageId: string, options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Message>;
    /**
     * Returns a list of messages
     * @summary List all messages
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    listMessages(options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Message[]>;
};
/**
 * MessageApi - factory interface
 * @export
 */
export declare const MessageApiFactory: (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) => {
    /**
     * Returns a single message
     * @summary Find message by ID
     * @param {string} messageId ID of message to return
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getMessageById(messageId: string, options?: any): AxiosPromise<Message>;
    /**
     * Returns a list of messages
     * @summary List all messages
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    listMessages(options?: any): AxiosPromise<Message[]>;
};
/**
 * MessageApi - object-oriented interface
 * @export
 * @class MessageApi
 * @extends {BaseAPI}
 */
export declare class MessageApi extends BaseAPI {
    /**
     * Returns a single message
     * @summary Find message by ID
     * @param {string} messageId ID of message to return
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof MessageApi
     */
    getMessageById(messageId: string, options?: any): AxiosPromise<Message>;
    /**
     * Returns a list of messages
     * @summary List all messages
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof MessageApi
     */
    listMessages(options?: any): AxiosPromise<Message[]>;
}
/**
 * TemplateApi - axios parameter creator
 * @export
 */
export declare const TemplateApiAxiosParamCreator: (configuration?: Configuration) => {
    /**
     *
     * @summary Create a new Template
     * @param {Template} template A Template object with associated parameters
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    addTemplate(template: Template, options?: any): RequestArgs;
    /**
     *
     * @summary Deletes a Template
     * @param {string} templateId Template id to delete
     * @param {string} [apiKey]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    deleteTemplate(templateId: string, apiKey?: string, options?: any): RequestArgs;
    /**
     * Returns a single template
     * @summary Find Template by ID
     * @param {string} templateId ID of template to return
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getTemplateById(templateId: string, options?: any): RequestArgs;
    /**
     * Returns a list of templates
     * @summary List all Templates
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    listTemplate(options?: any): RequestArgs;
    /**
     *
     * @summary Update an existing Template
     * @param {string} templateId ID of template to return
     * @param {Template} template A Template object with associated parameters
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    updateTemplate(templateId: string, template: Template, options?: any): RequestArgs;
};
/**
 * TemplateApi - functional programming interface
 * @export
 */
export declare const TemplateApiFp: (configuration?: Configuration) => {
    /**
     *
     * @summary Create a new Template
     * @param {Template} template A Template object with associated parameters
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    addTemplate(template: Template, options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Response>;
    /**
     *
     * @summary Deletes a Template
     * @param {string} templateId Template id to delete
     * @param {string} [apiKey]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    deleteTemplate(templateId: string, apiKey?: string, options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Response>;
    /**
     * Returns a single template
     * @summary Find Template by ID
     * @param {string} templateId ID of template to return
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getTemplateById(templateId: string, options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Template>;
    /**
     * Returns a list of templates
     * @summary List all Templates
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    listTemplate(options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Template[]>;
    /**
     *
     * @summary Update an existing Template
     * @param {string} templateId ID of template to return
     * @param {Template} template A Template object with associated parameters
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    updateTemplate(templateId: string, template: Template, options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Response>;
};
/**
 * TemplateApi - factory interface
 * @export
 */
export declare const TemplateApiFactory: (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) => {
    /**
     *
     * @summary Create a new Template
     * @param {Template} template A Template object with associated parameters
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    addTemplate(template: Template, options?: any): AxiosPromise<Response>;
    /**
     *
     * @summary Deletes a Template
     * @param {string} templateId Template id to delete
     * @param {string} [apiKey]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    deleteTemplate(templateId: string, apiKey?: string, options?: any): AxiosPromise<Response>;
    /**
     * Returns a single template
     * @summary Find Template by ID
     * @param {string} templateId ID of template to return
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getTemplateById(templateId: string, options?: any): AxiosPromise<Template>;
    /**
     * Returns a list of templates
     * @summary List all Templates
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    listTemplate(options?: any): AxiosPromise<Template[]>;
    /**
     *
     * @summary Update an existing Template
     * @param {string} templateId ID of template to return
     * @param {Template} template A Template object with associated parameters
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    updateTemplate(templateId: string, template: Template, options?: any): AxiosPromise<Response>;
};
/**
 * TemplateApi - object-oriented interface
 * @export
 * @class TemplateApi
 * @extends {BaseAPI}
 */
export declare class TemplateApi extends BaseAPI {
    /**
     *
     * @summary Create a new Template
     * @param {Template} template A Template object with associated parameters
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof TemplateApi
     */
    addTemplate(template: Template, options?: any): AxiosPromise<Response>;
    /**
     *
     * @summary Deletes a Template
     * @param {string} templateId Template id to delete
     * @param {string} [apiKey]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof TemplateApi
     */
    deleteTemplate(templateId: string, apiKey?: string, options?: any): AxiosPromise<Response>;
    /**
     * Returns a single template
     * @summary Find Template by ID
     * @param {string} templateId ID of template to return
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof TemplateApi
     */
    getTemplateById(templateId: string, options?: any): AxiosPromise<Template>;
    /**
     * Returns a list of templates
     * @summary List all Templates
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof TemplateApi
     */
    listTemplate(options?: any): AxiosPromise<Template[]>;
    /**
     *
     * @summary Update an existing Template
     * @param {string} templateId ID of template to return
     * @param {Template} template A Template object with associated parameters
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof TemplateApi
     */
    updateTemplate(templateId: string, template: Template, options?: any): AxiosPromise<Response>;
}
/**
 * UserApi - axios parameter creator
 * @export
 */
export declare const UserApiAxiosParamCreator: (configuration?: Configuration) => {
    /**
     * This can only be done by the logged in user.
     * @summary Create user
     * @param {User} user Created user object
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    createUser(user: User, options?: any): RequestArgs;
    /**
     * This can only be done by the logged in user.
     * @summary Delete user
     * @param {string} userId The ID of the user to be deleted
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    deleteUser(userId: string, options?: any): RequestArgs;
    /**
     *
     * @summary Get user by user name
     * @param {string} userId The user id.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getUserByName(userId: string, options?: any): RequestArgs;
    /**
     *
     * @summary Logs user into the system
     * @param {string} username The user name for login
     * @param {string} password The password for login in clear text
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    loginUser(username: string, password: string, options?: any): RequestArgs;
    /**
     *
     * @summary Logs out current logged in user session
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    logoutUser(options?: any): RequestArgs;
    /**
     * This can only be done by the logged in user.
     * @summary Updated user
     * @param {string} userId ID of the user ame that need to be updated
     * @param {User} user Updated user object
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    updateUser(userId: string, user: User, options?: any): RequestArgs;
};
/**
 * UserApi - functional programming interface
 * @export
 */
export declare const UserApiFp: (configuration?: Configuration) => {
    /**
     * This can only be done by the logged in user.
     * @summary Create user
     * @param {User} user Created user object
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    createUser(user: User, options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Response>;
    /**
     * This can only be done by the logged in user.
     * @summary Delete user
     * @param {string} userId The ID of the user to be deleted
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    deleteUser(userId: string, options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Response>;
    /**
     *
     * @summary Get user by user name
     * @param {string} userId The user id.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getUserByName(userId: string, options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<User>;
    /**
     *
     * @summary Logs user into the system
     * @param {string} username The user name for login
     * @param {string} password The password for login in clear text
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    loginUser(username: string, password: string, options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<string>;
    /**
     *
     * @summary Logs out current logged in user session
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    logoutUser(options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Response>;
    /**
     * This can only be done by the logged in user.
     * @summary Updated user
     * @param {string} userId ID of the user ame that need to be updated
     * @param {User} user Updated user object
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    updateUser(userId: string, user: User, options?: any): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Response>;
};
/**
 * UserApi - factory interface
 * @export
 */
export declare const UserApiFactory: (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) => {
    /**
     * This can only be done by the logged in user.
     * @summary Create user
     * @param {User} user Created user object
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    createUser(user: User, options?: any): AxiosPromise<Response>;
    /**
     * This can only be done by the logged in user.
     * @summary Delete user
     * @param {string} userId The ID of the user to be deleted
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    deleteUser(userId: string, options?: any): AxiosPromise<Response>;
    /**
     *
     * @summary Get user by user name
     * @param {string} userId The user id.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getUserByName(userId: string, options?: any): AxiosPromise<User>;
    /**
     *
     * @summary Logs user into the system
     * @param {string} username The user name for login
     * @param {string} password The password for login in clear text
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    loginUser(username: string, password: string, options?: any): AxiosPromise<string>;
    /**
     *
     * @summary Logs out current logged in user session
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    logoutUser(options?: any): AxiosPromise<Response>;
    /**
     * This can only be done by the logged in user.
     * @summary Updated user
     * @param {string} userId ID of the user ame that need to be updated
     * @param {User} user Updated user object
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    updateUser(userId: string, user: User, options?: any): AxiosPromise<Response>;
};
/**
 * UserApi - object-oriented interface
 * @export
 * @class UserApi
 * @extends {BaseAPI}
 */
export declare class UserApi extends BaseAPI {
    /**
     * This can only be done by the logged in user.
     * @summary Create user
     * @param {User} user Created user object
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof UserApi
     */
    createUser(user: User, options?: any): AxiosPromise<Response>;
    /**
     * This can only be done by the logged in user.
     * @summary Delete user
     * @param {string} userId The ID of the user to be deleted
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof UserApi
     */
    deleteUser(userId: string, options?: any): AxiosPromise<Response>;
    /**
     *
     * @summary Get user by user name
     * @param {string} userId The user id.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof UserApi
     */
    getUserByName(userId: string, options?: any): AxiosPromise<User>;
    /**
     *
     * @summary Logs user into the system
     * @param {string} username The user name for login
     * @param {string} password The password for login in clear text
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof UserApi
     */
    loginUser(username: string, password: string, options?: any): AxiosPromise<string>;
    /**
     *
     * @summary Logs out current logged in user session
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof UserApi
     */
    logoutUser(options?: any): AxiosPromise<Response>;
    /**
     * This can only be done by the logged in user.
     * @summary Updated user
     * @param {string} userId ID of the user ame that need to be updated
     * @param {User} user Updated user object
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof UserApi
     */
    updateUser(userId: string, user: User, options?: any): AxiosPromise<Response>;
}
