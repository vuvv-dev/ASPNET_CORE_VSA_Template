using System;
using System.Collections.Generic;
using Base.FX001.Common;

namespace Base.FX001.Entities;

public sealed class TodoTaskListEntity : BaseEntity<long>
{
    public string Name { get; set; }

    public DateTime CreatedDate { get; set; }

    #region Foreign Keys
    public long UserId { get; set; }
    #endregion

    #region Navigations
    public AdditionalUserInfoEntity User { get; set; }

    public IEnumerable<TodoTaskEntity> TodoTasks { get; set; }
    #endregion

    public static class Metadata
    {
        public static readonly string TableName = "todo_task_list";

        public static class Properties
        {
            public static class Name
            {
                public const string ColumnName = "name";

                public const bool IsNotNull = true;

                public const ushort MaxLength = 255;
            }

            public static class CreatedDate
            {
                public const string ColumnName = "created_date";

                public const bool IsNotNull = true;
            }

            public static class UserId
            {
                public const string ColumnName = "user_id";

                public const bool IsNotNull = true;
            }
        }
    }
}
