﻿using Microsoft.AspNetCore.Mvc;
using VDOEasy.ApplicationCore.Models;

namespace VDOEasy.ApplicationCore.Extensions
{
    public static class DataTableExtension
    {
        public static JsonResult ToJsonResult<T>(this DataTableResultModel<T> result, DataTableOptionModel option) where T : class, new()
        {
            return new JsonResult(new
            {
                draw = option.draw,
                recordsFiltered = result.Rows,
                recordsTotal = result.Rows,
                data = result.Data
            });
        }

        private static IOrderedQueryable<T> ApplyOrderBy<T>(this IQueryable<T> query,
            DataTableOptionModel option,
            Dictionary<string, string> fieldMappings = null) where T : class, new()
        {
            var order = option.order;
            var columns = option.columns;

            if (HasOrderBy(option))
            {
                var columnName = columns[order[0].column].data;
                if (fieldMappings != null && fieldMappings.ContainsKey(columnName))
                {
                    columnName = fieldMappings[columnName];
                }

                var properties = query.ElementType.GetProperties();
                foreach (var propertyInfo in properties)
                {
                    if (propertyInfo.Name.ToLower() == columnName.ToLower())
                    {
                        var isAsc = order[0].dir.ToLower() == "asc";
                        if (isAsc)
                        {
                            return query.OrderBy(propertyInfo.Name);
                        }
                        return query.OrderByDescending(propertyInfo.Name);
                    }
                }
            }

            return (IOrderedQueryable<T>)query;
        }

        private static bool HasOrderBy(DataTableOptionModel option)
        {
            var order = option.order;
            var columns = option.columns;

            return order != null
                   && order.Count > 0
                   && columns[order[0].column].data != null
                   && columns[order[0].column].orderable == true;
        }

        public static DataTableResultModel<T> ToDataTableResult<T>(this IQueryable<T> query, DataTableOptionModel option) where T : class, new()
        {
            if (HasOrderBy(option) == false)
            {
                return query.ReturnDataTableResult(option);
            }

            IOrderedQueryable<T> orderedQueryable = query.ApplyOrderBy(option);
            return orderedQueryable.ReturnDataTableResult(option);
        }

        private static DataTableResultModel<T> ReturnDataTableResult<T>(this IQueryable<T> query, DataTableOptionModel option) where T : class, new()
        {
            var data = query.Skip(option.start).Take(option.length).ToList();
            var rows = query.Count();

            return new DataTableResultModel<T>
            {
                Data = data,
                Rows = rows
            };
        }
    }
}
