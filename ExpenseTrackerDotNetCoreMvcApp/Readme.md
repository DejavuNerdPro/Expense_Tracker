```sql
CREATE TABLE [dbo].[Tbl_ExpenseTracker](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[Description] [nvarchar](250) NULL,
	[TransactionType] [nvarchar](50) NULL,
	[Amount] [decimal](20, 2) NULL,
 CONSTRAINT [PK_Tbl_ExpenseTracker] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```