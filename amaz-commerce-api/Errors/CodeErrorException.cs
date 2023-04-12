﻿using Newtonsoft.Json;

namespace amaz_commerce_api.Errors
{
    public class CodeErrorException : CodeErrorResponse
    {
        [JsonProperty(PropertyName = "details")]
        public string? Details { get; set; }

        public CodeErrorException(int statusCode, string[]? message = null, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }
    }
}
