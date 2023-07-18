CREATE TABLE [dbo].[Tasks] (
    [TaskID] INT          NOT NULL,
    [Task]   VARCHAR (50) NOT NULL,
    [Status] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([TaskID] ASC)
);

