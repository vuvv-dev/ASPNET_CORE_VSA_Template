using System;
using Base.FX001.Common;

namespace Base.FX001.Entities;

public sealed class TodoTaskStepEntity : BaseEntity<long>
{
    public string Content { get; set; }

    public DateTime CreatedDate { get; set; }

    #region Foreign Keys
    public long TodoTaskId { get; set; }
    #endregion

    #region Navigations
    public TodoTaskEntity TodoTask { get; set; }
    #endregion

    public static class Metadata
    {
        public static readonly string TableName = "todo_task_step";

        public static class Properties
        {
            public static class Content
            {
                public const string ColumnName = "content";

                public const bool IsNotNull = true;

                public const short MaxLength = 255;
            }

            public static class CreatedDate
            {
                public const string ColumnName = "created_date";

                public const bool IsNotNull = true;
            }

            public static class TodoTaskId
            {
                public const string ColumnName = "todo_task_id";

                public const bool IsNotNull = true;
            }
        };
    }
}
