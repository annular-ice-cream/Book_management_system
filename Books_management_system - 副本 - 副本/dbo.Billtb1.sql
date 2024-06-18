CREATE TABLE [dbo].[Billtb1]
(
	[BillId] INT  IDENTITY (1000,1) NOT NULL, 
    [UName] VARCHAR(50) NOT NULL, 
    [Amount] INT NOT NULL,
	PRIMARY KEY CLUSTERED ([BillId] ASC)
)
