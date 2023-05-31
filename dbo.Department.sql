CREATE TABLE [dbo].[Department] (
    [Uid]           INT           NOT NULL,
    [Name]          NVARCHAR (50) NULL,
    [UidInstitute] INT           NOT NULL,
    [Number] NVARCHAR(50) NULL, 
    PRIMARY KEY CLUSTERED ([Uid] ASC),
	FOREIGN KEY (UidInstitute) REFERENCES Institute(Uid)
);

