CREATE [WeSplit]
GO


USE [WeSplit]
GO
/****** Object:  Table [dbo].[Destination]    Script Date: 12/19/2020 3:56:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Destination](
	[DestinationID] [int] NOT NULL,
	[DName] [nvarchar](100) NULL,
	[DIntroduce] [nvarchar](3000) NULL,
	[DImage] [varchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[DestinationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expenditure]    Script Date: 12/19/2020 3:56:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expenditure](
	[ExpenditureID] [int] NOT NULL,
	[EName] [nvarchar](100) NULL,
	[EPrice] [varchar](100) NULL,
	[JourneyID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ExpenditureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JImage]    Script Date: 12/19/2020 3:56:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JImage](
	[JImageID] [int] NOT NULL,
	[JImageLink] [varchar](1000) NULL,
	[JourneyID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[JImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Journey]    Script Date: 12/19/2020 3:56:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Journey](
	[JourneyID] [int] NOT NULL,
	[JName] [nvarchar](100) NULL,
	[JStatus] [bit] NULL,
	[JDayStart] [date] NULL,
	[JDayEnd] [date] NULL,
	[DestinationID] [int] NULL,
	[JIntroduce] [nvarchar](2000) NULL,
PRIMARY KEY CLUSTERED 
(
	[JourneyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 12/19/2020 3:56:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[MemberID] [int] NOT NULL,
	[MName] [nvarchar](100) NULL,
	[MDonation] [varchar](100) NULL,
	[MAvatar] [varchar](1000) NULL,
	[JourneyID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 12/19/2020 3:56:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[status] [int] NOT NULL,
	[value] [int] NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Destination] ([DestinationID], [DName], [DIntroduce], [DImage]) VALUES (1, N'Đà Lạt', N'Thiên nhiên và con người Đà Lạt đi vào những áng văn thơ, những bức tranh ảnh, vào nghệ thuật, và trong tim mỗi người. Song dù có cố gắng miêu tả thế nào, chỉ khi tự mình đặt chân đến, bạn mới có những cảm nhận thật nhất của riêng mình. Không dám tự nhận là đã hiểu hết về Đà Lạt, xin mạo muội đưa ra những đúc kết của riêng mình về thành phố Đà Lạt – những lý do mà mỗi chúng ta đều yêu mến nơi này.', N'Images\da-lat.jpg')
GO
INSERT [dbo].[Destination] ([DestinationID], [DName], [DIntroduce], [DImage]) VALUES (2, N'Mù Cang Chải', N'Người ta thường bảo, Tây Bắc đẹp vào tất cả các ngày trong năm. Và Mù Cang Chải là nơi đã góp vào đó một vẻ đẹp bình dị của hương lúa mạ non. Đây cũng là 1 trong số những nét đẹp khiến cả thế giới phải say lòng.', N'Images\mu-cang-chai.jpg')
GO
INSERT [dbo].[Destination] ([DestinationID], [DName], [DIntroduce], [DImage]) VALUES (3, N'Hội An', N'Hội An – nơi mà cuộc sống cứ bình lặng như thế. Hội An – nơi mà dường như dòng chảy vô tình của thời gian chẳng thể nào vùi lấp đi cái không khí cổ xưa. Những mái ngói cũ phủ đầy rêu phong, những con đường ngập trong sắc đỏ của đèn lồng, những bức hoành phi được chạm trổ tinh vi, tất cả như đưa ta về với một thế giới của vài trăm năm trước. Đó mới chỉ là một phần dung dị ở khu phố cổ Hội An nhưng cũng đã đủ khiến người ta phải đắm say, đi quên lối.', N'Images\hoi-an.jpg')
GO
INSERT [dbo].[Destination] ([DestinationID], [DName], [DIntroduce], [DImage]) VALUES (4, N'Bà Bà Hill', N'Đến Đà Nẵng thì địa danh Bà Nà chắc đã không còn trở nên xa lạ với hầu hết mọi người. Với những yêu yêu thích du lịch thì đây là một điểm tới cực kỳ hấp dẫn bởi cái vẻ huyền ảo, mênh mang và tĩnh lặng của nó. Tour bà nà hill giúp du khách được trải nghiệm khí hậu bốn mùa trong một ngày, chu du trên những tuyến cáp treo, đắm chìm trong cảnh quan thiên nhiên của những cánh rừng nguyên sinh nối tiếp ở Bà Nà Núi Chúa, thưởng thức ẩm thực đa dạng và tận hưởng không khí lễ hội ngập tràn.', N'Images\ba-na-hill.jpg')
GO
INSERT [dbo].[Destination] ([DestinationID], [DName], [DIntroduce], [DImage]) VALUES (5, N'Vũng Tàu', N'Đến với Vũng Tàu bạn sẽ có cảm giác bình yên, dễ chịu với những con đường rộng rãi, thoáng đãng. Dưới là biển xanh, trên là những ngọn núi to, núi nhỏ, cùng những ngôi chùa thanh tịnh… Tất cả tạo nên một Vũng Tàu đầy ma lực, một thành phố Vũng Tàu không chỉ hiền hòa bình dị mà còn vô vàn những danh lam thắng cảnh nổi tiếng.', N'Images\vung-tau.jpg')
GO
INSERT [dbo].[Destination] ([DestinationID], [DName], [DIntroduce], [DImage]) VALUES (6, N'Huế', N'Thẹn thùng nghiêng nghiêng chiếc nón bài thơ, thả làn tóc mây bay bay trong gió lộng, nhẹ nhàng ngân nga câu hát Huế thương trên sông Hương mộng mơ. Huế luôn làm người ta nhớ đến những khoảnh khắc rất tình và rất ngọt như thế. Chỉ một tiếng được cất lên thôi, cũng đủ làm lòng người bình yên đến vậy. Để khách du lịch đã về thăm Huế, đều lưu luyến một mảnh tình sâu đậm, chẳng thể nào xóa nhòa dù dòng thời gian có dịch chuyển bao lâu. Nếu có mệt mỏi, nếu muốn chạy trốn khỏi tất bật, thì Huế là điểm dừng chân không thể hoàn hảo hơn. Nào, cùng xuôi về miền Trung, tự tình trên chiếc thuyền nhỏ, lướt từ nội thành ra vùng biên, thưởng ngoạn bức tranh êm đềm của Huế, biết được Huế đẹp đến nhường nào.', N'Images\hue.jpg')
GO
INSERT [dbo].[Expenditure] ([ExpenditureID], [EName], [EPrice], [JourneyID]) VALUES (1, N'Đi lại', N'200000', 1)
GO
INSERT [dbo].[Expenditure] ([ExpenditureID], [EName], [EPrice], [JourneyID]) VALUES (2, N'Ăn uống', N'400000', 1)
GO
INSERT [dbo].[Expenditure] ([ExpenditureID], [EName], [EPrice], [JourneyID]) VALUES (3, N'Phát sinh', N'300000', 1)
GO
INSERT [dbo].[Expenditure] ([ExpenditureID], [EName], [EPrice], [JourneyID]) VALUES (4, N'Homestay', N'150000', 1)
GO
INSERT [dbo].[Expenditure] ([ExpenditureID], [EName], [EPrice], [JourneyID]) VALUES (5, N'Gói Tour', N'800000', 2)
GO
INSERT [dbo].[Expenditure] ([ExpenditureID], [EName], [EPrice], [JourneyID]) VALUES (6, N'Ăn uống', N'500000', 2)
GO
INSERT [dbo].[Expenditure] ([ExpenditureID], [EName], [EPrice], [JourneyID]) VALUES (7, N'Phát sinh', N'300000', 2)
GO
INSERT [dbo].[Expenditure] ([ExpenditureID], [EName], [EPrice], [JourneyID]) VALUES (8, N'Gói Tour', N'2500000', 3)
GO
INSERT [dbo].[Expenditure] ([ExpenditureID], [EName], [EPrice], [JourneyID]) VALUES (9, N'Ăn uống', N'500000', 3)
GO
INSERT [dbo].[Expenditure] ([ExpenditureID], [EName], [EPrice], [JourneyID]) VALUES (10, N'Lặt vặt', N'300000', 3)
GO
INSERT [dbo].[Expenditure] ([ExpenditureID], [EName], [EPrice], [JourneyID]) VALUES (11, N'Xe khách', N'600000', 4)
GO
INSERT [dbo].[Expenditure] ([ExpenditureID], [EName], [EPrice], [JourneyID]) VALUES (12, N'Homestay', N'900000', 4)
GO
INSERT [dbo].[Expenditure] ([ExpenditureID], [EName], [EPrice], [JourneyID]) VALUES (13, N'Ăn uống', N'900000', 4)
GO
INSERT [dbo].[Expenditure] ([ExpenditureID], [EName], [EPrice], [JourneyID]) VALUES (14, N'Tiền tour', N'500000', 5)
GO
INSERT [dbo].[Expenditure] ([ExpenditureID], [EName], [EPrice], [JourneyID]) VALUES (15, N'Phát sinh', N'200000', 4)
GO
INSERT [dbo].[JImage] ([JImageID], [JImageLink], [JourneyID]) VALUES (1, N'Images\2.jpg', 5)
GO
INSERT [dbo].[JImage] ([JImageID], [JImageLink], [JourneyID]) VALUES (2, N'Images\3.jpg', 5)
GO
INSERT [dbo].[JImage] ([JImageID], [JImageLink], [JourneyID]) VALUES (3, N'Images\1.jpg', 4)
GO
INSERT [dbo].[JImage] ([JImageID], [JImageLink], [JourneyID]) VALUES (4, N'Images\4.png', 5)
GO
INSERT [dbo].[JImage] ([JImageID], [JImageLink], [JourneyID]) VALUES (5, N'Images\6.jpg', 2)
GO
INSERT [dbo].[JImage] ([JImageID], [JImageLink], [JourneyID]) VALUES (6, N'Images\phocohoian.jpg', 2)
GO
INSERT [dbo].[JImage] ([JImageID], [JImageLink], [JourneyID]) VALUES (7, N'Images\5.jpg', 3)
GO
INSERT [dbo].[JImage] ([JImageID], [JImageLink], [JourneyID]) VALUES (8, N'Images\images.jpg', 3)
GO
INSERT [dbo].[JImage] ([JImageID], [JImageLink], [JourneyID]) VALUES (9, N'Images\b8d1bf741daa5c485a4fce3ae08ace6b.jpg', 3)
GO
INSERT [dbo].[JImage] ([JImageID], [JImageLink], [JourneyID]) VALUES (10, N'Images\202010280926538593-37a8ea0d-f98a-427c-a4c7-97ff8810c410.jpeg', 1)
GO
INSERT [dbo].[JImage] ([JImageID], [JImageLink], [JourneyID]) VALUES (11, N'Images\1541041134-5A6F6EF0-4CEA-4C1D-9965-D0915B1AD4B4.jpg', 1)
GO
INSERT [dbo].[Journey] ([JourneyID], [JName], [JStatus], [JDayStart], [JDayEnd], [DestinationID], [JIntroduce]) VALUES (1, N'Xuôi thuyền vượt biển', 1, CAST(N'2020-01-03' AS Date), CAST(N'2020-01-05' AS Date), 5, N'')
GO
INSERT [dbo].[Journey] ([JourneyID], [JName], [JStatus], [JDayStart], [JDayEnd], [DestinationID], [JIntroduce]) VALUES (2, N'Ghé qua kỉ niệm', 1, CAST(N'2020-03-20' AS Date), CAST(N'2020-03-25' AS Date), 3, N'')
GO
INSERT [dbo].[Journey] ([JourneyID], [JName], [JStatus], [JDayStart], [JDayEnd], [DestinationID], [JIntroduce]) VALUES (3, N'Mù Cang Chải gói mang về', 1, CAST(N'2020-05-11' AS Date), CAST(N'2020-05-17' AS Date), 2, N'Đi chơi rất vui nha mọi người! Rất chân thật không rì viu giả trân đâu!')
GO
INSERT [dbo].[Journey] ([JourneyID], [JName], [JStatus], [JDayStart], [JDayEnd], [DestinationID], [JIntroduce]) VALUES (4, N'Thành phố Ngàn hoa', 0, CAST(N'2020-12-10' AS Date), CAST(N'1900-01-01' AS Date), 1, N'')
GO
INSERT [dbo].[Journey] ([JourneyID], [JName], [JStatus], [JDayStart], [JDayEnd], [DestinationID], [JIntroduce]) VALUES (5, N'Ăn kem Đà Lạt', 0, CAST(N'2020-12-18' AS Date), CAST(N'2020-12-19' AS Date), 1, N'Đi chơi rất vui nha mọi người! Rất chân thật không rì viu giả trân đâu!')
GO
INSERT [dbo].[Member] ([MemberID], [MName], [MDonation], [MAvatar], [JourneyID]) VALUES (1, N'Lê Anh Phi', N'200000', N'', 1)
GO
INSERT [dbo].[Member] ([MemberID], [MName], [MDonation], [MAvatar], [JourneyID]) VALUES (2, N'Đoàn Phước Thống', N'200000', N'', 1)
GO
INSERT [dbo].[Member] ([MemberID], [MName], [MDonation], [MAvatar], [JourneyID]) VALUES (3, N'Nguyễn Thị Phương Anh', N'200000', N'', 1)
GO
INSERT [dbo].[Member] ([MemberID], [MName], [MDonation], [MAvatar], [JourneyID]) VALUES (4, N'Lê Anh Phi', N'800000', N'', 2)
GO
INSERT [dbo].[Member] ([MemberID], [MName], [MDonation], [MAvatar], [JourneyID]) VALUES (5, N'Lê Thị Anh Phương', N'500000', N'', 2)
GO
INSERT [dbo].[Member] ([MemberID], [MName], [MDonation], [MAvatar], [JourneyID]) VALUES (6, N'Nguyễn Thị Phương Anh', N'1000000', N'', 3)
GO
INSERT [dbo].[Member] ([MemberID], [MName], [MDonation], [MAvatar], [JourneyID]) VALUES (7, N'Nguyễn Thị Cẩm My', N'1000000', N'', 3)
GO
INSERT [dbo].[Member] ([MemberID], [MName], [MDonation], [MAvatar], [JourneyID]) VALUES (8, N'Nguyễn Hoàn Tuyết Nhi', N'1000000', N'', 3)
GO
INSERT [dbo].[Member] ([MemberID], [MName], [MDonation], [MAvatar], [JourneyID]) VALUES (9, N'Lê Anh Phi', N'500000', N'', 4)
GO
INSERT [dbo].[Member] ([MemberID], [MName], [MDonation], [MAvatar], [JourneyID]) VALUES (10, N'Đoàn Phước Thống', N'500000', N'', 4)
GO
INSERT [dbo].[Member] ([MemberID], [MName], [MDonation], [MAvatar], [JourneyID]) VALUES (11, N'Nguyễn Thị Phương Anh', N'500000', N'', 4)
GO
INSERT [dbo].[Member] ([MemberID], [MName], [MDonation], [MAvatar], [JourneyID]) VALUES (12, N'Lê Anh Phi', N'1000000', NULL, 5)
GO
INSERT [dbo].[Member] ([MemberID], [MName], [MDonation], [MAvatar], [JourneyID]) VALUES (13, N'Nguyễn Vân Vân', N'200000', NULL, 4)
GO
INSERT [dbo].[Status] ([status], [value]) VALUES (0, 1)
GO
INSERT [dbo].[Status] ([status], [value]) VALUES (1, 1)
GO
ALTER TABLE [dbo].[Expenditure]  WITH CHECK ADD FOREIGN KEY([JourneyID])
REFERENCES [dbo].[Journey] ([JourneyID])
GO
ALTER TABLE [dbo].[JImage]  WITH CHECK ADD FOREIGN KEY([JourneyID])
REFERENCES [dbo].[Journey] ([JourneyID])
GO
ALTER TABLE [dbo].[Journey]  WITH CHECK ADD FOREIGN KEY([DestinationID])
REFERENCES [dbo].[Destination] ([DestinationID])
GO
ALTER TABLE [dbo].[Member]  WITH CHECK ADD FOREIGN KEY([JourneyID])
REFERENCES [dbo].[Journey] ([JourneyID])
GO
