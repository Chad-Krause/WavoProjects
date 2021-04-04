CREATE TABLE [dbo].[Project] (
    [Id]          INT                IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (1000)    NULL,
    [Description] NVARCHAR (MAX)     NULL,
    [PriorityId]  INT                NULL,
    [StartedOn]   DATETIMEOFFSET (7) NULL,
    [CreatedOn]   DATETIMEOFFSET (7) CONSTRAINT [DF_Project_CreatedOn] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [UpdatedOn]   DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Project_Priority] FOREIGN KEY ([PriorityId]) REFERENCES [dbo].[Priority] ([Id])
);

