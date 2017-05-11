use CHRISDEMO

--drop table [Location]
create table [Location] (
	[Id] int not null identity(1,1),
	[Name] varchar(255) not null,
	constraint [PK_Location] primary key clustered ([Id]),
)

--drop table [Category]
create table [Category] (
	[Id] int not null identity(1,1),
	[Name] varchar(255) not null,
	[Description] varchar(255),
	constraint [PK_Category] primary key clustered ([Id]),
)

--drop table [Item]
create table [Item] (
	[Id] int not null identity(1,1),
	[CategoryId] int not null,
	[ItemNumber] varchar(255) not null,
	[Description] varchar(255),
	constraint [PK_Item] primary key clustered ([Id]),
	constraint [FK_Item_CategoryId] foreign key ([CategoryId]) references [Category]([Id]),
)

--drop table [ItemLocation]
create table [ItemLocation] (
	[ItemId] int not null,
	[LocationId] int not null
	constraint [PK_ItemLocation] primary key clustered ([ItemId], [LocationId]),
	constraint [FK_ItemLocation_ItemId] foreign key ([ItemId]) references [Item]([Id]),
	constraint [FK_ItemLocation_LocationId] foreign key ([LocationId]) references [Location]([Id]),
)

create table [Order] (
	[Id] int not null identity(1,1),
	[CustomerId] int not null,
	[LocationId] int not null,
	[Date] datetime not null,
	constraint [PK_Order] primary key clustered ([Id]),
	constraint [FK_Order_LocationId] foreign key ([LocationId]) references [Location]([Id]),
	constraint [FK_Order_CustomerId] foreign key ([CustomerId]) references [Customer]([Id])
)

create table [OrderItem] (
	[Id] int not null identity(1,1),
	[OrderId] int not null,
	[ItemId] int not null,
	[Price] decimal(12,2)
	constraint [PK_OrderItem] primary key clustered ([Id]),
	constraint [FK_OrderItem_ItemId] foreign key ([ItemId]) references [Item]([Id]),
	constraint [FK_OrderItem_OrderId] foreign key ([OrderId]) references [Order]([Id])
)

create table [Customer] (
	[Id] int not null identity(1,1),
	[LocationId] int not null,
	[Name] varchar(255) not null,
	[Address] varchar(255),
	[City] varchar(255),
	[State] char(2),
	[Zip] smallint,
	constraint [PK_Customer] primary key clustered ([Id]),
	constraint [FK_Customer_LocationId] foreign key ([LocationId]) references [Location]([Id])
)

--drop table [CustomerItem]
create table [CustomerItem] (
	[Id] int not null identity(1,1),
	[CustomerId] int not null,
	[CustomerItemNumber] varchar(255) not null,
	[ItemId] int not null,
	[Note] varchar(255),
	constraint [PK_CustomerItem] primary key clustered ([Id]),
	constraint [FK_CustomerItem_CustomerId] foreign key ([CustomerId]) references [Customer]([Id]),
	constraint [FK_CustomerItem_ItemId] foreign key ([ItemId]) references [Item]([Id]),
)