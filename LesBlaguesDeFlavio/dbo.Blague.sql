CREATE TABLE [dbo].[Blague] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Question] NVARCHAR (MAX) NULL,
    [Reponse]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Blague] PRIMARY KEY CLUSTERED ([Id] ASC)
);

