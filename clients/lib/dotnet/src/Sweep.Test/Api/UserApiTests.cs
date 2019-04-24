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
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using RestSharp;
using Xunit;

using Sweep.Client;
using Sweep.Api;
using Sweep.Model;

namespace Sweep.Test
{
    /// <summary>
    ///  Class for testing UserApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class UserApiTests : IDisposable
    {
        private UserApi instance;

        public UserApiTests()
        {
            instance = new UserApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of UserApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsInstanceOfType' UserApi
            //Assert.IsType(typeof(UserApi), instance, "instance is a UserApi");
        }

        
        /// <summary>
        /// Test DeleteUser
        /// </summary>
        [Fact]
        public void DeleteUserTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //instance.DeleteUser();
            
        }
        
        /// <summary>
        /// Test GetUserInfo
        /// </summary>
        [Fact]
        public void GetUserInfoTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.GetUserInfo();
            //Assert.IsType<User> (response, "response is User");
        }
        
        /// <summary>
        /// Test LoginUser
        /// </summary>
        [Fact]
        public void LoginUserTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string username = null;
            //string password = null;
            //var response = instance.LoginUser(username, password);
            //Assert.IsType<string> (response, "response is string");
        }
        
        /// <summary>
        /// Test LogoutUser
        /// </summary>
        [Fact]
        public void LogoutUserTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //instance.LogoutUser();
            
        }
        
        /// <summary>
        /// Test UpdateUser
        /// </summary>
        [Fact]
        public void UpdateUserTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //UserRequestBody userRequestBody = null;
            //instance.UpdateUser(userRequestBody);
            
        }
        
    }

}