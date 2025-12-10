CREATE DATABASE FoodieDB
GO

USE FoodieDB
GO

-- 1. Bảng Users
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Mobile VARCHAR(20),
    Address VARCHAR(MAX),
    Password VARCHAR(200) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- 2. Bảng Employees
CREATE TABLE Employees (
    EmployeeId INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE,
    Phone VARCHAR(20),
    Password VARCHAR(200),
    Role VARCHAR(50), -- 'Giao hàng', 'Duyệt đơn', 'Admin'
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- 3. Bảng Categories
CREATE TABLE Categories (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    ImageUrl VARCHAR(MAX),
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- 4. Bảng Products
CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    Description VARCHAR(MAX),
    Price DECIMAL(18,2) NOT NULL,
    ImageUrl VARCHAR(MAX),
    CategoryId INT NOT NULL REFERENCES Categories(CategoryId),
    Quantity INT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- 5. Bảng Carts
CREATE TABLE Carts (
    CartId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT REFERENCES Users(UserId),
    CreatedDate DATETIME DEFAULT GETDATE(),
    Status VARCHAR(50) DEFAULT 'Pending'   -- Chờ duyệt / Đang giao / Hoàn tất
);

-- 6. Bảng CartItems
CREATE TABLE CartItems (
    CartItemId INT PRIMARY KEY IDENTITY(1,1),
    CartId INT REFERENCES Carts(CartId) ON DELETE CASCADE,
    ProductId INT REFERENCES Products(ProductId),
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL
);

-- 7. Bảng Orders
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT REFERENCES Users(UserId),
    CartId INT REFERENCES Carts(CartId),
    OrderNo VARCHAR(30) UNIQUE,
    OrderDate DATETIME DEFAULT GETDATE(),
    DeliveryDate DATETIME NULL,
    Status VARCHAR(50),  -- Đã duyệt / Đang giao / Hoàn tất / Hủy
    PaymentMode VARCHAR(50)
);

-- 8. Bảng OrdersDetails
CREATE TABLE OrderDetails (
    OrderDetailId INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT REFERENCES Orders(OrderId) ON DELETE CASCADE,
    ProductId INT REFERENCES Products(ProductId),
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL
);

-- 9. Bảng OrderApproval - Nhân viên duyệt
CREATE TABLE OrderApproval (
    ApprovalId INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT REFERENCES Orders(OrderId),
    EmployeeId INT REFERENCES Employees(EmployeeId),
    ApprovedDate DATETIME DEFAULT GETDATE()
);

-- 10. Bảng OrderDelivery - Nhân viên giao
CREATE TABLE OrderDelivery (
    DeliveryId INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT REFERENCES Orders(OrderId),
    EmployeeId INT REFERENCES Employees(EmployeeId),
    DeliveryStatus VARCHAR(50),
    UpdatedDate DATETIME DEFAULT GETDATE()
);

-- 11. Bảng Payment
CREATE TABLE Payment (
    PaymentId int PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Name varchar(50) NULL,
    CardNo varchar(50) NULL,
    ExpiryDate varchar(50) NULL,
    CvvNo int NULL,
    Address varchar(max) NULL,
    PaymentMode varchar(50) NULL
);

GO