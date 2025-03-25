using System;
using F015.Common;
using FCommon.FeatureService;

namespace F015.Models;

public sealed class AppResponseModel : IServiceResponse
{
    public Constant.AppCode AppCode { get; set; }

    public BodyModel Body { get; set; }

    public sealed class BodyModel
    {
        public TodoTaskModel TodoTask { get; set; }

        public sealed class TodoTaskModel
        {
            public long Id { get; set; }

            public string Content { get; set; }

            public DateTime DueDate { get; set; }

            public bool IsExpired { get; set; }

            public bool IsInMyDay { get; set; }

            public bool IsImportant { get; set; }

            public string Note { get; set; }

            public bool IsCompleted { get; set; }
        }
    }
}
