CREATE TABLE products (
    [product_id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    [name] VARCHAR(255) NOT NULL,
    [brand_id] INT,
    [category_id] INT,
    [created_at] DATETIME DEFAULT GETDATE(),
    [created_by] INT NOT NULL,
    [updated_at] DATETIME,
    [updated_by] INT
);

CREATE TABLE variants (
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
