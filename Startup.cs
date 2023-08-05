services.AddDbContext<BookDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));