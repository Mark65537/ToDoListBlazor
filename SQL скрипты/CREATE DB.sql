CREATE TABLE [dbo].[ProblemSet] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [Title]                 NVARCHAR (MAX) NOT NULL,
    [Description]           NVARCHAR (MAX) NULL,
    [Executors]             NVARCHAR (MAX) NOT NULL,
    [StartDate]             DATETIME2 (7)  NOT NULL,
    [Status]                INT            NOT NULL,
    [PlannedComplexityTime] INT            NOT NULL,
    [FactTime]              INT            NULL,
    [FinishDate]            DATETIME2 (7)  NULL,
    [SubProblemsId]         TEXT           NULL,
    CONSTRAINT [PK_ProblemSet] PRIMARY KEY CLUSTERED ([Id] ASC)
);