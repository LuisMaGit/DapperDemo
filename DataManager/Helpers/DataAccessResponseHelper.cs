using System;
using System.Collections.Generic;
using System.Linq;
using DataManager.Domain;

namespace DataManager.Helpers
{
    public abstract class DataAccessResponseHelper
    {
        public static DataResponseModel<T> SingleDataException<T>(Exception c)
        {
            return new DataResponseModel<T>
            {
                Errors = new[] {c.ToString()}
            };
        }

        public static DataResponseModel<T> SingleDataResponse<T>(List<T> listData)
        {
            if (!listData.Any())
            {
                return new DataResponseModel<T>();
            }

            return new DataResponseModel<T>
            {
                Data = listData.First()
            };
        }

        public static DataResponseModel<List<T>> ListDataException<T>(Exception c)
        {
            return new DataResponseModel<List<T>>
            {
                Errors = new[] {c.ToString()}
            };
        }

        public static DataResponseModel<List<T>> ListDataResponse<T>(List<T> listData)
        {
            if (!listData.Any())
            {
                return new DataResponseModel<List<T>>();
            }

            return new DataResponseModel<List<T>>
            {
                Data = listData
            };
        }
    }
}