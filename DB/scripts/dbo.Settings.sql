CREATE TABLE [dbo].[Settings] (
    [ID]          INT           NOT NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [Value]       VARCHAR (255)  NOT NULL,
    [Description] VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

