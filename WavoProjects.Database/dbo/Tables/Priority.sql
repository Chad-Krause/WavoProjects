CREATE TABLE [dbo].[Priority] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_Priority] PRIMARY KEY CLUSTERED ([Id] ASC)
);

