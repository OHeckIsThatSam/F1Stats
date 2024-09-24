USE [SystemsProgramming]
GO

/****** Object:  Table [dbo].[DriverRaces]    Script Date: 13/12/2022 15:31:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DriverRaces](
	[DriverId] [int] NOT NULL,
	[RaceId] [int] NOT NULL,
	[Postion] [int] NOT NULL,
	[Points] [int] NOT NULL,
	[DNF] [bit] NULL,
	[Laps] [int] NULL,
 CONSTRAINT [PK_DriverRaces] PRIMARY KEY CLUSTERED 
(
	[DriverId] ASC,
	[RaceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DriverRaces]  WITH CHECK ADD  CONSTRAINT [FK_DriverRaces_Drivers_DriverId] FOREIGN KEY([DriverId])
REFERENCES [dbo].[Drivers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[DriverRaces] CHECK CONSTRAINT [FK_DriverRaces_Drivers_DriverId]
GO

ALTER TABLE [dbo].[DriverRaces]  WITH CHECK ADD  CONSTRAINT [FK_DriverRaces_Races_RaceId] FOREIGN KEY([RaceId])
REFERENCES [dbo].[Races] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[DriverRaces] CHECK CONSTRAINT [FK_DriverRaces_Races_RaceId]
GO

