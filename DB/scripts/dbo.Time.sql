CREATE TABLE [dbo].[Time] (
    [TimeID]      INT           IDENTITY (1, 1) NOT NULL,
    [Item]        VARCHAR (50)  NOT NULL,
    [TicketNo]    INT           NOT NULL,
    [Start]       VARCHAR (50)  NOT NULL,
    [End]         VARCHAR (50)  NOT NULL,
    [Total]       VARCHAR (50)  NOT NULL,
    [TimeType]    VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (255) NOT NULL,
    [TicketType]  VARCHAR (50)  NOT NULL,
    [Date]        VARCHAR (50)  NOT NULL,
    [IsCaptured]  INT           NULL,
    PRIMARY KEY CLUSTERED ([TimeID] ASC),
    CONSTRAINT [CK_Time_TimeID] CHECK ([TimeID]<>NULL AND [TimeID]>(0))
);

