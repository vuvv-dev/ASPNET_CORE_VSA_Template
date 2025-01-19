using System;
using System.Collections.Generic;
using FA1.Common;

namespace FA1.Entities;

public sealed class TodoTaskEntity : BaseEntity<long>
{
    public string Content { get; set; }

    public string Note { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime DueDate { get; set; }

    public bool IsInMyDay { get; set; }

    public bool IsImportant { get; set; }

    public bool IsFinished { get; set; }

    public string RecurringExpression { get; set; }

    #region Foreign Keys
    public long TodoTaskListId { get; set; }
    #endregion

    #region Navigations
    public TodoTaskListEntity TodoTaskList { get; set; }

    public IEnumerable<TodoTaskStepEntity> TodoTaskSteps { get; set; }
    #endregion

    public static class Metadata
    {
        public static readonly string TableName = "todo_task";

        public static class Properties
        {
            public static class Content
            {
                public const string ColumnName = "content";

                public const bool IsNotNull = true;

                public const ushort MaxLength = 255;
            }

            public static class Note
            {
                public const string ColumnName = "note";

                public const bool IsNotNull = true;

                public const short MaxLength = 255;
            }

            public static class CreatedDate
            {
                public const string ColumnName = "created_date";

                public const bool IsNotNull = true;
            }

            public static class DueDate
            {
                public const string ColumnName = "due_date";

                public const bool IsNotNull = true;
            }

            public static class IsInMyDay
            {
                public const string ColumnName = "is_in_my_day";

                public const bool IsNotNull = true;
            }

            public static class IsImportant
            {
                public const string ColumnName = "is_important";

                public const bool IsNotNull = true;
            }

            public static class IsFinished
            {
                public const string ColumnName = "is_finished";

                public const bool IsNotNull = true;
            }

            public static class RecurringExpression
            {
                public const string ColumnName = "recurring_expression";

                public const bool IsNotNull = true;

                public const short MaxLength = 255;
            }

            public static class TodoTaskListId
            {
                public const string ColumnName = "task_list_id";

                public const bool IsNotNull = true;
            }
        };
    }
}
