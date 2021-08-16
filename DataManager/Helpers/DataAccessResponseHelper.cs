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

        public static DataResponseModel<T> SingleDataResponse<T>(T data)
        {
            return new DataResponseModel<T>
            {
                Data = data
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

        public static DataResponseModel<List<T>> HandlePaginationResponse<T, TU>
            (List<TU> dataPaginated, int page) where TU : IPaginationBaseFields, T
        {
            if (!dataPaginated.Any())
            {
                return new DataResponseModel<List<T>>();
            }

            var totalCount = dataPaginated.First().TotalCount;
            var data = new List<T>();
            dataPaginated.ForEach(s => data.Add(s));
            var pages = totalCount % PaginationHelper.SIZE_PAGE == 0
                ? totalCount / PaginationHelper.SIZE_PAGE
                : totalCount / PaginationHelper.SIZE_PAGE + 1;
            return new DataResponseModel<List<T>>
            {
                Data = data,
                Pagination = new PaginationResponseModel
                {
                    Pages = pages,
                    Total = totalCount,
                    CurrentPage = page,
                }
            };
        }
    }
}