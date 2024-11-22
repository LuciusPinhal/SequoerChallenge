-- Tabela Order
CREATE TABLE [Order] (
    [Order] VARCHAR(50) PRIMARY KEY,
    Quantity NUMERIC(18, 2),
    ProductCode VARCHAR(50)
);

-- Tabela Product
CREATE TABLE Product (
    ProductCode VARCHAR(50) PRIMARY KEY,
    ProductDescription VARCHAR(50),
    Image VARCHAR(500),
    CycleTime NUMERIC(18, 2)
);

-- Tabela Material
CREATE TABLE Material (
    MaterialCode VARCHAR(50) PRIMARY KEY,
    MaterialDescription VARCHAR(500)
);

-- Tabela ProductMaterial
CREATE TABLE ProductMaterial (
    ProductCode VARCHAR(50),
    MaterialCode VARCHAR(50),
    PRIMARY KEY (ProductCode, MaterialCode),
    FOREIGN KEY (ProductCode) REFERENCES Product(ProductCode),
    FOREIGN KEY (MaterialCode) REFERENCES Material(MaterialCode)
);

-- Tabela User
CREATE TABLE [User] (
    Email VARCHAR(100) PRIMARY KEY,
    Name VARCHAR(50),
    InitialDate DATETIME
);

-- Tabela Production
CREATE TABLE Production (
    ID BIGINT IDENTITY(1,1) PRIMARY KEY,
    Email VARCHAR(100),
    [Order] VARCHAR(50),
    [Date] DATETIME,
    Quantity NUMERIC(18, 2),
    MaterialCode VARCHAR(50),
    CycleTime NUMERIC(18, 2),
    FOREIGN KEY (Email) REFERENCES [User](Email),
    FOREIGN KEY ([Order]) REFERENCES [Order]([Order]),
    FOREIGN KEY (MaterialCode) REFERENCES Material(MaterialCode)
);
