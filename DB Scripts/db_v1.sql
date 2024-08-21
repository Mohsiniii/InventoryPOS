IF OBJECT_ID('dbo.products', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.products (
        [product_id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        [name] VARCHAR(255) NOT NULL,
        [brand_id] INT,
        [category_id] INT,
        [created_at] DATETIME DEFAULT GETDATE(),
        [created_by] INT NOT NULL,
        [updated_at] DATETIME,
        [updated_by] INT
    );
END
IF NOT EXISTS (
    SELECT * 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'products' 
    AND COLUMN_NAME = 'description'
)
BEGIN
    ALTER TABLE products
    ADD [description] VARCHAR(255);
END
IF NOT EXISTS (
    SELECT * 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'products' 
    AND COLUMN_NAME = 'status'
)
BEGIN
    ALTER TABLE products
    ADD [status] VARCHAR(50);
END

IF OBJECT_ID('dbo.variants', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.variants (
        [variant_id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        [product_id] INT NOT NULL,
        [size_weight] VARCHAR(50),
        [unit_price] DECIMAL(10, 2) NOT NULL,
        [stock] INT NOT NULL,
        [created_at] DATETIME DEFAULT GETDATE(),
        [created_by] INT NOT NULL,
        [updated_at] DATETIME,
        [updated_by] INT
    );
END

IF OBJECT_ID('dbo.categories', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.categories (
        [category_id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        [name] VARCHAR(255) NOT NULL,
        [status] VARCHAR(50) DEFAULT 'Active',
        [created_at] DATETIME DEFAULT GETDATE(),
        [created_by] INT NOT NULL,
        [updated_at] DATETIME,
        [updated_by] INT
    );
END
IF NOT EXISTS (
    SELECT * 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'categories' 
    AND COLUMN_NAME = 'description'
)
BEGIN
    ALTER TABLE categories
    ADD [description] VARCHAR(255);
END


IF OBJECT_ID('dbo.brands', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.brands (
        [brand_id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        [name] VARCHAR(255) NOT NULL,
        [status] VARCHAR(50) DEFAULT 'Active',
        [created_at] DATETIME DEFAULT GETDATE(),
        [created_by] INT NOT NULL,
        [updated_at] DATETIME,
        [updated_by] INT
    );
END
IF NOT EXISTS (
    SELECT * 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'brands' 
    AND COLUMN_NAME = 'description'
)
BEGIN
    ALTER TABLE brands
    ADD [description] VARCHAR(255);
END