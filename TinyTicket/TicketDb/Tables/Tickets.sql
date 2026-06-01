CREATE TABLE [dbo].[Tickets] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [TicketTitle]       NVARCHAR (50)  NOT NULL,
    [TicketDescription] NVARCHAR (MAX) NOT NULL,
    [DateCreated]       DATETIME2 (7)  CONSTRAINT [DEFAULT_Tickits_DateCreated] DEFAULT (getutcdate()) NOT NULL,
    [DateCompleted]     DATETIME2 (7)  NULL,
    [Priority]          INT            CONSTRAINT [DEFAULT_Tickits_Priority] DEFAULT ((5)) NOT NULL,
    CONSTRAINT [PK_Tickits] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

