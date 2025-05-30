﻿using System.Net;

namespace SMPT.Entities.Dtos
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<KeyValuePair<string, string>> ErrorMessage { get; set; }
        public object Data { get; set; }
    }
}
