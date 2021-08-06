using System;

namespace DataManager.Domain
{
    public class DataResponseModel<T>
    {
        public T Data { get; set; }
        public string[] Errors { get; set; } = { };
        
    }
}